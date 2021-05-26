using escuela.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SqlClient;
using System.Data;

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
            Document document = new Document(iTextSharp.text.PageSize.LETTER, 30f, 20f, 130f, 40f);
            PdfWriter writer = PdfWriter.GetInstance(document, HttpContext.Current.Response.OutputStream);
            string grado = "";
            string seccion = "";

            writer.PageEvent = new HeaderFooterPDF("Estudiante", "Datos personales", "" + DateTime.Now.Year);
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
            PdfPCell _cell = new PdfPCell(new Paragraph("Estudiante: ", fontTextBold));
            _cell.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell.Border = 0;
            tbDetalles.AddCell(_cell);

            //Detalle titulo Docente
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
            _cell = new PdfPCell(new Paragraph(this.estudiante.Carnet, fontText));
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

            //Titulo Cursando
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
                //Detalle titulo Cursando
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
            _cell = new PdfPCell(new Paragraph(this.estudiante.ResponsableNombre + " " + this.estudiante.ResponsableApellido, fontText));
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
            _cell = new PdfPCell(new Paragraph(this.estudiante.Email, fontText));
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
            Response.AddHeader("content-disposition","attachment;filename=Datos Personales"+this.estudiante.Apellido+" "+this.estudiante.Nombre+".pdf");
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