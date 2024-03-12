using Client.Components;

namespace Client.UserControls;

internal partial class GameView : UserControl
{
	private readonly TcpClientHandler tcpClientHandler;
	private readonly CardsRow localPlayerCardsRow;
	private readonly CardsRow dealerCardsRow;
	private bool inGame;
	private bool isReady;

	public GameView(TcpClientHandler tcpClientHandler)
	{
		InitializeComponent();
		this.tcpClientHandler = tcpClientHandler;

		seePlayersCardsButton.Click += (s, e) => OnSeePlayersCardsButton();
		readyButton.Click += (s, e) => OnReadyButtonClick();

		foldButton.Click += (s, e) => OnFoldButtonClick();
		hitButton.Click += (s, e) => OnHitButtonClick();

		playAgainButton.Click += (s, e) => OnPlayAgainButton();
		leaveToLobbySelectionButton.Click += (s, e) => OnLeaveToLobbySelectionButton();

		localPlayerCardsRow = new CardsRow();
		localPlayerCardsRow.Width = Width - 300;
		localPlayerCardsRow.Location = new Point(Width / 6, Height - 200);

		dealerCardsRow = new CardsRow();
		dealerCardsRow.Width = Width - 300;
		dealerCardsRow.Location = new Point(Width / 6, 50);

		seePlayersCardsButton.Visible = false;
		yourHandLabel.Visible = false;
		localPlayerCardsRow.Visible = false;
		dealerCardsRow.Visible = false;
		foldButton.Visible = false;
		hitButton.Visible = false;
		scoreLabel.Visible = false;
		dealerScoreLabel.Visible = false;
		playAgainButton.Visible = false;
		leaveToLobbySelectionButton.Visible = false;

		Controls.Add(localPlayerCardsRow);
		Controls.Add(dealerCardsRow);

		inGame = true;
		ReadLoop();
	}

	private void OnFoldButtonClick()
	{
		_ = tcpClientHandler.WriteMessage("Fold");
		foldButton.Enabled = false;
		hitButton.Enabled = false;
	}

	private void OnHitButtonClick()
	{
		_ = tcpClientHandler.WriteMessage("Hit");
		foldButton.Enabled = false;
		hitButton.Enabled = false;
	}

	private async void ReadLoop()
	{
		while (inGame)
		{
			string? msg = await tcpClientHandler.ReadMessage();
			if (msg is null)
				break;

			DecodeMessage(msg);
		}
	}

	private async void DecodeMessage(string msg)
	{
		await Console.Out.WriteLineAsync($"Received message: {msg}");

		if (msg.Contains("RequestPlayerCards"))
			DisplayPlayerCardsForm(msg.Split(':')[1]);
		else if (msg.Contains("PlayerReadyCount"))
			DisplayPlayerReadyCount(msg.Split(':')[1]);
		else if (msg.Contains("StartGameSetup"))
			StartGameSetup();
		else if (msg.Contains("EnableReadyUnreadyButton"))
			EnableReadyButton(true);
		else if (msg.Contains("DisableReadyUnreadyButton"))
			EnableReadyButton(false);
		else if (msg.Contains("WaitingForAction"))
			EnableActionButtons();
		else if (msg.Contains("YouLost"))
			Lost();
		else if (msg.Contains("Tie"))
			Tie();
		else if (msg.Contains("YouWon"))
			YouWon();
		else if (msg.Contains("GameEnded"))
			GameEnded();
		else if (msg.Contains("ReceivedCard"))
			ReceiveCard(msg.Split(':')[1]);
		else if (msg.Contains("GiveDealerCard"))
			GiveDealerCard(msg.Split(':')[1]);
	}

	private bool firstCardDealer = true;
	private void GiveDealerCard(string card)
	{
		if (firstCardDealer)
		{
			// Give 1 upside down
			firstCardDealer = false;
		}

		dealerCardsRow.AddCard(card);
		RecalculateDealerScore();
	}

	private void RecalculateDealerScore()
	{
		int score = CardsBank.GetDeckScore(dealerCardsRow.Cards.ToArray());
		dealerScoreLabel.Text = $"Dealer score: {score}";
	}

	private void ReceiveCard(string card)
	{
		localPlayerCardsRow.AddCard(card);
		RecalculateScore();
	}

	private void RecalculateScore()
	{
		int score = CardsBank.GetDeckScore(localPlayerCardsRow.Cards.ToArray());
		scoreLabel.Text = $"Your score: {score}";
	}

	private void OnPlayAgainButton()
	{
		_ = tcpClientHandler.WriteMessage("PlayAgain");

		localPlayerCardsRow.ClearCards();
		dealerCardsRow.ClearCards();

		dealerScoreLabel.Text = "Dealer score:";
		scoreLabel.Text = "Your score:";
		readyButton.Text = "Ready";
		isReady = false;

		seePlayersCardsButton.Visible = false;
		yourHandLabel.Visible = false;
		localPlayerCardsRow.Visible = false;
		dealerCardsRow.Visible = false;
		foldButton.Visible = false;
		hitButton.Visible = false;
		scoreLabel.Visible = false;
		dealerScoreLabel.Visible = false;
		playAgainButton.Visible = false;
		leaveToLobbySelectionButton.Visible = false;

		readyButton.Enabled = true;
		readyButton.Visible = true;
		playerReadyCountLabel.Visible = true;
	}

	private async void OnLeaveToLobbySelectionButton()
	{
		_ = tcpClientHandler.WriteMessage("LeaveToLobbySelection");

		// Wait for server to process request
		await Task.Delay(500);

		MainForm.Instance.NavigateTo(new LobbySelector(tcpClientHandler));
	}

	private void GameEnded()
	{
		playAgainButton.Visible = true;
		leaveToLobbySelectionButton.Visible = true;
	}

	private void YouWon()
	{
		MessageBox.Show("You won");
	}

	private void Tie()
	{
		MessageBox.Show("It's a tie");
	}

	private void Lost()
	{
		MessageBox.Show("You lost");
	}

	private void EnableActionButtons()
	{
		foldButton.Enabled = true;
		hitButton.Enabled = true;
	}

	private void EnableReadyButton(bool enable)
	{
		readyButton.Enabled = enable;
	}

	private void StartGameSetup()
	{
		readyButton.Visible = false;
		playerReadyCountLabel.Visible = false;

		seePlayersCardsButton.Visible = true;
		yourHandLabel.Visible = true;
		dealerCardsRow.Visible = true;
		localPlayerCardsRow.Visible = true;
		foldButton.Visible = true;
		hitButton.Visible = true;
		scoreLabel.Visible = true;
		dealerScoreLabel.Visible = true;

		foldButton.Enabled = false;
		hitButton.Enabled = false;
	}

	private void DisplayPlayerReadyCount(string readyCount)
	{
		playerReadyCountLabel.Text = $"Players ready: {readyCount.Split(',')[0]}/{readyCount.Split(',')[1]}";
	}

	private void DisplayPlayerCardsForm(string cardsRepresentation)
	{
		new PlayerCardsForm(cardsRepresentation).ShowDialog();
	}

	private void OnReadyButtonClick()
	{
		_ = tcpClientHandler.WriteMessage($"Ready");
		if (isReady)
			readyButton.Text = "Ready";
		else
			readyButton.Text = "Unready";
		readyButton.Enabled = false;
		isReady = !isReady;
	}

	private void OnSeePlayersCardsButton()
	{
		_ = tcpClientHandler.WriteMessage("RequestPlayerCards");
	}
}