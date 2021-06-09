using System;
using System.Threading.Tasks;

namespace voicemodchat.core.models.interfaces
{
    public interface IClient : IDisposable
    {
         Task ConnectAsync(string ipAddress);

         void SendMessage(string message);

         void Close();         
    }
}