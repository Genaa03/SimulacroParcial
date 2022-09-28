using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using RecetasSLN.dominio;

namespace RecetasSLN.datos
{
    internal class GestorDB
    {
        private IRecetaDAO RDao;
        public DataTable listarIngredientes()
        {
            RDao = new RecetaDAO();
            return RDao.cargarIngredientes();
        }
        public int proximaReceta()
        {
            RDao = new RecetaDAO();
            return RDao.siguienteReceta();
        }
        public bool ejecutarSP(Receta oReceta)
        {
            RDao = new RecetaDAO();
            return RDao.ejecutarSP(oReceta);
        }
    }
}
