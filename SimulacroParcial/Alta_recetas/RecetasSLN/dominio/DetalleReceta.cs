using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.dominio
{
    public class DetalleReceta
    {
        public DetalleReceta(Receta receta, Ingrediente ingrediente, double cantidad)
        {
            oReceta = receta;
            oIngrediente = ingrediente;
            Cantidad = cantidad;
        }

        public Receta oReceta { get; set; }
        public Ingrediente oIngrediente { get; set; }  
        public double Cantidad { get; set; }
    }
}
