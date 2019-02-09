using ConverterEDI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConverterEDI.Services
{
    public interface IConversionService
    {
        List<ConvertedData> _ConvertedData { get; set; }
        bool Convert(string currentEan, string convertedEan, decimal conversionQuantity, string userName, string convertedProductName, string unit);
    }
}
