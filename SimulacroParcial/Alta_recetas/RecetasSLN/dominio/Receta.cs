using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.dominio
{
    public class Receta
    {
        public int RecetaNro { get; set; }
        public string Nombre { get; set; }
        public string Cheff { get; set; }
        public int TipoReceta { get; set; }

        public List<DetalleReceta> DetalleRecetas { get; set; }

        public Receta()
        {
            DetalleRecetas = new List<DetalleReceta>();
        }

        public void AgregarDetalle(DetalleReceta detalle)
        {
            DetalleRecetas.Add(detalle);
        }

        public void EliminarDetalle(int indice)
        {
            DetalleRecetas.RemoveAt(indice);
        }
    }
}
