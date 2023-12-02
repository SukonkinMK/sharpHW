﻿using Network;
using System.Net.Sockets;
using System.Net;
using System.Text;

internal class Program
{
    static void Main(string[] args)
    {
        Server("Hello");
    }

    public void task1()
    {
        Message msg = new Message() { Text = "Hello", DateTime = DateTime.Now, NicknameFrom = "Artem", NicknameTo = "All" };
        string json = msg.SerializeMessageToJson();
        Console.WriteLine(json);
        Message? msgDeserialized = Message.DeserializeFromJson(json);
    }

    public static void Server(string name)
    {
        UdpClient udpClient = new UdpClient(12345);
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);

        Console.WriteLine("Сервер ждет сообщение от клиента");

        while (true)
        {
            byte[] buffer = udpClient.Receive(ref iPEndPoint);

            if (buffer == null) break;
            var messageText = Encoding.UTF8.GetString(buffer);

            Message message = Message.DeserializeFromJson(messageText);
            message.Print();

            byte[] reply = Encoding.UTF8.GetBytes("Сообщение получено");
            udpClient.Send(reply, reply.Length, iPEndPoint);
        }
    }

}