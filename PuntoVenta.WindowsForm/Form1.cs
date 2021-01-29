using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LayerData;
using LayerData.ClassLibrary;
using LayerLogic;
using LayerLogic.ClassLibrary;
using LayerLogic.ClassLibrary.objetos;
using PuntoVenta.WindowsForm.SRI;

namespace PuntoVenta.WindowsForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            obtenerCertificado();
        }

        private void obtenerCertificado()
        {
            

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //TBL_USUARIO usuario = new TBL_USUARIO();
            //usuario = LogicUsuario.getUserXID(1);

            //usuario.usu_nombres = "Juanito";

            //try
            //{
            //    if (LogicUsuario.updateUser(usuario))
            //    {
            //        MessageBox.Show("usuario modificado correctamente");
            //    }
            //    else
            //    {
            //        MessageBox.Show("usuario no modificado");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("usuario no modificado " + ex.Message);

            //}

            uploadFile();


        }

        private void button1_Click(object sender, EventArgs e)
        {

            logicSRI.sendClaveAcceso("1301202101179282192400120010050000298690002986913");

            List<DETALLE_COMPROBANTE> _listaDetalle = new List<DETALLE_COMPROBANTE>();

            DETALLE_COMPROBANTE _infoDetalle = new DETALLE_COMPROBANTE();
            _infoDetalle.dec_id = 1;
            _infoDetalle.dec_cantidad = 1;
            _infoDetalle.dec_codigoproducto = "PRO001";
            _infoDetalle.dec_descripcion = "COMPUTADOR";
            _infoDetalle.dec_descuento = 0;
            _infoDetalle.dec_iva0 = 0;
            _infoDetalle.dec_precio = 1000;
            _infoDetalle.dec_ivagravado = 120;
            _infoDetalle.dec_valortotal = 1120;

            DETALLE_COMPROBANTE _infoDetalle2 = new DETALLE_COMPROBANTE();
            _infoDetalle2.dec_id = 2;
            _infoDetalle2.dec_cantidad = 1;
            _infoDetalle2.dec_codigoproducto = "PRO002";
            _infoDetalle2.dec_descripcion = "TECLADO";
            _infoDetalle2.dec_descuento = 0;
            _infoDetalle2.dec_iva0 = 0;
            _infoDetalle2.dec_precio = 100;
            _infoDetalle2.dec_ivagravado = 12;
            _infoDetalle2.dec_valortotal = 112;

            _listaDetalle.Add(_infoDetalle);
            _listaDetalle.Add(_infoDetalle2);

            COMPROBANTE _infoComprobante = new COMPROBANTE();

            DatosElectronicos _infoDatosElectronicos = new DatosElectronicos();


            var res = LayerLogic.ClassLibrary.PlantillaXml.xmlFactura.generateFacturaXml(_infoComprobante, _listaDetalle, _infoDatosElectronicos);

            //Convert Base 64
            string xmlSinFirma = res.Declaration.ToString() + res.ToString();
            byte[] newByte = new byte[xmlSinFirma.Length];
            newByte = Encoding.UTF8.GetBytes(xmlSinFirma);
            string base64 = Convert.ToBase64String(newByte);

            LayerLogic.ClassLibrary.Complementos.Firmar.firmar(base64);



            res.Save(@"C:\XML\result.xml");


            string fecha = DateTime.Now.ToString("ddMMyyyy");
            string tipoCOmprobante = "02";
            string ruc = "1725667054001";
            string codigoNumerico = "12345";
            string secuencia = "00000001";
            string establecimiento = "001";
            string emsion = "002";



        }

        private void uploadFile()
        {
            var res = File.ReadAllLines(@"C:\XML\test.csv")
                .Select(x => x.Split(';'))
                      .Select(x =>
                        new TBL_USUARIO
                        {
                            usu_correo = x[0],
                            usu_apellidos = x[1],
                            usu_nombres = x[2],
                            rol_id = x[3].ToString() == "Administrador" ? Convert.ToByte(1) :
                                     x[3].ToString() == "Cliente" ? Convert.ToByte(2) :
                                     Convert.ToByte(0),
                            usu_password = LayerLogic.ClassLibrary.Complementos.Encriptar.GetMD5(x[4].ToString())
                        });

            int i = 1;
            foreach (var item in res)
            {
                var validaUsuario = LogicUsuario.getUsersXCorreo(item.usu_correo);
                

                if (validaUsuario!=null)
                {
                    MessageBox.Show("Error usuario ya existe verifique linea "+i);
                    return;
                }
                else
                {
                    LogicUsuario.saveUser(item);
                }

                i++;

                //Guardar Logic
            }


        }

    }
}
