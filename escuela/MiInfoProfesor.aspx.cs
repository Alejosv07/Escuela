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
    public partial class MiInfoProfesor : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                GradoImpt gradoImpt = new GradoImpt();
                Grado grado = new Grado();

                grado = gradoImpt.gradoId(this.profesores.IdGrado);
                this.txtApellidos.Text = this.profesores.Apellido;
                this.txtEmail.Text = this.profesores.Email;
                this.txtGrado.Text = grado.Nombre;
                this.txtNombre.Text = this.profesores.Nombre;
                this.txtPass1.Text = this.profesores.Contra;
                this.txtPass2.Text = this.profesores.Contra;
                this.txtUsuario.Text = this.profesores.Usuario;
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session["Cuenta"] = null;
            Server.Transfer("Login.aspx");
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            Document document = new Document(iTextSharp.text.PageSize.LETTER, 30f, 20f, 130f, 40f);
            PdfWriter writer = PdfWriter.GetInstance(document, HttpContext.Current.Response.OutputStream);
            string grado = "";
            string seccion = "";

            writer.PageEvent = new HeaderFooterPDF("Docentes", "Datos personales", "" + DateTime.Now.Year);
            //Abrimos documento
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
            document.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=DatosPersonales.pdf");
            HttpContext.Current.Response.Write(document);
            Response.Flush();
            Response.End();
        }

        protected void btnGuardarInfo_Click(object sender, EventArgs e)
        {
            if (this.txtPass1.Text.Trim().Equals(this.txtPass2.Text.Trim()))
            {
                if (!string.IsNullOrEmpty(this.txtEmail.Text.Trim()))
                {
                    this.profesores.Contra = this.txtPass1.Text.Trim();
                    this.profesores.Email = this.txtEmail.Text.Trim();
                    ProfesoresImpt profesoresImpt = new ProfesoresImpt();
                    profesoresImpt.Modificar(this.profesores);
                    Session["Cuenta"] = this.profesores;
                    Response.Write("<script>alert('" + "Cuenta actualizada" + "');</script>");
                }
                else
                {
                    Response.Write("<script>alert('" + "Campo email vacio" + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('" + "Contraseñas no coiciden" + "');</script>");
            }
        }
    }
}