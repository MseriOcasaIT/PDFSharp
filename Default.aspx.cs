using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //******************** DE UNA SOLA PÁGINA **************************

            //// Create a new PDF document
            //PdfDocument document = new PdfDocument();          
            //document.Info.Title = "Created with PDFsharp";

            //// Create an empty page
            //PdfPage page = document.AddPage();

            //// Define tamaño de Página (Oficio)
            //page.Width  = XUnit.FromMillimeter(210);
            //page.Height = XUnit.FromMillimeter(297);

            //// Get an XGraphics object for drawing
            //XGraphics gfx = XGraphics.FromPdfPage(page);
            //// Create a font for Titles
            //XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

            //// Dibuja una línea
            //gfx.DrawLine(new XPen(XColors.Red, 2), 0, page.Height / 2, page.Width, page.Height / 2);

            //// Dibuja un rectángulo que ocupa todo el ancho de la página
            //gfx.DrawRectangle(XPens.Black, new XRect(10, 5, (page.Width-20), 30));

            //// Draw the text            

            //// Línea Superior
            //    gfx.DrawString("Hello, World1!", font, XBrushes.Black, new XRect(0, -400, page.Width, page.Height), XStringFormats.Center);


            //// Detalle de Datos
            //// Create a font for Detail
            //XFont fontLines = new XFont("Arial", 10, XFontStyle.Regular);

            //var linea = 60;
            //while (linea < 1000)
            //{
            //    if (linea >= 800)
            //    {
            //        // Create an empty page
            //        PdfPage page2 = document.AddPage();
            //        // Get an XGraphics object for drawing
            //        XGraphics gfx2 = XGraphics.FromPdfPage(page2);
            //        linea = 60;

            //        gfx2.DrawString("Hello, Col1-" + linea.ToString() + "!", fontLines, XBrushes.Black, new XRect(10, linea, 100, 25), XStringFormats.TopLeft);
            //        gfx2.DrawString("Hello, Col2-" + linea.ToString() + "!", fontLines, XBrushes.Black, new XRect(115, linea, 100, 25), XStringFormats.TopLeft);
            //    }
            //    else
            //    {
            //        gfx.DrawString("Hello, Col1-" + linea.ToString() + "!", fontLines, XBrushes.Black, new XRect(10, linea, 100, 25), XStringFormats.TopLeft);
            //        gfx.DrawString("Hello, Col2-" + linea.ToString() + "!", fontLines, XBrushes.Black, new XRect(115, linea, 100, 25), XStringFormats.TopLeft);
            //    }

            //    linea = linea + 20;
            //}

            //////Medio Izquierdo
            ////    gfx.DrawString("Hello, World2!", font, XBrushes.Black, new XRect(-200, 0, page.Width, page.Height), XStringFormats.Center);            
            ////// Medio Derecho
            ////gfx.DrawString("Hello, World3!", font, XBrushes.Black, new XRect(200, 0, page.Width, page.Height), XStringFormats.Center);

            //// Línea Inferior
            //    gfx.DrawString("Hello, World4!", font, XBrushes.Black, new XRect(0, 400, page.Width, page.Height), XStringFormats.Center);

            //// Save the document...
            //const string filename = "HelloWorld.pdf";
            //document.Save(filename);


            //********************** DE VARIAS PÁGINAS ************************
            
            PdfDocument document = new PdfDocument();
            // Sample uses DIN A4, page height is 29.7 cm. We use margins of 2.5 cm.
            LayoutHelper helper = new LayoutHelper(document, XUnit.FromCentimeter(2.5), XUnit.FromCentimeter(29.7 - 2.5));
            XUnit left = XUnit.FromCentimeter(2.5);


            // Random generator with seed value, so created document will always be the same.
            Random rand = new Random(42);
            
            const int headerFontSize = 20;
            const int normalFontSize = 10;
            XFont fontHeader = new XFont("Verdana", headerFontSize, XFontStyle.BoldItalic);
            XFont fontNormal = new XFont("Verdana", normalFontSize, XFontStyle.Regular);

            const int totalLines = 666;
            bool washeader = false;
            for (int line = 0; line < totalLines; ++line)
            {
                //bool isHeader = line == 0 || !washeader && line < totalLines - 1 && rand.Next(15) == 0;
                bool isHeader = line == 0 || line%56==0;
                washeader = isHeader;
                // We do not want a single header at the bottom of the page, so if we have a header we require space for header and a normal text line.
                
                //XUnit top = helper.GetLinePosition(isHeader ? headerFontSize + 5 : normalFontSize + 2, isHeader ? headerFontSize + 5 + normalFontSize : normalFontSize);
                XUnit top = helper.GetLinePosition(normalFontSize + 2, normalFontSize);

                //helper.Gfx.DrawString
                //    (
                //        isHeader
                //        ? "Sed massa libero, semper a nisi nec"
                //        : "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                //            isHeader ? fontHeader : fontNormal, XBrushes.Black, left, top, XStringFormats.TopLeft
                //    );

                if (isHeader)
                {
                    // TÍTULO DE PÁGINA
                    // Dibuja un rectángulo que ocupa todo el ancho de la página
                    helper.Gfx.DrawRectangle(XPens.Black, new XRect(10, 5, (helper.Page.Width - 20), 30));
                    // Línea Superior
                    helper.Gfx.DrawString("TÍTULO DEL DOCUMENTO", fontHeader, XBrushes.Black, new XRect(0, -400, helper.Page.Width, helper.Page.Height), XStringFormats.Center);
                }

                helper.Gfx.DrawString("Hello, Col1-" + line.ToString() + "!", fontNormal, XBrushes.Black, left, top, XStringFormats.TopLeft);
                helper.Gfx.DrawString("Hello, Col2-" + line.ToString() + "!", fontNormal, XBrushes.Black, left+100, top, XStringFormats.TopLeft);

            }
            // Save the document...
            const string filename = "C:/VARIOS/PDFSharp_MultiplePages.pdf";
            document.Save(filename);
        }


        public class LayoutHelper
        {
            private readonly PdfDocument _document;
            private readonly XUnit _topPosition;
            private readonly XUnit _bottomMargin;
            private XUnit _currentPosition;
            public XGraphics Gfx { get; private set; }
            public PdfPage Page { get; private set; }

            public LayoutHelper(PdfDocument document, XUnit topPosition, XUnit bottomMargin)
        {
            _document = document;
            _topPosition = topPosition;
            _bottomMargin = bottomMargin;
            // Set a value outside the page - a new page will be created on the first request.
            _currentPosition = bottomMargin + 10000;
        }
 
            public XUnit GetLinePosition(XUnit requestedHeight)
        {
            return GetLinePosition(requestedHeight, -1f);
        }


            public XUnit GetLinePosition(XUnit requestedHeight, XUnit requiredHeight)
        {
            XUnit required = requiredHeight == -1f ? requestedHeight : requiredHeight;
            if (_currentPosition + required > _bottomMargin)
                CreatePage();
            XUnit result = _currentPosition;
            _currentPosition += requestedHeight;
            return result;
        }

            void CreatePage()
            {
                Page = _document.AddPage();
                Page.Size = PageSize.A4;
                Gfx = XGraphics.FromPdfPage(Page);
                _currentPosition = _topPosition;
            }
        }   


    }
}