using Network;
using System.Net.Sockets;
using System.Net;
using System.Text;

internal class Program
{
    private static CancellationTokenSource cts = new CancellationTokenSource();
    private static CancellationToken ct = cts.Token;
    static void Main(string[] args)
    {
        //StartServer();
        (new Server()).Start();
    }
    public static void StartServer()
    {
        using UdpClient udpClient = new UdpClient(12345);
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
        Console.WriteLine("Сервер ждет сообщение от клиента");
        while (!ct.IsCancellationRequested)
        {
            byte[] buffer = udpClient.Receive(ref iPEndPoint);
            var messageText = Encoding.UTF8.GetString(buffer);
            Task.Run(() =>
            {
                try
                {
                    Message message = Message.DeserializeFromJson(messageText);
                    message.Print();
                    if (message.Text.Equals("exit"))
                        throw new ArgumentException("Exit");
                    byte[] reply = Encoding.UTF8.GetBytes("Сообщение получено");
                    udpClient.Send(reply, reply.Length, iPEndPoint);
                }
                catch (ArgumentException)
                {
                    byte[] reply = Encoding.UTF8.GetBytes("ended");
                    udpClient.Send(reply, reply.Length, iPEndPoint);
                    Console.WriteLine("Работа сервера завершена");
                    cts.Cancel();
                }
            }, ct);
        }
    }
}