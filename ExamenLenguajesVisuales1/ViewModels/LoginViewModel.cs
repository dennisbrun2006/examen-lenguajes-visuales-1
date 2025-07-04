using System.ComponentModel;
using System.Linq;
using ExamenLenguajesVisuales1.Data;

namespace ExamenLenguajesVisuales1.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string correo;
        public string Correo
        {
            get { return correo; }
            set { correo = value; OnPropertyChanged(nameof(Correo)); }
        }

        private string mensaje;
        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; OnPropertyChanged(nameof(Mensaje)); }
        }

        public bool Login(string password)
        {
            if (string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(password))
            {
                Mensaje = "Completa todos los campos.";
                return false;
            }

            using (var db = new AppDbContext())
            {
                var user = db.Usuarios.FirstOrDefault(u => u.Correo == Correo && u.Contraseña == password);
                if (user != null)
                {
                    Mensaje = "";
                    return true;
                }
                else
                {
                    Mensaje = "Correo o contraseña incorrectos.";
                    return false;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
