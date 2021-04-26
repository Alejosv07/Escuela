using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using escuela.Interfaces;

namespace escuela.Clases
{
    public class AlumnoTrimestreImpt : Conexion, IAlumnoTrimestre
    {
        public void Agregar(AlumnoTrimestre t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into AlumnoTrimestre values(@idAlumno,@idTrimestre)";
            cmd.Parameters.AddWithValue("@idAlumno", t.IdAlumno);
            cmd.Parameters.AddWithValue("@idTrimestre", t.IdTrimestre);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public void Eliminar(AlumnoTrimestre t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM AlumnoTrimestre WHERE idAlumnoTrimestre = @idAlumnoTrimestre";
            cmd.Parameters.AddWithValue("@idAlumnoTrimestre", t.IdAlumnoTrimestre);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public List<AlumnoTrimestre> Listar()
        {
            List<AlumnoTrimestre> alAlumnoTrimestres = new List<AlumnoTrimestre>();
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from AlumnoTrimestre";
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                AlumnoTrimestre alumnoTrimestre = new AlumnoTrimestre();
                alumnoTrimestre.IdAlumnoTrimestre = Convert.ToInt32(dr[0].ToString());
                alumnoTrimestre.IdAlumno = Convert.ToInt32(dr[1].ToString());
                alumnoTrimestre.IdTrimestre = Convert.ToInt32(dr[2].ToString());
                alAlumnoTrimestres.Add(alumnoTrimestre);
            }
            dr.Close();
            con.Close();
            return alAlumnoTrimestres;
        }

        public void Modificar(AlumnoTrimestre t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update AlumnoTrimestre set idAlumno = @idAlumno,idTrimestre = @idTrimestre where idAlumnoTrimestre = @idAlumnoTrimestre";
            cmd.Parameters.AddWithValue("@idAlumno", t.IdAlumno);
            cmd.Parameters.AddWithValue("@idTrimestre", t.IdTrimestre);
            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}