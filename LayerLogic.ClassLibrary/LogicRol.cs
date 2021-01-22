using LayerData.ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerLogic.ClassLibrary
{
    public class LogicRol
    {


        private static dcFactguracionDataContext dc = new dcFactguracionDataContext();

        public static List<TBL_ROL> getAllRol()
        {
            try
            {
                var lista = dc.TBL_ROL.Where(rol => rol.rol_status == 'A');
                return lista.ToList();
            }
            catch (Exception ex)
            {
                //Archivos de log
                throw new ArgumentException("Error al obtener Roles desde la base de datos " + ex.Message);
            }

        }
    }
}
