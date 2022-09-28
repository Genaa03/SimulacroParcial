using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace RecetasSLN.datos
{
    internal class AccesoDB
    {
        protected static AccesoDB instancia;
        protected SqlConnection conexion;
        protected SqlCommand comando;

        protected AccesoDB()
        {
            conexion = new SqlConnection(Properties.Resources.conexionString);
        }

        protected static AccesoDB ObtenerInstancia()
        {
            if(instancia == null)
            {
                instancia = new AccesoDB();
            }
            return instancia;
        }

        protected void conectar()
        {
            conexion.Open();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
        }

        protected void desconectar()
        {
            conexion.Close();
        }

        public SqlConnection ObetenerConexion()
        {
            return this.conexion;
        }
    }
    
}
