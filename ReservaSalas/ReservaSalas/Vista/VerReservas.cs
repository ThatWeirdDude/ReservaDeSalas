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
    public partial class VerReserva : UserControl
    {
        IEnumerable<Reserva> _reservas;

        public VerReserva()
        {
            InitializeComponent();
        }

        private void VerReserva_Load(object sender, EventArgs e)
        {
            CargarReservas();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {

            if (dgvReservas.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvReservas.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[colCheckbox.Name].Value) == true)
                    {
                        Reserva.AnularReserva(int.Parse(row.Cells[colIDReserva.Name].Value.ToString()));
                    }
                }

                CargarReservas();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Sesion.panel.Controls.Clear();
        }

        private void CargarReservas()
        {
            dgvReservas.Rows.Clear();
            
            _reservas = Reserva.ObtenerReservas();

            if (_reservas != null && _reservas.Count() > 0)
            {
                foreach (Reserva reserva in _reservas)
                {
                    dgvReservas.Rows.Add(reserva.IDReserva, Sesion.usuario.nombre, Sala.BuscarNombreSala(reserva.IDSala), reserva.fechaReserva,reserva.descripcion);
                }
            }
        }

    }
}
