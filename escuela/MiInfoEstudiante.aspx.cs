using escuela.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace escuela
{
    public partial class MiInfoEstudiante : System.Web.UI.Page
    {
        Estudiante estudiante;
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
            this.lbActu.Text = "Ultima actualización " + DateTime.Now.ToString();
            this.navbarDropdown.InnerText = estudiante.Nombre + " " + estudiante.Apellido;

            if (!IsPostBack)
            {
                GradoImpt gradoImpt = new GradoImpt();
                Grado grado = new Grado();

                grado = gradoImpt.gradoId(this.estudiante.Grado);
                this.txtApellidos.Text = this.estudiante.Apellido;
                this.txtCarnet.Text = this.estudiante.Carnet;
                this.txtEmail.Text = this.estudiante.Email;
                this.txtGrado.Text = grado.Nombre;
                this.txtNombre.Text = this.estudiante.Nombre;
                this.txtPass1.Text = this.estudiante.Contra;
                this.txtPass2.Text = this.estudiante.Contra;
                this.txtUsuario.Text = this.estudiante.Usuario;
                this.txtResponsableNombre.Text = this.estudiante.ResponsableNombre;
                this.txtResponsableApellido.Text = this.estudiante.ResponsableApellido;
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
            PdfWriter pdfWriter = PdfWriter.GetInstance(document,HttpContext.Current.Response.OutputStream);

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
            Font fonTextG = FontFactory.GetFont(FontFactory.TIMES,13);

            document.Add(new Paragraph(10, "Nombres: "+this.estudiante.Nombre.Trim(), fonTextG));
            document.Add(new Chunk("\n"));
            document.Add(new Paragraph(10, "Apellidos: "+this.estudiante.Apellido.Trim(), fonTextG));
            document.Add(new Chunk("\n"));
            document.Add(new Paragraph(10, "Carnet: "+this.estudiante.Carnet,fonTextG));
            document.Add(new Chunk("\n"));
            document.Add(new Paragraph(10, "Grado: "+this.estudiante.Grado,fonTextG));
            document.Add(new Chunk("\n"));
            document.Add(new Paragraph(10, "Responsable nombre: "+this.estudiante.ResponsableNombre,fonTextG));
            document.Add(new Chunk("\n"));            
            document.Add(new Paragraph(10, "Responsable apellido: "+this.estudiante.ResponsableApellido,fonTextG));
            document.Add(new Chunk("\n"));
            document.Add(new Paragraph(10, "Email: "+this.estudiante.Email,fonTextG));
            document.Add(new Chunk("\n"));           
            document.Add(new Paragraph(10, "Usuario: "+this.estudiante.Usuario,fonTextG));
            document.Add(new Chunk("\n"));
            document.Add(new Paragraph(10, "Contraseña: " + this.estudiante.Contra,fonTextG));
            document.Add(new Chunk("\n"));
            document.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition","attachment;filename=DatosPersonales.pdf");
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
                    this.estudiante.Contra = this.txtPass1.Text.Trim();
                    this.estudiante.Email = this.txtEmail.Text.Trim();
                    EstudianteImp estudianteImp = new EstudianteImp();
                    estudianteImp.Modificar(this.estudiante);
                    Session["Cuenta"] = this.estudiante;
                    Response.Write("<script>alert('" + "Cuenta actualizada" + "');</script>");
                }
                else
                {
                    Response.Write("<script>alert('" + "Campo email vacio" + "');</script>");
                }
            }
            else { 
                Response.Write("<script>alert('" + "Contraseñas no coiciden" + "');</script>");
            }
        }
    }
}