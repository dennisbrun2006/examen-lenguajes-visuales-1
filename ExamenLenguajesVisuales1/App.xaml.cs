using System.Windows;
using ExamenLenguajesVisuales1.Views;

namespace ExamenLenguajesVisuales1
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Abre primero la ventana de Login
            var login = new LoginWindow();
            login.Show();
        }
    }
}
