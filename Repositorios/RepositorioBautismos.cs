using FoliosApp.Comun;
using FoliosApp.Modelos;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Linq;

namespace FoliosApp.Repositorios
{
    public class RepositorioBautismos
    {
        ConectorMySql conectorBD = new ConectorMySql();
        IDbConnection dbConnection = null;

        public List<Bautismo> GetAllBautismos(Error error)
        {
            List<Bautismo> retorno = new List<Bautismo>();
            string sSql = "";
            DynamicParameters parametrosDapperLocal = new DynamicParameters();

            sSql = @"   SELECT
                            COALESCE(id, 0) AS Id,
                            COALESCE(documento, '') AS Documento,
                            COALESCE(TRIM(apellido), '') AS Apellido,
                            COALESCE(TRIM(nombre), '') AS Nombre,
                            COALESCE(numero_libro, 0) AS Libro,
                            COALESCE(numero_folio, 0) AS Folio
                        FROM
                            certificados_bautismo
                        ORDER BY
                            libro ASC, apellido ASC;";

            try
            {
                dbConnection = conectorBD.GetConexion(error);
                retorno = dbConnection.Query<Bautismo>(sSql, parametrosDapperLocal, commandType: CommandType.Text).ToList();
                error.CodError = 1;
            }
            catch(Exception ex)
            {
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                error.CodError = -1;
            }

            return retorno;
        }

        public List<Bautismo> GetBautismosCriterio(CriteriosBusqueda criterio, string busqueda, Error error)
        {
            List<Bautismo> retorno = new List<Bautismo>();
            string sSql = "";
            DynamicParameters parametrosDapperLocal = new DynamicParameters();
            string criterioString = "";

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
                    criterioString = $@"    apellido LIKE '%{busqueda.Split(' ')[0]}%'
                                            AND nombre LIKE '%{busqueda.Split(' ')[1]}%'";
                    break;
            }

            sSql = $@"  SELECT
                            COALESCE(id, 0) AS Id,
                            COALESCE(documento, '') AS Documento,
                            COALESCE(TRIM(apellido), '') AS Apellido,
                            COALESCE(TRIM(nombre), '') AS Nombre,
                            COALESCE(numero_libro, 0) AS Libro,
                            COALESCE(numero_folio, 0) AS Folio
                        FROM
                            certificados_bautismo
                        WHERE
                            {criterioString}
                        ORDER BY
                            libro ASC, apellido ASC;";

            try
            {
                dbConnection = conectorBD.GetConexion(error);
                retorno = dbConnection.Query<Bautismo>(sSql, parametrosDapperLocal, commandType: CommandType.Text).ToList();
                error.CodError = 1;
            }
            catch (Exception ex)
            {
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                error.CodError = -1;
            }

            return retorno;
        }

        public void AgregarBautismo(Bautismo bautismo, Error error)
        {
            string sSql;
            DynamicParameters parametrosLocal = new DynamicParameters();

            sSql = @"   INSERT INTO bautismos
                            (
                                documento,
                                apellido,
                                nombre,
                                numero_libro,
                                numero_folio
                            )
                        VALUES
                            (
                                @Documento,
                                @Apellido,
                                @Nombre,
                                @Libro,
                                @Folio
                            );";

            parametrosLocal.Add("@Documento", bautismo.Documento);
            parametrosLocal.Add("@Apellido", bautismo.Apellido);
            parametrosLocal.Add("@Nombre", bautismo.Nombre);
            parametrosLocal.Add("@Libro", bautismo.Libro);
            parametrosLocal.Add("@Folio", bautismo.Folio);

            try
            {
                dbConnection = conectorBD.GetConexion(error);
                dbConnection.Execute(sSql, parametrosLocal, commandType: CommandType.Text);
                error.CodError = 1;
            }
            catch(Exception ex)
            {
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                error.CodError = -1;
            }
        }
    }
}
