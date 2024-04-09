using PC_Spiele.Models;
using PCGamesFinal.Data;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;

namespace PCGamesFinal.Service
{
    public class InvoiceGeneratorService
    {

        public InvoiceGeneratorService() {
            CustomFontResolver.Apply();
        }

        public MemoryStream getPDF(Order order, List<GameOrders> gameOrders)
        {
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Arial", 12);
            var h1 = new XFont("Arial", 20);
            var boldFont = new XFont("Arial", 14, XFontStyleEx.Bold);

            // Überschrift
            XRect rect = new XRect(10, 10, page.Width, 40);
            gfx.DrawRectangle(XBrushes.LightGray, rect);
            gfx.DrawString("Rechnung", h1, XBrushes.Black, rect, XStringFormats.Center);

            // Rechnungsinformationen
            gfx.DrawString($"Bestellnummer: {order.Id}", font, XBrushes.Black, new XRect(10, 60, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Datum: {order.Date.ToShortDateString()}", font, XBrushes.Black, new XRect(10, 80, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Benutzer: {order.user.UserName}", font, XBrushes.Black, new XRect(10, 100, page.Width, page.Height), XStringFormats.TopLeft);

            double totalAmount = 0;
            double totalPrice = 0;


            foreach (var gameOrder in gameOrders)
            {
                totalAmount += gameOrder.Amount;
                totalPrice += gameOrder.getTotalPrice();
            }

            int xPos = 10;
            int yPos = 130;
            int columnWidth = 150;
            // Überschriften
            gfx.DrawRectangle(XPens.Black, xPos, yPos, columnWidth, 20);
            gfx.DrawString("Spiel", font, XBrushes.Black, new XRect(xPos, yPos, columnWidth, 20), XStringFormats.Center);
            xPos += columnWidth;

            gfx.DrawRectangle(XPens.Black, xPos, yPos, columnWidth, 20);
            gfx.DrawString("Menge", font, XBrushes.Black, new XRect(xPos, yPos, columnWidth, 20), XStringFormats.Center);
            xPos += columnWidth;

            gfx.DrawRectangle(XPens.Black, xPos, yPos, columnWidth, 20);
            gfx.DrawString("Preis (€)", font, XBrushes.Black, new XRect(xPos, yPos, columnWidth, 20), XStringFormats.Center);
            xPos += columnWidth;

            gfx.DrawRectangle(XPens.Black, xPos, yPos, columnWidth, 20);
            gfx.DrawString("Gesamt (€)", font, XBrushes.Black, new XRect(xPos, yPos, columnWidth, 20), XStringFormats.Center);

            yPos += 20;
            foreach (var gameOrder in gameOrders)
            {
                xPos = 10;
                var game = gameOrder.Game;

                gfx.DrawRectangle(XPens.Black, xPos, yPos, columnWidth, 20);
                gfx.DrawString(game.Name, font, XBrushes.Black, new XRect(xPos, yPos, columnWidth, 20), XStringFormats.Center);
                xPos += columnWidth;

                gfx.DrawRectangle(XPens.Black, xPos, yPos, columnWidth, 20);
                gfx.DrawString(gameOrder.Amount.ToString(), font, XBrushes.Black, new XRect(xPos, yPos, columnWidth, 20), XStringFormats.Center);
                xPos += columnWidth;

                gfx.DrawRectangle(XPens.Black, xPos, yPos, columnWidth, 20);
                gfx.DrawString(game.Price.ToString(), font, XBrushes.Black, new XRect(xPos, yPos, columnWidth, 20), XStringFormats.Center);
                xPos += columnWidth;

                gfx.DrawRectangle(XPens.Black, xPos, yPos, columnWidth, 20);
                gfx.DrawString(gameOrder.getTotalPrice().ToString(), font, XBrushes.Black, new XRect(xPos, yPos, columnWidth, 20), XStringFormats.Center);

                yPos += 20;
            }

            yPos += 10;
            // Gesamtsumme
            gfx.DrawString($"Gesamtanzahl Spiele: {totalAmount}", font, XBrushes.Black, new XRect(10, yPos, page.Width, page.Height), XStringFormats.TopLeft);

            // Linie unter Gesamtpreis
            yPos += 10;
            gfx.DrawLine(XPens.Black, 10, yPos + 10, page.Width - 10, yPos + 10);

            yPos += 15;
            gfx.DrawString($"Gesamtpreis: {totalPrice}€", boldFont, XBrushes.Black, new XRect(10, yPos, page.Width, page.Height), XStringFormats.TopLeft);
            // Speichere das Dokument in einem MemoryStream
            MemoryStream memoryStream = new MemoryStream();
            document.Save(memoryStream, false);
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Rückgabe des PDF-Dokuments
            return memoryStream;
        }

    }

    public class CustomFontResolver : IFontResolver
{
    byte[] IFontResolver.GetFont(string faceName)
    {
        using (var stream = new FileStream("arial.ttf", FileMode.Open, FileAccess.Read))
        {
            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }

    FontResolverInfo IFontResolver.ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        return new FontResolverInfo("Arial");
    }

    internal static CustomFontResolver OurGlobalFontResolver = null;

        /// <summary>
        /// Ensure the font resolver is only applied once (or an exception is thrown)
        /// </summary>
        internal static void Apply()
        {
            if (OurGlobalFontResolver == null || GlobalFontSettings.FontResolver == null)
            {
                if (OurGlobalFontResolver == null)
                    OurGlobalFontResolver = new CustomFontResolver();

                GlobalFontSettings.FontResolver = OurGlobalFontResolver;
            }
        }
    }
}
