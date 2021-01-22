using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LayerLogic.ClassLibrary.objetos
{
    public class DatosElectronicos
    {
        public string ClaveAcceso { get; set; }
        public string NumeroAutorizacion { get; set; }
        public string ambiente { get; set; }
        public DateTime FechaAutorizacion { get; set; }
        //public XDocument Comprobante { get; set; }
        public string Base64 { get; set; }
        public string ComprobanteFirmado { get; set; }
        public DatosElectronicos()
        {

        }
    }
}
