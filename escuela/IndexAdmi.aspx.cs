﻿using escuela.Clases;
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
    public partial class IndexAdmi : System.Web.UI.Page
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
        }

        protected void GuardarEstudiante(object sender, EventArgs e)
        {
            ProfesoresImpt profesoresImpt = new ProfesoresImpt();
            Profesores profesores = new Profesores();

            if (string.IsNullOrEmpty(this.txtApellido.Text.Trim()) 
                || string.IsNullOrEmpty(this.txtContra.Text.Trim()) || string.IsNullOrEmpty(this.txtEmail.Text.Trim())
                || string.IsNullOrEmpty(this.txtNombre.Text.Trim()) || string.IsNullOrEmpty(this.txtApellido.Text.Trim())
                || !this.txtEmail.Text.Contains("@"))
            {
                Response.Write("<script>alert('" + "Campos nulos o invalidos" + "');</script>");
                return;
            }
            try
            {
                profesores.Nivel = Convert.ToInt32(this.DropDownList3.SelectedValue.ToString());
                profesores.Apellido = this.txtApellido.Text.Trim();
                profesores.Contra = this.txtContra.Text.Trim();
                profesores.IdGrado = Convert.ToInt32(this.DropDownList1.SelectedValue.ToString());
                profesores.Email = this.txtEmail.Text.Trim();
                profesores.Nombre = this.txtNombre.Text.Trim();
                profesores.Usuario = this.txtEmail.Text.Trim().Substring(0,this.txtEmail.Text.Trim().IndexOf('@')).Replace("@","");
                profesoresImpt.Agregar(profesores);
                Response.Redirect("IndexAdmi.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script>alert('" + "Campo nivel invalido" + "');</script>");
            }
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
        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session["Cuenta"] = null;
            Server.Transfer("Login.aspx");
        }

        protected void btnPDFProfesores_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, HttpContext.Current.Response.OutputStream);
            dt = dtG("select Profesores.nombre,Profesores.apellido,Grado.nombre as grado,Grado.seccion,Profesores.email,Profesores.usuario,Profesores.contra,Profesores.nivel from Profesores inner join Grado on Grado.idGrado = Profesores.idGrado;");
            if (dt.Rows.Count > 0)
            {
                document.Open();
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                Font fontTitle = new Font(baseFont, 16, 1);
                Paragraph p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Profesores Registrados", fontTitle));
                document.Add(p);
                Font font9 = FontFactory.GetFont(FontFactory.TIMES, 12);
                document.Add(new Chunk("\n"));
                BaseFont baseFont2 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fontAutor = new Font(baseFont2, 8, 2, BaseColor.GRAY);
                Paragraph pA = new Paragraph();
                pA.Alignment = Element.ALIGN_RIGHT;
                pA.Add(new Chunk("Administrador: " + this.profesores.Nombre + " " + this.profesores.Apellido, fontAutor));
                pA.Add(new Chunk("\nFecha de impresión: " + DateTime.Now.ToShortDateString(), fontAutor));


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
                Response.AddHeader("content-disposition", "attachment;filename=ProfesoresRegistrados"+DateTime.Now.ToShortDateString() + ".pdf");
                HttpContext.Current.Response.Write(document);
                Response.Flush();
                Response.End();
            }
        }
    }
}