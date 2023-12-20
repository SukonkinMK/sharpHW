using DBLesson.Abstracts;
using DBLesson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DBLesson.Services
{
    public class Server
    {
        public bool IsWork { get => work; }
        public int ClientsCount { get => _clients.Count; }
        private readonly IMessageSource _messageSource;
        private bool work = true;
        private Dictionary<string, IPEndPoint> _clients = new Dictionary<string, IPEndPoint>();
        private IPEndPoint ep;
        public Server(IMessageSource messageSource)
        {
            _messageSource = messageSource;
            ep = new IPEndPoint(IPAddress.Any, 0);
        }

        public void Stop()
        {
            work = false;
        }

        async Task ProcessMessage(NetMessage message)
        {
            switch (message.Command)
            {
                case Command.Register:
                    await Register(message);
                    break;
                case Command.Message:
                    await RelyMessage(message);
                    break;
                case Command.Confirmation:
                    await ConfirmMessageReceived(message.Id);
                    break;
            }
        }
        private async Task ConfirmMessageReceived(int? id)
        {
            Console.WriteLine($"Message confirmation id = {id}");
            using (ChatContext chatContext = new ChatContext())
            {
                Message msg = chatContext.Messages.FirstOrDefault(x => x.MessageId == id);
                if (msg != null)
                {
                    msg.IsSent = true;
                    await chatContext.SaveChangesAsync();
                }
            }
        }
        private async Task RelyMessage(NetMessage message)
        {
            if (_clients.TryGetValue(message.NickNameTo, out IPEndPoint endPoint))
            {
                int? id = 0;
                using (ChatContext chatContext = new ChatContext())
                {
                    var fromUser = chatContext.Users.First(x => x.FullName == message.NickNameFrom);
                    var toUser = chatContext.Users.First(x => x.FullName == message.NickNameTo);
                    var msg = new Message { UserFrom = fromUser, UserTo = toUser, IsSent = false, Text = message.Text };
                    chatContext.Messages.Add(msg);
                    chatContext.SaveChanges();
                    id = msg.MessageId;
                }
                message.Id = id;
                await _messageSource.SendAsync(message, endPoint);
                Console.WriteLine($"Message relied, from = {message.NickNameFrom} to = {message.NickNameTo}");
            }
            else
            {
                Console.WriteLine("Пользователь не найден");
            }
        }
        private async Task Register(NetMessage message)
        {
            Console.WriteLine($"Message Register name = {message.NickNameFrom}");
            if (_clients.TryAdd(message.NickNameFrom, message.EndPoint))
            {
                using (ChatContext chatContext = new ChatContext())
                {
                    chatContext.Users.Add(new User() { FullName = message.NickNameFrom });
                    await chatContext.SaveChangesAsync();
                }
            }
        }

        public async Task Start()
        {
            work = true;
            Console.WriteLine("Сервер ожидает сообщения");
            while (work)
            {
                try
                {
                    NetMessage message = _messageSource.Receive(ref ep);
                    if (message != null)
                    {
                        Console.WriteLine(message?.ToString());

                        await ProcessMessage(message);
                    }
                    else
                    {
                        Console.WriteLine("Получено пустое сообщение");
                        Stop();
                    }
                }
                catch (Exception e)
                {
                    Stop();
                    Console.WriteLine(e);
                }
            }
        }
    }
}
