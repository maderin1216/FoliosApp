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
                retorno = ConectorMySql.GetConexionSistema(error).Query<Confirmacion>(sSql, parametrosDapperLocal, commandType: CommandType.Text).ToList();
                error.CodError = 1;
            }
            catch(Exception ex)
            {
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                error.CodError = -1;
            }

            return retorno;
        }

        public List<Confirmacion> GetConfirmacionesCriterio(CriteriosBusquedaConfirmacion criterio, string busqueda, Error error)
        {
            List<Confirmacion> retorno = new List<Confirmacion>();
            string sSql = "";
            DynamicParameters parametrosDapperLocal = new DynamicParameters();
            string criterioString = "";
            string[] busquedaSplitted = busqueda.Split(' ');
            DateTime resultFecha;

            try
            {
                switch (criterio)
                {
                    case CriteriosBusquedaConfirmacion.TodasLasColumnas:
                        criterioString = $@"    documento LIKE '%{busqueda}%'
                                                OR apellido LIKE '%{busqueda}%'
                                                OR nombre LIKE '%{busqueda}%'
                                                OR numero_libro = '{busqueda}'
                                                OR numero_folio = '{busqueda}'
                                                OR fecha = '{(DateTime.TryParse(busqueda, out resultFecha) ? resultFecha.ToString("yyyy-MM-dd") : "")}'";
                        break;
                    case CriteriosBusquedaConfirmacion.Documento:
                        criterioString = $"documento LIKE '%{busqueda}%'";
                        break;
                    case CriteriosBusquedaConfirmacion.Apellido:
                        criterioString = $"apellido LIKE '%{busqueda}%'";
                        break;
                    case CriteriosBusquedaConfirmacion.Nombre:
                        criterioString = $"nombre LIKE '%{busqueda}%'";
                        break;
                    case CriteriosBusquedaConfirmacion.Libro:
                        criterioString = $"numero_libro = '{busqueda}'";
                        break;
                    case CriteriosBusquedaConfirmacion.Folio:
                        criterioString = $"numero_folio = '{busqueda}'";
                        break;
                    case CriteriosBusquedaConfirmacion.ApellidoNombre:                        
                        criterioString = $@"    CONCAT(apellido, ' ', nombre) LIKE '%{busqueda}%'
                                                OR CONCAT(nombre, ' ', apellido) LIKE '%{busqueda}'";
                        break;
                    case CriteriosBusquedaConfirmacion.FechaConfirmacion:
                        criterioString = $"fecha = '{(DateTime.TryParse(busqueda, out resultFecha) ? resultFecha.ToString("yyyy-MM-dd") : "")}'";
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

                retorno = ConectorMySql.GetConexionSistema(error).Query<Confirmacion>(sSql, parametrosDapperLocal, commandType: CommandType.Text).ToList();
                error.CodError = 1;
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
                ConectorMySql.GetConexionSistema(error).Execute(sSql1, parametrosLocal, commandType: CommandType.Text);

                iIdInsertado = ConectorMySql.GetConexionSistema(error).Query<int>(sSql2, parametrosLocal, commandType: CommandType.Text).FirstOrDefault();
                confirmacion.Id = iIdInsertado;

                error.CodError = 1;
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
                ConectorMySql.GetConexionSistema(error).Execute(sSql, parametrosLocal, commandType: CommandType.Text);
                error.CodError = 1;
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
                ConectorMySql.GetConexionSistema(error).Execute(sSql, parametrosLocal, commandType: CommandType.Text);
                error.CodError = 1;
            }
            catch (Exception ex)
            {
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                error.CodError = -1;
            }
        }
    }
}
