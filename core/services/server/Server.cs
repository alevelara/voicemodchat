using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Fleck;
using voicemodchat.core.models.interfaces;
using voicemodchat.core.utils.constants;
using voicemodchat.core.utils.extensions;

namespace voicemodchat.core.services.server
{
    public class Server : IServer
    {

        private string _ipAddress;
        private IWebSocketConnection _currentConnection;
        private WebSocketServer _webSocketServer;
        public List<IWebSocketConnection> Connections { get; set; }
                
        public Server(string IpAddress)
        {
            _ipAddress = IpAddress;
        }

        public void Dispose()
        {
            _webSocketServer.Dispose();
        }

        public Task Ping()
        {
            return _currentConnection.SendPing(Constants.PING.ToByteArray());
        }

        public Task Pong()
        {
            return _currentConnection.SendPong(Constants.PONG.ToByteArray());
        }
        
        public void Start()
        {
            _webSocketServer = new WebSocketServer(_ipAddress);
            _webSocketServer.Start(connection => 
            {
                    connection.OnOpen = () =>
                        {
                            Console.WriteLine("Open!");
                            Connections.Add(connection);
                        };
                    connection.OnClose = () =>
                        {
                            Console.WriteLine("Close!");
                            Connections.Remove(connection);
                        };
                    connection.OnMessage = message =>
                        {
                            Console.WriteLine(message);
                            Connections.ToList().ForEach(s => s.Send("Echo: " + message));
                        };
                    connection.OnError = (Exception exception) =>
                        {
                            Console.WriteLine(exception.Message);
                            Connections.Clear();
                            connection.Close();
                            this.Dispose();
                        };
            });
        }

        public void Stop()
        {
            Connections.Clear();
            this.Dispose();
        }
    }
}