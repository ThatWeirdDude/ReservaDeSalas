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
    public partial class CambiarContraseña : UserControl
    {
        public CambiarContraseña()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Cambiar();
        }
        
        private void txtRepetir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Cambiar();
            }
        }

        private void Cambiar()
        {
            string oldPassword  = txtContraseña.Text;
            string newPassword = txtRepetir.Text;

            if (oldPassword != "" && newPassword != "")
            {
                if (oldPassword == newPassword)
                {
                    //cambiamos la contraseña
                    Usuario.CambiarContraseña(Sesion.usuario, newPassword);

                    //actualizamos las sesion
                    Sesion.usuario = Usuario.BuscarUsuario(Sesion.usuario.nombre, newPassword);

                    MessageBox.Show("Contraseña cambiada con exito");

                }
                else
                {
                    MessageBox.Show("Las contraseñas deben coincidir");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Sesion.panel.Controls.Clear();
        }

        

    }
}
