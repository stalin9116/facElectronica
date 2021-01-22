using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LayerData;
using LayerData.ClassLibrary;
using LayerLogic;
using LayerLogic.ClassLibrary;

namespace PuntoVenta.WindowsForm
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ingreso();
        }

        private void ingreso()
        {
            string user = txtUser.Text.TrimStart().TrimEnd();
            string password = txtPassword.Text.TrimStart().TrimEnd();

            if (!string.IsNullOrEmpty(user))
            {
                if (!string.IsNullOrEmpty(password))
                {
                    TBL_USUARIO _infoUsuario = new TBL_USUARIO();
                    _infoUsuario = LogicUsuario.getUserXLogin(user, password);
                    if (_infoUsuario != null)
                    {
                        FrmPrincipal frmPrincipal = new FrmPrincipal();
                        frmPrincipal.Show();
                        this.Hide();
                    }
                    else
                    {
                        txtPassword.Clear();
                        txtPassword.Focus();
                        MessageBox.Show("Usuario o Contraseña incorrecta", "Sistema Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Clave campo obligatorio");
                }
            }
            else
            {
                MessageBox.Show("Usuario campo obligatorio");
            }


        }

        private void btnExportXML_Click(object sender, EventArgs e)
        {
            
        }
    }
}
