using BotFS;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotFS.Utils
{
    public static class Logger
    {
        public static void Log(string Name, string Message) 
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"<{DateTime.Now.ToString("MMMM dd - HH:mm:ss")}> [BotFS] MongoDB | [{Name}] - {Message}");
            Console.ResetColor();
        }
        public static void Warn(string Name, string Message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"<{DateTime.Now.ToString("MMMM dd - HH:mm:ss")}> [BotFS] MongoDB | [{Name}] - {Message}");
            Console.ResetColor();
        }
        public static void Err(string Name, string Message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"<{DateTime.Now.ToString("MMMM dd - HH:mm:ss")}> [BotFS] MongoDB | [{Name}] - {Message}");
            Console.ResetColor();
        }
    }
}
