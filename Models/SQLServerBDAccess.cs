using OwYO.Models.Interfaces;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace OwYO.Models
{
    public class SQLServerBDAccess : IDBAccess
    {



        #region "Propiedades y Constructor"

        private readonly IConfiguration _config;
        private String __cadenaConexion;

        public SQLServerBDAccess(IConfiguration config)
        {
            this._config = config;
            this.__cadenaConexion = _config.GetConnectionString("SQLServerConnectionString"); // "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Overwatch;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";//this._configSQLServer.Value.SQLServerConnectionString;
        }       

        #endregion

        #region ...Cliente...
        public Cliente ComprobarLogin(String Nickname, String pass)
        {


            SqlConnection __conexionBD = new SqlConnection(this.__cadenaConexion);
            __conexionBD.Open();

            SqlCommand __SelectHeroe = new SqlCommand();
            __SelectHeroe.Connection = __conexionBD;
            __SelectHeroe.CommandText = "SELECT Nickname, Password, IdCliente, Nombre, Apellido FROM Clientes WHERE Nickname = @Nickname;";

            __SelectHeroe.Parameters.AddWithValue("@Nickname", Nickname);

            SqlDataReader __resultado = __SelectHeroe.ExecuteReader();

            Cliente _Cliente = new Cliente();
            String PassBD = "";

            while (__resultado.Read())
            {
                _Cliente.IdCliente = System.Convert.ToInt32(__resultado["IdCliente"]);

                _Cliente.Nickname = __resultado["Nickname"].ToString();
                _Cliente.Nombre = __resultado["Nombre"].ToString();
                _Cliente.Apellido = __resultado["Apellido"].ToString();
                PassBD = __resultado["Password"].ToString();
         
            }

            __conexionBD.Close();
            //hash
            if (PassBD == pass) {

               
            }
            else
            {
                return null;
            }

            return _Cliente;
        }

        #endregion

        #region "Metodos Recuperacion datos"





        public List<Heroe> RecuperarListaHeroes()
        {
            SqlConnection __conexionBD = new SqlConnection(this.__cadenaConexion);
            __conexionBD.Open();

            SqlCommand __SelectHeroes = new SqlCommand();
            __SelectHeroes.Connection = __conexionBD;
            __SelectHeroes.CommandText = "SELECT IdHeroe, Nombre FROM dbo.Heroe ORDER BY Nombre ASC;";

            SqlDataReader __resultado = __SelectHeroes.ExecuteReader();
            

            List<Heroe> __listaADevolver = new List<Heroe>();
            while (__resultado.Read())
            {
                __listaADevolver.Add(new Heroe
                {
                    IdHeroe = System.Convert.ToInt32(__resultado["IdHeroe"]),
                    Nombre = __resultado["Nombre"].ToString()
                });
            }
            __conexionBD.Close();
            return __listaADevolver;
        }

        public Heroe RecuperarHeroe (int IdHeroe)
        {
            SqlConnection __conexionBD = new SqlConnection(this.__cadenaConexion);
            __conexionBD.Open();

            SqlCommand __SelectHeroe = new SqlCommand();
            __SelectHeroe.Connection = __conexionBD;
            __SelectHeroe.CommandText = "SELECT IdHeroe, Nombre, Rol, Vida, Dano, Cura FROM dbo.Heroe WHERE IdHeroe = @IdHeroe;";

            __SelectHeroe.Parameters.AddWithValue("@IdHeroe", IdHeroe);

            SqlDataReader __resultado = __SelectHeroe.ExecuteReader();

            Heroe _Heroe = new Heroe();
            
            while (__resultado.Read())
            {
                _Heroe.IdHeroe = System.Convert.ToInt32(__resultado["IdHeroe"]);
                _Heroe.Nombre = __resultado["Nombre"].ToString();
                _Heroe.Rol = __resultado["Rol"].ToString();
                _Heroe.Vida =  System.Convert.ToInt32(__resultado["Vida"]);
                _Heroe.Cura =  System.Convert.ToInt32(__resultado["Cura"]);
                _Heroe.Dano = System.Convert.ToInt32(__resultado["Dano"]);
            }


            __conexionBD.Close();
            return _Heroe;
        }

        #endregion

        #region "Metodos borrado datos"

        public bool BorrarHeroe(int idHeroe)
        {
            try
            {
                //1º conectarnos al servidor y a la BD
                SqlConnection __miconexion = new SqlConnection();
                __miconexion.ConnectionString = this.__cadenaConexion;

                __miconexion.Open();

                //2º lanzar comando INSERT sobre tabla dbo.Clientes
                SqlCommand __deleteClientes = new SqlCommand();
                __deleteClientes.Connection = __miconexion;

                //construccion query
                string sql = "";

                sql = "DELETE  FROM dbo.Heroe  WHERE IdHeroe = @IdHeroe ";


                __deleteClientes.CommandText = sql;
                __deleteClientes.Parameters.AddWithValue("@IdHeroe", idHeroe);


                int __filasafectadas = __deleteClientes.ExecuteNonQuery();

                __miconexion.Close();

                if (__filasafectadas > 0)
                {
                    return true;
                } 
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        #endregion

        #region "Metodos Insercion datos"

        public bool CrearHeroe(Heroe newHero)
        {
            try
            {
                //1º conectarnos al servidor y a la BD
                SqlConnection __miconexion = new SqlConnection();
                __miconexion.ConnectionString = this.__cadenaConexion;

                __miconexion.Open();

                //2º lanzar comando INSERT sobre tabla dbo.Clientes
                SqlCommand __insertClientes = new SqlCommand();
                __insertClientes.Connection = __miconexion;

                //construccion query
                string sql = "";

                sql = "INSERT INTO dbo.Heroe ";
                sql += " ( ";
                sql += " Nombre, ";
                sql += " Rol, ";
                sql += " Vida, ";
                sql += " Dano, ";
                sql += " Cura ";
                sql += " ) ";

                sql += " VALUES ";
                sql += " ( ";
                sql += " @Nombre, ";
                sql += " @Rol, ";
                sql += " @Vida, ";
                sql += " @Dano, ";
                sql += " @Cura ";
                sql += " ) ";

                __insertClientes.CommandText = sql;
                __insertClientes.Parameters.AddWithValue("@Nombre", newHero.Nombre);
                __insertClientes.Parameters.AddWithValue("@Rol", newHero.Rol);
                __insertClientes.Parameters.AddWithValue("@Vida", newHero.Vida);
                __insertClientes.Parameters.AddWithValue("@Dano", newHero.Dano);
                __insertClientes.Parameters.AddWithValue("@Cura", newHero.Cura);

                int __filasafectadas = __insertClientes.ExecuteNonQuery();

                __miconexion.Close();

                if (__filasafectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }   
            catch (Exception ex)
            {

                return false;
            }
        }

        #endregion

        #region "Metodos Actualizacion datos"

        public bool ActualizarHeroe(Heroe Heroe)
        {
            try
            {
                //1º conectarnos al servidor y a la BD
                SqlConnection __miconexion = new SqlConnection();
                __miconexion.ConnectionString = this.__cadenaConexion;

                __miconexion.Open();

                //2º lanzar comando INSERT sobre tabla dbo.Clientes
                SqlCommand __ActualizarHeroe = new SqlCommand();
                __ActualizarHeroe.Connection = __miconexion;

                //construccion query
                string sql = "";

                sql = "UPDATE dbo.Heroe ";
                sql += " SET ";
                sql += " Nombre = @Nombre, ";
                sql += " Rol = @Rol, ";
                sql += " Vida = @Vida, ";
                sql += " Dano = @Dano, ";
                sql += " Cura = @Cura ";

                sql += " WHERE IdHeroe = @IdHeroe ";

                __ActualizarHeroe.CommandText = sql;
                __ActualizarHeroe.Parameters.AddWithValue("@Nombre", Heroe.Nombre);
                __ActualizarHeroe.Parameters.AddWithValue("@Rol", Heroe.Rol);
                __ActualizarHeroe.Parameters.AddWithValue("@Vida", Heroe.Vida);
                __ActualizarHeroe.Parameters.AddWithValue("@Dano", Heroe.Dano);
                __ActualizarHeroe.Parameters.AddWithValue("@Cura", Heroe.Cura);
                __ActualizarHeroe.Parameters.AddWithValue("@IdHeroe", Heroe.IdHeroe);

                int __filasafectadas = __ActualizarHeroe.ExecuteNonQuery();

                __miconexion.Close();

                if (__filasafectadas !=  0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool ActualizarHeroe2(Heroe heroeActualizar)
        {
            try
            {
                //1º conectarnos al servidor y a la BD
                SqlConnection __miconexion = new SqlConnection();
                __miconexion.ConnectionString = this.__cadenaConexion;

                __miconexion.Open();

                //2º lanzar comando INSERT sobre tabla dbo.Clientes
                SqlCommand __insertClientes = new SqlCommand();
                __insertClientes.Connection = __miconexion;

                //construccion query
                string sql = "";
                //UPDATE Customers SET ContactName = 'Alfred Schmidt', City = 'Frankfurt' WHERE CustomerID = 1;

                /*
                 * actualiza tablaheroes establece como nombre
                 */
                sql = "Update dbo.Heroe SET  ";
                sql += " ( ";
                sql += " Nombre, ";
                sql += " Rol, ";
                sql += " Vida, ";
                sql += " Dano, ";
                sql += " Cura ";
                sql += " ) ";

                sql += " VALUES ";
                sql += " ( ";
                sql += " @Nombre, ";
                sql += " @Rol, ";
                sql += " @Vida, ";
                sql += " @Dano, ";
                sql += " @Cura ";
                sql += " ) ";
                sql += "WHERE idHeroe = 1";

                __insertClientes.CommandText = sql;
                __insertClientes.Parameters.AddWithValue("@Nombre", heroeActualizar.Nombre);
                __insertClientes.Parameters.AddWithValue("@Rol", heroeActualizar.Rol);
                __insertClientes.Parameters.AddWithValue("@Vida", heroeActualizar.Vida);
                __insertClientes.Parameters.AddWithValue("@Dano", heroeActualizar.Dano);
                __insertClientes.Parameters.AddWithValue("@Cura", heroeActualizar.Cura);

                int __filasafectadas = __insertClientes.ExecuteNonQuery();

                __miconexion.Close();

                if (__filasafectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        #endregion
    }
}
