using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public class MessageBuilder : IBuilder
    {
        private Command _command;
        private string _nickNameFrom;
        private string _nickNameTo;
        private string _text;

        public MessageBuilder()
        {
            _command = Command.Undef;
            _nickNameFrom = "";
            _nickNameTo = "";
            _text = "";
        }

        public void BuildCommandToServer(Command command)
        {
            _command = command;
        }

        public void BuildNickNameFrom(string nickNameFrom)
        {
            _nickNameFrom = nickNameFrom;
        }

        public void BuildNickNameTo(string nickNameTo)
        {
            _nickNameTo = nickNameTo;
        }

        public void BuildText(string text)
        {
            _text = text;
        }

        public Message Build()
        {
            return new Message() { CommandToServer = _command, Text = _text, DateTime = DateTime.Now, NicknameFrom = _nickNameFrom, NicknameTo = _nickNameTo };
        }
    }
}
