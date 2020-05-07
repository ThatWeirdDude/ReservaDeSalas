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
    public partial class CrearUsuario : UserControl
    {
        public CrearUsuario()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string nombre = txtUsuario.Text;
            string password = txtPassword.Text;
            string admin = "0";
            Usuario user;

            if (chbAdmin.Checked)
            {
                admin = "1";
            }

            if (nombre != "" || password != "")
            {
                user = Usuario.CrearUsuario(nombre,password,admin);

                if (user != null)
                {
                     MessageBox.Show("Usuario creado con exito");
                     txtUsuario.Text = "";
                     txtPassword.Text = "";
                     chbAdmin.Checked = false;
                }
            }
            else
            {
                MessageBox.Show("Falta Usuario o Contraseña");
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Sesion.panel.Controls.Clear();
        }

       
    }
}
