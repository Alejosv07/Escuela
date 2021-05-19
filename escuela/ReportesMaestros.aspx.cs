using escuela.Clases;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace escuela
{
    public partial class ReportesMaestros : System.Web.UI.Page
    {
        Profesores profesores;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Cuenta"] == null)
            {
                Server.Transfer("Login.aspx");
            }
            else
            {
                profesores = (Profesores)Session["Cuenta"];
            }
            this.lbActu.Text = "Ultima actualización " + DateTime.Now.ToString();
            this.navbarDropdown.InnerText = profesores.Nombre + " " + profesores.Apellido;
        }

        protected void btnImprimirClick(object sender, EventArgs e)
        {

        }

        public DataTable dtProfesorNomina()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bd.mdf;Integrated Security=True");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Alumnos.nombre as 'Nombre', Alumnos.apellido as 'Apellido'  from Evaluaciones inner join Alumnos on Alumnos.idAlumno = Evaluaciones.idAlumno inner join Profesores on Profesores.idProfesores = Evaluaciones.idProfesores where Evaluaciones.idProfesores = @idProfesores group by Alumnos.nombre,Alumnos.apellido";
            cmd.Parameters.AddWithValue("@idProfesores", this.profesores.IdProfesores);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        protected void btnImprimirNominaClick(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, HttpContext.Current.Response.OutputStream);
            dt = dtProfesorNomina();
            if (dt.Rows.Count > 0)
            {
                document.Open();
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                Font fontTitle = new Font(baseFont, 16, 1);
                Font fontSubTitle = new Font(baseFont, 14, 1);
                Paragraph p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                Paragraph h2 = new Paragraph();
                h2.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Colegio Santa Ana", fontTitle));
                document.Add(p);
                document.Add(new Chunk("\n"));

                Font fontEscuela = FontFactory.GetFont(FontFactory.TIMES, 12);
                Paragraph para = new Paragraph();
                para.Alignment = Element.ALIGN_LEFT;
                para.Add(new Chunk("Docente: " + this.profesores.Nombre + " " + this.profesores.Apellido, fontEscuela));
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bd.mdf;Integrated Security=True");
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT nombre from Grado where idGrado = @idGrado";
                cmd.Parameters.AddWithValue("@idGrado", this.profesores.IdGrado);
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    para.Add(new Chunk("\nImpartiendo: " + dr[0].ToString(), fontEscuela));
                }
                dr.Close();
                con.Close();
                para.Add(new Chunk("\nFecha de impresión: " + DateTime.Now.ToShortDateString(), fontEscuela));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));
                h2.Add(new Chunk("\nNomina de alumnos", fontSubTitle));
                h2.Add(new Chunk("\n", fontSubTitle));
                document.Add(para);
                document.Add(h2);
                document.Add(new Chunk("\n"));

                Font font9 = FontFactory.GetFont(FontFactory.TIMES, 12);
                PdfPTable table = new PdfPTable(dt.Columns.Count);


                float[] widths = new float[dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                    widths[i] = 4f;

                table.SetWidths(widths);
                table.WidthPercentage = 90;

                PdfPCell cell = new PdfPCell(new Phrase("columns"));
                cell.Colspan = dt.Columns.Count;

                foreach (DataColumn c in dt.Columns)
                {
                    table.AddCell(new Phrase(c.ColumnName, font9));
                }

                foreach (DataRow r in dt.Rows)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int h = 0; h < dt.Columns.Count; h++)
                        {
                            table.AddCell(new Phrase(r[h].ToString(), font9));
                        }
                    }
                }
                document.Add(table);
                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=NominaAlumnos" + ".pdf");
                HttpContext.Current.Response.Write(document);
                Response.Flush();
                Response.End();
            }
        }
        
        protected void btnImprimirPromedioClick(object sender, EventArgs e)
        {

        }


        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session["Cuenta"] = null;
            Server.Transfer("Login.aspx");
        }
    }
}