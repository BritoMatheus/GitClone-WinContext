using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;

namespace GitCloneWinContextInstaller
{
    class Program
    {
        //DesktopBackground
        //Directory
        //*
        private const string Exe = @"\GitCloneWinContext.exe";
        private const string MenuName = "\\shell\\GitCloneOption";
        private const string Command = "\\shell\\GitCloneOption\\command";
        private const string MenuLabel = "Git Clone";

        private const string Title = "GIT | WinContextInstaller";

        static void Main(string[] args)
        {
            Console.Title = Title;

            try
            {
                AddRegistry("*");
                AddRegistry("Directory");
                AddRegistry(@"Directory\Background");
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal static void AddRegistry(string root)
        {
            RegistryKey regmenu = null;
            RegistryKey regcmd = null;
            try
            {
                var currentDirectory = Directory.GetCurrentDirectory();

                regmenu = Registry.ClassesRoot.CreateSubKey($"{root}{MenuName}");
                if (regmenu != null)
                {
                    regmenu.SetValue("", MenuLabel);
                    regmenu.SetValue("Icon", $@"{currentDirectory}\{Exe}");

                }
                regcmd = Registry.ClassesRoot.CreateSubKey($"{root}{Command}");
                if (regcmd != null)
                {
                    regcmd.SetValue("", $@"{currentDirectory}\{Exe}");
                }
                MessageBox.Show("Add with success", Title);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Execute as adm", Title, MessageBoxButtons.OK);
                Close(regmenu, regcmd);
                throw;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.ToString()}", Title);
                Close(regmenu, regcmd);
                throw;
            }
            finally
            {
                Close(regmenu, regcmd);
            }
        }

        internal static void Close(RegistryKey regmenu, RegistryKey regcmd)
        {
            if (regmenu != null)
                regmenu.Close();
            if (regcmd != null)
                regcmd.Close();
        }
    }
}
