using System;
using System.Threading.Tasks;

namespace voicemodchat.core.models.interfaces
{
    public interface IServer : IDisposable
    {
        void Start();

        void Stop();

        Task Ping();

        Task Pong();
    }
}