using PdfSharp.Drawing;
using PdfSharp.Pdf;
using RetailDesktop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Services
{
    public class ReceiptPrinter
    {
        public void GenerateReceiptPdf(SaleGet sale, string filePath)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Чек продажи";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 12);
            int lineHeight = 20;
            int y = 40;

            gfx.DrawString($"Магазин: {sale.Store.FullName}", font, XBrushes.Black, new XRect(40, y, page.Width, page.Height), XStringFormats.TopLeft);
            y += lineHeight;

            gfx.DrawString($"Продавец: {sale.Seller.FullName}", font, XBrushes.Black, new XRect(40, y, page.Width, page.Height), XStringFormats.TopLeft);
            y += lineHeight;

            gfx.DrawString($"Дата: {sale.DateOfSale:dd.MM.yyyy HH:mm}", font, XBrushes.Black, new XRect(40, y, page.Width, page.Height), XStringFormats.TopLeft);
            y += lineHeight * 2;

            gfx.DrawString("Товары:", font, XBrushes.Black, new XRect(40, y, page.Width, page.Height), XStringFormats.TopLeft);
            y += lineHeight;

            foreach (var item in sale.SaleItems)
            {
                var line = $"{item.Product.FullName} - {item.Amount} x {item.Product.CurrentPrice:C2} = {item.Total:C2}";
                gfx.DrawString(line, font, XBrushes.Black, new XRect(60, y, page.Width, page.Height), XStringFormats.TopLeft);
                y += lineHeight;
            }

            y += lineHeight;
            gfx.DrawString($"Итого: {sale.TotalPrice:C2}", font, XBrushes.Black, new XRect(40, y, page.Width, page.Height), XStringFormats.TopLeft);

            document.Save(filePath);
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
        }
    }
}
