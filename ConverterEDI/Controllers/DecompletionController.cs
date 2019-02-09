using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace ConverterEDI.Controllers
{
    [Authorize]
    public class DecompletionController : Controller
    {
        private IConversionService _conversionService { get; set; }
        private ApplicationDbContext _dbContext { get; set; }
        public DecompletionController(IConversionService conversionService, ApplicationDbContext dbContext)
        {
            _conversionService = conversionService;
            _dbContext = dbContext;
        }

        public IActionResult Index(string supplierId)
        {
            return View();
        }

        public async Task<IActionResult> ListWithSupplier(string supplierId)
        {
            var model = await _dbContext.TranslationRows.Where(x => x.SupplierId == supplierId).ToListAsync();
            ViewBag.Selection = true;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddConversion([Bind("TranslationRowId,SupplierId,SupplierItemCode,SupplierItemDescription,BuyerItemCode,BuyerItemDescription,Ratio,SupplierUnitOfMeasure,BuyerUnitOfMeasure")]TranslationRow translationRow)
        {
            if (!string.IsNullOrEmpty(translationRow.BuyerItemCode)
                && !string.IsNullOrEmpty(translationRow.BuyerItemDescription)
                && !string.IsNullOrEmpty(translationRow.BuyerUnitOfMeasure))
            {
                try
                {
                    await _dbContext.AddAsync(translationRow);
                    await _dbContext.SaveChangesAsync();
                    return Ok(new { formData = translationRow });
                }
                catch (DbUpdateException ex)
                {
                    return BadRequest(new { error = ex, formData = translationRow });
                }

            }

            return BadRequest(new { formData = translationRow });
        }

        [HttpPost]
        public async Task<IActionResult> GetRowData(string ean)
        {
            var rowData = await Task.FromResult(_conversionService._ConvertedData.FirstOrDefault(x => x.UserName == User.Identity.Name).ConvertedFile.FirstOrDefault(f => f.EAN == ean));

            return Ok(new { data = rowData });
        }
    }
}