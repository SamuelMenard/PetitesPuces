PDF Generation for .Net Developers
IronPDF is the fun, stable C# PDF Library. It makes use of HTML and CSS skills that developers already have to  generating PDFs.

It can generate PDF Files from HTML5,XHTML,Javascript and CSS as well as Jpg, Png, Gif , Bmp and, Tiff and SVG images.


C# Code Examples - HTML to PDF:
----------------------------------
  
using IronPdf;

IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();

Renderer.PrintOptions.DPI = 300;
// There are many advanced  PDF Settings

// Render an HTML document or snippet as a string     
Renderer.RenderHtmlAsPdf("<h1>Hello World</h1>").SaveAs("html-string.pdf");

//  You can also set a BasePath parameter so that assets images, css and js can be loaded
Renderer.RenderHtmlAsPdf("<img src='photo.jpg'", @"C:\path\to\my\assets").SaveAs("with-assets.pdf");

// Render a  local HTML file and all of its assets
Renderer.RenderHTMLFileAsPdf(@"C:\mysite\index.html").SaveAs("html-file.pdf");

// Render a local or remote url
Renderer.RenderUrlAsPdf("http//ironpdf.com/").SaveAs("url.pdf");

C# Code Example - URL to PDF:
----------------------------------
        using IronPdf;
        //
        HtmlToPdf HtmlToPdf = new IronPdf.HtmlToPdf();
        PdfResource PDF = HtmlToPdf.RenderUrlAsPdf(new Uri("http://ironpdf.com"));
        PDF.SaveAs(@"Path\File.Pdf");


C# ASP.NET Code Example - ASPX:

      using IronPdf;

      private void Form1_Load(object sender, EventArgs e)
      {
      AspxToPdf.RenderThisPageAsPDF();      
      //Changes the ASPX output into a PDF file instead of Html   
      }
      
      

Homepage & Documentation: http://ironpdf.com

