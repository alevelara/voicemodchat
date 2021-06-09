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
        private WebSocketServer _webSocketServer;
        
        public IWebSocketConnection CurrentConnection { get; set; }                         
        public List<IWebSocketConnection> Connections { get; set; }
                
        public Server(string IpAddress)
        {
            _ipAddress = IpAddress;
            Connections = new List<IWebSocketConnection>();
        }

        public void Dispose()
        {
            _webSocketServer.Dispose();
        }

        public Task Ping()
        {
            return CurrentConnection.SendPing(Constants.PING.ToByteArray());
        }

        public Task Pong()
        {
            return CurrentConnection.SendPong(Constants.PONG.ToByteArray());
        }
        
        public void Start()
        {
            _webSocketServer = new WebSocketServer(_ipAddress);
            _webSocketServer.Start(connection => 
            {
                    connection.OnOpen = () =>
                        {
                            Log.Info("New User connected to the server");
                            CurrentConnection = connection;
                            Connections.Add(connection);
                        };
                    connection.OnClose = () =>
                        {
                            Console.WriteLine("Close!");                            
                            Connections.Remove(connection);
                            CloseCurrentConnection(connection);
                        };
                    connection.OnMessage = message =>
                        {
                            Console.WriteLine(message);
                            Connections.ToList().ForEach(s => s.Send("Echo: " + message));
                        };
                    connection.OnError = (Exception exception) =>
                        {
                            Console.WriteLine(exception.Message);                            
                            CloseCurrentConnection(connection);
                            Connections.Clear();
                        };
            });
        }

        public void Stop()
        {
            Connections.Clear();
            this.Dispose();
        }

        private void CloseCurrentConnection(IWebSocketConnection connection) {            
            CurrentConnection = null;
            connection.Close();
            this.Dispose();
        }
    }
}