using FoliosApp.Comun;
using FoliosApp.Modelos;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Linq;

namespace FoliosApp.Repositorios
{
    public class RepositorioConfirmaciones
    {
        ConectorMySql conectorBD = new ConectorMySql();
        IDbConnection dbConnection = null;

        public List<Confirmacion> GetAllConfirmaciones(Error error)
        {
            List<Confirmacion> retorno = new List<Confirmacion>();
            string sSql = "";
            DynamicParameters parametrosDapperLocal = new DynamicParameters();

            sSql = @"   SELECT
                            COALESCE(id, 0) AS Id,
                            COALESCE(documento, '') AS Documento,
                            COALESCE(TRIM(apellido), '') AS Apellido,
                            COALESCE(TRIM(nombre), '') AS Nombre,
                            COALESCE(numero_libro, 0) AS Libro,
                            COALESCE(numero_folio, 0) AS Folio,
                            COALESCE(fecha, '2000-01-01') AS Fecha
                        FROM
                            certificados_confirmacion
                        ORDER BY
                            libro ASC, apellido ASC;";

            try
            {
                dbConnection = conectorBD.GetConexion(error);
                retorno = dbConnection.Query<Confirmacion>(sSql, parametrosDapperLocal, commandType: CommandType.Text).ToList();
                error.CodError = 1;

                dbConnection.Close();
            }
            catch(Exception ex)
            {
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                error.CodError = -1;
            }

            return retorno;
        }

        public List<Confirmacion> GetConfirmacionesCriterio(CriteriosBusqueda criterio, string busqueda, Error error)
        {
            List<Confirmacion> retorno = new List<Confirmacion>();
            string sSql = "";
            DynamicParameters parametrosDapperLocal = new DynamicParameters();
            string criterioString = "";
            string[] busquedaSplitted = busqueda.Split(' ');

            try
            {
                switch (criterio)
                {
                    case CriteriosBusqueda.TodasLasColumnas:
                        criterioString = $@"    documento LIKE '%{busqueda}%'
                                                OR apellido LIKE '%{busqueda}%'
                                                OR nombre LIKE '%{busqueda}%'
                                                OR numero_libro = '{busqueda}'
                                                OR numero_folio = '{busqueda}'";
                        break;
                    case CriteriosBusqueda.Documento:
                        criterioString = $"documento LIKE '%{busqueda}%'";
                        break;
                    case CriteriosBusqueda.Apellido:
                        criterioString = $"apellido LIKE '%{busqueda}%'";
                        break;
                    case CriteriosBusqueda.Nombre:
                        criterioString = $"nombre LIKE '%{busqueda}%'";
                        break;
                    case CriteriosBusqueda.Libro:
                        criterioString = $"numero_libro = '{busqueda}'";
                        break;
                    case CriteriosBusqueda.Folio:
                        criterioString = $"numero_folio = '{busqueda}'";
                        break;
                    case CriteriosBusqueda.ApellidoNombre:                        
                        criterioString = $@"    CONCAT(apellido, ' ', nombre) LIKE '%{busqueda}%'
                                                OR CONCAT(nombre, ' ', apellido) LIKE '%{busqueda}'";
                        break;
                }

                sSql = $@"  SELECT
                            COALESCE(id, 0) AS Id,
                            COALESCE(documento, '') AS Documento,
                            COALESCE(TRIM(apellido), '') AS Apellido,
                            COALESCE(TRIM(nombre), '') AS Nombre,
                            COALESCE(numero_libro, 0) AS Libro,
                            COALESCE(numero_folio, 0) AS Folio,
                            COALESCE(fecha, '2000-01-01') AS Fecha
                        FROM
                            certificados_confirmacion
                        WHERE
                            {criterioString}
                        ORDER BY
                            libro ASC, apellido ASC;";


                dbConnection = conectorBD.GetConexion(error);
                retorno = dbConnection.Query<Confirmacion>(sSql, parametrosDapperLocal, commandType: CommandType.Text).ToList();
                error.CodError = 1;

                dbConnection.Close();
            }
            catch (Exception ex)
            {
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                error.CodError = -1;
            }

            return retorno;
        }

        public void AgregarConfirmacion(Confirmacion confirmacion, Error error)
        {
            string sSql1;
            string sSql2;
            DynamicParameters parametrosLocal = new DynamicParameters();
            int iIdInsertado;

            sSql1 = @"   INSERT INTO certificados_confirmacion
                            (
                                documento,
                                apellido,
                                nombre,
                                numero_libro,
                                numero_folio,
                                fecha
                            )
                        VALUES
                            (
                                @Documento,
                                @Apellido,
                                @Nombre,
                                @Libro,
                                @Folio,
                                @Fecha
                            );";

            sSql2 = @"   SELECT LAST_INSERT_ID();";

            parametrosLocal.Add("@Documento", confirmacion.Documento);
            parametrosLocal.Add("@Apellido", confirmacion.Apellido);
            parametrosLocal.Add("@Nombre", confirmacion.Nombre);
            parametrosLocal.Add("@Libro", confirmacion.Libro);
            parametrosLocal.Add("@Folio", confirmacion.Folio);
            parametrosLocal.Add("@Fecha", confirmacion.Fecha);

            try
            {
                dbConnection = conectorBD.GetConexion(error);
                dbConnection.Execute(sSql1, parametrosLocal, commandType: CommandType.Text);

                iIdInsertado = dbConnection.Query<int>(sSql2, parametrosLocal, commandType: CommandType.Text).FirstOrDefault();
                confirmacion.Id = iIdInsertado;

                error.CodError = 1;

                dbConnection.Close();
            }
            catch(Exception ex)
            {
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                error.CodError = -1;
            }
        }

        public void EditarConfirmacion(Confirmacion confirmacion, Error error)
        {
            string sSql;
            DynamicParameters parametrosLocal = new DynamicParameters();

            sSql = @"   UPDATE 
                            certificados_confirmacion 
                        SET
                            documento = @Documento,
                            apellido = @Apellido,
                            nombre = @Nombre,
                            numero_libro = @Libro,
                            numero_folio = @Folio,
                            fecha = @Fecha
                        WHERE
                            id = @Id;";

            parametrosLocal.Add("@Id", confirmacion.Id);
            parametrosLocal.Add("@Documento", confirmacion.Documento);
            parametrosLocal.Add("@Apellido", confirmacion.Apellido);
            parametrosLocal.Add("@Nombre", confirmacion.Nombre);
            parametrosLocal.Add("@Libro", confirmacion.Libro);
            parametrosLocal.Add("@Folio", confirmacion.Folio);
            parametrosLocal.Add("@Fecha", confirmacion.Fecha);

            try
            {
                dbConnection = conectorBD.GetConexion(error);
                dbConnection.Execute(sSql, parametrosLocal, commandType: CommandType.Text);
                error.CodError = 1;

                dbConnection.Close();
            }
            catch (Exception ex)
            {
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                error.CodError = -1;
            }
        }

        public void BorrarConfirmacion(Confirmacion confirmacion, Error error)
        {
            string sSql;
            DynamicParameters parametrosLocal = new DynamicParameters();

            sSql = @"   DELETE FROM
                            certificados_confirmacion
                        WHERE
                            id = @Id;";

            parametrosLocal.Add("@Id", confirmacion.Id);

            try
            {
                dbConnection = conectorBD.GetConexion(error);
                dbConnection.Execute(sSql, parametrosLocal, commandType: CommandType.Text);
                error.CodError = 1;

                dbConnection.Close();
            }
            catch (Exception ex)
            {
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                error.CodError = -1;
            }
        }
    }
}
