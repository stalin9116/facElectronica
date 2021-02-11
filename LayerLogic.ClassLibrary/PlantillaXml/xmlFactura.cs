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
        public static XDocument generateFacturaXml( List<DETALLE_COMPROBANTE> _listaDetalleProductos, DatosElectronicos _infoDatosElectronicos)
        {
            XElement factura = new XElement("factura", new XAttribute("id", "comprobante"), new XAttribute("version", "1.1.0"));

            //InfoTributaria

            factura.Add(
                new XElement("infoTributaria",
                    new XElement("ambiente", _infoDatosElectronicos.ambiente),
                    new XElement("tipoEmision", "1"),
                    new XElement("razonSocial", "MERO MOSQUERA FABRICIO FORTUNATO"),
                    new XElement("nombreComercial", "EL ASESOR CONTABLE"),
                    new XElement("ruc", "0801895186001"),
                    new XElement("claveAcceso", _infoDatosElectronicos.ClaveAcceso),
                    new XElement("codDoc", "01"),
                    new XElement("estab", _infoDatosElectronicos.comprobate.com_establecimiento),
                    new XElement("ptoEmi", _infoDatosElectronicos.comprobate.com_emision),
                    new XElement("secuencial", _infoDatosElectronicos.comprobate.com_secuencial),
                    new XElement("dirMatriz", "JUAN ACEVEDO N29-09 Y LAS CASAS")

                ));

            //infoFactura

            XElement infoFactura = new XElement("infoFactura");
            infoFactura.Add(
                new XElement("fechaEmision", _infoDatosElectronicos.comprobate.com_fecha.ToString("dd/MM/yyyy")),
                new XElement("dirEstablecimiento", _infoDatosElectronicos.comprobate.com_direccion),
                //new XElement("contribuyenteEspecial", "NO"),
                new XElement("obligadoContabilidad", "NO"),
                new XElement("tipoIdentificacionComprador", _infoDatosElectronicos.comprobate.com_tipoIdentificacion=="CF" ? "07" : 
                                                            _infoDatosElectronicos.comprobate.com_tipoIdentificacion == "R" ? "04" :
                                                            _infoDatosElectronicos.comprobate.com_tipoIdentificacion == "P" ? "06" :
                                                            _infoDatosElectronicos.comprobate.com_tipoIdentificacion == "C" ? "05" : null),
                //new XElement("direccionComprador", _infoDatosElectronicos.comprobate.com_direccion),
                new XElement("razonSocialComprador", _infoDatosElectronicos.comprobate.com_nombres),

                new XElement("identificacionComprador", _infoDatosElectronicos.comprobate.com_identificacion),

                new XElement("totalSinImpuestos", _infoDatosElectronicos.comprobate.com_subtotalgravado.ToString("0.00")),
                new XElement("totalDescuento", _infoDatosElectronicos.comprobate.com_totalDescuento.ToString("0.00")),
                new XElement("totalConImpuestos", 
                    new XElement("totalImpuesto",
                    new XElement("codigo","2"),
                    new XElement("codigoPorcentaje", "2"),
                    new XElement("baseImponible", _infoDatosElectronicos.comprobate.com_subtotalgravado),
                    new XElement("valor", _infoDatosElectronicos.comprobate.com_ivagravado))),
                new XElement("propina", "0.00"),
                new XElement("importeTotal", _infoDatosElectronicos.comprobate.com_total),
                new XElement("moneda", "DOLAR"),
                new XElement("pagos",
                    new XElement("pago",
                        new XElement("formaPago","20"),
                        new XElement("total", _infoDatosElectronicos.comprobate.com_total)
                        //new XElement("plazo", "3"),
                        //new XElement("unidadTiempo", "30")
                        ))    
                );


            //InfoDetalle
            XElement detalleFactura = new XElement("detalles",
                    from a in _listaDetalleProductos
                    select new XElement("detalle",
                        new XElement("codigoPrincipal", a.dec_codigoproducto),
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
                new XDeclaration("1.0", "utf-8", "no")
                
                );

            factura.Add(infoFactura);
            factura.Add(detalleFactura);
            
            //infoAdicional a factura


            doc.Add(factura);

            return doc;

        }


    }
}
