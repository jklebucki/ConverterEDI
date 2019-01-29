using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConverterEDI.Models;

namespace ConverterEDI.Services
{
    public class ConversionService : IConversionService
    {
        public List<ConvertedData> _ConvertedData { get; set; }

        public ConversionService()
        {
            _ConvertedData = new List<ConvertedData>();
        }
    }
}
