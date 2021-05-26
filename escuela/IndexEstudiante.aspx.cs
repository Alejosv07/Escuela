using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using escuela.Clases;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

namespace escuela
{
    public partial class IndexEstudiante : System.Web.UI.Page
    {
        Estudiante estudiante;
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
                estudiante = (Estudiante)Session["Cuenta"];
            }
            this.evaluaciones = new Evaluaciones();
            this.evaluacionesImpt = new EvaluacionesImpt();

            this.evaluaciones.IdAlumno = this.estudiante.IdAlumno;
            if (!IsPostBack)
            {
                this.lbAlumnoSeleccionado.Text = Convert.ToString(this.estudiante.IdAlumno);
                Session["Periodo"] = 1;
                this.cargarTablaPrincipal(1);
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bd.mdf;Integrated Security=True");
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT nombre from Grado where idGrado = @idGrado";
                cmd.Parameters.AddWithValue("@idGrado", this.estudiante.Grado);
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    this.lbGrado.Text = "Cursando: " + dr[0].ToString();
                }
                dr.Close();
                con.Close();
            }
            this.lbActu.Text = "Ultima actualización " + DateTime.Now.ToString();
            this.navbarDropdown.InnerText = estudiante.Nombre + " " + estudiante.Apellido;
        }

        public void cargarTablaPrincipal(int trimestre) {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(this.evaluacionesImpt.cargarTabla(this.evaluaciones,estudiante, trimestre));
            da.Fill(dt);
            this.grid1.DataSource = dt;
            this.grid1.DataBind();
        }
        
        public DataTable dtAlumno() {
            DataTable dt = new DataTable();
            int trimes = (int)Session["Periodo"];
            SqlDataAdapter da = new SqlDataAdapter(this.evaluacionesImpt.cargarTabla(this.evaluaciones,estudiante, trimes));
            da.Fill(dt);
            da.Dispose();
            return dt;
        }
        public DataTable dtAlumnoF() {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bd.mdf;Integrated Security=True");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session["Cuenta"] = null;
            Server.Transfer("Login.aspx");
        }
        protected void btnP1_Click(object sender, EventArgs e)
        {
            Session["Periodo"] = 1;
            this.cargarTablaPrincipal(1);
        }
        protected void btnP2_Click(object sender, EventArgs e)
        {
            Session["Periodo"] = 2;
            this.cargarTablaPrincipal(2);
        }
        protected void btnP3_Click(object sender, EventArgs e)
        {
            Session["Periodo"] = 3;
            this.cargarTablaPrincipal(3);
        }
        protected void btnPF_Click(object sender, EventArgs e)
        {
            Session["Periodo"] = 4;
            this.cargarTablaPrincipal(4);
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Document document = new Document(iTextSharp.text.PageSize.LETTER, 30f, 20f, 130f, 40f);
            PdfWriter writer = PdfWriter.GetInstance(document, HttpContext.Current.Response.OutputStream);
            string grado = "";
            string seccion = "";
            int trimes = (int)Session["Periodo"];
            dt = dtAlumno();
            if (dt.Rows.Count > 0)
            {
                if (trimes <= 4)
                {
                    writer.PageEvent = new HeaderFooterPDF("Estudiante", "Periodo " + trimes, "" + DateTime.Now.Year);
                }
                else
                {
                    writer.PageEvent = new HeaderFooterPDF("Estudiante", "Notas finales", "" + DateTime.Now.Year);
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

                //Titulo Estudiante
                PdfPCell _cell = new PdfPCell(new Paragraph("Estudiante: ", fontTextBold));
                _cell.HorizontalAlignment = Element.ALIGN_LEFT;
                _cell.Border = 0;
                tbDetalles.AddCell(_cell);

                //Detalle titulo Estudiante
                _cell = new PdfPCell(new Paragraph(this.estudiante.Nombre + " " + this.estudiante.Apellido, fontText));
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


                //Titulo Carnet
                _cell = new PdfPCell(new Paragraph("Carnet: ", fontTextBold));
                _cell.HorizontalAlignment = Element.ALIGN_LEFT;
                _cell.Border = 0;
                tbDetalles.AddCell(_cell);

                //Detalle titulo Carnet
                _cell = new PdfPCell(new Paragraph(this.estudiante.Carnet,fontText));
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


                //Titulo Grado
                _cell = new PdfPCell(new Paragraph("Cursando: ", fontTextBold));
                _cell.HorizontalAlignment = Element.ALIGN_LEFT;
                _cell.Border = 0;
                tbDetalles.AddCell(_cell);

                //Abrimos conexion para saber el grado
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bd.mdf;Integrated Security=True");
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT nombre from Grado where idGrado = @idGrado";
                cmd.Parameters.AddWithValue("@idGrado", this.estudiante.Grado);
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    grado = dr[0].ToString();
                    //Detalle titulo Grado
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
                cmd.Parameters.AddWithValue("@idGrado", this.estudiante.Grado);
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

                //Titulo Responsable
                _cell = new PdfPCell(new Paragraph("Responsable: ", fontTextBold));
                _cell.HorizontalAlignment = Element.ALIGN_LEFT;
                _cell.Border = 0;
                tbDetalles.AddCell(_cell);

                //Detalle titulo Responsable
                _cell = new PdfPCell(new Paragraph(this.estudiante.ResponsableNombre+" "+this.estudiante.ResponsableApellido, fontText));
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

                //Linea
                Chunk linea = new Chunk(new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -1));
                document.Add(new Paragraph(linea));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("Calificaciones: ", fontTextUnderline));

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
                Response.AddHeader("content-disposition", "attachment;filename=CalificacionesAlumno" + this.estudiante.Apellido+" "+this.estudiante.Nombre+ ".pdf");
                HttpContext.Current.Response.Write(document);
                Response.Flush();
                Response.End();
            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Periodo"] = Convert.ToInt32(this.DropDownList1.SelectedItem.Text);
            cargarTablaPrincipal(Convert.ToInt32(this.DropDownList1.SelectedItem.Text));
        }
    }
}