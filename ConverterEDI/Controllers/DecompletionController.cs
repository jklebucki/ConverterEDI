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

namespace ConverterEDI.Controllers
{
    public class DecompletionController
    {
        private IConversionService _conversionService { get; set; }
        private ApplicationDbContext _dbContext { get; set; }
        public DecompletionController(IConversionService conversionService, ApplicationDbContext dbContext)
        {
            _conversionService = conversionService;
            _dbContext = dbContext;
        }
    }
}