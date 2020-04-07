using System;
using stdout = System.Console;
using color = System.ConsoleColor;

namespace BotFS.Utils
{
	public static class Logger
	{
		private static void WriteDate()
		{
			stdout.ResetColor();
			stdout.Write($"\n <{DateTime.Now:MM/dd - HH:mm:ss}> ");
		}

		public static void Log(string source, string message)
		{
			WriteDate();
			stdout.ForegroundColor = color.Cyan;
			stdout.Write($" {source} | {message} \t");
			stdout.ResetColor();
		}

		public static void Warn(string source, string message)
		{
			WriteDate();
			stdout.ForegroundColor = color.Yellow;
			stdout.Write($" {source} | {message} \t");
			stdout.ResetColor();
		}

		public static void Err(string source, string message)
		{
			WriteDate();
			stdout.ForegroundColor = color.Red;
			stdout.Write($" {source} | {message} \t");
			stdout.ResetColor();
		}
	}
}