using FoliosApp.Comun;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace FoliosApp.Comun
{
    public static class ConectorMySql
    {
        static string stringConexion = $"Server=sql10.freesqldatabase.com; Port=3306; Database=sql10766266; Uid=sql10766266; Pwd=1y4NGdm57Z; default command timeout=3600;";
        public static IDbConnection dbConnection;

        public static void CrearConexion(Error error)
        {
            try
            {
                dbConnection = new MySqlConnection(stringConexion);
                dbConnection.Open();
                error.CodError = 1;
            }
            catch(Exception ex)
            {
                error.CodError = -1;
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
            }
        }

        public static IDbConnection GetConexionSistema(Error error)
        {
            try
            {
                ActualizarEstadoConexion(error);

                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                    error.CodError = 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Rutinas.GetMensajeError(ex.Message));
            }

            return dbConnection;
        }

        public static IDbConnection GetConexionEfimera(Error error)
        {
            IDbConnection dbConnectionEfimera = null;

            try
            {
                dbConnectionEfimera = new MySqlConnection(stringConexion);
                dbConnectionEfimera.Open();
                error.CodError = 1;
            }
            catch (Exception ex)
            {
                error.CodError = -1;
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
            }

            return dbConnectionEfimera;
        }

        private static bool ActualizarEstadoConexion(Error error)
        {
            try
            {
                using (var cmd = dbConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT 1";
                    cmd.ExecuteScalar();
                }

                return true;
            }
            catch (Exception ex)
            {
                error.CodError = -1;
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                return false;
            }
        }
    }
}
