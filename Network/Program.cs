using Network;
using System.Net.Sockets;
using System.Net;
using System.Text;

internal class Program
{
    private static bool serverStatus = true;

    static void Main(string[] args)
    {
        Server("Hello");
    }

    public void task1()
    {
        Message msg = new Message() { Text = "Hello", DateTime = DateTime.Now, NicknameFrom = "Artem", NicknameTo = "All" };
        string json = msg.SerializeMessageToJson();
        Console.WriteLine(json);
        Message? msgDeserialized = Message.DeserializeFromJson(json);
    }

    public static void Server(string name)
    {
        using UdpClient udpClient = new UdpClient(12345);
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);

        Console.WriteLine("Сервер ждет сообщение от клиента");

        while (serverStatus)
        {
            byte[] buffer = udpClient.Receive(ref iPEndPoint);
            var messageText = Encoding.UTF8.GetString(buffer);
            if (serverStatus)
            {
                new Thread(() =>
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
                        serverStatus = false;
                        byte[] reply = Encoding.UTF8.GetBytes("ended");
                        udpClient.Send(reply, reply.Length, iPEndPoint);
                        Console.WriteLine("Работа сервера завершена");
                    }
                }).Start();
            }            
        }
    }
}