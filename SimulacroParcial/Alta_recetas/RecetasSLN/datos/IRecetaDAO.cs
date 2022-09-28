using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using RecetasSLN.dominio;

namespace RecetasSLN.datos
{
    internal interface IRecetaDAO
    {
        DataTable cargarIngredientes();

        int siguienteReceta();

        bool ejecutarSP(Receta oReceta);
    }
}
