using Network;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            //SentMessage(args[0], args[1]);
            SentMessage("Mikhail", "127.0.0.1");
        }

        public static void SentMessage(string From, string ip)
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);
            IBuilder builder = new MessageBuilder();
            builder.BuildNickNameFrom(From);
            
            while (true)
            {
                string input;
                string messageText = "";
                string address = "";
                do
                {
                    Console.Write("Введите сообщение: ");
                    input = Console.ReadLine();
                    if (!string.IsNullOrEmpty(input))
                    {
                        if (input.Split(" ").Length >= 2)
                        {
                            address = input.Split(" ")[0];
                            messageText = input.Split(" ")[1];
                        }
                        else if (input.Split(" ").Length == 1)
                            messageText = input;
                    }
                }
                while (string.IsNullOrEmpty(messageText));
                builder.BuildText(messageText);
                builder.BuildNickNameTo(address);
                if(address.Length > 0)
                {
                    if (messageText.Contains("register"))
                        builder.BuildCommandToServer(Command.Register);
                    else if (messageText.Contains("delete"))
                        builder.BuildCommandToServer(Command.Delete);
                    else if (messageText.Contains("exit"))
                        builder.BuildCommandToServer(Command.List);
                }
                Message message = builder.Build();
                string json = message.SerializeMessageToJson();

                byte[] data = Encoding.UTF8.GetBytes(json);
                udpClient.Send(data, data.Length, iPEndPoint);

                byte[] buffer = udpClient.Receive(ref iPEndPoint);
                string replyText = Encoding.UTF8.GetString(buffer);
                if (replyText.Equals("ended"))
                {
                    udpClient.Send(data, data.Length, iPEndPoint);
                    Console.WriteLine("Сервер завершил работу");
                    break;
                }
                Console.WriteLine(replyText);
            }
            Console.WriteLine("Работа клиента завершена");
        }
    }
}
