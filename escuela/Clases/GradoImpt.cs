using escuela.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace escuela.Clases
{
    public class GradoImpt : Conexion, IGrado
    {
        public void Agregar(Grado t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Grado values(@nombre,@seccion)";
            cmd.Parameters.AddWithValue("@nombre", t.Nombre);
            cmd.Parameters.AddWithValue("@seccion", t.Seccion);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public void Eliminar(Grado t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Grado WHERE idGrado = @idGrado";
            cmd.Parameters.AddWithValue("@idGrado", t.IdGrado);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public Grado gradoId(int id)
        {
            Grado grado = new Grado();
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Grado where idGrado = @idGrado";
            cmd.Parameters.AddWithValue("@idGrado", id);
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                grado.IdGrado = Convert.ToInt32(dr[0].ToString());
                grado.Nombre = dr[1].ToString();
                grado.Seccion = dr[2].ToString();
            }
            else {
                grado = null;
            }
            dr.Close();
            con.Close();
            return grado;
        }

        public List<Grado> Listar()
        {
            List<Grado> alGrado = new List<Grado>();
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Grado";
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Grado grado = new Grado();
                grado.IdGrado = Convert.ToInt32(dr[0].ToString());
                grado.Nombre = dr[1].ToString();
                grado.Seccion = dr[2].ToString();
                alGrado.Add(grado);
            }
            dr.Close();
            con.Close();
            return alGrado;
        }

        public void Modificar(Grado t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Grado set nombre = @nombre, seccion = @seccion where idGrado = @idGrado";
            cmd.Parameters.AddWithValue("@nombre", t.Nombre);
            cmd.Parameters.AddWithValue("@seccion", t.Seccion);
            cmd.Parameters.AddWithValue("@idGrado", t.IdGrado);
            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}