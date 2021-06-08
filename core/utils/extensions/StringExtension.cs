using System;

namespace voicemodchat.core.utils.extensions
{
    public static class StringExtension
    {
         public static byte[] ToByteArray(this string str)
        {
            return System.Text.Encoding.ASCII.GetBytes(str);
        }

        public static bool TryParsetoUshort(this string str, out ushort port) 
        {
            return ushort.TryParse(str, out port);
        }
    }
}