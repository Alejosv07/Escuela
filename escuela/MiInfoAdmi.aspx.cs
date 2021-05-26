using escuela.Clases;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace escuela
{
    public partial class MiInfoAdmi : System.Web.UI.Page
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

            writer.PageEvent = new HeaderFooterPDF("Administrador", "Datos personales", "" + DateTime.Now.Year);
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
            document.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=DatosPersonalesAdministrador.pdf");
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
                else { 
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