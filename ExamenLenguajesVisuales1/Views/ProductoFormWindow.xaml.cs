using System.Windows;
using ExamenLenguajesVisuales1.Models;
using ExamenLenguajesVisuales1.ViewModels;

namespace ExamenLenguajesVisuales1.Views
{
    public partial class ProductoFormWindow : Window
    {
        private ProductoFormViewModel vm;

        public ProductoFormWindow(Producto producto = null)
        {
            InitializeComponent();
            vm = new ProductoFormViewModel();
            DataContext = vm;
            if (producto != null)
                vm.CargarProducto(producto);
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (vm.Guardar())
            {
                MessageBox.Show(vm.EsEdicion ? "Producto actualizado correctamente." : "Producto agregado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
        }

        

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
