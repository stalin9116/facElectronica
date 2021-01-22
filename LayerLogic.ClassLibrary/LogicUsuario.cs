using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LayerData;
using LayerData.ClassLibrary;

namespace LayerLogic.ClassLibrary
{
    

    public class LogicUsuario
    {
        private static dcFactguracionDataContext dc = new dcFactguracionDataContext();

        public static List<TBL_USUARIO> getAllUsers()
        {

            try
            {
                var lista = dc.TBL_USUARIO.Where(user => user.usu_status == 'A');
                return lista.ToList();
            }
            catch (Exception ex)
            {
                //Archivos de log
                throw new ArgumentException("Error al obtener Usuarios desde la base "+ex.Message);
            }
        
        }

        public static List<TBL_USUARIO> getAllUsersXRol(string rolDescripcion)
        {

            try
            {
                var lista = dc.TBL_USUARIO.Where(user => user.usu_status == 'A'
                                                && user.TBL_ROL.rol_descripcion.StartsWith(rolDescripcion));
                return lista.ToList();

                //SELECT * FROM TLB_USUARIO U INNER JOIN TBL_ROL R ON U.rol_id=R.ro_id WHERE rol_descripcion="Administrador"


            }
            catch (Exception ex)
            {
                //Archivos de log
                throw new ArgumentException("Error al obtener Usuarios desde la base " + ex.Message);
            }

        }

        public static TBL_USUARIO getUserXID(int idUsuario)
        {

            try
            {
                var user = dc.TBL_USUARIO.FirstOrDefault(data => data.usu_status == 'A'
                                                         && data.usu_id.Equals(idUsuario));
                return user;

            }
            catch (Exception ex)
            {
                //Archivos de log
                throw new ArgumentException("Error al obtener Usuarios desde la base " + ex.Message);
            }

        }

        public static TBL_USUARIO getUserXLogin(string username, string password)
        {

            try
            {
                var user = dc.TBL_USUARIO.FirstOrDefault(data => data.usu_status == 'A'
                                                         && data.usu_correo.Equals(username)
                                                         && data.usu_password.Equals(password));
                return user;

            }
            catch (Exception ex)
            {
                //Archivos de log
                throw new ArgumentException("Error al obtener Usuarios desde la base " + ex.Message);
            }

        }

        public static List<TBL_USUARIO> getUsersXApellidos(string apellidos)
        {

            try
            {
                var users = dc.TBL_USUARIO.Where(data => data.usu_status == 'A'
                                                         && data.usu_apellidos.StartsWith(apellidos));
                return users.ToList();

            }
            catch (Exception ex)
            {
                //Archivos de log
                throw new ArgumentException("Error al obtener Usuarios desde la base " + ex.Message);
            }

        }

        public static List<TBL_USUARIO> getUsersXCorreo(string correo)
        {

            try
            {
                var users = dc.TBL_USUARIO.Where(data => data.usu_status == 'A'
                                                         && data.usu_correo.StartsWith(correo));
                return users.ToList();

            }
            catch (Exception ex)
            {
                //Archivos de log
                throw new ArgumentException("Error al obtener Usuarios desde la base " + ex.Message);
            }

        }

        public static List<TBL_USUARIO> getUsersXNombres(string nombres)
        {

            try
            {
                var users = dc.TBL_USUARIO.Where(data => data.usu_status == 'A'
                                                         && data.usu_nombres.StartsWith(nombres));
                return users.ToList();

            }
            catch (Exception ex)
            {
                //Archivos de log
                throw new ArgumentException("Error al obtener Usuarios desde la base " + ex.Message);
            }

        }

        public static bool saveUser(TBL_USUARIO _infoUsuario)
        {

            try
            {
                bool res = false;
                dc = new dcFactguracionDataContext();
                _infoUsuario.usu_add = DateTime.Now;
                _infoUsuario.usu_status = 'A';
                //enviar mi objeto al orm
                dc.TBL_USUARIO.InsertOnSubmit(_infoUsuario);
                //commit de la base
                dc.SubmitChanges();
                res = true;
                return res;

            }
            catch (Exception ex)
            {                
                //Archivos de log
                throw new ArgumentException("Error al guardar un usuario " + ex.Message);
            }

        }

        public static bool updateUser(TBL_USUARIO _infoUsuario)
        {

            try
            {
                bool res = false;
                _infoUsuario.usu_add = DateTime.Now;
                //enviar mi objeto al orm
                //commit de la base
                dc.SubmitChanges();
                res = true;
                return res;

            }
            catch (Exception ex)
            {
                //Archivos de log
                

                throw new ArgumentException("Error al guardar un usuario " + ex.Message);
            }
        }

        public static bool updateUser2(TBL_USUARIO _infoUsuario)
        {

            try
            {
                bool res = false;
                _infoUsuario.usu_add = DateTime.Now;

                dc.ExecuteCommand("UPDATE [dbo].[TBL_USUARIO] SET[usu_correo]={0}, [usu_password]={1}, [usu_apellidos]={2}, [usu_nombres]={3}, [usu_add]={4}, [rol_id]={5} WHERE [usu_id]={6}", new object[] {
                    _infoUsuario.usu_correo,
                    _infoUsuario.usu_password,
                    _infoUsuario.usu_apellidos,
                    _infoUsuario.usu_nombres,                    
                    _infoUsuario.usu_add,
                    _infoUsuario.rol_id,
                    _infoUsuario.usu_id
                });

                //enviar mi consulta al orm
                dc.Refresh(RefreshMode.OverwriteCurrentValues, dc.TBL_USUARIO);
                //commit de la base
                dc.SubmitChanges();
                res = true;
                return res;

            }
            catch (Exception ex)
            {
                //Archivos de log


                throw new ArgumentException("Error al guardar un usuario " + ex.Message);
            }
        }

        public static bool updateUser3(TBL_USUARIO _infoUsuario)
        {
            try
            {
                dc = new dcFactguracionDataContext();
                bool res = false;

                int resultado;

                var ri = dc.spUpdateUsuario(_infoUsuario.usu_id, _infoUsuario.usu_correo, _infoUsuario.usu_password, _infoUsuario.usu_apellidos, _infoUsuario.usu_nombres, Convert.ToByte(_infoUsuario.rol_id));
                var resp = ri.FirstOrDefault<spUpdateUsuarioResult>();
                resultado = resp.Column1;

                
                //commit de la base
                dc.SubmitChanges();
                res = true;
                return res;

            }
            catch (Exception ex)
            {
                //Archivos de log


                throw new ArgumentException("Error al guardar un usuario " + ex.Message);
            }
        }


        public static bool DeleteUser(TBL_USUARIO _infoUsuario)
        {

            try
            {
                bool res = false;
                _infoUsuario.usu_add = DateTime.Now;
                _infoUsuario.usu_status = 'I';
                //enviar mi objeto al orm
                //commit de la base
                dc.SubmitChanges();
                res = true;
                return res;

            }
            catch (Exception ex)
            {
                //Archivos de log
                throw new ArgumentException("Error al guardar un usuario " + ex.Message);
            }
        }


    }
}
