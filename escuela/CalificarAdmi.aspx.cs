using escuela.Clases;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace escuela
{
    public partial class CalificarAdmi : System.Web.UI.Page
    {
        Profesores profesores;
        Profesores profesores2;
        Evaluaciones evaluaciones;
        EvaluacionesImpt evaluacionesImpt;
        ProfesoresImpt profesoresImpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Cuenta"] == null)
            {
                Server.Transfer("Login.aspx");
            }
            else
            {
                this.profesoresImpt = new ProfesoresImpt();
                profesores = (Profesores)Session["Cuenta"];
                if (Session["Cuenta2"] != null)
                {
                    profesores2 = (Profesores)Session["Cuenta2"];
                    
                }
                else {
                    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bd.mdf;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select top 1 Grado.idGrado from Grado inner join Profesores on Profesores.idGrado = Grado.idGrado where Profesores.nivel <> 1 ;";
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr = cmd.ExecuteReader();
                    int idGrado;
                    if (dr.Read())
                    {
                        idGrado = Convert.ToInt32(dr[0].ToString().Trim());
                        Session["Cuenta2"] = this.profesoresImpt.listaIdxGrado(idGrado);
                        profesores2 = (Profesores)Session["Cuenta2"];
                    }
                    dr.Close();
                    con.Close();
                }
                if (this.profesores2.IdGrado >= 18)
                {
                    this.lbPeriodos.Text = "Trimestre";
                }
                else
                {
                    this.lbPeriodos.Text = "Periodo";
                }
            }
            this.evaluaciones = new Evaluaciones();
            this.evaluacionesImpt = new EvaluacionesImpt();
            this.txtProfesorSeleccionado.Text = Convert.ToString(profesores.IdProfesores);
            this.GridView1.Columns[0].Visible = false;
            this.txtProfesorSeleccionado.Text = this.profesores.IdProfesores.ToString().Trim();
            this.lbActu.Text = "Ultima actualización " + DateTime.Now.ToString();
            this.navbarDropdown.InnerText = profesores.Nombre + " " + profesores.Apellido;
            if (!IsPostBack)
            {
                Session["Periodo"] = 1;
                this.drlGrado.SelectedIndex = 0;
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session["Cuenta"] = null;
            Server.Transfer("Login.aspx");
        }

        protected void btnExcelPfMactual_Click(object sender, EventArgs e)
        {
            Session["Periodo2"] = 6;
            Session["Periodo"] = Convert.ToInt32(this.drlTrimestre.SelectedItem.ToString().Trim());
            this.btnGeneraExcel_Click(sender,e);
        }
        protected void btnExcelPfMT_Click(object sender, EventArgs e)
        {
            Session["Periodo2"] = 7;
            Session["Periodo"] = Convert.ToInt32(this.drlTrimestre.SelectedItem.ToString().Trim());
            this.btnGeneraExcel_Click(sender, e);
        }
        protected void btnExcelPf_Click(object sender, EventArgs e)
        {
            Session["Periodo"] = 5;
            this.btnGeneraExcel_Click(sender, e);
        }
        protected void btnTF_Click(object sender, EventArgs e)
        {
            Session["Periodo"] = 5;
            this.btnImprimirClick(sender, e);
        }
        
        protected void btnTP_Click(object sender, EventArgs e)
        {
            Session["Periodo2"] = 6;
            Session["Periodo"] = Convert.ToInt32(this.drlTrimestre.SelectedItem.ToString().Trim());
            this.btnImprimirClick(sender, e);
        }
        protected void btnTPT_Click(object sender, EventArgs e)
        {
            Session["Periodo2"] = 7;
            Session["Periodo"] = Convert.ToInt32(this.drlTrimestre.SelectedItem.ToString().Trim());
            this.btnImprimirClick(sender, e);
        }
        
        protected Stream DataTableToExcel()
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ISheet sheet;
            int trimes = (int)Session["Periodo"];
            XSSFRow headerRow;
            
            if (trimes==5 && Session["Periodo2"] == null)
            {
                sheet = workbook.CreateSheet("NotasFinales");
                headerRow = headerRow = (XSSFRow)sheet.CreateRow(0);
                try
                {
                    DataTable dt = dtProfesorFinales();

                    //Query
                    foreach (DataColumn column in dt.Columns)
                        headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                    int rowIndex = 1;
                    foreach (DataRow row in dt.Rows)
                    {
                        XSSFRow dataRow = (XSSFRow)sheet.CreateRow(rowIndex);
                        foreach (DataColumn column in dt.Columns)
                            dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                        ++rowIndex;
                    }
                    for (int i = 0; i <= dt.Columns.Count; ++i)
                        sheet.AutoSizeColumn(i);
                    workbook.Write(ms);
                    ms.Flush();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else if ((int)Session["Periodo2"] == 6)
            {
                sheet = workbook.CreateSheet("Notas_finales");
                headerRow = (XSSFRow)sheet.CreateRow(0);
                try
                {
                    List<Estudiante> alEstudiante = new EstudianteImp().ListarGrado(Convert.ToInt32(this.drlGrado.SelectedValue.ToString().Trim()));
                    foreach (Estudiante estudiante in alEstudiante)
                    {
                        DataTable dt = dtTPeriodo(estudiante);

                        //Query
                        foreach (DataColumn column in dt.Columns)
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                        int rowIndex = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            XSSFRow dataRow = (XSSFRow)sheet.CreateRow(rowIndex);
                            foreach (DataColumn column in dt.Columns)
                                dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                            ++rowIndex;
                        }
                        for (int i = 0; i <= dt.Columns.Count; ++i)
                            sheet.AutoSizeColumn(i);
                        workbook.Write(ms);
                        ms.Flush();
                    }
                    Session.Remove("Periodo2");
                }
                catch (Exception ex)
                {

                    return null;
                }
                finally
                {
                    ms.Close();
                    sheet = null;
                    headerRow = null;
                    workbook = null;
                }
            }
            else
            {
                sheet = workbook.CreateSheet("Periodo_" + trimes);
                headerRow = (XSSFRow)sheet.CreateRow(0);
                try
                {
                    List<Estudiante> alEstudiante = new EstudianteImp().ListarGrado(Convert.ToInt32(this.drlGrado.SelectedValue.ToString().Trim()));
                    foreach (Estudiante estudiante in alEstudiante)
                    {
                        DataTable dt = dtTPeriodo(estudiante);

                        //Query
                        foreach (DataColumn column in dt.Columns)
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                        int rowIndex = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            XSSFRow dataRow = (XSSFRow)sheet.CreateRow(rowIndex);
                            foreach (DataColumn column in dt.Columns)
                                dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                            ++rowIndex;
                        }
                        for (int i = 0; i <= dt.Columns.Count; ++i)
                            sheet.AutoSizeColumn(i);
                        workbook.Write(ms);
                        ms.Flush();
                    }
                    Session.Remove("Periodo2");
                }
                catch (Exception ex)
                {

                    return null;
                }
                finally
                {
                    ms.Close();
                    sheet = null;
                    headerRow = null;
                    workbook = null;
                }
            }
            
            return ms;
        }

        protected void btnGeneraExcel_Click(object sender, EventArgs e)
        {
            Stream s = DataTableToExcel();
            if (s != null)
            {
                MemoryStream ms = s as MemoryStream;
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=" + HttpUtility.UrlEncode("DumpNotas") + ".xlsx"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Length", ms.ToArray().Length.ToString());
                Response.BinaryWrite(ms.ToArray());
                Response.Flush();
                ms.Close();
                ms.Dispose();
            }
        }
        public DataTable dtProfesor()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bd.mdf;Integrated Security=True");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Alumnos.apellido as 'Apellido', Alumnos.nombre as 'Nombre', Evaluaciones.evaluacion1 as 'Evaluación1(35%)', Evaluaciones.evaluacion2 as 'Evaluación2(35%)', Evaluaciones.evaluacion3 as 'Evaluación3(30%)',((Evaluaciones.evaluacion1*0.35)+(Evaluaciones.evaluacion2*0.35)+(Evaluaciones.evaluacion3*0.30)) as 'PromedioPeriodo' FROM Evaluaciones INNER JOIN Alumnos ON Evaluaciones.idAlumno = Alumnos.idAlumno INNER JOIN Profesores ON Profesores.idProfesores = Evaluaciones.idProfesores INNER JOIN Grado ON Profesores.idGrado = Grado.idGrado WHERE (Evaluaciones.idMateria = @idMateria and Grado.idGrado = @idGrado and Evaluaciones.idTrimestre = @idTrimestre)";
            cmd.Parameters.AddWithValue("@idMateria", this.drlMateria.SelectedValue.ToString().Trim());
            cmd.Parameters.AddWithValue("@idGrado", this.drlGrado.SelectedValue.ToString().Trim());
            cmd.Parameters.AddWithValue("@idTrimestre", this.drlTrimestre.SelectedValue.ToString().Trim());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }
        public DataTable dtTPeriodo(Estudiante estudiante)
        {
            DataTable dt = new DataTable();
            int trimes = (int)Session["Periodo2"];
            SqlDataAdapter da = new SqlDataAdapter();
            if (trimes == 6)
            {
                da = new SqlDataAdapter(this.evaluacionesImpt.cargarTabla(this.evaluaciones, estudiante, 5));
            }
            else { 
                da = new SqlDataAdapter(this.evaluacionesImpt.cargarTabla(this.evaluaciones, estudiante, (int)Session["Periodo"]));
            }
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public DataTable dtProfesorFinales()
        {
            this.profesores2 = (Profesores)Session["Cuenta2"];
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bd.mdf;Integrated Security=True");
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (Convert.ToInt32(this.drlGrado.SelectedValue.ToString().Trim()) <= 18)
            {
                //Menores a bachillerato
                cmd.CommandText = "EXEC dbo.NotasPorMateria @usuarioProfesor = " + this.profesores2.Usuario + ", @materiaGrado = " + this.drlMateria.SelectedItem.ToString().Trim() + ";";
            }
            else
            {
                //Bachillerato
                cmd.CommandText = "EXEC dbo.NotasPorMateriaBachillerato @usuarioProfesor = '" + this.profesores2.Usuario + "', @materiaGrado = '" + this.drlMateria.SelectedItem.ToString().Trim() + "';";
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        protected void btnImprimirClick(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Document document = new Document(iTextSharp.text.PageSize.LETTER, 30f, 20f, 130f, 40f);
            PdfWriter writer = PdfWriter.GetInstance(document, HttpContext.Current.Response.OutputStream);
            int trimes = (int)Session["Periodo"];
            string grado = "";
            string seccion = "";
            if (trimes <= 4 && Session["Periodo2"] == null)
            {
                dt = dtProfesor();
            }
            else if (trimes == 5 && Session["Periodo2"] == null)
            {
                dt = dtProfesorFinales();
            } else if (Session["Periodo2"] != null) {
                List<Estudiante> alEstudiante = new EstudianteImp().ListarGrado(Convert.ToInt32(this.drlGrado.SelectedValue.ToString().Trim()));

                //Abrimos conexion para saber el grado
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bd.mdf;Integrated Security=True");
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT nombre from Grado where idGrado = @idGrado";
                cmd.Parameters.AddWithValue("@idGrado", alEstudiante[0].Grado);
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    grado = dr[0].ToString();
                }
                dr.Close();
                con.Close();

                //Diferenciamos el tipo de documento
                if ((int)Session["Periodo2"] == 6)
                {
                    writer.PageEvent = new HeaderFooterPDF("Administrador", "Notas finales "+ grado.ToUpper(), "" + DateTime.Now.Year);
                }
                else
                {
                    writer.PageEvent = new HeaderFooterPDF("Administrador", "Periodo " + trimes +" "+ grado.ToUpper(), "" + DateTime.Now.Year);
                }
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

                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));

                
                //Recorremos la lista
                foreach (Estudiante estudiante in alEstudiante)
                {
                    this.evaluaciones.IdAlumno = estudiante.IdAlumno;
                    dt = dtTPeriodo(estudiante);
                    //Linea
                    Chunk linea = new Chunk(new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -1));
                    document.Add(new Paragraph(linea));
                    document.Add(new Chunk("\n"));
                    document.Add(new Chunk("Alumno: " + estudiante.Nombre + " " + estudiante.Apellido, fontTextUnderline));
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
                }
                document.Close();
                Session.Remove("Periodo2");
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=CalificacionesAlumnosPeriodoAdministrador.pdf");
                HttpContext.Current.Response.Write(document);
                Response.Flush();
                Response.End();
            }
            if (dt.Rows.Count > 0)
            {
                //Diferenciamos el tipo de documento
                if (trimes <= 4)
                {
                    writer.PageEvent = new HeaderFooterPDF("Administrador", "Periodo " + trimes, "" + DateTime.Now.Year);
                }
                else
                {
                    writer.PageEvent = new HeaderFooterPDF("Administrador", "Notas finales", "" + DateTime.Now.Year);
                }
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

                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));

                //Linea
                Chunk linea = new Chunk(new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -1));
                document.Add(new Paragraph(linea));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("Materia: " + this.drlMateria.SelectedItem.Text+" "+this.drlGrado.SelectedItem.Text, fontTextUnderline));

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
                Response.AddHeader("content-disposition", "attachment;filename=CalificacionesAlumnos" + grado + seccion + ".pdf");
                HttpContext.Current.Response.Write(document);
                Response.Flush();
                Response.End();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Cuenta2"] = this.profesoresImpt.listaIdxGrado(Convert.ToInt32(this.drlGrado.SelectedValue.ToString().Trim()));
            
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Periodo"] = Convert.ToInt32(this.drlTrimestre.SelectedValue.ToString().Trim());
        }

    }
}