using System;
using System.Windows.Forms;
using OrganizerApp;

namespace Auth
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());
        }
    }
}