using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using RecetasSLN.dominio;

namespace RecetasSLN.datos
{
    internal class RecetaDAO:AccesoDB, IRecetaDAO
    {
        public DataTable cargarIngredientes()
        {
            DataTable tabla = new DataTable();

            conectar();
            comando.CommandText = "SP_CONSULTAR_INGREDIENTES";
            tabla.Load(comando.ExecuteReader());
            desconectar();
            return tabla;
        }

        public int siguienteReceta()
        {
            conectar();

            comando.CommandText = "ProximaReceta";
            SqlParameter pOut = new SqlParameter("@prox", SqlDbType.Int);
            pOut.Direction = ParameterDirection.Output;
            comando.Parameters.Add(pOut);
            comando.ExecuteNonQuery();

            desconectar();

            return (int)pOut.Value;
        }

        public bool ejecutarSP(Receta oReceta)
        {
            bool estado = true;
            SqlTransaction t = null;

            try
            {
                conectar();
                t = conexion.BeginTransaction();
                comando.CommandText = "SP_INSERTAR_RECETA";
                comando.Transaction = t;
                comando.Parameters.AddWithValue("@id_receta",oReceta.RecetaNro);
                comando.Parameters.AddWithValue("@tipo_receta", oReceta.TipoReceta);
                comando.Parameters.AddWithValue("@nombre", oReceta.Nombre);
                if(oReceta.Cheff != null)
                {
                    comando.Parameters.AddWithValue("@cheff", oReceta.Cheff);
                }
                else
                {
                    comando.Parameters.AddWithValue("@cheff", DBNull.Value);
                }

                comando.ExecuteNonQuery();
                comando.Parameters.Clear();
                int count = 1;

                foreach (DetalleReceta detalle in oReceta.DetalleRecetas)
                {
                    comando.CommandText = "SP_INSERTAR_DETALLES";
                    comando.Parameters.AddWithValue("@id_receta", oReceta.RecetaNro);
                    comando.Parameters.AddWithValue("@id_ingrediente", detalle.oIngrediente.IngredienteID);
                    comando.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                    count++;
                    comando.ExecuteNonQuery();
                    comando.Parameters.Clear();

                }

                t.Commit();

            }
            catch (SqlException)
            {
                if (t != null)
                {
                    t.Rollback();
                    estado = false;
                }
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    desconectar();
                }
            }

            return estado;
        }
    }
}
