using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocaColaToEDI.Services;
using ConverterEDI.Models;
using ConverterEDI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CocaColaToEDI.Models.InputFileModel;

namespace ConverterEDI.Controllers
{
    [Authorize]
    public class ConverterController : Controller
    {
        private IConversionService _conversionService { get; set; }

        public ConverterController(IConversionService conversionService)
        {
            _conversionService = conversionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DownloadFile(string importType, string fileVersion)
        {

            var data = _conversionService._ConvertedData.FirstOrDefault(x => x.UserName == User.Identity.Name && x.ConversionCode == importType).ConvertedFile;

            string rows = "";
            foreach (var row in data)
            {
                rows += row.EAN + ";"
                    + row.Quantity + ";"
                    + row.PurchasePrice + ";"
                    + row.ProductName + ";"
                    + row.VatRate + ";"
                    + row.PKWIUCode + ";"
                    + row.Unit + ";"
                    + row.ProductCode + fileVersion == "full" ? (";" + row.StationId + ";" + row.SellingPrice) : ""
                    + Environment.NewLine;
                /*
                Teraz objaśnienie dla pól:
                1. EAN             (kolor żółty)
                2. Ilość           (kolor niebieski)
                3. Cena zakupu     (kolor turkusowy)
                4. Nazwa towaru    (kolor zielony)
                5. Stawka VAT      (kolor różowy)
                6. SWW / PKWIU     (kolor fioletowy)
                7. Jednostka       (kolor pomarańczowy)
                8. Indeks towarowy (kolor szary)
                9. ID stacji       (kolor biały)
                10. Cena sprzedaży (kolor czerwony)
                 */
            }


            byte[] bytes = Encoding.GetEncoding("windows-1250").GetBytes(rows);
            var result = new FileContentResult(bytes, "application/octet-stream");
            result.FileDownloadName = "document.csv";
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files, string supplierId)
        {
            DeserializeService deserialize = new DeserializeService();
            DocumentInvoice _documentInvoice = new DocumentInvoice();

            _conversionService._ConvertedData.RemoveAll(x => x.UserName == User.Identity.Name);

            bool isError = false;

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var sr = file.OpenReadStream();
                    _documentInvoice = await deserialize.ImportStream(sr);
                }
            }

            List<EdiDataRow> rows = new List<EdiDataRow>();

            try
            {
                if (_documentInvoice.InvoiceLines.Line.Count > 0)
                {
                    foreach (var item in _documentInvoice.InvoiceLines.Line)
                    {
                        rows.Add(new EdiDataRow
                        {
                            EAN = item.LineItem.EAN,
                            Quantity = item.LineItem.InvoiceQuantity.Replace(',', '.'),
                            PurchasePrice = item.LineItem.InvoiceUnitNetPrice.Replace(',', '.'),
                            ProductName = item.LineItem.ItemDescription,
                            VatRate = item.LineItem.TaxRate.Split('.')[0],
                            PKWIUCode = "",
                            Unit = item.LineItem.UnitOfMeasure,
                            ProductCode = "",
                            StationId = "",
                            SellingPrice = ""
                        });

                    }
                }
            }
            catch
            {
                isError = true;
            }



            isError = deserialize.IsError;
            isError = rows.Count == 0 ? true : false;
            try
            {
                _conversionService._ConvertedData.Add(new ConvertedData
                {
                    ConversionCode = "CC",
                    UserName = User.Identity.Name,
                    ConvertedFile = rows
                });
            }
            catch
            {
                isError = true;
            }

            return Json(new { error = isError, data = rows });
        }
    }
}