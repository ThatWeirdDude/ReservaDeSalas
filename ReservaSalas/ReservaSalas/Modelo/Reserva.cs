using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ReservaSalas.Controlador;
using System.Data.SQLite;
 
namespace ReservaSalas.Modelo
{
    public class Reserva
    {
        public int IDReserva;
        public string fechaReserva;
        public int IDUsuario;
        public int IDSala;
        public string descripcion;

        public Reserva()
        {
        }

        public static Reserva BuscarReservaPorID(int IDReserva)
        {
            var result = new Reserva();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM reservas"
                            + " WHERE id =" + IDReserva;

                using (var command = new SQLiteCommand(query, ctx))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.IDReserva = Convert.ToInt32(reader["id"].ToString());
                            result.IDUsuario = Convert.ToInt32(reader["idUsuario"].ToString());
                            result.IDSala = Convert.ToInt32(reader["idSala"].ToString());
                            result.fechaReserva = reader["fecha"].ToString();
                        }
                    }
                }
            }

            return result;
        }

        public static Reserva BuscarReserva(string fecha, int IdUsuario, int IdSala)
        {
            var result = new Reserva();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM reservas"
                            + " WHERE idUsuario = " + IdUsuario
                            + " AND idSala=" + IdSala
                            + " AND fecha='" + fecha + "'";

                using (var command = new SQLiteCommand(query, ctx))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.IDReserva = Convert.ToInt32(reader["id"].ToString());
                            result.IDUsuario = Convert.ToInt32(reader["idUsuario"].ToString());
                            result.IDSala = Convert.ToInt32(reader["idSala"].ToString());
                            result.fechaReserva = reader["fecha"].ToString();
                        }
                    }
                }
            }

            return result;
        }

        public static IEnumerable<Reserva> ObtenerReservas()
        {
            var result = new List<Reserva>();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "";

                if (Sesion.usuario.admin == "1")
                {
                    query = "SELECT * FROM reservas";
                }
                else
                {
                    query = "SELECT * FROM reservas WHERE idUsuario= "+ Sesion.usuario.IDUsuario;

                }

                using (var command = new SQLiteCommand(query, ctx))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Reserva
                            {
                                IDReserva = Convert.ToInt32(reader["id"].ToString()),
                                IDUsuario = Convert.ToInt32(reader["idUsuario"].ToString()),
                                IDSala = Convert.ToInt32(reader["idSala"].ToString()),
                                fechaReserva = reader["fecha"].ToString(),
                                descripcion = reader["descripcion"].ToString(),
                            });
                        }
                    }
                }
            }

            return result;
        }

        public static int RegistrarReserva(string fecha, int idUsuario, int idSala,string descripcion)
        {
            int result = 0;

            using (var ctx = DbContext.GetInstance())
            {
                var query = "INSERT INTO reservas (fecha, idUsuario, idSala,descripcion) VALUES (?, ?, ?,?)";

                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("fecha", fecha));
                    command.Parameters.Add(new SQLiteParameter("idUsuario", idUsuario));
                    command.Parameters.Add(new SQLiteParameter("idSala", idSala));
                    command.Parameters.Add(new SQLiteParameter("descripcion", descripcion));

                    result = command.ExecuteNonQuery();
                }
            }

            return result;
        }

        public static void AnularReserva(int idReserva)
        {
            using (var ctx = DbContext.GetInstance())
            {
                var query = "DELETE FROM reservas WHERE id = " + idReserva;

                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
