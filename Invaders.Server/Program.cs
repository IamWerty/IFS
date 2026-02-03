using Invaders.Server.Network;

Console.Title = "Invaders File Server";

var server = new TcpServer(9000);
server.Start();

Console.ReadLine();
