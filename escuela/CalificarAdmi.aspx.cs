using escuela.Clases;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
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
    public partial class CalificarAdmi : System.Web.UI.Page
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
                Session["Periodo"] = 1;
                this.GridView1.Columns[6].Visible = false;
            }
            this.GridView1.Columns[0].Visible = false;
            this.txtProfesorSeleccionado.Text = this.profesores.IdProfesores.ToString().Trim();
            this.lbActu.Text = "Ultima actualización " + DateTime.Now.ToString();
            this.navbarDropdown.InnerText = profesores.Nombre + " " + profesores.Apellido;
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session["Cuenta"] = null;
            Server.Transfer("Login.aspx");
        }

        protected void btnTF_Click(object sender, EventArgs e)
        {
            Session["Periodo"] = 5;
            this.btnImprimirClick(sender, e);
        }


        public DataTable dtProfesor()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bd.mdf;Integrated Security=True");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (Convert.ToInt32(this.drlGrado.SelectedValue.ToString().Trim()) <= 18)
            {
                cmd.CommandText = "SELECT Alumnos.apellido as 'Apellido', Alumnos.nombre as 'Nombre', Evaluaciones.evaluacion1 as 'Evaluación1(35%)', Evaluaciones.evaluacion2 as 'Evaluación2(35%)', Evaluaciones.evaluacion3 as 'Evaluación3(30%)',((Evaluaciones.evaluacion1*0.35)+(Evaluaciones.evaluacion2*0.35)+(Evaluaciones.evaluacion3*0.30)) as 'PromedioPeriodo' FROM Evaluaciones INNER JOIN Alumnos ON Evaluaciones.idAlumno = Alumnos.idAlumno INNER JOIN Profesores ON Profesores.idProfesores = Evaluaciones.idProfesores INNER JOIN Grado ON Profesores.idGrado = Grado.idGrado WHERE (Evaluaciones.idMateria = @idMateria and Grado.idGrado = @idGrado and Evaluaciones.idTrimestre = @idTrimestre)";
            }
            else
            {
                cmd.CommandText = "SELECT Alumnos.apellido as 'Apellido', Alumnos.nombre as 'Nombre', Evaluaciones.evaluacion1 as 'Evaluación1(25%)', Evaluaciones.evaluacion2 as 'Evaluación2(25%)', Evaluaciones.evaluacion3 as 'Evaluación3(25%)',Evaluaciones.evaluacion4 as 'Evaluación4(25%)',((Evaluaciones.evaluacion1*0.25)+(Evaluaciones.evaluacion2*0.25)+(Evaluaciones.evaluacion3*0.25)+(Evaluaciones.evaluacion4*0.25)) as 'PromedioPeriodo' FROM Evaluaciones INNER JOIN Alumnos ON Evaluaciones.idAlumno = Alumnos.idAlumno INNER JOIN Profesores ON Profesores.idProfesores = Evaluaciones.idProfesores INNER JOIN Grado ON Profesores.idGrado = Grado.idGrado WHERE (Evaluaciones.idMateria = @idMateria and Grado.idGrado = @idGrado and Evaluaciones.idTrimestre = @idTrimestre)";
            }
            cmd.Parameters.AddWithValue("@idMateria", this.drlMateria.SelectedValue.ToString().Trim());
            cmd.Parameters.AddWithValue("@idGrado", this.drlGrado.SelectedValue.ToString().Trim());
            cmd.Parameters.AddWithValue("@idTrimestre", this.drlTrimestre.SelectedValue.ToString().Trim());
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
            if (this.profesores.IdGrado <= 18)
            {
                //Menores a bachillerato
                cmd.CommandText = "EXEC dbo.NotasPorMateria @usuarioProfesor = " + this.profesores.Usuario + ", @materiaGrado = " + this.drlMateria.SelectedItem.ToString().Trim() + ";";
            }
            else
            {
                //Falta
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        protected void btnImprimirClick(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Document document = new Document(iTextSharp.text.PageSize.LETTER, 30f, 20f, 130f, 40f);
            PdfWriter writer = PdfWriter.GetInstance(document, HttpContext.Current.Response.OutputStream);
            int trimes = (int)Session["Periodo"];
            string grado = "";
            string seccion = "";
            if (trimes <= 4)
            {
                dt = dtProfesor();
            }
            else
            {
                dt = dtProfesorFinales();
            }
            if (dt.Rows.Count > 0)
            {
                //Diferenciamos el tipo de documento
                if (trimes <= 4)
                {
                    writer.PageEvent = new HeaderFooterPDF("Administrador", "Periodo " + trimes, "" + DateTime.Now.Year);
                }
                else
                {
                    writer.PageEvent = new HeaderFooterPDF("Administrador", "Notas finales", "" + DateTime.Now.Year);
                }
                document.Open();

                //Letra personalizada
                string nameFont = HttpContext.Current.Server.MapPath("assets/fonts/ArialCE.ttf");

                BaseFont baseFont = BaseFont.CreateFont(nameFont, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fontText = new Font(baseFont, 10, 0, BaseColor.BLACK);
                Font fontTextBold = new Font(baseFont, 10, 1, BaseColor.BLACK);
                Font fontTextUnderline = new Font(baseFont, 10, 4, BaseColor.BLACK);

                //Table detalles
                PdfPTable tbDetalles = new PdfPTable(6);
                tbDetalles.WidthPercentage = 100f;
                tbDetalles.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tbDetalles.DefaultCell.Border = 0;

                //Titulo Administrador
                PdfPCell _cell = new PdfPCell(new Paragraph("Administrador: ", fontTextBold));
                _cell.HorizontalAlignment = Element.ALIGN_LEFT;
                _cell.Border = 0;
                tbDetalles.AddCell(_cell);

                //Detalle titulo Administrador
                _cell = new PdfPCell(new Paragraph(this.profesores.Nombre + " " + this.profesores.Apellido, fontText));
                _cell.HorizontalAlignment = Element.ALIGN_LEFT;
                _cell.Border = 0;
                tbDetalles.AddCell(_cell);

                //Celda vacia
                tbDetalles.AddCell(new Paragraph());

                //Celda vacia
                tbDetalles.AddCell(new Paragraph());

                //Celda vacia
                tbDetalles.AddCell(new Paragraph());

                //Celda vacia
                tbDetalles.AddCell(new Paragraph());

                //Titulo Email
                _cell = new PdfPCell(new Paragraph("Email: ", fontTextBold));
                _cell.HorizontalAlignment = Element.ALIGN_LEFT;
                _cell.Border = 0;
                tbDetalles.AddCell(_cell);

                //Detalle titulo Email
                _cell = new PdfPCell(new Paragraph(this.profesores.Email, fontText));
                _cell.HorizontalAlignment = Element.ALIGN_LEFT;
                _cell.Border = 0;
                tbDetalles.AddCell(_cell);

                //Celda vacia
                tbDetalles.AddCell(new Paragraph());

                //Celda vacia
                tbDetalles.AddCell(new Paragraph());

                //Celda vacia
                tbDetalles.AddCell(new Paragraph());

                //Celda vacia
                tbDetalles.AddCell(new Paragraph());

                document.Add(tbDetalles);

                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));

                //Linea
                Chunk linea = new Chunk(new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -1));
                document.Add(new Paragraph(linea));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("Materia: " + this.drlMateria.SelectedItem.Text, fontTextUnderline));

                PdfPTable table = new PdfPTable(dt.Columns.Count);

                table.WidthPercentage = 100f;

                _cell = new PdfPCell();

                foreach (DataColumn c in dt.Columns)
                {
                    _cell = new PdfPCell(new Paragraph(new Chunk(c.ColumnName, fontText)));
                    _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(_cell);
                }

                foreach (DataRow r in dt.Rows)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int h = 0; h < dt.Columns.Count; h++)
                        {
                            _cell = new PdfPCell(new Paragraph(new Chunk(r[h].ToString(), fontText)));
                            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.AddCell(_cell);
                        }
                    }
                }
                document.Add(table);
                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=CalificacionesAlumnos" + grado + seccion + ".pdf");
                HttpContext.Current.Response.Write(document);
                Response.Flush();
                Response.End();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.drlGrado.SelectedValue.ToString().Trim()) <= 18)
            {
                this.GridView1.Columns[6].Visible = false;
            }
            else {
                this.GridView1.Columns[6].Visible = true;
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Periodo"] = Convert.ToInt32(this.drlTrimestre.SelectedValue.ToString().Trim());
        }
    }
}