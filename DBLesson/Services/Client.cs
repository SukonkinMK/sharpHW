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
    public class Client
    {
        private readonly string _name;
        private IMessageSource _messageSource;
        private IUserInterface _userInterface;
        private IPEndPoint remoteEndPoint;
        private bool work = true;
        public Client(IMessageSource messageSource, IUserInterface userInterface, string name, string address, int port)
        {
            _name = name;
            _messageSource = messageSource;
            _userInterface = userInterface;
            remoteEndPoint = new IPEndPoint(IPAddress.Parse(address), port);
        }
        public async Task Start()
        {
            await ClientListener();
            await ClientSender();
        }

        public async Task ClientSender()
        {
            await Register(remoteEndPoint);

            do
            {
                _userInterface.Write("Введите имя получателя сообщения: ");
                var nameTo = _userInterface.ReadLine();
                _userInterface.Write("Введите сообщение: ");
                var text = _userInterface.ReadLine();
                if (string.IsNullOrEmpty(nameTo) && string.IsNullOrEmpty(text))
                    break;
                var msg = new NetMessage() { Command = Command.Message, NickNameFrom = _name, NickNameTo = nameTo, Text = text, DateTime = DateTime.Now };
                await _messageSource.SendAsync(msg, remoteEndPoint);
                _userInterface.WriteLine("Сообщение отправлено");
            }
            while (work);
        }

        private async Task Register(IPEndPoint remoteEndPoint)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            var msg = new NetMessage() { NickNameFrom = _name, NickNameTo = null, Text = null, Command = Command.Register, EndPoint = ep };
            await _messageSource.SendAsync(msg, remoteEndPoint);
        }

        public async Task ClientListener()
        {
            do
            {
                try
                {
                    var messageReceived = _messageSource.Receive(ref remoteEndPoint);
                    if (messageReceived != null)
                    {
                        _userInterface.WriteLine($"Получено сообщение от {messageReceived.NickNameFrom}: {messageReceived.Text}");
                        await Confirm(messageReceived, remoteEndPoint);
                    }
                    else
                    {
                        Stop();
                        _userInterface.WriteLine("Получено пустое сообщение");
                    }
                }
                catch (Exception e)
                {
                    Stop();
                    _userInterface.WriteLine(e.ToString());
                }
            }
            while (work);
        }

        private async Task Confirm(NetMessage messageReceived, IPEndPoint remoteEndPoint)
        {
            messageReceived.Command = Command.Confirmation;
            await _messageSource.SendAsync(messageReceived, remoteEndPoint);
        }
        public void Stop()
        {
            work = false;
        }
    }
}
