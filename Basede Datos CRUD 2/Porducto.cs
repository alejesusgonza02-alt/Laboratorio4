using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejemplo_en_clase_1
{
    internal class Producto
    {
        public int Id { get; set; }
        public string ProductoNombre { get; set; } = "";
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string Imagen { get; set; } = "";
    }
}