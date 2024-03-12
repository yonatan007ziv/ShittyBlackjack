using Server.Components;

namespace Server;

internal class Program
{
	public static async Task Main()
	{
		DatabaseHandler.Initialize();
		await new TcpServer("127.0.0.1", 5000).Run();
	}
}