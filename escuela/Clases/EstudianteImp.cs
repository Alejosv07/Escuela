using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace escuela.Clases
{
    public class EstudianteImp : Conexion,IEstudiante
    {
        public void Agregar(Estudiante t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Alumnos values(@nombres,@apellido,@carnet,@idGrado,@responsableNombre,@responsableApellido,@email,@usuario,@contrasena)";
            cmd.Parameters.AddWithValue("@nombres", t.Nombre);
            cmd.Parameters.AddWithValue("@apellidos", t.Apellido);
            cmd.Parameters.AddWithValue("@carnet",t.Carnet);
            cmd.Parameters.AddWithValue("@idGrado", t.Grado);
            cmd.Parameters.AddWithValue("@responsableNombre", t.ResponsableNombre);
            cmd.Parameters.AddWithValue("@responsableApellido", t.ResponsableApellido);
            cmd.Parameters.AddWithValue("@email", t.Email);
            cmd.Parameters.AddWithValue("@usuario", t.Usuario);
            cmd.Parameters.AddWithValue("@contrasena", t.Contra);

            cmd.ExecuteNonQuery();

            con.Close();
        }
        public void AgregarProce(Estudiante t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (t.Grado <= 18)
            {
                cmd.CommandText = "EXEC dbo.ingresarAlumno @nombre = '" + t.Nombre + "',@apellido = " +
                    "'" + t.Apellido + "',@carnet = '" + t.Carnet + "',@idGrado = " + t.Grado + "," +
                    "@responsableNombre = '" + t.ResponsableNombre + "',@responsableApellido = '" + t.ResponsableApellido + "'," +
                    "@email = '" + t.Email + "',@usuario = '" + t.Usuario + "',@contra = '" + t.Contra + "';";
            }
            else { 
                cmd.CommandText = "EXEC dbo.ingresarAlumnoBachiller @nombre = '" + t.Nombre + "',@apellido = " +
                    "'" + t.Apellido + "',@carnet = '" + t.Carnet + "',@idGrado = " + t.Grado + "," +
                    "@responsableNombre = '" + t.ResponsableNombre + "',@responsableApellido = '" + t.ResponsableApellido + "'," +
                    "@email = '" + t.Email + "',@usuario = '" + t.Usuario + "',@contra = '" + t.Contra + "';";
            }
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Eliminar(Estudiante t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Alumnos WHERE idAlumno = @idAlumno";
            cmd.Parameters.AddWithValue("@idAlumno", t.IdAlumno);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public Estudiante listaId(int id)
        {
            Estudiante estudiante = new Estudiante();
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Alumnos where idAlumno = @idAlumno";
            cmd.Parameters.AddWithValue("@idAlumno", id);
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                estudiante.IdAlumno = Convert.ToInt32(dr[0].ToString());
                estudiante.Nombre = dr[1].ToString();
                estudiante.Apellido = dr[2].ToString();
                estudiante.Carnet = dr[3].ToString();
                estudiante.Grado = Convert.ToInt32(dr[4].ToString());
                estudiante.ResponsableNombre = dr[5].ToString();
                estudiante.ResponsableApellido = dr[6].ToString();
                estudiante.Email = dr[7].ToString();
                estudiante.Usuario = dr[8].ToString();
                estudiante.Contra = dr[9].ToString();
            }
            dr.Close();
            con.Close();
            return estudiante;
        }

        public Estudiante listaLogin(string email, string contrasena)
        {
            Estudiante estudiante = new Estudiante();
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Alumnos where lower(email) = @email and contra = @contra";
            cmd.Parameters.AddWithValue("@email", email.ToLower().Trim());
            cmd.Parameters.AddWithValue("@contra", contrasena.Trim());
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                estudiante.IdAlumno = Convert.ToInt32(dr[0].ToString());
                estudiante.Nombre = dr[1].ToString();
                estudiante.Apellido = dr[2].ToString();
                estudiante.Carnet = dr[3].ToString();
                estudiante.Grado = Convert.ToInt32(dr[4].ToString());
                estudiante.ResponsableNombre = dr[5].ToString();
                estudiante.ResponsableApellido = dr[6].ToString();
                estudiante.Email = dr[7].ToString();
                estudiante.Usuario = dr[8].ToString();
                estudiante.Contra = dr[9].ToString();
            }
            else {
                estudiante = null;
            }
            dr.Close();
            con.Close();
            return estudiante;
        }

        public Estudiante listaLogin(string email)
        {
            Estudiante estudiante = new Estudiante();
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Alumnos where lower(email) = @email";
            cmd.Parameters.AddWithValue("@email", email.ToLower().Trim());
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                estudiante.IdAlumno = Convert.ToInt32(dr[0].ToString());
                estudiante.Nombre = dr[1].ToString();
                estudiante.Apellido = dr[2].ToString();
                estudiante.Carnet = dr[3].ToString();
                estudiante.Grado = Convert.ToInt32(dr[4].ToString());
                estudiante.ResponsableNombre = dr[5].ToString();
                estudiante.ResponsableApellido = dr[6].ToString();
                estudiante.Email = dr[7].ToString();
                estudiante.Usuario = dr[8].ToString();
                estudiante.Contra = dr[9].ToString();
            }
            else
            {
                estudiante = null;
            }
            dr.Close();
            con.Close();
            return estudiante;
        }

        public List<Estudiante> Listar()
        {
            List<Estudiante> alEstudiante = new List<Estudiante>();
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Alumnos";
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Estudiante estudiante = new Estudiante();
                estudiante.IdAlumno = Convert.ToInt32(dr[0].ToString());
                estudiante.Nombre = dr[1].ToString();
                estudiante.Apellido = dr[2].ToString();
                estudiante.Carnet = dr[3].ToString();
                estudiante.Grado = Convert.ToInt32(dr[4].ToString());
                estudiante.ResponsableNombre = dr[5].ToString();
                estudiante.ResponsableApellido = dr[6].ToString();
                estudiante.Email = dr[7].ToString();
                estudiante.Usuario = dr[8].ToString();
                estudiante.Contra = dr[9].ToString();

                alEstudiante.Add(estudiante);
            }
            dr.Close();
            con.Close();
            return alEstudiante;
        }

        public void Modificar(Estudiante t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Alumnos set nombre = @nombre,apellido=@apellido," +
                "carnet=@carnet,idGrado=@idGrado,responsableNombre=@responsableNombre," +
                "responsableApellido=@responsableApellido,email=@email,usuario=@usuario,contra=@contra where idAlumno = @idAlumno";
            cmd.Parameters.AddWithValue("@nombre", t.Nombre);
            cmd.Parameters.AddWithValue("@apellido", t.Apellido);
            cmd.Parameters.AddWithValue("@carnet", t.Carnet);
            cmd.Parameters.AddWithValue("@idGrado", t.Grado);
            cmd.Parameters.AddWithValue("@responsableNombre", t.ResponsableNombre);
            cmd.Parameters.AddWithValue("@responsableApellido", t.ResponsableApellido);
            cmd.Parameters.AddWithValue("@email", t.Email);
            cmd.Parameters.AddWithValue("@usuario", t.Usuario);
            cmd.Parameters.AddWithValue("@contra", t.Contra);
            cmd.Parameters.AddWithValue("@idAlumno", t.IdAlumno);

            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}