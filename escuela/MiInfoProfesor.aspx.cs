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
            Document document = new Document();
            PdfWriter pdfWriter = PdfWriter.GetInstance(document, HttpContext.Current.Response.OutputStream);

            //Abrimos documento
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
            h2.Add(new Paragraph(10, "Datos personales: " + this.profesores.Nombre.Trim(), fontSubTitle));
            document.Add(h2);
            para.Add(new Paragraph(10, "\n "));
            para.Add(new Paragraph(10, "\nNombres: " + this.profesores.Nombre.Trim(), fontEscuela));
            para.Add(new Paragraph(10, "\nApellido: " + this.profesores.Apellido.Trim(), fontEscuela));
            para.Add(new Paragraph(10, "\nEmail: " + this.profesores.Email.Trim(), fontEscuela));
            para.Add(new Paragraph(10, "\nUsuario: " + this.profesores.Usuario.Trim(), fontEscuela));
            para.Add(new Paragraph(10, "\nContraseña: " + this.profesores.Contra.Trim(), fontEscuela));
            para.Add(new Paragraph(10, "\nFecha de impresión: " + DateTime.Now.ToShortDateString(), fontEscuela));
            document.Add(para);
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