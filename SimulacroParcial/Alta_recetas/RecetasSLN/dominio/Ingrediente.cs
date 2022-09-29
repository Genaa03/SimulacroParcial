using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.dominio
{
    public class Ingrediente
    {
        public Ingrediente(int ingredienteID, string nombre)
        {
            IngredienteID = ingredienteID;
            Nombre = nombre;
        }

        public int IngredienteID { get; set; }
        public string Nombre { get; set; }
        public string UnidadMedida { get; set; }

        public override string ToString()
        {
            return Nombre;
        }

    }
}
