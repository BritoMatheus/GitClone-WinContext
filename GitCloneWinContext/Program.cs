using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace GitCloneWinContext
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ConfigureTerminal();

            var currentDirectory = Directory.GetCurrentDirectory();
            try
            {

                var clipboard = Clipboard.GetText();
                if (!ValidClipboard(clipboard))
                    return;
                Clone(clipboard, currentDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error => {ex.Message}");
                Console.ReadLine();
            }
        }

        internal static void ConfigureTerminal()
        {
            Console.Title = "GIT | WINCONTEXT";
        }

        internal static bool ValidClipboard(string clipboard)
        {
            if (!clipboard.EndsWith(".git") || (!clipboard.StartsWith("https") && !clipboard.StartsWith("http")))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("     ");
                Console.WriteLine("     ");
                Console.WriteLine("     ");
                Console.WriteLine("     ");
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "SEM REPOSITÓRIO PARA CLONAR!!!"));
                Console.ReadKey();
                return false;
            }
            return true;
        }

        internal static void Clone(string url, string directory)
        {
            new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"git",
                    Arguments = $@"clone {url}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    WorkingDirectory = $@"{directory}\"
                }
            }.Start();
        }
    }
}
