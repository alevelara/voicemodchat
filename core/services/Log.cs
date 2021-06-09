using System;
using Fleck;

namespace voicemodchat.core.services
{
    public static class Log
    {
        public static void Warn(string message, Exception ex = null) 
        {
            FleckLog.Warn(message, ex);
        }

        public static void Error(string message, Exception ex = null) {
            FleckLog.Error(message, ex);
        }

        public static void Debug(string message, Exception ex = null) {
            FleckLog.Debug(message, ex);
        }

        public static void Info(string message, Exception ex = null) {
            FleckLog.Info(message, ex);
        }
    }
}