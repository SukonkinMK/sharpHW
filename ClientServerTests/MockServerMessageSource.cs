using DBLesson.Abstracts;
using DBLesson.Models;
using DBLesson.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerTests
{
    public class MockServerMessageSource : IMessageSource
    {
        private Queue<NetMessage> messages = new();
        public Server Server { get; }
        private IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
        public MockServerMessageSource()
        {
            Server = new Server(this);
            messages.Enqueue(new NetMessage() { Command = Command.Register, NickNameFrom = "Вася" });
            messages.Enqueue(new NetMessage() { Command = Command.Register, NickNameFrom = "Юля" });
            messages.Enqueue(new NetMessage() { Command = Command.Message, NickNameFrom = "Вася", NickNameTo = "Юля", Text = "От Васи" });
            messages.Enqueue(new NetMessage() { Command = Command.Message, NickNameTo = "Вася", NickNameFrom = "Юля", Text = "От Юли" });
        }
        public NetMessage Receive(ref IPEndPoint ep)
        {
            ep = endPoint;
            if (messages.Count == 0)
            {
                Server.Stop();
                return null;
            }
            return messages.Dequeue();
        }

        public async Task SendAsync(NetMessage message, IPEndPoint ep)
        {
            await Task.CompletedTask;
        }
    }
}
