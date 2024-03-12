using Server.Components.ClientStages;

namespace Server.Components;

internal class Lobby
{
	public static readonly Dictionary<int, Lobby> lobbies = new Dictionary<int, Lobby>();
	public static bool AddPlayerToLobby(TcpClientHandler tcpClientHandler, int lobbyId)
	{
		if (!lobbies.ContainsKey(lobbyId))
			return false;

		Lobby lobby = lobbies[lobbyId];
		if (lobby.players.Count >= lobby.MaxNumOfPlayers)
			return false;

		lobbies[lobbyId].OnPlayerConnect(new GameClientHandler(tcpClientHandler));
		return true;
	}

	private readonly List<GameClientHandler> players = new List<GameClientHandler>();
	private readonly List<string> dealerCards = new List<string>();
	private readonly List<string> availableCards = new List<string>();
	private int readyCount;

	public int Id { get; set; }
	public string Name { get; set; } = "";
	public string HostName { get; }
	public int MaxNumOfPlayers { get; set; }
	public int PlayerCount => players.Count;

	public Lobby(TcpClientHandler host, int id, string name, int maxNumOfPlayers)
	{
		Id = id;
		Name = name;
		HostName = host.Username;
		MaxNumOfPlayers = maxNumOfPlayers;

		availableCards = new List<string>(CardsBank.Cards);

		lobbies.Add(Id, this);
		OnPlayerConnect(new GameClientHandler(host));
	}

	private string TakeRandomCard()
	{
		int index = new Random().Next(availableCards.Count);
		string card = availableCards[index];
		availableCards.RemoveAt(index);
		return card;
	}

	private async void BeginGameClientRead(GameClientHandler playerHandler)
	{
		while (playerHandler.InGame)
		{
			string? msg = await playerHandler.TcpClientHandler.ReadMessage();
			if (msg is null)
			{
				OnPlayerDisconnect(playerHandler);
				break;
			}

			InterpretMessage(playerHandler, msg);
		}
	}

	private void InterpretMessage(GameClientHandler sender, string msg)
	{
		Console.WriteLine($"Received Message: {msg}");

		if (msg.Contains("RequestPlayerCards"))
		{
			string playerCardsRepresentation = $"RequestPlayerCards:{PlayerCount}";

			foreach (GameClientHandler player in players)
				playerCardsRepresentation += $"({player.TcpClientHandler.Username},{player.GetCards()})";

			_ = sender.TcpClientHandler.WriteMessage(playerCardsRepresentation);
		}
		else if (msg.Contains("PlayAgain"))
		{
			sender.InGame = false;
			if (!lobbies.ContainsKey(Id))
				_ = new Lobby(sender.TcpClientHandler, Id, Name, MaxNumOfPlayers);
			else
				AddPlayerToLobby(sender.TcpClientHandler, Id);
		}
		else if (msg.Contains("LeaveToLobbySelection"))
		{
			LobbySelectorHandler.AddClientToSelector(sender.TcpClientHandler);
			sender.InGame = false;
		}
		else if (msg.Contains("Fold"))
		{
			sender.Folded = true;
			foldCount++;
			terminalStatePlayersCount++;
		}
		else if (msg.Contains("Hit"))
		{
			string card = TakeRandomCard();
			sender.Cards.Add(card);
			_ = sender.TcpClientHandler.WriteMessage($"ReceivedCard:{card}");
			hitCount++;
		}
		else if (msg.Contains("Ready"))
		{
			if (!sender.Ready)
			{
				readyCount++;
				sender.Ready = true;
			}
			else
			{
				readyCount--;
				sender.Ready = false;
			}

			BroadcastPlayerReadyCount();

			if (readyCount == PlayerCount && PlayerCount >= 2)
				StartGame();
			else
				_ = sender.TcpClientHandler.WriteMessage("EnableReadyUnreadyButton");
		}
	}

	private int hitCount = 0, foldCount = 0, terminalStatePlayersCount;
	private async void StartGame()
	{
		// Remove lobby from selection
		lobbies.Remove(Id);
		LobbySelectorHandler.ResendLobbies();

		Console.WriteLine("Game starting");

		foreach (GameClientHandler player in players)
			_ = player.TcpClientHandler.WriteMessage($"DisableReadyUnreadyButton");

		await Task.Delay(2500);

		foreach (GameClientHandler player in players)
			_ = player.TcpClientHandler.WriteMessage($"StartGameSetup");

		// Give dealer 2 cards
		string currentCard;
		currentCard = TakeRandomCard();
		dealerCards.Add(currentCard);
		currentCard = TakeRandomCard();
		dealerCards.Add(currentCard);

		// Give 2 cards to each player
		for (int i = 0; i < 2; i++)
		{
			foreach (GameClientHandler player in players)
			{
				currentCard = TakeRandomCard();
				player.Cards.Add(currentCard);
				_ = player.TcpClientHandler.WriteMessage($"ReceivedCard:{currentCard}");

			}
			await Task.Delay(750);
		}

		for (int i = 0; i < 2; i++)
		{
			foreach (GameClientHandler player in players)
				_ = player.TcpClientHandler.WriteMessage($"GiveDealerCard:{dealerCards[i]}");
			await Task.Delay(750);
		}

		while (true)
		{
			hitCount = 0;

			// Iterate over players and wait for actions
			foreach (GameClientHandler player in players)
				if (!player.Lost && !player.Folded)
					_ = player.TcpClientHandler.WriteMessage($"WaitingForAction");

			// Wait for all replies
			while (hitCount + foldCount < PlayerCount && terminalStatePlayersCount != PlayerCount)
			{
				// Check if someone lost
				foreach (GameClientHandler player in players)
					if (CardsBank.GetDeckScore(player.Cards.ToArray()) > 21 && !player.Lost)
					{
						player.Lost = true;
						terminalStatePlayersCount++;
					}

				await Task.Delay(25);
			}

			if (CardsBank.GetDeckScore(dealerCards.ToArray()) < 17)
			{
				currentCard = TakeRandomCard();
				dealerCards.Add(currentCard);
				foreach (GameClientHandler player in players)
					_ = player.TcpClientHandler.WriteMessage($"GiveDealerCard:{currentCard}");

				// Wait 0.75 seconds for dramatic effect
				await Task.Delay(750);
			}

			// Check if someone lost
			foreach (GameClientHandler player in players)
				if (CardsBank.GetDeckScore(player.Cards.ToArray()) > 21 && !player.Lost)
				{
					player.Lost = true;
					terminalStatePlayersCount++;
				}

			if (terminalStatePlayersCount == PlayerCount)
			{
				EndGame();
				return;
			}
		}
	}

	private async void EndGame()
	{
		// Roll dealer until 17 and above
		while (CardsBank.GetDeckScore(dealerCards.ToArray()) < 17)
		{
			string currentCard = TakeRandomCard();
			dealerCards.Add(currentCard);
			foreach (GameClientHandler player in players)
				_ = player.TcpClientHandler.WriteMessage($"GiveDealerCard:{currentCard}");

			// Wait 0.75 seconds for dramatic effect
			await Task.Delay(750);
		}

		int dealerScore = CardsBank.GetDeckScore(dealerCards.ToArray());
		foreach (GameClientHandler player in players)
		{
			int playerScore = CardsBank.GetDeckScore(player.Cards.ToArray());

			if (playerScore > 21 && dealerScore > 21)
			{
				if (playerScore < dealerScore)
					_ = player.TcpClientHandler.WriteMessage("YouWon");
				else if (playerScore == dealerScore)
					_ = player.TcpClientHandler.WriteMessage("Tie");
				else
					_ = player.TcpClientHandler.WriteMessage("YouLost");
			}
			else if (dealerScore > 21 && playerScore <= 21)
				_ = player.TcpClientHandler.WriteMessage("YouWon");
			else if (playerScore > 21 || playerScore < dealerScore)
				_ = player.TcpClientHandler.WriteMessage("YouLost");
			else if (playerScore == dealerScore)
				_ = player.TcpClientHandler.WriteMessage("Tie");
			else
				_ = player.TcpClientHandler.WriteMessage("YouWon");
		}

		foreach (GameClientHandler player in players)
			_ = player.TcpClientHandler.WriteMessage("GameEnded");
	}

	private void BroadcastPlayerReadyCount()
	{
		Console.WriteLine("Broadcasting ready count");
		foreach (GameClientHandler player in players)
			_ = player.TcpClientHandler.WriteMessage($"PlayerReadyCount:{readyCount},{PlayerCount}");
	}

	private async void OnPlayerConnect(GameClientHandler player)
	{
		player.InGame = true;
		players.Add(player);

		BeginGameClientRead(player);
		LobbySelectorHandler.ResendLobbies();

		// Wait idk why
		await Task.Delay(500);
		BroadcastPlayerReadyCount();
	}

	private void OnPlayerDisconnect(GameClientHandler player)
	{
		player.InGame = false;
		players.Remove(player);
		if (player.Ready)
			readyCount--;

		BroadcastPlayerReadyCount();

		if (PlayerCount == 0)
			lobbies.Remove(Id);

		LobbySelectorHandler.ResendLobbies();
	}
}