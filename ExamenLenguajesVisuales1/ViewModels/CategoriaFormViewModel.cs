using System.ComponentModel;
using ExamenLenguajesVisuales1.Data;
using ExamenLenguajesVisuales1.Models;
using System.Linq;

namespace ExamenLenguajesVisuales1.ViewModels
{
    public class CategoriaFormViewModel : INotifyPropertyChanged
    {
        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; OnPropertyChanged(nameof(Nombre)); }
        }

        private string mensaje;
        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; OnPropertyChanged(nameof(Mensaje)); }
        }

        public Categoria CategoriaEditada { get; set; }
        public bool EsEdicion { get; set; } = false;

        public void CargarCategoria(Categoria categoria)
        {
            if (categoria == null) return;
            CategoriaEditada = categoria;
            EsEdicion = true;
            Nombre = categoria.Nombre;
        }

        public bool Guardar()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                Mensaje = "El nombre es obligatorio.";
                return false;
            }

            using (var db = new AppDbContext())
            {
                if (EsEdicion && CategoriaEditada != null)
                {
                    var cat = db.Categorias.Find(CategoriaEditada.Id);
                    if (cat != null)
                    {
                        cat.Nombre = Nombre;
                        db.SaveChanges();
                    }
                }
                else
                {
                    // Verificar si ya existe una categoría con ese nombre
                    if (db.Categorias.Any(c => c.Nombre == Nombre))
                    {
                        Mensaje = "Ya existe una categoría con ese nombre.";
                        return false;
                    }
                    var nueva = new Categoria { Nombre = Nombre };
                    db.Categorias.Add(nueva);
                    db.SaveChanges();
                }
            }
            Mensaje = "";
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
