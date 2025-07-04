using System.Collections.ObjectModel;
using System.ComponentModel;
using ExamenLenguajesVisuales1.Data;
using ExamenLenguajesVisuales1.Models;
using System.Linq;

namespace ExamenLenguajesVisuales1.ViewModels
{
    public class ProductoFormViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Categoria> ListaCategorias { get; set; }
        private Categoria categoriaSeleccionada;
        public Categoria CategoriaSeleccionada
        {
            get { return categoriaSeleccionada; }
            set { categoriaSeleccionada = value; OnPropertyChanged(nameof(CategoriaSeleccionada)); }
        }

        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; OnPropertyChanged(nameof(Nombre)); }
        }

        private string precio;
        public string Precio
        {
            get { return precio; }
            set { precio = value; OnPropertyChanged(nameof(Precio)); }
        }

        private string mensaje;
        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; OnPropertyChanged(nameof(Mensaje)); }
        }

        public Producto ProductoEditado { get; set; }
        public bool EsEdicion { get; set; } = false;

        public ProductoFormViewModel()
        {
            // Cargar categorías
            using (var db = new AppDbContext())
            {
                ListaCategorias = new ObservableCollection<Categoria>(db.Categorias.ToList());
            }
        }

        public void CargarProducto(Producto producto)
        {
            if (producto == null) return;
            ProductoEditado = producto;
            EsEdicion = true;
            Nombre = producto.Nombre;
            Precio = producto.Precio.ToString();
            CategoriaSeleccionada = ListaCategorias.FirstOrDefault(c => c.Id == producto.CategoriaId);
        }

        public bool Guardar()
        {
            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Precio) || CategoriaSeleccionada == null)
            {
                Mensaje = "Todos los campos son obligatorios.";
                return false;
            }
            if (!decimal.TryParse(Precio, out decimal precioDecimal) || precioDecimal < 0)
            {
                Mensaje = "El precio debe ser un número positivo.";
                return false;
            }

            using (var db = new AppDbContext())
            {
                if (EsEdicion && ProductoEditado != null)
                {
                    var prod = db.Productos.Find(ProductoEditado.Id);
                    if (prod != null)
                    {
                        prod.Nombre = Nombre;
                        prod.Precio = precioDecimal;
                        prod.CategoriaId = CategoriaSeleccionada.Id;
                        db.SaveChanges();
                    }
                }
                else
                {
                    var nuevo = new Producto
                    {
                        Nombre = Nombre,
                        Precio = precioDecimal,
                        CategoriaId = CategoriaSeleccionada.Id
                    };
                    db.Productos.Add(nuevo);
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
