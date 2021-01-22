using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerLogic.ClassLibrary.objetos
{
    public class Factura
    {
        public DateTime FechaEmision { get; set; }
        public string Identificacion { get; set; }
        
        //Enum
        public string TipoIdentificacion { get; set; }
        public string Cliente { get; set; }

        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string base0 { get; set; }
        public string base12 { get; set; }



    }
}
