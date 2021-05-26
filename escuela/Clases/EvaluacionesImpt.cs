using escuela.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace escuela.Clases
{
    public class EvaluacionesImpt : Conexion, IEvaluaciones
    {
        public void Agregar(Evaluaciones t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Evaluaciones values(@evaluacion1, @evaluacion2, @evaluacion3,@recuperacion, @idTrimestre, " +
                "@idMateria, @idAlumno, idProfesores)";
            cmd.Parameters.AddWithValue("@evaluacion1", t.Evaluacion1);
            cmd.Parameters.AddWithValue("@evaluacion2", t.Evaluacion2);
            cmd.Parameters.AddWithValue("@evaluacion3", t.Evaluacion3);
            cmd.Parameters.AddWithValue("@recuperacion ", t.Recuperacion);
            cmd.Parameters.AddWithValue("@idTrimestre", t.IdTrimestre);
            cmd.Parameters.AddWithValue("@idMateria", t.IdMateria);
            cmd.Parameters.AddWithValue("@idAlumno", t.IdAlumno);
            cmd.Parameters.AddWithValue("@idProfesores", t.IdProfesores);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public SqlCommand cargarTabla(Evaluaciones t, Estudiante e, int trimestre)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (e.Grado <= 18)
            {
                if (trimestre == 5)
                {
                    cmd.CommandText = "EXEC dbo.NotasFinales @usuario = '" + e.Usuario + "'";
                }
                else
                {
                    cmd.CommandText = "select Materia.materia as Materia,evaluacion1 as 'Evaluacion1(35%)',evaluacion2 as 'Evaluacion2(35%)',evaluacion3 as 'Evaluacion3(30%)',CONVERT(DECIMAL(10,2),((evaluacion1*0.35)+(evaluacion2*0.35)+(evaluacion3*0.30))) as Promedio, Case When CONVERT(DECIMAL(10, 2), ((evaluacion1 * 0.35) + (evaluacion2 * 0.35) + (evaluacion3 * 0.30))) > 5 THEN 'Aprobado' When CONVERT(DECIMAL(10,2),((evaluacion1 * 0.35) + (evaluacion2 * 0.35) + (evaluacion3 * 0.30))) < 6 THEN 'Reprobado' END AS Estado from Evaluaciones inner join Materia on Materia.idMateria = Evaluaciones.idMateria where Evaluaciones.idAlumno = @idAlumno and Evaluaciones.idTrimestre = @idTrimestre and Evaluaciones.idMateria <= 5; ";
                    cmd.Parameters.AddWithValue("@idAlumno", t.IdAlumno);
                    cmd.Parameters.AddWithValue("@idTrimestre", trimestre);
                }
            }
            else {
                //Es bachillerato
                if (trimestre == 5)
                {
                    cmd.CommandText = "EXEC dbo.NotasFinales2 @usuario = '" + e.Usuario + "'";
                }
                else
                {
                    cmd.CommandText = "select Materia.materia as Materia,evaluacion1 as 'Evaluacion1(25%)',evaluacion2 as 'Evaluacion2(25%)',evaluacion3 as 'Evaluacion3(25%)',evaluacion4 as 'Evaluacion4(25%)',CONVERT(DECIMAL(10,2),((evaluacion1*0.25)+(evaluacion2*0.25)+(evaluacion3*0.25)+(evaluacion4*0.25))) as Promedio, Case When CONVERT(DECIMAL(10, 2), ((evaluacion1 * 0.35) + (evaluacion2 * 0.35) + (evaluacion3 * 0.30))) > 5 THEN 'Aprobado' When CONVERT(DECIMAL(10,2),((evaluacion1 * 0.35) + (evaluacion2 * 0.35) + (evaluacion3 * 0.30))) < 6 THEN 'Reprobado' END AS Estado from Evaluaciones inner join Materia on Materia.idMateria = Evaluaciones.idMateria where Evaluaciones.idAlumno = @idAlumno and Evaluaciones.idTrimestre = @idTrimestre; ";
                    cmd.Parameters.AddWithValue("@idAlumno", t.IdAlumno);
                    cmd.Parameters.AddWithValue("@idTrimestre", trimestre);
                }
            }
            cmd.ExecuteNonQuery();

            con.Close();
            return cmd;
        }

        public void Eliminar(Evaluaciones t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Evaluaciones WHERE idEvaluaciones = @idEvaluaciones";
            cmd.Parameters.AddWithValue("@idEvaluaciones", t.IdEvaluaciones);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public List<Evaluaciones> ListaEvaluacionesId(Evaluaciones t, int trimestre)
        {
            List<Evaluaciones> alEvaluaciones = new List<Evaluaciones>();
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Materia.materia,evaluacion1,evaluacion2,evaluacion3 from Evaluaciones inner join Materia on Materia.idMateria " +
                "= Evaluaciones.idMateria where Evaluaciones.idAlumno = @idAlumno and Evaluaciones.idTrimestre = @idTrimestre;";
            cmd.Parameters.AddWithValue("@idAlumno", t.IdAlumno);
            cmd.Parameters.AddWithValue("@idTrimestre", trimestre);
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Evaluaciones evaluaciones = new Evaluaciones();
                evaluaciones.Evaluacion1 = Convert.ToDouble(dr[1].ToString());
                evaluaciones.Evaluacion2 = Convert.ToDouble(dr[2].ToString());
                evaluaciones.Evaluacion3 = Convert.ToDouble(dr[3].ToString());
                alEvaluaciones.Add(evaluaciones);
            }
            dr.Close();
            con.Close();
            return alEvaluaciones;
        }

        public List<Evaluaciones> ListaEvaluacionesIdFinales(Evaluaciones t)
        {
            List<Evaluaciones> alEvaluaciones = new List<Evaluaciones>();
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Evaluaciones where Evaluaciones.idAlumno = @idAlumno and Evaluaciones.idTrimestre = @idTrimestre;";
            cmd.Parameters.AddWithValue("@idAlumno", t.IdAlumno);
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Evaluaciones evaluaciones = new Evaluaciones();
                evaluaciones.IdEvaluaciones = Convert.ToInt32(dr[0].ToString());
                evaluaciones.Evaluacion1 = Convert.ToDouble(dr[1].ToString());
                evaluaciones.Evaluacion2 = Convert.ToDouble(dr[2].ToString());
                evaluaciones.Evaluacion3 = Convert.ToDouble(dr[3].ToString());
                if (String.IsNullOrEmpty(dr[4].ToString()))
                {
                    evaluaciones.Recuperacion = 0;
                }
                else
                {
                    evaluaciones.Recuperacion = Convert.ToDouble(dr[4].ToString());
                }
                evaluaciones.IdTrimestre = Convert.ToInt32(dr[5].ToString());
                evaluaciones.IdMateria = Convert.ToInt32(dr[6].ToString());
                evaluaciones.IdAlumno = Convert.ToInt32(dr[7].ToString());
                alEvaluaciones.Add(evaluaciones);
            }
            dr.Close();
            con.Close();
            return alEvaluaciones;
        }

        public List<Evaluaciones> Listar()
        {
            List<Evaluaciones> alEvaluaciones = new List<Evaluaciones>();
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Evaluaciones";
            cmd.ExecuteNonQuery();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Evaluaciones evaluaciones = new Evaluaciones();
                evaluaciones.IdEvaluaciones = Convert.ToInt32(dr[0].ToString());
                evaluaciones.Evaluacion1 = Convert.ToDouble(dr[1].ToString());
                evaluaciones.Evaluacion2 = Convert.ToDouble(dr[2].ToString());
                evaluaciones.Evaluacion3 = Convert.ToDouble(dr[3].ToString());
                if (String.IsNullOrEmpty(dr[4].ToString()))
                {
                    evaluaciones.Recuperacion = 0;
                }
                else
                {
                    evaluaciones.Recuperacion = Convert.ToDouble(dr[4].ToString());
                }
                evaluaciones.IdTrimestre = Convert.ToInt32(dr[5].ToString());
                evaluaciones.IdMateria = Convert.ToInt32(dr[6].ToString());
                evaluaciones.IdAlumno = Convert.ToInt32(dr[7].ToString());
                alEvaluaciones.Add(evaluaciones);
            }
            dr.Close();
            con.Close();
            return alEvaluaciones;
        }

        public void Modificar(Evaluaciones t)
        {
            SqlConnection con = this.conexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Evaluaciones set evaluacion1 = @evaluacion1," +
                "evaluacion2=@evaluacion2,evaluacion3=@evaluacion3,recuperacion = @recuperacion,idTrimestre=@idTrimestre," +
                "idMateria=@idMateria,idAlumno=@idAlumno where idEvaluaciones = @idEvaluaciones";
            cmd.Parameters.AddWithValue("@evaluacion1", t.Evaluacion1);
            cmd.Parameters.AddWithValue("@evaluacion2", t.Evaluacion2);
            cmd.Parameters.AddWithValue("@evaluacion3", t.Evaluacion3);
            cmd.Parameters.AddWithValue("@recuperacion", t.Recuperacion);
            cmd.Parameters.AddWithValue("@idTrimestre", t.IdTrimestre);
            cmd.Parameters.AddWithValue("@idMateria", t.IdMateria);
            cmd.Parameters.AddWithValue("@idAlumno", t.IdAlumno);
            cmd.Parameters.AddWithValue("@idEvaluaciones", t.IdEvaluaciones);
            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}