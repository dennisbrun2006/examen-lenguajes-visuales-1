using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ExamenLenguajesVisuales1.Data;
using ExamenLenguajesVisuales1.Models;
using Microsoft.EntityFrameworkCore; // IMPORTANTE para .Include()

namespace ExamenLenguajesVisuales1.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // Colecciones base
        private ObservableCollection<Producto> productos;
        public ObservableCollection<Producto> Productos
        {
            get => productos;
            set { productos = value; OnPropertyChanged(nameof(Productos)); }
        }

        private ObservableCollection<Categoria> categorias;
        public ObservableCollection<Categoria> Categorias
        {
            get => categorias;
            set { categorias = value; OnPropertyChanged(nameof(Categorias)); }
        }

        // Filtros
        private string filtroProducto;
        public string FiltroProducto
        {
            get => filtroProducto;
            set { filtroProducto = value; FiltrarProductos(); OnPropertyChanged(nameof(FiltroProducto)); }
        }

        private string filtroCategoria;
        public string FiltroCategoria
        {
            get => filtroCategoria;
            set { filtroCategoria = value; FiltrarCategorias(); OnPropertyChanged(nameof(FiltroCategoria)); }
        }

        // Colecciones filtradas (lo que se ve en los DataGrid)
        private ObservableCollection<Producto> productosFiltrados;
        public ObservableCollection<Producto> ProductosFiltrados
        {
            get => productosFiltrados;
            set { productosFiltrados = value; OnPropertyChanged(nameof(ProductosFiltrados)); }
        }

        private ObservableCollection<Categoria> categoriasFiltradas;
        public ObservableCollection<Categoria> CategoriasFiltradas
        {
            get => categoriasFiltradas;
            set { categoriasFiltradas = value; OnPropertyChanged(nameof(CategoriasFiltradas)); }
        }

        // Seleccionados
        private Producto productoSeleccionado;
        public Producto ProductoSeleccionado
        {
            get => productoSeleccionado;
            set { productoSeleccionado = value; OnPropertyChanged(nameof(ProductoSeleccionado)); }
        }

        private Categoria categoriaSeleccionada;
        public Categoria CategoriaSeleccionada
        {
            get => categoriaSeleccionada;
            set { categoriaSeleccionada = value; OnPropertyChanged(nameof(CategoriaSeleccionada)); }
        }

        // Constructor
        public MainWindowViewModel()
        {
            CargarDatos();
        }

        public void CargarDatos()
        {
            using (var db = new AppDbContext())
            {
                var categoriasList = db.Categorias.ToList();
                Categorias = new ObservableCollection<Categoria>(categoriasList);
                CategoriasFiltradas = new ObservableCollection<Categoria>(categoriasList);

                var productosList = db.Productos.Include("Categoria").ToList();
                Productos = new ObservableCollection<Producto>(productosList);
                ProductosFiltrados = new ObservableCollection<Producto>(productosList);
            }
        }

        public void FiltrarProductos()
        {
            if (string.IsNullOrWhiteSpace(FiltroProducto))
                ProductosFiltrados = new ObservableCollection<Producto>(Productos);
            else
                ProductosFiltrados = new ObservableCollection<Producto>(
                    Productos.Where(p => p.Nombre.ToLower().Contains(FiltroProducto.ToLower())));
        }

        public void FiltrarCategorias()
        {
            if (string.IsNullOrWhiteSpace(FiltroCategoria))
                CategoriasFiltradas = new ObservableCollection<Categoria>(Categorias);
            else
                CategoriasFiltradas = new ObservableCollection<Categoria>(
                    Categorias.Where(c => c.Nombre.ToLower().Contains(FiltroCategoria.ToLower())));
        }

        // Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public int TotalProductos => Productos.Count;
        public int TotalCategorias => Categorias.Count;

    }
}
