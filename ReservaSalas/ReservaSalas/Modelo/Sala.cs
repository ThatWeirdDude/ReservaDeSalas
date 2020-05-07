using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ReservaSalas.Controlador;
using System.Data.SQLite;

namespace ReservaSalas.Modelo
{
    public class Sala
    {
        public int IDSala;
        public string nombre;
        public string estado;

        public Sala()
        { 

        }

        public Sala (string nombreSala, string estadoSala)
        {
            nombre = nombreSala;
            estado = estadoSala;
        }

        private void RegistrarSala(Sala sala)
        {
            using (var ctx = DbContext.GetInstance())
            {
                var query = "INSERT INTO salas (nombre, estado) VALUES (?, ?)";

                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("nombre", sala.nombre));
                    command.Parameters.Add(new SQLiteParameter("estado", sala.estado));

                    command.ExecuteNonQuery();
                }
            }
        }

        public static Sala BuscarSala(string nombre)
        {
            var result = new Sala();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM salas"
                            + " WHERE nombre ='" + nombre + "'";

                using (var command = new SQLiteCommand(query, ctx))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.IDSala = Convert.ToInt32(reader["id"].ToString());
                            result.nombre = reader["nombre"].ToString();
                            result.estado = reader["estado"].ToString();
                        }
                    }
                }
            }

            return result;
        }

        public static string BuscarNombreSala(int idSala)
        {
            var result = "";

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT nombre FROM salas"
                            + " WHERE id =" + idSala;

                using (var command = new SQLiteCommand(query, ctx))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader["nombre"].ToString();
                        }
                    }
                }
            }

            return result;
        }



        public static IEnumerable<Sala> ObtenerSalas()
        {
            var result = new List<Sala>();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM salas";

                using (var command = new SQLiteCommand(query, ctx))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Sala
                            {
                                IDSala= Convert.ToInt32(reader["id"].ToString()),
                                nombre = reader["nombre"].ToString(),
                                estado = reader["estado"].ToString(),
                            });
                        }
                    }
                }
            }

            return result;
        }

    }
}
