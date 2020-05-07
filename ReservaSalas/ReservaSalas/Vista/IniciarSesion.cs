using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReservaSalas.Modelo;

namespace ReservaSalas.Vista
{
    public partial class IniciarSesion : Form
    {
       
        public IniciarSesion()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Iniciar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Iniciar();
            }
        }

        private void Iniciar()
        {
            string nombre = txtUsuario.Text;
            string password = txtPassword.Text;

            txtUsuario.Text = "";
            txtPassword.Text = "";

            if (nombre != "" || password != "")
            {
                Sesion.usuario = Usuario.BuscarUsuario(nombre, password);

                if (Sesion.usuario != null)
                {
                    this.Hide();

                    PanelDeControl panel = new PanelDeControl();
                    panel.Show();
                }
                else
                {
                    MessageBox.Show("Usuario o Contraseña incorrectos");
                }
            }
            else
            {
                MessageBox.Show("Falta Usuario o Contraseña");
            }
        }


    }
}
