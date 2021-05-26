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
            Document document = new Document(iTextSharp.text.PageSize.LETTER, 30f, 20f, 130f, 40f);
            PdfWriter writer = PdfWriter.GetInstance(document, HttpContext.Current.Response.OutputStream);
            dt = dtProfesorNomina();
            string grado = "";
            string seccion = "";
            if (dt.Rows.Count > 0)
            {
                writer.PageEvent = new HeaderFooterPDF("Docentes", "Nomina de alumnos", "" + DateTime.Now.Year);
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

                //Titulo Docente
                PdfPCell _cell = new PdfPCell(new Paragraph("Docente: ", fontTextBold));
                _cell.HorizontalAlignment = Element.ALIGN_LEFT;
                _cell.Border = 0;
                tbDetalles.AddCell(_cell);

                //Detalle titulo Docente
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

                //Titulo Impartiendo
                _cell = new PdfPCell(new Paragraph("Impartiendo: ", fontTextBold));
                _cell.HorizontalAlignment = Element.ALIGN_LEFT;
                _cell.Border = 0;
                tbDetalles.AddCell(_cell);

                //Abrimos conexion para saber el grado
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
                    grado = dr[0].ToString();
                    //Detalle titulo Impartiendo
                    _cell = new PdfPCell(new Paragraph(grado, fontText));
                    _cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    _cell.Border = 0;
                    tbDetalles.AddCell(_cell);
                }
                dr.Close();
                con.Close();

                //Celda vacia
                tbDetalles.AddCell(new Paragraph());

                //Celda vacia
                tbDetalles.AddCell(new Paragraph());

                //Celda vacia
                tbDetalles.AddCell(new Paragraph());

                //Celda vacia
                tbDetalles.AddCell(new Paragraph());

                //Titulo Seccion
                _cell = new PdfPCell(new Paragraph("Seccion: ", fontTextBold));
                _cell.HorizontalAlignment = Element.ALIGN_LEFT;
                _cell.Border = 0;
                tbDetalles.AddCell(_cell);

                //Abrimos conexion para saber la Seccion
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bd.mdf;Integrated Security=True");
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT seccion from Grado where idGrado = @idGrado";
                cmd.Parameters.AddWithValue("@idGrado", this.profesores.IdGrado);
                cmd.ExecuteNonQuery();
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    seccion = dr[0].ToString();
                    //Detalle titulo Seccion
                    _cell = new PdfPCell(new Paragraph(seccion, fontText));
                    _cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    _cell.Border = 0;
                    tbDetalles.AddCell(_cell);
                }
                dr.Close();
                con.Close();

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
                document.Add(new Chunk("Nomina de alumnos: ", fontTextUnderline));

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