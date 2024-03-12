namespace Client.UserControls
{
	public partial class Card : UserControl
	{
		public Card(string cardName)
		{
			InitializeComponent();
			BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject(cardName)!;
		}
	}
}