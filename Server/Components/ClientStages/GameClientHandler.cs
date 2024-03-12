
namespace Server.Components.ClientStages;

internal class GameClientHandler
{
	public List<string> Cards { get; } = new List<string>();
	public TcpClientHandler TcpClientHandler { get; set; }
	public bool InGame { get; set; }
	public bool Ready { get; set; }
	public bool Lost { get; set; }
	public bool Folded { get; set; }

	public GameClientHandler(TcpClientHandler tcpClientHandler)
	{
		TcpClientHandler = tcpClientHandler;
	}

	public string GetCards()
	{
		string cards = "";
		foreach (string card in Cards)
			cards += $"{card},";
		return cards;
	}
}
