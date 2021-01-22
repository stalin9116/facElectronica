using PuntoVenta.WindowsForm.wsSriAtorizacionProduccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace PuntoVenta.WindowsForm.SRI
{
    public class logicSRI
    {
        public static bool sendClaveAcceso(string claveAcceso)
        {
            bool result = false;
            try
            {
                AutorizacionComprobantesOfflineService ServiceSri = new AutorizacionComprobantesOfflineService();
                XmlNode[] xmlRespuesta = (XmlNode[])ServiceSri.autorizacionComprobante(claveAcceso);
                if (!string.IsNullOrEmpty(xmlRespuesta[2].InnerXml))
                {
                    XDocument res = new XDocument();
                    res = XDocument.Parse(xmlRespuesta[2].InnerXml);
                    //res.Save("");
                    result = true;
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
