using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using escuela.Clases;
using escuela.Interfaces;
using System.Data;
using System.Data.SqlClient;
namespace escuela.Clases
{
    public class TrimestreImpt : Conexion, ITrimestre
    {
        public void Agregar(Trimestre t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Trimestre values(@numeroTrimestre)";
            cmd.Parameters.AddWithValue("@numeroTrimestre", t.NumeroTrimestre);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public void Eliminar(Trimestre t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Trimestre WHERE idTrimestre = @idTrimestre";
            cmd.Parameters.AddWithValue("@idTrimestre", t.IdTrimestre);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public List<Trimestre> Listar()
        {
            List<Trimestre> alTrimestre = new List<Trimestre>();
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Trimestre";
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Trimestre trimestre = new Trimestre();
                trimestre.IdTrimestre = Convert.ToInt32(dr[0].ToString());
                trimestre.NumeroTrimestre = Convert.ToInt32(dr[1].ToString());
                alTrimestre.Add(trimestre);
            }
            dr.Close();
            con.Close();
            return alTrimestre;
        }

        public void Modificar(Trimestre t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Trimestre set numeroTrimestre = @numeroTrimestre where idTrimestre = @idTrimestre";
            cmd.Parameters.AddWithValue("@numeroTrimestre", t.NumeroTrimestre);
            cmd.Parameters.AddWithValue("@idTrimestre", t.IdTrimestre);
            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}