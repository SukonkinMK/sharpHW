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
    public class MockClientMessageSource : IMessageSource
    {
        public List<NetMessage> answers = new();
        private Queue<NetMessage> messages = new();
        private IUserInterface mockUser = new MockUserInterface();
        public Client Client { get; }
        private IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
        public MockClientMessageSource()
        {
            Client = new Client(this, mockUser, "Misha", "127.0.0.1", 12345);
            messages.Enqueue(new NetMessage() { Command = Command.Message, NickNameFrom = "Вася", NickNameTo = "Юля", Text = "От Васи" });
            messages.Enqueue(new NetMessage() { Command = Command.Message, NickNameTo = "Вася", NickNameFrom = "Юля", Text = "От Юли" });
        }
        public NetMessage Receive(ref IPEndPoint ep)
        {
            ep = endPoint;
            if (messages.Count == 0)
            {
                Client.Stop();
                return null;
            }
            return messages.Dequeue();
        }

        public async Task SendAsync(NetMessage message, IPEndPoint ep)
        {
            answers.Add(message);
            await Task.CompletedTask;
            //throw new NotImplementedException();
        }
    }
}
