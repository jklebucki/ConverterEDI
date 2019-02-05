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
    public class DecompletionController : Controller
    {
        private IConversionService _conversionService { get; set; }
        private ApplicationDbContext _dbContext { get; set; }
        public DecompletionController(IConversionService conversionService, ApplicationDbContext dbContext)
        {
            _conversionService = conversionService;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index(string supplierId)
        {
            var model = await _dbContext.TranslationRows.Where(x => x.SupplierId == supplierId).ToListAsync();
            return View(model);
        }
    }
}