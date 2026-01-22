using System.Reflection.PortableExecutable;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System.Text;

namespace rh.BackOffice.Services.Cv
{
    public static class CvTextExtractor
    {
        public static string ExtractText(string filePath)
        {
            using var reader = new PdfReader(filePath);
            using var pdf = new PdfDocument(reader);

            var sb = new StringBuilder();

            for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
            {
                sb.Append(PdfTextExtractor.GetTextFromPage(pdf.GetPage(i)));
            }

            return sb.ToString();
        }
    }
}
