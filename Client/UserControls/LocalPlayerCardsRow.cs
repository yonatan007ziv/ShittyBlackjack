
namespace Client.UserControls
{
	public partial class CardsRow : UserControl
	{
		public readonly List<string> Cards = new List<string>();

		public CardsRow()
		{
			InitializeComponent();
		}

		public void AddCard(string cardName)
		{
			Cards.Add(cardName);

			int offsetX = 0;
			cardsList.Controls.Clear();
			foreach (string card in Cards)
			{
				Card currentCard = new Card(card);
				currentCard.Location = new Point(offsetX, 0);
				currentCard.Height = (int)(Height / 1.25f);
				currentCard.Width = (int)(currentCard.Height / 1.25f);

				offsetX += currentCard.Width;

				cardsList.Controls.Add(currentCard);
			}
		}

		public void ClearCards()
		{
			Cards.Clear();
			cardsList.Controls.Clear();
		}
	}
}
