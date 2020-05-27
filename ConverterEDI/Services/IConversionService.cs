using ConverterEDI.Models;
using System.Collections.Generic;

namespace ConverterEDI.Services
{
    public interface IConversionService
    {
        List<ConvertedData> _ConvertedData { get; set; }
        bool Convert(string currentEan, string convertedEan, decimal conversionQuantity, string userName, string convertedProductName, string unit);
        bool ConvertBack(string currentEan, string userName);
    }
}
