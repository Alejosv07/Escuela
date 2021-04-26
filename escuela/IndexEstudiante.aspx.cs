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
            }
            this.lbActu.Text = "Ultima actualización " + DateTime.Now.ToString();
            this.navbarDropdown.InnerText = estudiante.Nombre + " " + estudiante.Apellido;
        }

        public void cargarTablaPrincipal(int trimestre) {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(this.evaluacionesImpt.cargarTabla(this.evaluaciones, trimestre));
            da.Fill(dt);
            this.grid1.DataSource = dt;
            this.grid1.DataBind();
        }
        
        public DataTable dtAlumno() {
            DataTable dt = new DataTable();
            int trimes = (int)Session["Periodo"];
            SqlDataAdapter da = new SqlDataAdapter(this.evaluacionesImpt.cargarTabla(this.evaluaciones, trimes));
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
            dt = dtAlumno();
            if (dt.Rows.Count > 0)
            {
                document.Open();
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN,BaseFont.CP1252,BaseFont.NOT_EMBEDDED);

                Font fontTitle = new Font(baseFont,16,1);
                Paragraph p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Cuadro de notas",fontTitle));
                document.Add(p);
                Font font9 = FontFactory.GetFont(FontFactory.TIMES, 12);  
                //Font fontTitle = FontFactory.GetFont(FontFactory.COURIER_BOLD, 25);
                //Font font9 = FontFactory.GetFont(FontFactory.TIMES, 12);

                //document.Add(new Paragraph(20, "Cuadro de notas", fontTitle));
                document.Add(new Chunk("\n"));
                int trimes = (int)Session["Periodo"];


                BaseFont baseFont2 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fontAutor = new Font(baseFont2, 8, 2, BaseColor.GRAY);
                Paragraph pA = new Paragraph();
                pA.Alignment = Element.ALIGN_RIGHT;
                pA.Add(new Chunk("Estudiante: " + this.estudiante.Nombre + " " + this.estudiante.Apellido, fontAutor));
                pA.Add(new Chunk("\nCarnet: " + this.estudiante.Carnet, fontAutor));
                pA.Add(new Chunk("\nPeriodo: " + trimes , fontAutor));
                pA.Add(new Chunk("\nFecha de impresión: " + DateTime.Now.ToShortDateString(), fontAutor));
                document.Add(pA);

                PdfPTable table = new PdfPTable(dt.Columns.Count);

                //document.Add(new Paragraph(20, "Estudiante: "+this.estudiante.Nombre+" "+ this.estudiante.Apellido, font9));
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
                Response.AddHeader("content-disposition", "attachment;filename=Calificaciones" + ".pdf");
                HttpContext.Current.Response.Write(document);
                Response.Flush();
                Response.End();
            }

        }
        
    }
}