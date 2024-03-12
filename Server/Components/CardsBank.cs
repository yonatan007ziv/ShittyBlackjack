namespace Server.Components;

internal class CardsBank
{
	public static readonly string[] Cards =
	{
		"ace_of_spades", "_2_of_spades","_3_of_spades","_4_of_spades","_5_of_spades","_6_of_spades","_7_of_spades","_8_of_spades","_9_of_spades","_10_of_spades", "jack_of_spades","queen_of_spades","king_of_spades",
		"ace_of_clubs", "_2_of_clubs","_3_of_clubs","_4_of_clubs","_5_of_clubs","_6_of_clubs","_7_of_clubs","_8_of_clubs","_9_of_clubs","_10_of_clubs", "jack_of_clubs","queen_of_clubs","king_of_clubs",
		"ace_of_diamonds", "_2_of_diamonds","_3_of_diamonds","_4_of_diamonds","_5_of_diamonds","_6_of_diamonds","_7_of_diamonds","_8_of_diamonds","_9_of_diamonds","_10_of_diamonds", "jack_of_diamonds","queen_of_diamonds","king_of_diamonds",
		"ace_of_hearts", "_2_of_hearts","_3_of_hearts","_4_of_hearts","_5_of_hearts","_6_of_hearts","_7_of_hearts","_8_of_hearts","_9_of_hearts","_10_of_hearts", "jack_of_hearts","queen_of_hearts","king_of_hearts",
	};

	public static int GetDeckScore(string[] deck)
	{
		int score = 0;
		int numOfAces = 0;
		foreach (string card in deck)
		{
			if (card.Contains("ace"))
				numOfAces++;
			else
				score += GetCardScore(card);
		}

		while (score <= 10 && numOfAces > 0)
		{
			score += 11;
			numOfAces--;
		}

		return score + numOfAces * 1;
	}

	// Except ace
	private static int GetCardScore(string card)
	{
		if (card[1] == '2')
			return 2;
		else if (card[1] == '3')
			return 3;
		else if (card[1] == '4')
			return 4;
		else if (card[1] == '5')
			return 5;
		else if (card[1] == '6')
			return 6;
		else if (card[1] == '7')
			return 7;
		else if (card[1] == '8')
			return 8;
		else if (card[1] == '9')
			return 9;
		else if (card[1] == '1' && card[2] == '0')
			return 10;
		else if (card.Contains("jack"))
			return 10;
		else if (card.Contains("queen"))
			return 10;
		else if (card.Contains("king"))
			return 10;

		return -1;
	}
}