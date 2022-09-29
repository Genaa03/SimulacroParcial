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
    class RecetaDAO : IRecetaDAO
    {
        public DataTable cargarIngredientes()
        {
            return HelperDAO.ObtenerInstancia().Consulta("SP_CONSULTAR_INGREDIENTES");
        }

        public bool insertarMaestroDetalle(Receta oReceta)
        {
            return HelperDAO.ObtenerInstancia().Ejecutar("SP_INSERTAR_RECETA", "SP_INSERTAR_DETALLES", oReceta);
        }

        public int siguienteReceta()
        {

            return HelperDAO.ObtenerInstancia().ConsultaNumero("SP_PROXIMO_ID", "@next");
        }
    }
}
