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
            Document document = new Document();
            PdfWriter pdfWriter = PdfWriter.GetInstance(document, HttpContext.Current.Response.OutputStream);

            //Abrimos documento
            document.Open();

            BaseFont baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            Font fontTitle = new Font(baseFont, 16, 1);
            Paragraph p = new Paragraph();
            p.Alignment = Element.ALIGN_CENTER;
            p.Add(new Chunk("Datos personales", fontTitle));
            document.Add(p);
            Font font9 = FontFactory.GetFont(FontFactory.TIMES, 12);
            document.Add(new Chunk("\n"));
            BaseFont baseFont2 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fontAutor = new Font(baseFont2, 8, 2, BaseColor.GRAY);
            Paragraph pA = new Paragraph();
            pA.Alignment = Element.ALIGN_RIGHT;
            pA.Add(new Chunk("\nFecha de impresión: " + DateTime.Now.ToShortDateString(), fontAutor));
            document.Add(new Chunk("\n"));
            //Escogemos el tipo de letra que tendra el pdf
            Font fonTextG = FontFactory.GetFont(FontFactory.TIMES, 13);

            //Agregamos una tablaPdf
            PdfPTable table = new PdfPTable(1);

            //Agregamos el titulo al documento
            document.Add(new Paragraph(10, "Nombres: " + this.profesores.Nombre.Trim(), fonTextG));
            document.Add(new Chunk("\n"));
            document.Add(new Paragraph(10, "Apellidos: " + this.profesores.Apellido.Trim(), fonTextG));
            document.Add(new Chunk("\n"));
            //document.Add(new Paragraph(10, "Grado: " + this.estudiante.Grado, fonTextG));
            document.Add(new Chunk("\n"));
            document.Add(new Paragraph(10, "Email: " + this.profesores.Email, fonTextG));
            document.Add(new Chunk("\n"));
            document.Add(new Paragraph(10, "Usuario: " + this.profesores.Usuario, fonTextG));
            document.Add(new Chunk("\n"));
            document.Add(new Paragraph(10, "Contraseña: " + this.profesores.Contra, fonTextG));
            document.Add(new Chunk("\n"));
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