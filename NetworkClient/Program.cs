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

            while (true)
            {
                string messageText;
                do
                {
                    Console.Write("Введите сообщение: ");
                    messageText = Console.ReadLine();

                }
                while (string.IsNullOrEmpty(messageText));

                Message message = new Message() { Text = messageText, NicknameFrom = From, NicknameTo = "Server", DateTime = DateTime.Now };
                string json = message.SerializeMessageToJson();

                byte[] data = Encoding.UTF8.GetBytes(json);
                udpClient.Send(data, data.Length, iPEndPoint);

                byte[] buffer = udpClient.Receive(ref iPEndPoint);
                if(buffer != null)
                {
                    Console.WriteLine(Encoding.UTF8.GetString(buffer));
                }
            }

        }
    }
}
