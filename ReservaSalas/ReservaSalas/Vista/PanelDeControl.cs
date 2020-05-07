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
    public partial class PanelDeControl : Form
    {

        public PanelDeControl()
        {
            InitializeComponent();

            if (Sesion.usuario.admin == "1")
            {
                usuariosToolStripMenuItem.Visible = true;
            }
            else
            {
                usuariosToolStripMenuItem.Visible = false;
            }
        }

        private void PanelDeControl_Load(object sender, EventArgs e)
        {
            this.Text += " " + Sesion.usuario.nombre;
            Sesion.panel = panelUI;
        }

        //-------------------------------- Usuario ---------------------------------------//

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Close();

            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(IniciarSesion))
                {
                    form.Show();
                    return;
                }
            }
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarContraseña cambiarContrasena = new CambiarContraseña();
            panelUI.Controls.Clear();
            panelUI.Controls.Add(cambiarContrasena);
        }


        //-------------------------------- Reservas ---------------------------------------//

        private void realizarReservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RealizarReserva realizarReserva = new RealizarReserva();
            panelUI.Controls.Clear();
            panelUI.Controls.Add(realizarReserva);
        }

        private void verReservasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerReserva anularReserva = new VerReserva();
            panelUI.Controls.Clear();
            panelUI.Controls.Add(anularReserva);
        }

        private void PanelDeControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(IniciarSesion))
                {
                    form.Show();
                    return;
                }
            }
        }


        //-------------------------------- Usuarios ---------------------------------------//

        private void crearUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CrearUsuario crearUsuario = new CrearUsuario();
            panelUI.Controls.Clear();
            panelUI.Controls.Add(crearUsuario);
        }

        private void verUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerUsuarios verUsuarios = new VerUsuarios();
            panelUI.Controls.Clear();
            panelUI.Controls.Add(verUsuarios);
        }

    }
}
