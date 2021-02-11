using LayerLogic.ClassLibrary.objetos;
using PuntoVenta.WindowsForm.wsSriAtorizacionProduccion;
using PuntoVenta.WindowsForm.wsSriRecepcionProduccion;
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
                    res.Save(@"C:\XML\final");
                    result = true;
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static DatosElectronicos sendXmlFirmadoWSSriProducción(DatosElectronicos _infoDatosElectronicos)
        {

            byte[] bytesXml = Convert.FromBase64String(_infoDatosElectronicos.Base64);

            RecepcionComprobantesOfflineService wsrecep = new RecepcionComprobantesOfflineService();
            XmlNode[] xmlRespuesta = (XmlNode[])wsrecep.validarComprobante(bytesXml);
            string statusSri = xmlRespuesta[0].FirstChild.Value;
            if (!string.IsNullOrEmpty(statusSri))
            {
                if (statusSri.Equals("RECIBIDA"))
                {
                    _infoDatosElectronicos.statusSRI = statusSri;
                }
                else if (statusSri.Equals("DEVUELTA"))
                {
                    XDocument res = new XDocument();
                    res = XDocument.Parse(xmlRespuesta[1].InnerXml);
                    var query = res.Descendants("mensajes").Elements("mensaje").Select(data => new
                    {
                        Identificador = data.Element("identificador").Value,
                        Mensaje = data.Element("mensaje").Value

                    });

                    foreach (var item in query)
                    {
                        _infoDatosElectronicos.identificadorSri = item.Identificador;
                        _infoDatosElectronicos.mensajeSRI = item.Mensaje;
                       
                    }

                }
            }


            return _infoDatosElectronicos;

        }

    }
}
