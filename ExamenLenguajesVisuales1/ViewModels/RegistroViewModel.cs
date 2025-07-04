using System.ComponentModel;
using System.Linq;
using ExamenLenguajesVisuales1.Data;
using ExamenLenguajesVisuales1.Models;

namespace ExamenLenguajesVisuales1.ViewModels
{
    public class RegistroViewModel : INotifyPropertyChanged
    {
        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; OnPropertyChanged(nameof(Nombre)); }
        }

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

        public bool Registrar(string password)
        {
            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(password))
            {
                Mensaje = "Todos los campos son obligatorios.";
                return false;
            }

            using (var db = new AppDbContext())
            {
                if (db.Usuarios.Any(u => u.Correo == Correo))
                {
                    Mensaje = "Ya existe un usuario con ese correo.";
                    return false;
                }

                var usuario = new Usuario
                {
                    Nombre = Nombre,
                    Correo = Correo,
                    Contraseña = password
                };
                db.Usuarios.Add(usuario);
                db.SaveChanges();
            }

            Mensaje = "";
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
