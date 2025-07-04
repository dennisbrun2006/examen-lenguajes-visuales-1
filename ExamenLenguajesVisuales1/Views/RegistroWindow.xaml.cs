using System;
using System.Windows;
using ExamenLenguajesVisuales1.ViewModels;

namespace ExamenLenguajesVisuales1.Views
{
    public partial class RegistroWindow : Window
    {
        private RegistroViewModel vm;

        public RegistroWindow()
        {
            InitializeComponent();
            vm = new RegistroViewModel();
            DataContext = vm;
        }

        private void Registrar_Click(object sender, RoutedEventArgs e)
        {
            string password = txtPassword.Password;
            bool registrado = vm.Registrar(password);

            if (registrado)
            {
                MessageBox.Show("¡Usuario registrado! Ahora puedes iniciar sesión.", "Registro exitoso");
                var login = new LoginWindow();
                login.Show();
                this.Close();
            }
        }

        private void VolverLogin_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}
