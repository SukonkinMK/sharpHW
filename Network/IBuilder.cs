using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public interface IBuilder
    {
        public void BuildText(string text);
        public void BuildCommandToServer(Command command);
        public void BuildNickNameFrom(string nickNameFrom);
        public void BuildNickNameTo(string nickNameTo);

        public Message Build();
    }
}
