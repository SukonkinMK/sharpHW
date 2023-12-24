using ChatAppLib;
using System.Net;

internal class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            var s = new Server<IPEndPoint>(new UdpMessageSouceServer());
            await s.Start();
        }
        else if (args.Length == 1)
        {
            var c = new Client<IPEndPoint>(new UdpMessageSourceClient(), args[0]);
            await c.Start();
        }
        Console.ReadLine();
    }
}
