using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace escuela.Clases
{
    public class Conexion
    {
        private string url = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bd.mdf;Integrated Security=True";
        public SqlConnection conexion() {
            SqlConnection con = new SqlConnection(url);
            if (con.State ==  ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            return con;
        }
    }
}