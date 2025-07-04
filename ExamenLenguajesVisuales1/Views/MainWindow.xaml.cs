using System.Windows;
using ExamenLenguajesVisuales1.ViewModels;
using ExamenLenguajesVisuales1.Views;
using ExamenLenguajesVisuales1.Models;
using ExamenLenguajesVisuales1.Data;

namespace ExamenLenguajesVisuales1.Views
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel vm;

        public MainWindow()
        {
            InitializeComponent();
            vm = new MainWindowViewModel();
            DataContext = vm;
        }

        // --- CRUD de PRODUCTOS ---
        private void AgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            var win = new ProductoFormWindow();
            var result = win.ShowDialog();
            if (result == true)
                vm.CargarDatos();
        }

        private void EditarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (vm.ProductoSeleccionado == null)
            {
                MessageBox.Show("Selecciona un producto para editar.");
                return;
            }
            var win = new ProductoFormWindow(vm.ProductoSeleccionado);
            var result = win.ShowDialog();
            if (result == true)
                vm.CargarDatos();
        }

        private void EliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (vm.ProductoSeleccionado == null)
            {
                MessageBox.Show("Selecciona un producto para eliminar.");
                return;
            }
            var confirm = MessageBox.Show("¿Seguro que quieres eliminar el producto?", "Confirmar", MessageBoxButton.YesNo);
            if (confirm == MessageBoxResult.Yes)
            {
                using (var db = new AppDbContext())
                {
                    var prod = db.Productos.Find(vm.ProductoSeleccionado.Id);
                    if (prod != null)
                    {
                        db.Productos.Remove(prod);
                        db.SaveChanges();
                    }
                }
                vm.CargarDatos();
            }


        }

        private void BuscarProducto_Click(object sender, RoutedEventArgs e)
        {
            vm.FiltrarProductos();
        }

        // --- CRUD de CATEGORÍAS ---
        private void AgregarCategoria_Click(object sender, RoutedEventArgs e)
        {
            var win = new CategoriaFormWindow();
            var result = win.ShowDialog();
            if (result == true)
                vm.CargarDatos();
        }

        private void EditarCategoria_Click(object sender, RoutedEventArgs e)
        {
            if (vm.CategoriaSeleccionada == null)
            {
                MessageBox.Show("Selecciona una categoría para editar.");
                return;
            }
            var win = new CategoriaFormWindow(vm.CategoriaSeleccionada);
            var result = win.ShowDialog();
            if (result == true)
                vm.CargarDatos();
        }

        private void EliminarCategoria_Click(object sender, RoutedEventArgs e)
        {
            if (vm.CategoriaSeleccionada == null)
            {
                MessageBox.Show("Selecciona una categoría para eliminar.");
                return;
            }

            // Validar que la categoría no tenga productos asociados
            using (var db = new AppDbContext())
            {
                var cat = db.Categorias.Find(vm.CategoriaSeleccionada.Id);
                var tieneProductos = db.Productos.Any(p => p.CategoriaId == vm.CategoriaSeleccionada.Id);
                if (tieneProductos)
                {
                    MessageBox.Show("No se puede eliminar una categoría que tiene productos asociados.", "Operación no permitida", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (cat != null)
                {
                    var confirm = MessageBox.Show("¿Seguro que quieres eliminar la categoría?", "Confirmar", MessageBoxButton.YesNo);
                    if (confirm == MessageBoxResult.Yes)
                    {
                        db.Categorias.Remove(cat);
                        db.SaveChanges();
                    }
                }
            }
            vm.CargarDatos();
        }

        

        private void BuscarCategoria_Click(object sender, RoutedEventArgs e)
        {
            vm.FiltrarCategorias();
        }
    }
}
