using System.Windows;
using ExamenLenguajesVisuales1.ViewModels;

namespace ExamenLenguajesVisuales1.Views
{
    public partial class LoginWindow : Window
    {
        private LoginViewModel vm;

        public LoginWindow()
        {
            InitializeComponent();
            vm = new LoginViewModel();
            DataContext = vm;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string password = txtPassword.Password;
            bool loginOk = vm.Login(password);

            if (loginOk)
            {
                MessageBox.Show("¡Bienvenido!");
                var mainWin = new MainWindow();
                mainWin.Show();
                this.Close();
            }
            // Si loginOk es false, el mensaje se muestra en la vista automáticamente
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var registro = new RegistroWindow();
            registro.Show();
            this.Close();
        }
    }
}
