using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using RecetasSLN.dominio;

namespace RecetasSLN.dominio
{
    internal class HelperDAO
    {
        private static HelperDAO instancia;
        private SqlConnection conexion;
        private SqlCommand comando = new SqlCommand();

        private HelperDAO()
        {
            conexion = new SqlConnection(Properties.Resources.conexion2);
        }

        public static HelperDAO ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new HelperDAO();
            }
            return instancia;
        }

        private void conectar()
        {
            conexion.Open();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;

        }

        private void desconectar()
        {
            conexion.Close();
        }
        
        public DataTable Consulta(string nombreSP)
        {
            conectar();
            DataTable tabla = new DataTable();

            comando.CommandText = nombreSP;
            tabla.Load(comando.ExecuteReader());

            desconectar();
            return tabla;
        }

        public int ConsultaNumero(string nombreSP, string paramSalida)
        {
            SqlParameter pOut = new SqlParameter(paramSalida, SqlDbType.Int);
            try
            {
                conectar();
                comando.Parameters.Clear();
                comando.CommandText = nombreSP;

                pOut.Direction = ParameterDirection.Output;
                comando.Parameters.Add(pOut);
                comando.ExecuteNonQuery();

                return (int)pOut.Value;
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                desconectar();
            }  
        }


        public bool Ejecutar(string MaestroSP, string DetalleSP, Receta receta)
        {
            bool control = true;
            SqlTransaction t = null;

            try
            {
                conectar();
                comando.Parameters.Clear();
                t = conexion.BeginTransaction();
                comando.Transaction = t;

                // Maestro
                comando.CommandText = MaestroSP;
                comando.Parameters.AddWithValue("@id_receta", receta.RecetaNro);
                comando.Parameters.AddWithValue("@tipo_receta", receta.TipoReceta);
                comando.Parameters.AddWithValue("@nombre", receta.Nombre);
                comando.Parameters.AddWithValue("@cheff", receta.Cheff);
                comando.ExecuteNonQuery();
                comando.Parameters.Clear();



                // Detalle

                int count = 1;

                foreach(DetalleReceta detalle in receta.DetalleRecetas)
                {
                    comando.CommandText = DetalleSP;

                    comando.Parameters.AddWithValue("@id_receta", receta.RecetaNro);
                    comando.Parameters.AddWithValue("@id_ingrediente", detalle.oIngrediente.IngredienteID);
                    comando.Parameters.AddWithValue("@cantidad", detalle.Cantidad);

                    count++;

                    comando.ExecuteNonQuery();
                    comando.Parameters.Clear();
                }

                t.Commit();

            }
            catch (Exception)
            {
                t.Rollback();
                control = false;
            }

            finally
            {
                desconectar();
            }
            return control;
        }


    
    
    }
}
