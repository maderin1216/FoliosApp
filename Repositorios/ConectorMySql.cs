using FoliosApp.Comun;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace FoliosApp.Repositorios
{
    public class ConectorMySql
    {
        string stringConexion = $"Server=sql10.freesqldatabase.com; Port=3306; Database=sql10766266; Uid=sql10766266; Pwd=1y4NGdm57Z; default command timeout=3600;";

        public IDbConnection GetConexion(Error error)
        {
            string sLabel = $"[{typeof(ConectorMySql).Namespace}.{Rutinas.GetNombreMetodo()}]: ";
            IDbConnection dbConnection = new MySqlConnection(stringConexion);

            AbrirConexion(ref dbConnection, error);

            return dbConnection;
        }

        private void AbrirConexion(ref IDbConnection conexion, Error error)
        {
            string sLabel = $"[{typeof(ConectorMySql).Namespace}.{Rutinas.GetNombreMetodo()}]: ";

            try
            {
                conexion.Open();
                error.CodError = 1;
            }
            catch (Exception ex)
            {
                error.CodError = -1;
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
            }            
        }
    }
}
