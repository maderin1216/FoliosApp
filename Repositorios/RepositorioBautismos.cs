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
                            COALESCE(numero_folio, 0) AS Folio,
                            COALESCE(fecha_nacimiento, '2000-01-01') AS FechaNacimiento,
                            COALESCE(fecha_bautismo, '2000-01-01') AS FechaBautismo
                        FROM
                            certificados_bautismo
                        ORDER BY
                            libro ASC, apellido ASC;";

            try
            {
                retorno = ConectorMySql.GetConexionSistema(error).Query<Bautismo>(sSql, parametrosDapperLocal, commandType: CommandType.Text).ToList();
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
                            COALESCE(fecha_nacimiento, '2000-01-01') AS FechaNacimiento,
                            COALESCE(fecha_bautismo, '2000-01-01') AS FechaBautismo
                        FROM
                            certificados_bautismo
                        WHERE
                            {criterioString}
                        ORDER BY
                            libro ASC, apellido ASC;";

                retorno = ConectorMySql.GetConexionSistema(error).Query<Bautismo>(sSql, parametrosDapperLocal, commandType: CommandType.Text).ToList();
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
            string sSql1;
            string sSql2;
            DynamicParameters parametrosLocal = new DynamicParameters();
            int iIdInsertado;

            sSql1 = @"   INSERT INTO certificados_bautismo
                            (
                                documento,
                                apellido,
                                nombre,
                                numero_libro,
                                numero_folio,
                                fecha_nacimiento,
                                fecha_bautismo,
                                fecha_carga,
                                fecha_edicion
                            )
                        VALUES
                            (
                                @Documento,
                                @Apellido,
                                @Nombre,
                                @Libro,
                                @Folio,
                                @FechaNacimiento,
                                @FechaBautismo,
                                NOW(),
                                NOW()
                            );";

            sSql2 = @"   SELECT LAST_INSERT_ID();";

            parametrosLocal.Add("@Documento", bautismo.Documento);
            parametrosLocal.Add("@Apellido", bautismo.Apellido);
            parametrosLocal.Add("@Nombre", bautismo.Nombre);
            parametrosLocal.Add("@Libro", bautismo.Libro);
            parametrosLocal.Add("@Folio", bautismo.Folio);
            parametrosLocal.Add("@FechaNacimiento", bautismo.FechaNacimiento);
            parametrosLocal.Add("@FechaBautismo", bautismo.FechaBautismo);

            try
            {
                ConectorMySql.GetConexionSistema(error).Execute(sSql1, parametrosLocal, commandType: CommandType.Text);

                iIdInsertado = ConectorMySql.GetConexionSistema(error).Query<int>(sSql2, parametrosLocal, commandType: CommandType.Text).FirstOrDefault();
                bautismo.Id = iIdInsertado;

                error.CodError = 1;
            }
            catch(Exception ex)
            {
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                error.CodError = -1;
            }
        }

        public void EditarBautismo(Bautismo bautismo, Error error)
        {
            string sSql;
            DynamicParameters parametrosLocal = new DynamicParameters();

            sSql = @"   UPDATE 
                            certificados_bautismo 
                        SET
                            documento = @Documento,
                            apellido = @Apellido,
                            nombre = @Nombre,
                            numero_libro = @Libro,
                            numero_folio = @Folio,
                            fecha_nacimiento = @FechaNacimiento,
                            fecha_bautismo = @FechaBautismo,
                            fecha_edicion = NOW()
                        WHERE
                            id = @Id;";

            parametrosLocal.Add("@Id", bautismo.Id);
            parametrosLocal.Add("@Documento", bautismo.Documento);
            parametrosLocal.Add("@Apellido", bautismo.Apellido);
            parametrosLocal.Add("@Nombre", bautismo.Nombre);
            parametrosLocal.Add("@Libro", bautismo.Libro);
            parametrosLocal.Add("@Folio", bautismo.Folio);
            parametrosLocal.Add("@FechaNacimiento", bautismo.FechaNacimiento);
            parametrosLocal.Add("@FechaBautismo", bautismo.FechaBautismo);

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

        public void BorrarBautismo(Bautismo bautismo, Error error)
        {
            string sSql;
            DynamicParameters parametrosLocal = new DynamicParameters();

            sSql = @"   DELETE FROM
                            certificados_bautismo
                        WHERE
                            id = @Id;";

            parametrosLocal.Add("@Id", bautismo.Id);

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
