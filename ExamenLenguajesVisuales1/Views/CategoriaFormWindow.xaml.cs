using System.Windows;
using ExamenLenguajesVisuales1.Models;
using ExamenLenguajesVisuales1.ViewModels;

namespace ExamenLenguajesVisuales1.Views
{
    public partial class CategoriaFormWindow : Window
    {
        private CategoriaFormViewModel vm;

        public CategoriaFormWindow(Categoria categoria = null)
        {
            InitializeComponent();
            vm = new CategoriaFormViewModel();
            DataContext = vm;
            if (categoria != null)
                vm.CargarCategoria(categoria);
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (vm.Guardar())
            {
                MessageBox.Show(vm.EsEdicion ? "Categoría actualizada correctamente." : "Categoría agregada correctamente.",
                                "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
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
