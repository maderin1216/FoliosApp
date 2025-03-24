using FoliosApp.Comun;
using FoliosApp.Modelos;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Linq;
using FoliosApp.Ventanas;

namespace FoliosApp.Repositorios
{
    public class RepositorioUsuarios
    {        
        public Usuario GetUsuario(string nombre, Error error)
        {
            IDbConnection dbConnectionLocal = null;
            IEnumerable<Usuario> iEnumUsuarios;
            Usuario retorno = null;
            string sSql = "";
            DynamicParameters parametrosDapperLocal = new DynamicParameters();

            sSql = @"   SELECT
                            COALESCE(usuarios.id, 0) AS Id,
                            COALESCE(usuarios.nombre, '') AS Nombre,
                            COALESCE(usuarios.clave, '') AS Clave,
                            COALESCE(usuarios.id_nivel, 0) AS IdNivel,
                            COALESCE(usuarios_niveles.nivel, 0) AS Nivel
                        FROM
                            usuarios
                            LEFT JOIN usuarios_niveles ON usuarios_niveles.id = usuarios.id_nivel
                        WHERE
                            usuarios.nombre = @Nombre;";

            try
            {
                dbConnectionLocal = ConectorMySql.GetConexionEfimera(error);

                parametrosDapperLocal.Add("@Nombre", nombre);
                iEnumUsuarios = dbConnectionLocal.Query<Usuario>(sSql, parametrosDapperLocal, commandType: CommandType.Text);

                if (iEnumUsuarios != null && iEnumUsuarios.Count() > 0)
                {
                    error.CodError = 1;
                    retorno = iEnumUsuarios.FirstOrDefault();

                    dbConnectionLocal.Close();
                }
                else
                {
                    error.CodError = -1;
                    error.Mensaje = Rutinas.GetMensajeError("No se encontró usuario con la información dada.");
                }
            }
            catch(Exception ex)
            {
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                error.CodError = -1;
            }

            return retorno;
        }

        public Usuario GetUsuario(int idUsuario, Error error)
        {
            IDbConnection dbConnectionLocal = null;
            IEnumerable<Usuario> iEnumUsuarios;
            Usuario retorno = null;
            string sSql = "";
            DynamicParameters parametrosDapperLocal = new DynamicParameters();

            sSql = @"   SELECT
                            COALESCE(usuarios.id, 0) AS Id,
                            COALESCE(usuarios.nombre, '') AS Nombre,
                            COALESCE(usuarios.clave, '') AS Clave,
                            COALESCE(usuarios.id_nivel, 0) AS IdNivel,
                            COALESCE(usuarios_niveles.nivel, 0) AS Nivel
                        FROM
                            usuarios
                            LEFT JOIN usuarios_niveles ON usuarios_niveles.id = usuarios.id_nivel
                        WHERE
                            usuarios.id = @IdUsuario;";

            try
            {
                dbConnectionLocal = ConectorMySql.GetConexionSistema(error);

                parametrosDapperLocal.Add("@IdUsuario", idUsuario);
                iEnumUsuarios = dbConnectionLocal.Query<Usuario>(sSql, parametrosDapperLocal, commandType: CommandType.Text);

                if (iEnumUsuarios != null && iEnumUsuarios.Count() > 0)
                {
                    error.CodError = 1;
                    retorno = iEnumUsuarios.FirstOrDefault();

                    dbConnectionLocal.Close();
                }
                else
                {
                    error.CodError = -1;
                    error.Mensaje = Rutinas.GetMensajeError("No se encontró usuario con la información dada.");
                }
            }
            catch (Exception ex)
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
                iEnumUsuarios = ConectorMySql.GetConexionSistema(error).Query<Usuario>(sSql, parametrosDapperLocal, commandType: CommandType.Text);

                if (iEnumUsuarios != null && iEnumUsuarios.Count() > 0)
                {
                    error.CodError = 1;
                    retorno = iEnumUsuarios.ToList();
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
                iEnumUsuariosNiveles = ConectorMySql.GetConexionSistema(error).Query<UsuarioNivel>(sSql, parametrosDapperLocal, commandType: CommandType.Text);

                if (iEnumUsuariosNiveles != null && iEnumUsuariosNiveles.Count() > 0)
                {
                    error.CodError = 1;
                    retorno = iEnumUsuariosNiveles.ToList();
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
                ConectorMySql.GetConexionEfimera(error).Execute(sSql1, parametrosLocal, commandType: CommandType.Text);

                error.CodError = 1;
            }
            catch (Exception ex)
            {
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                error.CodError = -1;
            }
        }

        public void UpdateClave(int idUsuario, string nuevaClave, Error error)
        {
            string sSql1;
            DynamicParameters parametrosLocal = new DynamicParameters();

            sSql1 = @"  UPDATE 
                            usuarios
                        SET
                            clave = @NuevaClave
                        WHERE
                            usuarios.id = @IdUsuario;";

            parametrosLocal.Add("@IdUsuario", idUsuario);
            parametrosLocal.Add("@NuevaClave", nuevaClave);

            try
            {
                ConectorMySql.GetConexionSistema(error).Execute(sSql1, parametrosLocal, commandType: CommandType.Text);

                error.CodError = 1;
            }
            catch (Exception ex)
            {
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                error.CodError = -1;
            }
        }


        public void AgregarUsuario(Usuario usuario, Error error)
        {
            string sSql1;
            string sSql2;
            DynamicParameters parametrosLocal = new DynamicParameters();
            int iIdInsertado;

            sSql1 = @"   INSERT INTO usuarios
                            (
                                nombre,
                                clave,
                                id_nivel
                            )
                        VALUES
                            (
                                @Nombre,
                                @Clave,
                                @IdNivel
                            );";

            sSql2 = @"   SELECT LAST_INSERT_ID();";

            parametrosLocal.Add("@Nombre", usuario.Nombre);
            parametrosLocal.Add("@Clave", usuario.Clave);
            parametrosLocal.Add("@IdNivel", usuario.IdNivel);

            try
            {
                ConectorMySql.GetConexionSistema(error).Execute(sSql1, parametrosLocal, commandType: CommandType.Text);

                iIdInsertado = ConectorMySql.GetConexionSistema(error).Query<int>(sSql2, parametrosLocal, commandType: CommandType.Text).FirstOrDefault();
                usuario.Id = iIdInsertado;

                error.CodError = 1;
            }
            catch (Exception ex)
            {
                error.Mensaje = Rutinas.GetMensajeError(ex.Message);
                error.CodError = -1;
            }
        }

        public void ActualizarUsuario(Usuario usuario, Error error)
        {
            string sSql;
            DynamicParameters parametrosLocal = new DynamicParameters();

            sSql = @"   UPDATE 
                            usuarios 
                        SET
                            id_nivel = @IdNivel
                        WHERE
                            id = @Id;";

            parametrosLocal.Add("@Id", usuario.Id);
            parametrosLocal.Add("@IdNivel", usuario.IdNivel);

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

        public void BorrarUsuario(Usuario usuario, Error error)
        {
            string sSql;
            DynamicParameters parametrosLocal = new DynamicParameters();

            sSql = @"   DELETE FROM
                            usuarios
                        WHERE
                            id = @Id;";

            parametrosLocal.Add("@Id", usuario.Id);

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
