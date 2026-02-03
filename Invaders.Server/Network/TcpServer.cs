using System.Net;
using System.Net.Sockets;
using System.Text;
using Invaders.Server.Core;

namespace Invaders.Server.Network;

public class TcpServer
{
    private readonly int _port;
    private TcpListener _listener;
    private bool _running;

    public TcpServer(int port)
    {
        _port = port;
    }

    public void Start()
    {
        _listener = new TcpListener(IPAddress.Any, _port);
        _listener.Start();
        _running = true;

        Console.WriteLine($"[SERVER] Started on port {_port}");

        Task.Run(AcceptLoop);
    }

    private async Task AcceptLoop()
    {
        while (_running)
        {
            var client = await _listener.AcceptTcpClientAsync();
            Console.WriteLine($"[SERVER] Client connected: {client.Client.RemoteEndPoint}");

            var session = new ClientSession(client);
            _ = session.HandleAsync(); // запускаємо окремий потік
        }
    }
}
