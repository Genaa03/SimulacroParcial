using RecetasSLN.datos.Interfaces;
using RecetasSLN.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.datos.Implementaciones
{
    internal class GestorDAO : IGestorDAO
    {
        private IRecetaDAO oDAO;

        public GestorDAO()
        {
            oDAO = new RecetaDAO();
        }
        public DataTable cargarIngredientes()
        {
            return oDAO.cargarIngredientes();
        }

        public bool insertarMaestroDetalle(Receta oReceta)
        {
            return oDAO.insertarMaestroDetalle(oReceta);
        }

        public int siguienteReceta()
        {
            return oDAO.siguienteReceta();
        }
    }
}
