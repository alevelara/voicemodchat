using System;
using voicemodchat.core.utils.constants;
using voicemodchat.core.utils.extensions;

namespace voicemodchat.core.utils
{
    public static class Utils
    {
        public static string GetIpAddressFromArgs(string[] args) {
            
            string ipAddress = String.Empty;

            if(args.Length == 0) 
            {
                throw new Exception($"Port number can not be null: To start the application, write: {System.Reflection.Assembly.GetEntryAssembly().Location} PORT");
            }
            else {
                                
            if(!args[0].TryParseToUshort(out ushort port))
            {
                throw new Exception($"Port number is invalid: Number must be between [0 - {ushort.MaxValue}]");
            } 
            else 
            {
                ipAddress = Constants.LOCAL_IP + ":" + port;
            }
        }

        return ipAddress;

        }
    }
}