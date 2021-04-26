using escuela.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace escuela.Clases
{
    public class MateriaImpt : Conexion, IMateria
    {
        public void Agregar(Materia t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Materia values(@materia)";
            cmd.Parameters.AddWithValue("@materia", t.Materias);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public void Eliminar(Materia t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Materia WHERE idMateria = @idMateria";
            cmd.Parameters.AddWithValue("@idMateria", t.IdMateria);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public List<Materia> Listar()
        {
            List<Materia> alMateria = new List<Materia>();
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Materia";
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                Materia materia = new Materia();
                materia.IdMateria = Convert.ToInt32(dr[0].ToString());
                materia.Materias = dr[1].ToString();
                alMateria.Add(materia);
            }
            dr.Close();
            con.Close();
            return alMateria;
        }

        public Materia listId(int id)
        {
            Materia materia = new Materia();
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Materia where idMateria = @idMateria";
            cmd.Parameters.AddWithValue("@idMateria", id);
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                materia.IdMateria= Convert.ToInt32(dr[0].ToString());
                materia.Materias = dr[1].ToString();
            }
            dr.Close();
            con.Close();
            return materia;
        }

        public void Modificar(Materia t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Materia set materia = @materia where idMateria = @idMateria";
            cmd.Parameters.AddWithValue("@materia", t.Materias);
            cmd.Parameters.AddWithValue("@idMateria", t.IdMateria);
            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}