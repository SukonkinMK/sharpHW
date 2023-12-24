using ChatCommonLib.Models;
using System.Net;


namespace ChatCommonLib.Abstractions
{
    public interface IMessageSourceServer<T>
    {
        Task SendAsync(NetMessage message, T ep);

        NetMessage Receive(ref T ep);

        T CreateEndpoint();

        T CopyEndpoint(IPEndPoint ep);
    }
}
