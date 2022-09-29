using RecetasSLN.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.datos.Interfaces
{
    internal interface IGestorDAO
    {
        DataTable cargarIngredientes();

        int siguienteReceta();

        bool insertarMaestroDetalle(Receta oReceta);
    }
}
