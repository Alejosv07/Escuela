using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace escuela.Clases
{
    public class HeaderFooterPDF : PdfPageEventHelper
    {
        string nivel = null;
        string tipoDocumento = null;
        string anoAcademico = null;

        public HeaderFooterPDF(string nivel, string tipoDocumento, string anoAcademico) {
            this.nivel = nivel;
            this.tipoDocumento = tipoDocumento;
            this.anoAcademico = anoAcademico;
        }
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            //Header
            PdfPTable tbHeader = new PdfPTable(1);
            tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbHeader.DefaultCell.Border = 0;

            //Logo
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("assets/img/Logo.png"));
            logo.SetAbsolutePosition(document.LeftMargin, writer.PageSize.GetTop(document.TopMargin)+66);
            logo.ScaleAbsolute(40f, 40f);
            document.Add(logo);

            //Letra personalizada
            string nameFont = HttpContext.Current.Server.MapPath("assets/fonts/ArialCE.ttf");

            BaseFont baseFont = BaseFont.CreateFont(nameFont, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fontHeader = new Font(baseFont, 10, 1, BaseColor.BLACK);
            Font fontText = new Font(baseFont, 10, 0, BaseColor.BLACK);
            Font fontFooter = new Font(baseFont, 10, 0, BaseColor.BLACK);

            //Titulo central
            PdfPCell _cell = new PdfPCell(new Paragraph("COLEGIO SANTA ANA DE EL SALVADOR\n"+nivel.ToUpper(), fontHeader));
            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell.Border = 0;
            tbHeader.AddCell(_cell);
            
            //Email central
            _cell = new PdfPCell(new Paragraph("e-mail: snotas93@gmail.com\n\n", fontText));
            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell.Border = 0;
            tbHeader.AddCell(_cell);
            
            //TipoDocumento central
            _cell = new PdfPCell(new Paragraph(tipoDocumento.ToUpper(), fontText));
            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell.Border = 0;
            tbHeader.AddCell(_cell);
            
            //Ano academico central
            _cell = new PdfPCell(new Paragraph("PERIODO ACADEMICO " + anoAcademico.ToUpper(), fontText));
            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell.Border = 0;
            tbHeader.AddCell(_cell);

            //Linea
            Chunk linea = new Chunk(new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -1));
            tbHeader.AddCell(new Paragraph(linea));


            tbHeader.WriteSelectedRows(0,-1,document.LeftMargin,writer.PageSize.GetTop(document.TopMargin) + 110, writer.DirectContent);


            //Footer
            PdfPTable tbFooter = new PdfPTable(3);
            tbFooter.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbFooter.DefaultCell.Border = 0;

            //Linea
            _cell = new PdfPCell(new Paragraph());
            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell.Border = 1;
            _cell.BorderColorBottom = BaseColor.BLACK;
            tbFooter.AddCell(_cell);

            //Celda vacia
            tbFooter.AddCell(_cell);
            
            //Celda vacia
            tbFooter.AddCell(_cell);


            //Celda vacia
            tbFooter.AddCell(new Paragraph());

            //Celda central
            _cell = new PdfPCell(new Paragraph(new Chunk("FECHA DE IMPRESIÓN: " + DateTime.Now.ToShortDateString(),fontFooter)));
            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell.Border = 0;
            tbFooter.AddCell(_cell);
            
            //Celda derecha
            _cell = new PdfPCell(new Paragraph(new Chunk("PÁGINA: " + writer.PageNumber,fontFooter)));
            _cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _cell.Border = 0;
            tbFooter.AddCell(_cell);

            tbFooter.WriteSelectedRows(0,-1,document.LeftMargin,writer.PageSize.GetBottom(document.BottomMargin) - 2, writer.DirectContent);


        }
    }
}