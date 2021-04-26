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
    public partial class IndexProfesores : System.Web.UI.Page
    {
        Profesores profesores;
        Evaluaciones evaluaciones;
        EvaluacionesImpt evaluacionesImpt;

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
            this.evaluaciones = new Evaluaciones();
            this.evaluacionesImpt = new EvaluacionesImpt();
            this.txtProfesorSeleccionado.Text = Convert.ToString(profesores.IdProfesores);
            if (!IsPostBack)
            {
                System.Diagnostics.Debug.WriteLine("ID profesor:");
                System.Diagnostics.Debug.WriteLine(this.txtProfesorSeleccionado.Text);
                this.txtMateriaSeleccionada.Text = "1";
                this.txtMateriaShow.Text = "Lenguaje";
                this.txtTrimestreSeleccionado.Text = "1";
                this.txtTrimestreShow.Text = "Primer Trimestre";
            }
            this.lbActu.Text = "Ultima actualización " + DateTime.Now.ToString();
            this.navbarDropdown.InnerText = profesores.Nombre + " " + profesores.Apellido;
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session["Cuenta"] = null;
            Server.Transfer("Login.aspx");
        }
        protected void btnP1_Click(object sender, EventArgs e)
        {
            this.txtMateriaSeleccionada.Text = "1";
            this.txtMateriaShow.Text = "Lenguaje";
        }
        protected void btnP2_Click(object sender, EventArgs e)
        {
            this.txtMateriaSeleccionada.Text = "2";
            this.txtMateriaShow.Text = "Sociales";
        }
        protected void btnP3_Click(object sender, EventArgs e)
        {
            this.txtMateriaSeleccionada.Text = "3";
            this.txtMateriaShow.Text = "Matematica";
        }
        protected void btnP4_Click(object sender, EventArgs e)
        {
            this.txtMateriaSeleccionada.Text = "4";
            this.txtMateriaShow.Text = "Ciencias";
        }

        protected void btnP5_Click(object sender, EventArgs e)
        {
            this.txtMateriaSeleccionada.Text = "5";
            this.txtMateriaShow.Text = "Ingles";
        }
        protected void btnT1_Click(object sender, EventArgs e)
        {
            this.txtTrimestreSeleccionado.Text = "1";
            this.txtTrimestreShow.Text = "Primer Trimestre";
        }
        protected void btnT2_Click(object sender, EventArgs e)
        {
            this.txtTrimestreSeleccionado.Text = "2";
            this.txtTrimestreShow.Text = "Segundo Trimestre";
        }
        protected void btnT3_Click(object sender, EventArgs e)
        {
            this.txtTrimestreSeleccionado.Text = "3";
            this.txtTrimestreShow.Text = "Tercer Trimestre";
        }

        //public void cargarTablaPrincipal()
        //{
        //    this.GridView1.DataSource = dtProfesor();
        //    this.GridView1.DataBind();
        //}

        public DataTable dtProfesor()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bd.mdf;Integrated Security=True");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Alumnos.apellido, Alumnos.nombre, Evaluaciones.evaluacion1 as 'Evaluación1(15%)', Evaluaciones.evaluacion2 as 'Evaluación2(35%)', Evaluaciones.evaluacion3 as 'Evaluación3(50%)' FROM Evaluaciones INNER JOIN Alumnos ON Evaluaciones.idAlumno = Alumnos.idAlumno WHERE (Evaluaciones.idMateria = @idMateria and Evaluaciones.idProfesores = @idProfesores and Evaluaciones.idTrimestre = @idTrimestre)";
            cmd.Parameters.AddWithValue("@idMateria", this.txtMateriaSeleccionada.Text);
            cmd.Parameters.AddWithValue("@idProfesores", this.txtProfesorSeleccionado.Text);
            cmd.Parameters.AddWithValue("@idTrimestre", this.txtTrimestreSeleccionado.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        protected void btnImprimirClick(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, HttpContext.Current.Response.OutputStream);
            dt = dtProfesor();
            if (dt.Rows.Count > 0)
            {
                document.Open();
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                Font fontTitle = new Font(baseFont, 16, 1);
                Paragraph p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Cuadro de Notas", fontTitle));
                document.Add(p);
                Font font9 = FontFactory.GetFont(FontFactory.TIMES, 12);
                document.Add(new Chunk("\n"));
                BaseFont baseFont2 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fontAutor = new Font(baseFont2, 8, 2, BaseColor.GRAY);
                Paragraph pA = new Paragraph();
                pA.Alignment = Element.ALIGN_RIGHT;
                pA.Add(new Chunk("Profesor: " + this.profesores.Nombre + " " + this.profesores.Apellido, fontAutor));
                pA.Add(new Chunk("\nMateria: " + this.txtMateriaShow.Text, fontAutor));
                pA.Add(new Chunk("\nPeriodo: " + this.txtTrimestreShow.Text, fontAutor));
                pA.Add(new Chunk("\nFecha de impresión: " + DateTime.Now.ToShortDateString(), fontAutor));
                document.Add(new Chunk("\n"));

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
                Response.AddHeader("content-disposition", "attachment;filename=CalificacionesAlumnos" + ".pdf");
                HttpContext.Current.Response.Write(document);
                Response.Flush();
                Response.End();
            }
        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }
    }
}