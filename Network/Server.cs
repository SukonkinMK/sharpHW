using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public class Server
    {
        public string Name { get => "MyServer"; }
        private Dictionary<string, IPEndPoint> users;
        private static CancellationTokenSource cts = new CancellationTokenSource();
        private static CancellationToken ct = cts.Token;
        public Server()
        {
            users = new Dictionary<string, IPEndPoint>();
        }
        public void Register(string user, IPEndPoint ip)
        {
            if (!users.ContainsKey(user))
                users.Add(user, ip);
        }
        public void Delete(string user)
        {
            if (users.ContainsKey(user))
                users.Remove(user);
        }
        public List<string> GetUsersList()
        {
            return users.Keys.ToList();
        }
        private void SendByUdpClient(UdpClient udpClient, string msg, IPEndPoint iPEndPoint)
        {
            byte[] reply = Encoding.UTF8.GetBytes(msg);
            udpClient.Send(reply, reply.Length, iPEndPoint);
        }
        public void Start()
        {
            using UdpClient udpClient = new UdpClient(12345);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            Console.WriteLine("Сервер ждет сообщение от клиента");
            while (!ct.IsCancellationRequested)
            {
                byte[] buffer = udpClient.Receive(ref iPEndPoint);
                var messageText = Encoding.UTF8.GetString(buffer);
                IPEndPoint currentIPEndPoint = iPEndPoint;
                Task.Run(() =>
                {
                    try
                    {
                        Message message = Message.DeserializeFromJson(messageText);
                        message.Print();
                        string reply;
                        if (message.NicknameTo.Equals(Name))
                        {
                            switch (message.CommandToServer)
                            {
                                case Command.Register:
                                    Register(message.NicknameFrom, currentIPEndPoint);
                                    reply = "Пользователь добавлен в список";
                                    break;
                                case Command.Delete:
                                    Delete(message.NicknameFrom);
                                    reply = "Пользователь удален из списка";
                                    break;
                                case Command.List:
                                    reply = string.Join(" ", GetUsersList());
                                    break;
                                default:
                                    reply = "Неверная команда";
                                    break;
                            }
                            SendByUdpClient(udpClient, reply, currentIPEndPoint);
                        }
                        else
                        {
                            if (message.Text.Equals("exit"))
                                throw new ArgumentException("Exit");
                            SendByUdpClient(udpClient, "Сообщение получено", currentIPEndPoint);
                            if (message.NicknameTo.Equals(""))
                            {
                                foreach (var user in users)
                                {
                                    if (user.Value != currentIPEndPoint)
                                        SendByUdpClient(udpClient, messageText, user.Value);
                                }
                            }
                            else
                            {
                                if (users.ContainsKey(message.NicknameTo))
                                    SendByUdpClient(udpClient, messageText, users[message.NicknameTo]);
                            }
                        }
                    }
                    catch (ArgumentException)
                    {
                        SendByUdpClient(udpClient, "ended", currentIPEndPoint);
                        Console.WriteLine("Работа сервера завершена");
                        cts.Cancel();
                    }
                }, ct);
            }
        }
    }
}
