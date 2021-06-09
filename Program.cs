using System;
using voicemodchat.core.models.interfaces;
using voicemodchat.core.repositories.dependencies;
using voicemodchat.core.services.client;
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
                
                StartServer();
                ConnectClient(ipAddress);

                Console.Write("\nPress any key to exit...");
                Console.ReadKey();
            }catch(Exception e){
                Console.Write(e.Message);
                Console.ReadKey();
            }
        }

        private static void RegisterDependencies(string paramValue)
        {
            DependenciesManager.RegisterTypeWithParam<Server, IServer>("IpAddress", paramValue);
            DependenciesManager.RegisterType<Client, IClient>();
            DependenciesManager.LoadDependencies();
        }     

        private static void StartServer() 
        {
            var server = DependenciesManager.Container.ResolveDependency<IServer>();                                      
            server.Start();            
        }  

        private static void ConnectClient(string ipAddress) 
        {
            using(var client = DependenciesManager.Container.ResolveDependency<IClient>())
            {                
                client.ConnectAsync(ipAddress);                
            } 
        }              
    }
}