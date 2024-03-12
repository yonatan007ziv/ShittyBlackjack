namespace Client.UserControls
{
	public partial class PlayerCardsRow : UserControl
	{
		public PlayerCardsRow(string playerName, string[] cards)
		{
			InitializeComponent();

			int offsetX = 0;
			Label nameLabel = new Label() { Text = playerName };
			nameLabel.AutoSize = true;
			nameLabel.TextAlign = ContentAlignment.MiddleCenter;
			nameLabel.Font = new Font("Arial", 25);
			offsetX += nameLabel.Width;

			cardsList.Controls.Add(nameLabel);

			foreach (string card in cards)
			{
				Card currentCard = new Card(card);
				currentCard.Location = new Point(offsetX, 0);
				currentCard.Height = (int)(Height / 1.25f);
				currentCard.Width = (int)(currentCard.Height / 1.25f);

				offsetX += currentCard.Width;

				cardsList.Controls.Add(currentCard);
			}
		}
	}
}
