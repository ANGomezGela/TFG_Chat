using System;
using System.Windows.Forms;

namespace Cliente
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Aplikazioa hasieratu eta exekutatzen du
            ApplicationConfiguration.Initialize();
            Application.Run(new AppContext());
        }
    }

    // Aplikazioaren formularioak kudeatzeko klasea
    public class AppContext : ApplicationContext
    {
        public AppContext()
        {
            // Aplikazioa Login formularioarekin hasten da
            Login loginForm = new Login();
            loginForm.LoginSuccess += OnLoginSuccess;  // Login arrakastatsua denean gertatuko den ekitaldia
            loginForm.Show();  // Login formularioa bistaratzen du
        }

        private void OnLoginSuccess(object sender, LoginEventArgs e)
        {
            // Erabiltzailea autentikatzen denean, Login formularioa itxi
            ((Login)sender).Close();

            // Chat formularioa irekitzen du jasotako datuekin
            Chat chatForm = new Chat(e.Client, e.Stream, e.Reader, e.Writer, e.UserName);
            chatForm.FormClosed += (s, args) => ExitThread();  // Chat formularioa ixtean aplikazioa amaitu
            chatForm.Show();  // Chat formularioa bistaratzen du
        }
    }
}
