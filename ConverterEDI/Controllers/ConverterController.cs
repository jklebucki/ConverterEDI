using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarrefourMagnat.Services;
using CocaColaToEDI.Services;
using CocaColaTxtEDI.Services;
using ConverterEDI.Data;
using ConverterEDI.Infrustructure;
using ConverterEDI.Models;
using ConverterEDI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CocaColaToEDI.Models.InputFileModel;

namespace ConverterEDI.Controllers
{
    [Authorize]
    public class ConverterController : Controller
    {
        private IConversionService _conversionService { get; set; }
        private ApplicationDbContext _db { get; set; }

        public ConverterController(IConversionService conversionService, ApplicationDbContext db)
        {
            _conversionService = conversionService;
            _db = db;
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
                string option = fileVersion == "full" ? (";" + row.StationId + ";" + row.SellingPrice) : "";
                rows += row.EAN + ";"
                    + row.Quantity + ";"
                    + row.PurchasePrice + ";"
                    + row.ProductName + ";"
                    + row.VatRate + ";"
                    + row.PKWIUCode + ";"
                    + row.Unit + ";"
                    + row.ProductCode + option
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

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            byte[] bytes = Encoding.GetEncoding("Windows-1250").GetBytes(rows);
            var result = new FileContentResult(bytes, "application/octet-stream");
            result.FileDownloadName = fileVersion == "full" ? "document-full.csv" : "document-short.csv";
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files, string supplierId)
        {
            _conversionService._ConvertedData.RemoveAll(x => x.UserName == User.Identity.Name);
            bool isError = false;
            Stream sr = Stream.Null;
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    sr = file.OpenReadStream();
                }
            }

            List<EdiDataRow> rows = new List<EdiDataRow>();
            string conversionCode = "NA";
            string errorMessage = "";
            switch (supplierId)
            {
                case "1":
                    DeserializeServiceXml deserializeXml = new DeserializeServiceXml();
                    DocumentInvoice _documentInvoice = new DocumentInvoice();
                    _documentInvoice = await deserializeXml.ImportStream(sr);
                    isError = deserializeXml.IsError;
                    var dtXml = new LoadFromXml();
                    rows = dtXml.Load(_documentInvoice);
                    isError = dtXml.isError;
                    conversionCode = "CC";
                    break;
                case "2":
                    DeserializeServiceTxt deserializeTxt = new DeserializeServiceTxt();
                    var flatRows = await deserializeTxt.ImportStream(sr);
                    isError = deserializeTxt.IsError;
                    var dtTxt = new LoadFromTxt();
                    rows = dtTxt.Load(flatRows);
                    isError = dtTxt.isError;
                    conversionCode = "CC";
                    break;
                case "3":
                    DeserializeServiceMagnat deserializeMagnat = new DeserializeServiceMagnat();
                    var flatMagnatRows = await deserializeMagnat.ImportStream(sr);
                    isError = deserializeMagnat.IsError;
                    var dtMagnat = new LoadFromMagnat();
                    rows = dtMagnat.Load(flatMagnatRows);
                    isError = dtMagnat.isError;
                    conversionCode = "CC";
                    break;
                default:
                    break;
            }

            try
            {
                _conversionService._ConvertedData.Add(new ConvertedData
                {
                    ConversionCode = conversionCode,
                    UserName = User.Identity.Name,
                    ConvertedFile = rows
                });
            }
            catch
            {
                isError = true;
            }

            if (rows.Count > 0)
            {
                isError = false;
                rows = await Convert(rows, supplierId);
            }
            else
            {
                isError = true;
            }

            return Json(new { error = isError, message = errorMessage, data = rows });
        }

        [HttpPost]
        public async Task<IActionResult> GetData(string supplierId)
        {
            var rows = await Task.FromResult(_conversionService._ConvertedData.FirstOrDefault(x => x.UserName == User.Identity.Name).ConvertedFile);
            rows = await Convert(rows, supplierId);
            return Json(new { error = false, message = "no errors", data = rows });
        }

        public async Task<List<EdiDataRow>> Convert(List<EdiDataRow> rows, string supplierId)
        {
            var conversionRows = await _db.TranslationRows.Where(x => x.SupplierId == supplierId).ToListAsync();
            if (conversionRows.Count > 0)
            {
                foreach (var row in rows)
                {
                    if (!row.IsConverted)
                    {
                        var convertionRow = conversionRows.FirstOrDefault(x => x.SupplierItemCode == row.EAN);
                        if (convertionRow != null)
                            _conversionService.Convert(
                                row.EAN, 
                                convertionRow.BuyerItemCode, 
                                convertionRow.Ratio, 
                                User.Identity.Name, 
                                convertionRow.BuyerItemDescription,
                                convertionRow.BuyerUnitOfMeasure);
                    }
                }
            }
            return rows;
        }

    }
}