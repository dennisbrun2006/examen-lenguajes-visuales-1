using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenLenguajesVisuales1.Models
{
    // Models/Categoria.cs
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<Producto> Productos { get; set; }
    }

}
