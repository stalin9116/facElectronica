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
    public partial class FrmUsuarios : Form
    {
        private TBL_USUARIO _infoUsuario { get; set; }

        delegate List<TBL_USUARIO> delagateListUser(List<TBL_USUARIO> _listausuario);
        delegate TBL_ROL delageteRoles();
        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            loadUser();
            loadRol();
        }

        private void loadUser()
        {
            List<TBL_USUARIO> _listaUsuario = new List<TBL_USUARIO>();
            _listaUsuario = LogicUsuario.getAllUsers();
            if (_listaUsuario.Count > 0 && _listaUsuario != null)
            {
                gdvUsuarios.DataSource = _listaUsuario.Select(data => new
                {
                    CODIGO = data.usu_id,
                    APELLIDOS = data.usu_apellidos,
                    NOMBRES = data.usu_nombres,
                    CORREO = data.usu_correo,
                    ROL = data.TBL_ROL.rol_descripcion
                }).ToList();
            }
        }


        private List<TBL_USUARIO> ordenarLista(List<TBL_USUARIO> _listaUsuario)
        {
            return _listaUsuario.OrderBy(data => data.usu_apellidos).ToList();
        }

        private List<TBL_USUARIO> saveLista(List<TBL_USUARIO> _listaUsuario)
        {

            //save
            return _listaUsuario.OrderBy(data => data.usu_apellidos).ToList();
        }

        private TBL_ROL modificarRol()
        {
            return null;
        }

        private TBL_ROL eliminarRol()
        {
            return null;
        }


        private void loadUser2(List<TBL_USUARIO> _listaUsuario)
        {
            if (_listaUsuario.Count > 0 && _listaUsuario != null)
            {
                delagateListUser dlgOrdenar = ordenarLista;
                
                //delagateListUser dlgSave = saveLista;

                gdvUsuarios.DataSource = dlgOrdenar(_listaUsuario).Select(data => new
                {
                    CODIGO = data.usu_id,
                    APELLIDOS = data.usu_apellidos,
                    NOMBRES = data.usu_nombres,
                    CORREO = data.usu_correo,
                    ROL = data.TBL_ROL.rol_descripcion
                }).ToList();
            }
        }

        private void loadRol()
        {
            try
            {
                List<TBL_ROL> _listaRoles = new List<TBL_ROL>();
                _listaRoles = LogicRol.getAllRol();
                if (_listaRoles.Count > 0 && _listaRoles != null)
                {
                    _listaRoles.Insert(0, new TBL_ROL
                    {
                        rol_id = 0,
                        rol_descripcion = "Seleccione Rol"

                    });
                    cmbRol.DataSource = _listaRoles;
                    cmbRol.DisplayMember = "rol_descripcion";
                    cmbRol.ValueMember = "rol_id";
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void newUser()
        {
            lblCodigo.Text = "";
            txtApellidos.Clear();
            txtNombres.Clear();
            txtCorreo.Clear();
            txtClave.Clear();
            cmbRol.SelectedIndex = 0;

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            newUser();
        }

        private void saveUser()
        {
            try
            {
                TBL_USUARIO _infoUsuario = new TBL_USUARIO();
                _infoUsuario.usu_correo = txtCorreo.Text.TrimEnd().TrimStart();
                _infoUsuario.usu_apellidos = txtApellidos.Text.TrimEnd().TrimStart().ToUpper();
                _infoUsuario.usu_nombres = txtNombres.Text.TrimEnd().TrimStart().ToUpper();
                _infoUsuario.usu_password = txtClave.Text;
                _infoUsuario.rol_id = Convert.ToByte(cmbRol.SelectedValue);
                bool result = LogicUsuario.saveUser(_infoUsuario);
                if (result)
                {
                    MessageBox.Show("usuario Registrado correctamente", "Sistema Facturación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    newUser();
                    loadUser();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("usuario no Registrado " + ex.Message, "Sistema Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void deleteUser()
        {
            try
            {
                if (!string.IsNullOrEmpty(lblCodigo.Text))
                {
                    var res = MessageBox.Show("Desea eliminar el registro ?", "Sistema de facturacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res.ToString() == "Yes")
                    {
                        TBL_USUARIO _infoUsuario = new TBL_USUARIO();
                        _infoUsuario = LogicUsuario.getUserXID(Convert.ToInt32(lblCodigo.Text));
                        if (_infoUsuario != null)
                        {
                            if (LogicUsuario.DeleteUser(_infoUsuario))
                            {
                                MessageBox.Show("Registro eliminado correctamente jaja");
                                loadUser();
                                newUser();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningùn registro");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void updateUser()
        {
            try
            {
                if (!string.IsNullOrEmpty(lblCodigo.Text))
                {
                    var res = MessageBox.Show("Desea modificar el registro ?", "Sistema de facturacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res.ToString() == "Yes")
                    {
                        TBL_USUARIO _infoUsuario = new TBL_USUARIO();
                        _infoUsuario.usu_id = Convert.ToInt32(lblCodigo.Text);
                        _infoUsuario.usu_correo = txtCorreo.Text.TrimEnd().TrimStart();
                        _infoUsuario.usu_apellidos = txtApellidos.Text.TrimEnd().TrimStart().ToUpper();
                        _infoUsuario.usu_nombres = txtNombres.Text.TrimEnd().TrimStart().ToUpper();
                        _infoUsuario.usu_password = txtClave.Text;
                        _infoUsuario.rol_id = Convert.ToByte(cmbRol.SelectedValue);
                        bool result = LogicUsuario.updateUser3(_infoUsuario);
                        if (result)
                        {
                            MessageBox.Show("usuario Modificado correctamente", "Sistema Facturación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            newUser();
                            loadUser();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("usuario no Registrado " + ex.Message, "Sistema Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblCodigo.Text))
            {
                updateUser();
            }
            else
            {
                saveUser();
            }
        }

        private void gdvUsuarios_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var codigoUsuario = gdvUsuarios.Rows[e.RowIndex].Cells["CODIGO"].Value;
            var correoUsuario = gdvUsuarios.Rows[e.RowIndex].Cells["CORREO"].Value;
            var apellidosUsuario = gdvUsuarios.Rows[e.RowIndex].Cells["APELLIDOS"].Value;
            var nombresUsuario = gdvUsuarios.Rows[e.RowIndex].Cells["NOMBRES"].Value;
            var rolUsuario = gdvUsuarios.Rows[e.RowIndex].Cells["ROL"].Value;

            if (!string.IsNullOrEmpty(codigoUsuario.ToString()))
            {
                lblCodigo.Text = codigoUsuario.ToString();
                txtCorreo.Text = correoUsuario.ToString();
                txtApellidos.Text = apellidosUsuario.ToString();
                txtNombres.Text = nombresUsuario.ToString();
                cmbRol.SelectedIndex = cmbRol.FindString(rolUsuario.ToString());
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            deleteUser();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            search(cmbBuscar.Text);
        }

        private void search(string op)
        {
            if (!string.IsNullOrEmpty(txtBuscar.Text))
            {
                List<TBL_USUARIO> _listaUsuario = new List<TBL_USUARIO>();
                string datoABuscar = txtBuscar.Text.TrimEnd();

                switch (op)
                {
                    case "Todos":
                        _listaUsuario = LogicUsuario.getAllUsers();
                        loadUser2(_listaUsuario);
                    break;
                    case "Nombres":
                        _listaUsuario = LogicUsuario.getUsersXNombres(datoABuscar);
                        loadUser2(_listaUsuario);
                        break;
                    case "Correo":
                        _listaUsuario = LogicUsuario.getUsersXCorreo(datoABuscar);
                        loadUser2(_listaUsuario);
                        break;
                    case "Apellidos":
                        _listaUsuario = LogicUsuario.getUsersXApellidos(datoABuscar);
                        loadUser2(_listaUsuario);
                        break;
                    case "Rol":
                        _listaUsuario = LogicUsuario.getAllUsersXRol(datoABuscar);
                        loadUser2(_listaUsuario);
                        break;
                }
            }
        
        }
    }
}
