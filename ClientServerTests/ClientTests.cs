using DBLesson.Abstracts;
using DBLesson.Models;
using DBLesson.Services;
using Moq;
using NUnit.Framework.Interfaces;
using System.Net;


namespace ClientServerTests
{
    public class ClientTests
    {

        [Test]
        public async Task ReceiveTest()
        {
            var mock = new MockClientMessageSource();
            await mock.Client.ClientListener();
            Assert.IsTrue(mock.answers.Count == 2);
            Assert.AreEqual(2, mock.answers.FindAll(x => x.Command == Command.Confirmation).Count);
        }

        [Test]
        public async Task SenderTest()
        {
            var mock = new MockClientMessageSource();
            await mock.Client.ClientSender();           
            Assert.AreEqual(1, mock.answers.FindAll(x => x.Command == Command.Register).Count);
            var msg = mock.answers.FirstOrDefault(x => x.Command == Command.Message);
            Assert.IsNotNull(msg);
            Assert.AreEqual("Sasha",msg.NickNameTo);

        }

    }
}
