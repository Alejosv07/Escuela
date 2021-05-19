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
            System.Diagnostics.Debug.WriteLine("ID: estudiante");
            System.Diagnostics.Debug.WriteLine(estudiante.IdAlumno);
            System.Diagnostics.Debug.WriteLine("ID: trimeste");
            System.Diagnostics.Debug.WriteLine((int)Session["Periodo"]);
            System.Diagnostics.Debug.WriteLine("ID: grado");
            System.Diagnostics.Debug.WriteLine(estudiante.Grado);

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
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, HttpContext.Current.Response.OutputStream);
            int trimes = (int)Session["Periodo"];
            dt = dtAlumno();
            if (dt.Rows.Count > 0)
            {
                document.Open();
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN,BaseFont.CP1252,BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("assets/img/Logo.png"));
                document.Add(logo);
                Font fontTitle = new Font(baseFont,16,1);
                Font fontSubTitle = new Font(baseFont,14,1);
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
                para.Add(new Chunk("Estudiante: " + this.estudiante.Nombre+" "+this.estudiante.Apellido, fontEscuela));
                para.Add(new Chunk("\nCarnet: " + this.estudiante.Carnet, fontEscuela));
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
                    para.Add(new Chunk("\nCursando: " + dr[0].ToString(), fontEscuela));
                }
                dr.Close();
                con.Close();
                para.Add(new Chunk("\nFecha de impresión: " + DateTime.Now.ToShortDateString(), fontEscuela));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));
                if (trimes < 4)
                {
                    h2.Add(new Chunk("\nPeriodo " + trimes, fontSubTitle));
                }
                else
                {
                    h2.Add(new Chunk("\nNotas finales", fontSubTitle));
                }
                h2.Add(new Chunk("\n", fontSubTitle));
                document.Add(para);
                document.Add(h2);
                Font font9 = FontFactory.GetFont(FontFactory.TIMES, 12);  

                PdfPTable table = new PdfPTable(dt.Columns.Count);

                document.Add(new Chunk("\n"));

                float[] widths = new float[dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                    widths[i] = 4f;

                table.SetWidths(widths);
                table.WidthPercentage = 90;

                PdfPCell cell = new PdfPCell(new Phrase("columns"));
                cell.Colspan = dt.Columns.Count;

                foreach (DataColumn c in dt.Columns)
                {
                    table.AddCell(new Phrase(c.ColumnName, font9));
                }

                foreach (DataRow r in dt.Rows)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int h = 0; h < dt.Columns.Count; h++)
                        {
                            table.AddCell(new Phrase(r[h].ToString(), font9));
                        }
                    }
                }
                document.Add(table);
                document.Close();
                Response.ContentType = "application/pdf";
                if (trimes < 4)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=Calificaciones_Periodo " + trimes + " "+this.estudiante.Carnet+".pdf");
                }
                else { 
                    Response.AddHeader("content-disposition", "attachment;filename=Calificaciones_Finales " + this.estudiante.Carnet + ".pdf");
                }
                HttpContext.Current.Response.Write(document);
                Response.Flush();
                Response.End();
            }

        }
    }
}