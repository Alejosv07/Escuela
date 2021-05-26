using escuela.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace escuela.Clases
{
    public class ProfesoresImpt : Conexion, IProfesores
    {
        public void Agregar(Profesores t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Profesores values(@nombre,@apellido,@idGrado,@email,@usuario,@contra,@nivel)";
            cmd.Parameters.AddWithValue("@nombre", t.Nombre);
            cmd.Parameters.AddWithValue("@apellido", t.Apellido);
            cmd.Parameters.AddWithValue("@idGrado", t.IdGrado);
            cmd.Parameters.AddWithValue("@email", t.Email);
            cmd.Parameters.AddWithValue("@usuario", t.Usuario);
            cmd.Parameters.AddWithValue("@contra", t.Contra);
            cmd.Parameters.AddWithValue("@nivel", t.Nivel);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public void Eliminar(Profesores t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Profesores WHERE idProfesores = @idProfesores";
            cmd.Parameters.AddWithValue("@idProfesores", t.IdProfesores);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public Profesores listaId(int id)
        {
            Profesores profesores = new Profesores();
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Profesores where idProfesores = @idProfesores";
            cmd.Parameters.AddWithValue("@idProfesores", id);
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                profesores.IdProfesores = Convert.ToInt32(dr[0].ToString());
                profesores.Nombre = dr[1].ToString();
                profesores.Apellido = dr[2].ToString();
                profesores.IdGrado = Convert.ToInt32(dr[3].ToString());
                profesores.Email = dr[4].ToString();
                profesores.Usuario = dr[5].ToString();
                profesores.Contra = dr[6].ToString();
                profesores.Nivel = Convert.ToInt32(dr[7].ToString());
            }
            dr.Close();
            con.Close();
            return profesores;
        }

        public Profesores listaIdxGrado(int idGrado)
        {
            //select Evaluaciones.idProfesores from Evaluaciones inner join Profesores on Profesores.idProfesores = Evaluaciones.idProfesores Inner join Grado on Grado.idGrado = Profesores.idGrado where Grado.idGrado = 19 group by Evaluaciones.idProfesores;
            Profesores profesores = new Profesores();
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Evaluaciones.idProfesores from Evaluaciones inner join Profesores on Profesores.idProfesores = Evaluaciones.idProfesores Inner join Grado on Grado.idGrado = Profesores.idGrado where Grado.idGrado = @idGrado group by Evaluaciones.idProfesores;";
            cmd.Parameters.AddWithValue("@idGrado", idGrado);
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();
            int idProfesor = 0;
            if (dr.Read())
            {
                idProfesor = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            con.Close();
            profesores = this.listaId(idProfesor);
            return profesores;
        }

        public Profesores listaLogin(string email, string contrasena)
        {
            Profesores profesores = new Profesores();
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Profesores where lower(email) = @email and contra = @contra";
            cmd.Parameters.AddWithValue("@email", email.ToLower());
            cmd.Parameters.AddWithValue("@contra", contrasena);
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                profesores.IdProfesores = Convert.ToInt32(dr[0].ToString());
                profesores.Nombre = dr[1].ToString();
                profesores.Apellido = dr[2].ToString();
                profesores.IdGrado = Convert.ToInt32(dr[3].ToString());
                profesores.Email = dr[4].ToString();
                profesores.Usuario = dr[5].ToString();
                profesores.Contra = dr[6].ToString();
                profesores.Nivel = Convert.ToInt32(dr[7].ToString());
            }
            else {
                profesores = null;
            }
            dr.Close();
            con.Close();
            return profesores;
        }

        public Profesores listaLogin(string email)
        {
            Profesores profesores = new Profesores();
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Profesores where lower(email) = @email";
            cmd.Parameters.AddWithValue("@email", email.ToLower());
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                profesores.IdProfesores = Convert.ToInt32(dr[0].ToString());
                profesores.Nombre = dr[1].ToString();
                profesores.Apellido = dr[2].ToString();
                profesores.IdGrado = Convert.ToInt32(dr[3].ToString());
                profesores.Email = dr[4].ToString();
                profesores.Usuario = dr[5].ToString();
                profesores.Contra = dr[6].ToString();
            }
            else
            {
                profesores = null;
            }
            dr.Close();
            con.Close();
            return profesores;
        }

        public List<Profesores> Listar()
        {
            List<Profesores> alProfesores = new List<Profesores>();
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Profesores";
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Profesores profesores = new Profesores();
                profesores.IdProfesores = Convert.ToInt32(dr[0].ToString());
                profesores.Nombre = dr[1].ToString();
                profesores.Apellido = dr[2].ToString();
                profesores.IdGrado = Convert.ToInt32(dr[3].ToString());
                profesores.Email = dr[4].ToString();
                profesores.Usuario = dr[5].ToString();
                profesores.Contra = dr[6].ToString();
                profesores.Nivel = Convert.ToInt32(dr[7].ToString());
                alProfesores.Add(profesores);
            }
            dr.Close();
            con.Close();
            return alProfesores;
        }

        public void Modificar(Profesores t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Profesores set nombre = @nombre,apellido = @apellido," +
                "idGrado = @idGrado,email = @email,usuario = @usuario,contra = @contra," +
                "nivel = @nivel where idProfesores = @idProfesores";
            cmd.Parameters.AddWithValue("@nombre", t.Nombre);
            cmd.Parameters.AddWithValue("@apellido", t.Apellido);
            cmd.Parameters.AddWithValue("@idGrado", t.IdGrado);
            cmd.Parameters.AddWithValue("@email", t.Email);
            cmd.Parameters.AddWithValue("@usuario", t.Usuario);
            cmd.Parameters.AddWithValue("@contra", t.Contra);
            cmd.Parameters.AddWithValue("@nivel", t.Nivel);
            cmd.Parameters.AddWithValue("@idProfesores", t.IdProfesores);
            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}