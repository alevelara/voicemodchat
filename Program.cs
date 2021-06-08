using System;
using voicemodchat.core.models.interfaces;
using voicemodchat.core.repositories.dependencies;
using voicemodchat.core.services.server;
using voicemodchat.core.utils;

namespace voicemodchat
{
    class Program
    {
        static void Main(string[] args)
        {
            try{                                
                string ipAddress = Utils.GetIpAddressFromArgs(args);                
                RegisterDependencies(ipAddress);
                
                var server = DependenciesManager.GetDependency<IServer>();
                server.Start();

                Console.Write("\nPress any key to exit...");
                Console.ReadKey();
            }catch(Exception e){
                Console.Write(e.Message);

            }
        }

        private static void RegisterDependencies(string paramValue){
            DependenciesManager.RegisterTypeWithParam<Server, IServer>("ipAddress", paramValue);
            DependenciesManager.LoadDependencies();
        }
    }
}
