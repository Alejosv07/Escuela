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
    public partial class EstudianteAdmi : System.Web.UI.Page
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
            this.GridView1.Columns[0].Visible = false;
            this.lbActu.Text = "Ultima actualización " + DateTime.Now.ToString();
            this.navbarDropdown.InnerText = profesores.Nombre + " " + profesores.Apellido;
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session["Cuenta"] = null;
            Server.Transfer("Login.aspx");
        }
        protected void GuardarEstudiante(object sender, EventArgs e)
        {
            EstudianteImp estudianteImp = new EstudianteImp();
            Estudiante estudiante = new Estudiante();
            if (string.IsNullOrEmpty(this.txtApellido.Text.Trim()) || string.IsNullOrEmpty(this.txtContra.Text.Trim()) || string.IsNullOrEmpty(this.txtEmail.Text.Trim())
                || string.IsNullOrEmpty(this.txtNombre.Text.Trim()) || string.IsNullOrEmpty(this.txtApellido.Text.Trim())
                || string.IsNullOrEmpty(this.txtresponsableApellido.Text.Trim()) || string.IsNullOrEmpty(this.txtresponsableNombre.Text.Trim())
                || !this.txtEmail.Text.Contains("@") || string.IsNullOrEmpty(this.DropDownList2.SelectedValue.ToString()))
            {
                Response.Write("<script>alert('" + "Campos nulos o invalidos" + "');</script>");
                return;
            }

            estudiante.Apellido = this.txtApellido.Text.Trim();
            estudiante.Carnet = DateTime.Today.Year+this.txtApellido.Text.Substring(0,1)+this.txtNombre.Text.Substring(0,1)+Convert.ToString(new Random().Next(10,100));
            estudiante.Contra = this.txtContra.Text.Trim();
            estudiante.Email = this.txtEmail.Text.Trim();
            estudiante.Grado = Convert.ToInt32(this.DropDownList2.SelectedValue.ToString());
            estudiante.Nombre = this.txtNombre.Text.Trim();
            estudiante.ResponsableApellido = this.txtresponsableApellido.Text.Trim();
            estudiante.ResponsableNombre = this.txtresponsableNombre.Text.Trim();
            estudiante.Usuario = this.txtEmail.Text.Trim().Substring(0, this.txtEmail.Text.Trim().IndexOf('@')).Replace("@", "");
            estudianteImp.AgregarProce(estudiante);
            Response.Redirect("EstudianteAdmi.aspx");
        }

        public DataTable dtG(string query)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bd.mdf;Integrated Security=True");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        protected void btnPDFAlumnos_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Document document = new Document(iTextSharp.text.PageSize.LETTER, 30f, 20f, 130f, 40f);
            PdfWriter writer = PdfWriter.GetInstance(document, HttpContext.Current.Response.OutputStream);
            dt = dtG("select Alumnos.nombre as 'Nombre',Alumnos.apellido as 'Apellido',Alumnos.carnet as 'Carnet',(Grado.nombre+' '+Grado.seccion) as Grado,Alumnos.responsableNombre as 'Nombre responsable', Alumnos.responsableApellido as 'Apellido responsable' from Alumnos inner join Grado on Grado.idGrado = Alumnos.idGrado; ");
            if (dt.Rows.Count > 0)
            {
                writer.PageEvent = new HeaderFooterPDF("Administrador", "Nomina de alumnos", "" + DateTime.Now.Year);
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
                PdfPCell _cell = new PdfPCell(new Paragraph("Administrador: ", fontTextBold));
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


                document.Add(tbDetalles);

                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));

                //Linea
                Chunk linea = new Chunk(new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -1));
                document.Add(new Paragraph(linea));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("Nomina: ", fontTextUnderline));

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
                Response.AddHeader("content-disposition", "attachment;filename=NominaAlumnosRegistrados" + ".pdf");
                HttpContext.Current.Response.Write(document);
                Response.Flush();
                Response.End();
            }
        }
    }
}