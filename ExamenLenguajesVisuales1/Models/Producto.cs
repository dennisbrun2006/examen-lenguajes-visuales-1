﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenLenguajesVisuales1.Models
{
    // Models/Producto.cs
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }

}
