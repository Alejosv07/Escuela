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
                this.txtMateriaSeleccionada.Text = "1";
                this.txtMateriaShow.Text = "Lenguaje";
                this.txtTrimestreSeleccionado.Text = "1";
                this.txtTrimestreShow.Text = "Primer Trimestre";
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
                    this.lbGrado.Text = "Grado impartido: "+dr[0].ToString();
                }
                dr.Close();
                con.Close();
                Session["Periodo"] = 1;
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
            Session["Periodo"] = 1;
            this.txtTrimestreShow.Text = "Primer Trimestre";
        }
        protected void btnT2_Click(object sender, EventArgs e)
        {
            this.txtTrimestreSeleccionado.Text = "2";
            Session["Periodo"] = 2;
            this.txtTrimestreShow.Text = "Segundo Trimestre";
        }
        protected void btnT3_Click(object sender, EventArgs e)
        {
            this.txtTrimestreSeleccionado.Text = "3";
            Session["Periodo"] = 3;
            this.txtTrimestreShow.Text = "Tercer Trimestre";
        }
        protected void btnTF_Click(object sender, EventArgs e)
        {
            this.txtTrimestreSeleccionado.Text = "4";
            Session["Periodo"] = 4;
            this.btnImprimirClick(sender,e);
        }
        

        public DataTable dtProfesor()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bd.mdf;Integrated Security=True");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Alumnos.apellido as 'Apellido', Alumnos.nombre as 'Nombre', Evaluaciones.evaluacion1 as 'Evaluación1(35%)', Evaluaciones.evaluacion2 as 'Evaluación2(35%)', Evaluaciones.evaluacion3 as 'Evaluación3(30%)',((Evaluaciones.evaluacion1*0.35)+(Evaluaciones.evaluacion2*0.35)+(Evaluaciones.evaluacion3*0.30)) as 'PromedioPeriodo' FROM Evaluaciones INNER JOIN Alumnos ON Evaluaciones.idAlumno = Alumnos.idAlumno WHERE (Evaluaciones.idMateria = @idMateria and Evaluaciones.idProfesores = @idProfesores and Evaluaciones.idTrimestre = @idTrimestre)";
            cmd.Parameters.AddWithValue("@idMateria", this.txtMateriaSeleccionada.Text);
            cmd.Parameters.AddWithValue("@idProfesores", this.txtProfesorSeleccionado.Text);
            cmd.Parameters.AddWithValue("@idTrimestre", this.txtTrimestreSeleccionado.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }
        public DataTable dtProfesorFinales()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bd.mdf;Integrated Security=True");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            
            cmd.CommandText = "EXEC dbo.NotasPorMateria @usuarioProfesor = "+ this.profesores.Usuario+", @materiaGrado = "+this.txtMateriaShow.Text+";";
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
            int trimes = (int)Session["Periodo"];
            if (trimes < 4)
            {
                dt = dtProfesor();
            }
            else { 
                dt = dtProfesorFinales();
            }
            if (dt.Rows.Count > 0)
            {
                document.Open();
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("assets/img/Logo.png"));
                document.Add(logo);
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
                para.Add(new Chunk("\nMateria: " + this.txtMateriaShow.Text, fontEscuela));
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
                if (trimes < 4)
                {
                    h2.Add(new Chunk("\nPeriodo " + trimes, fontSubTitle));
                }
                else
                {
                    h2.Add(new Chunk("\nNotas finales", fontSubTitle));
                }
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