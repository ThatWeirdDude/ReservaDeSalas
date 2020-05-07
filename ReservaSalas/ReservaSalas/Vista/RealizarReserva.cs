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
    public partial class RealizarReserva : UserControl
    {
        IEnumerable<Sala> _salas;

        public RealizarReserva()
        {
            InitializeComponent();
        }

        private void RealizarReserva_Load(object sender, EventArgs e)
        {
            //cargar las salas
            _salas = Sala.ObtenerSalas();

            if (_salas != null)
            {
                foreach (Sala sala in _salas)
                {
                    cbSalas.Items.Add(sala.nombre);
                }
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string nombreSala = cbSalas.Text;
            string fecha = dtpFecha.Text;
            string descripcion = rtbDescripcion.Text;
            int result;

            if (nombreSala != "")
            {
                Sala sala = _salas.FirstOrDefault(s => s.nombre == nombreSala);

                if (sala != null)
                {
                    if (Reserva.BuscarReserva(fecha, Sesion.usuario.IDUsuario, sala.IDSala).IDReserva == 0)
                    {
                       result = Reserva.RegistrarReserva(fecha, Sesion.usuario.IDUsuario, sala.IDSala,descripcion);

                       if (result == 1)
                       {
                            MessageBox.Show("Sala reservada con éxito");
                            cbSalas.Text = "";
                            dtpFecha.ResetText();
                            rtbDescripcion.Text = "";
                       }

                    }
                    else
                    {
                        MessageBox.Show("La sala ya esta reservada en esa fecha");
                    }
                }
            }
            else
            {
                MessageBox.Show("Se debe seleccionar una sala");
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _salas = null;
            Sesion.panel.Controls.Clear();
        }
    }
}
