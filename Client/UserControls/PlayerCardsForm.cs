namespace Client.UserControls
{
	public partial class PlayerCardsForm : Form
	{
		public PlayerCardsForm(string playerCardsRepresentation)
		{
			InitializeComponent();

			if (!int.TryParse(playerCardsRepresentation.Split('(')[0], out int playerCount))
				MessageBox.Show("Error occurred while trying to display cards");

			int yOffset = 0;
			for (int i = 0; i < playerCount; i++)
			{
				string info = playerCardsRepresentation.Split('(')[i + 1].Split(')')[0];
				string username = info.Split(',')[0];
				string cards = info.Substring(username.Length + 1);

				PlayerCardsRow playerCardsRow = new PlayerCardsRow(username, cards.Split(','));
				playerCardsRow.Width = Size.Width - 30;
				playerCardsRow.Location = new Point(0, yOffset);
				yOffset += playerCardsRow.Height;

				rowsList.Controls.Add(playerCardsRow);
			}
		}
	}
}
