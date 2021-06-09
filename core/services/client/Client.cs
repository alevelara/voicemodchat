using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using voicemodchat.core.models.interfaces;

namespace voicemodchat.core.services.client
{
    public class Client : IClient
    {
        private ClientWebSocket _clientSocket;

        public Client()
        {   
            
        }
        
        public void Close()
        {
            throw new System.NotImplementedException();
        }

        public async Task ConnectAsync(string ipAddress)
        {
            _clientSocket = new ClientWebSocket();
            
            try
            {
                Log.Info("Connecting to the server...");
                await _clientSocket.ConnectAsync(new Uri(ipAddress), CancellationToken.None);
                Log.Info("Connected.");
            } catch(WebSocketException e)
            {
                Log.Error(e.Message);
            }            
        }

        public void Dispose()
        {
            _clientSocket.Dispose();
        }

        public void SendMessage(string message)
        {
            throw new System.NotImplementedException();
        }

        private async Task<ValueWebSocketReceiveResult> ReceiveDataFromServer()
        {
            Memory<Byte> buffer = new byte[1024];
            ValueTask<ValueWebSocketReceiveResult> result = _clientSocket.ReceiveAsync(buffer, CancellationToken.None);
            return result.Result;
        }
    }
}