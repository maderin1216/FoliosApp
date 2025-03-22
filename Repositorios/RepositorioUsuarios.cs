using FoliosApp.Comun;
using FoliosApp.Modelos;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Linq;

namespace FoliosApp.Repositorios
{
    public class RepositorioUsuarios
    {
        ConectorMySql conectorBD = new ConectorMySql();
        IDbConnection dbConnection = null;

        public Usuario GetUsuario(string nombre, Error error)
        {
            IEnumerable<Usuario> iEnumUsuarios;
            Usuario retorno = new Usuario();
            string sSql = "";
            DynamicParameters parametrosDapperLocal = new DynamicParameters();

            sSql = @"   SELECT
                            COALESCE(id, 0) AS Id,
                            COALESCE(nombre, '') AS Nombre,
                            COALESCE(clave, '') AS Clave,
                            COALESCE(id_nivel, 0) AS IdNivel
                        FROM
                            usuarios
                        WHERE
                            nombre = @Nombre;";

            try
            {
                dbConnection = conectorBD.GetConexion(error);

                parametrosDapperLocal.Add("@Nombre", nombre);
                iEnumUsuarios = dbConnection.Query<Usuario>(sSql, parametrosDapperLocal, commandType: CommandType.Text);

                if (iEnumUsuarios != null && iEnumUsuarios.Count() > 0)
                {
                    error.CodError = 1;
                    retorno = iEnumUsuarios.FirstOrDefault();

                    dbConnection.Close();
                }
                else
                {
                    error.CodError = -1;
                }
            }
            catch(Exception ex)
            {
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                error.CodError = -1;
            }

            return retorno;
        }

        public List<Usuario> GetAllUsuarios(Error error)
        {
            IEnumerable<Usuario> iEnumUsuarios;
            List<Usuario> retorno = new List<Usuario>();
            string sSql = "";
            DynamicParameters parametrosDapperLocal = new DynamicParameters();

            sSql = @"   SELECT
                            COALESCE(usuarios.id, 0) AS Id,
                            COALESCE(usuarios.nombre, '') AS Nombre,
                            COALESCE(usuarios.clave, '') AS Clave,
                            COALESCE(usuarios.id_nivel, 0) AS IdNivel,
                            COALESCE(usuarios_niveles.descripcion, '') AS NivelDescripcion
                        FROM
                            usuarios
                            LEFT JOIN usuarios_niveles ON usuarios_niveles.id = usuarios.id_nivel;";

            try
            {
                dbConnection = conectorBD.GetConexion(error);

                iEnumUsuarios = dbConnection.Query<Usuario>(sSql, parametrosDapperLocal, commandType: CommandType.Text);

                if (iEnumUsuarios != null && iEnumUsuarios.Count() > 0)
                {
                    error.CodError = 1;
                    retorno = iEnumUsuarios.ToList();

                    dbConnection.Close();
                }
                else
                {
                    error.CodError = -1;
                }
            }
            catch (Exception ex)
            {
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                error.CodError = -1;
            }

            return retorno;
        }

        public List<UsuarioNivel> GetAllNivelesUsuarios(Error error)
        {
            IEnumerable<UsuarioNivel> iEnumUsuariosNiveles;
            List<UsuarioNivel> retorno = new List<UsuarioNivel>();
            string sSql = "";
            DynamicParameters parametrosDapperLocal = new DynamicParameters();

            sSql = @"   SELECT
                            COALESCE(id, 0) AS IdNivel,
                            COALESCE(descripcion, '') AS Descripcion,
                            COALESCE(nivel, 0) AS Nivel
                        FROM
                            usuarios_niveles;";

            try
            {
                dbConnection = conectorBD.GetConexion(error);

                iEnumUsuariosNiveles = dbConnection.Query<UsuarioNivel>(sSql, parametrosDapperLocal, commandType: CommandType.Text);

                if (iEnumUsuariosNiveles != null && iEnumUsuariosNiveles.Count() > 0)
                {
                    error.CodError = 1;
                    retorno = iEnumUsuariosNiveles.ToList();

                    dbConnection.Close();
                }
                else
                {
                    error.CodError = -1;
                }
            }
            catch (Exception ex)
            {
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                error.CodError = -1;
            }

            return retorno;
        }

        public void GrabarIngreso(string usuario, int tipo, Error error)
        {
            string sSql1;
            DynamicParameters parametrosLocal = new DynamicParameters();

            sSql1 = @"   INSERT INTO ingresos_sistema
                            (
                                tipo_ingreso,
                                usuario,
                                fecha
                            )
                        VALUES
                            (
                                @TipoIngreso,
                                @Usuario,
                                NOW()
                            );";

            parametrosLocal.Add("@TipoIngreso", tipo);
            parametrosLocal.Add("@Usuario", usuario);            

            try
            {
                dbConnection = conectorBD.GetConexion(error);
                dbConnection.Execute(sSql1, parametrosLocal, commandType: CommandType.Text);

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
