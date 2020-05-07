using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data.SQLite;
using ReservaSalas.Controlador;

namespace ReservaSalas.Modelo
{
    public class Usuario
    {
        public int IDUsuario;
        public string nombre;
        public string password;
        public string admin;

        public Usuario()
        {
        
        }

        public Usuario(string nombreUsuario, string passwordUsuario)
        {
            nombre = nombreUsuario;
            password = passwordUsuario;
        }

        public static Usuario CrearUsuario(string nombre, string password, string admin)
        {
            var result = new Usuario();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "INSERT INTO usuarios (nombre, password, admin) VALUES (?, ?, ?)";

                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("nombre", nombre));
                    command.Parameters.Add(new SQLiteParameter("password", password));
                    command.Parameters.Add(new SQLiteParameter("admin", admin));

                    command.ExecuteNonQuery();
                }
            }

            return result;
        }

        public static Usuario BuscarUsuario(string nombre, string password)
        {
            var result = new Usuario();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM usuarios"
                            + " WHERE nombre ='" + nombre
                            + "' AND password='" + password + "'";

                using (var command = new SQLiteCommand(query, ctx))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.IDUsuario = Convert.ToInt32(reader["id"].ToString());
                            result.nombre = reader["nombre"].ToString();
                            result.password = reader["password"].ToString();
                            result.admin = reader["admin"].ToString();
                        }
                    }
                }
            }

            return result;
        }

        public static IEnumerable<Usuario> ObtenerUsuarios()
        {
            var result = new List<Usuario>();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM usuarios";

                using (var command = new SQLiteCommand(query, ctx))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Usuario
                            {
                                IDUsuario = Convert.ToInt32(reader["id"].ToString()),
                                nombre = reader["nombre"].ToString(),
                                password = reader["password"].ToString(),
                                admin = reader["admin"].ToString(),
                            });
                        }
                    }
                }
            }

            return result;
        }

        static public void ActualizarUsuario(int idUsuario,string nombre,string password,string admin)
        {
            using (var ctx = DbContext.GetInstance())
            {
                var query = "UPDATE usuarios SET "
                          + " nombre = '" + nombre + "',"
                          + " password = '" + password + "',"
                          + " admin = '" + admin + "'"
                          + " WHERE id = " + idUsuario;

                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        static public void CambiarContraseña(Usuario usuario, string nuevoPassword)
        {
            using (var ctx = DbContext.GetInstance())
            {
                var query = "UPDATE usuarios SET  password = '" + nuevoPassword
                          + " WHERE nombre ='" + usuario.nombre
                          + "' AND password='" + usuario.password + "'";

                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        static public void EliminarUsuario(int idUsuario)
        {
            using (var ctx = DbContext.GetInstance())
            {
                var query = "DELETE FROM usuarios WHERE id = " + idUsuario;

                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        

    }
}
