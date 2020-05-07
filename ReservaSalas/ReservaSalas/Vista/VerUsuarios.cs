using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReservaSalas.Modelo;

namespace ReservaSalas.Vista
{
    public partial class VerUsuarios : UserControl
    {
        private IEnumerable<Usuario> _usuarios;

        public VerUsuarios()
        {
            InitializeComponent();
        }

        private void VerUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            dgvUsuarios.Rows.Clear();
            _usuarios = Usuario.ObtenerUsuarios();
            string admin = "No";

            if (_usuarios != null && _usuarios.Count() > 0)
            {
                foreach (Usuario usuario in _usuarios)
                {
                    if (usuario.admin == "1")
                    {
                        admin = "si";    
                    }
                    else
                    {
                        admin = "no";
                    }

                    dgvUsuarios.Rows.Add(usuario.IDUsuario,usuario.nombre, usuario.password,admin);
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.Rows.Count > 0)
            {
                int id;
                string nombre;
                string passowrd;
                string admin;

                foreach (DataGridViewRow row in dgvUsuarios.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[colCheckbox.Name].Value) == true)
                    {
                        id = int.Parse(row.Cells[colIDUsuario.Name].Value.ToString());
                        nombre = row.Cells[colNombre.Name].Value.ToString();
                        passowrd = row.Cells[colPassword.Name].Value.ToString();
                        admin = row.Cells[colAdmin.Name].Value.ToString();

                        Usuario.ActualizarUsuario(id,nombre,passowrd,admin);
                    }
                }

                CargarUsuarios();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvUsuarios.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[colCheckbox.Name].Value) == true)
                    {
                        Usuario.EliminarUsuario(int.Parse(row.Cells[colIDUsuario.Name].Value.ToString()));
                    }
                }

                CargarUsuarios();
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Sesion.panel.Controls.Clear();
        }

       
        

    }
}
