using LayerData.ClassLibrary;
using LayerLogic.ClassLibrary.objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LayerLogic.ClassLibrary.PlantillaXml
{
    public class xmlFactura
    {
        public static XDocument generateFacturaXml(COMPROBANTE _infoComprobante, List<DETALLE_COMPROBANTE> _listaDetalleProductos, DatosElectronicos _infoDatosElectronicos)
        {
            XElement factura = new XElement("factura", new XAttribute("id", "comprobante"), new XAttribute("version", "1.1.0"));

            //InfoTributaria

            factura.Add(
                new XElement("infoTributaria",
                    new XElement("ambiente", _infoDatosElectronicos.ambiente),
                    new XElement("tipoEmision", "1"),
                    new XElement("razonSocial", "FABRICIO MERO"),
                    new XElement("nombreComercial", "EL ASESOR CONTABLE"),
                    new XElement("ruc", "0801895186001"),
                    new XElement("claveAcceso", _infoDatosElectronicos.ClaveAcceso),
                    new XElement("codDoc", "01"),
                    new XElement("estab", _infoComprobante.com_establecimiento),
                    new XElement("ptoEmi", _infoComprobante.com_emision),
                    new XElement("secuencial", _infoComprobante.com_secuencial),
                    new XElement("dirMatriz", "LAS CASAS")

                ));

            //infoFactura

            XElement infoFactura = new XElement("infoFactura");
            infoFactura.Add(
                new XElement("fechaEmision", _infoComprobante.com_fecha),
                new XElement("dirEstablecimiento", _infoComprobante.com_direccion),
                new XElement("contribuyenteEspecial", "NO"),
                new XElement("obligadoContabilidad", "NO"),
                new XElement("tipoIdentificacionComprador", _infoComprobante.com_tipoIdentificacion=="CF" ? "7" : 
                                                            _infoComprobante.com_tipoIdentificacion == "R" ? "4" :
                                                            _infoComprobante.com_tipoIdentificacion == "P" ? "6" :
                                                            _infoComprobante.com_tipoIdentificacion == "c" ? "5" : null),
                new XElement("direccionComprador", _infoComprobante.com_direccion),
                new XElement("razonSocialComprador", _infoComprobante.com_nombres),

                new XElement("identificacionComprador", _infoComprobante.com_identificacion),
                new XElement("totalSinImpuestos", _infoComprobante.com_total.ToString("0.00")),
                new XElement("totalDescuento", _infoComprobante.com_totalDescuento.ToString("0.00")),

                new XElement("totalConImpuestos", 
                    new XElement("totalImpuesto",
                    new XElement("codigo","2"),
                    new XElement("codigoPorcentaje", "2"),
                    new XElement("baseImponible", "50.00"),
                    new XElement("valor", "6.00"))),
                new XElement("propina", "0.00"),
                new XElement("importeTotal", "56.00"),
                new XElement("moneda", "DOLAR"),
                new XElement("pagos",
                    new XElement("pago",
                        new XElement("formaPago","18"),
                        new XElement("total", "56.00"),
                        new XElement("plazo", "3"),
                        new XElement("unidadTiempo", "30")
                        ))    
                );


            //InfoDetalle
            XElement detalleFactura = new XElement("detalles",
                    from a in _listaDetalleProductos
                    select new XElement("detalle",
                        new XElement("CodigoPrincipal", a.dec_codigoproducto),
                        new XElement("codigoAuxiliar", a.dec_codigoproducto),
                        new XElement("descripcion", a.dec_descripcion),
                        new XElement("cantidad", a.dec_cantidad),
                        new XElement("precioUnitario", a.dec_precio.ToString("0.00")),
                        new XElement("descuento", a.dec_descuento.ToString("0.00")),
                        new XElement("precioTotalSinImpuesto", a.dec_cantidad * a.dec_precio),
                        new XElement("impuestos",
                             new XElement("impuesto",
                             new XElement("codigo", "2"),
                             new XElement("codigoPorcentaje", "2"),
                             new XElement("tarifa", "12.00"),
                             new XElement("baseImponible", a.dec_cantidad * a.dec_precio),
                             new XElement("valor", a.dec_ivagravado)))));


            //InfoAdicional





            XDocument doc = new XDocument(
                new XDeclaration("1.0", "urf-8", "no")
                
                );

            factura.Add(infoFactura);
            factura.Add(detalleFactura);
            
            //infoAdicional a factura


            doc.Add(factura);

            return doc;

        }


    }
}
