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

        public bool Convert(string currentEan, string convertedEan, decimal conversionQuantity, string userName, string convertedProductName, string unit)
        {
            var import = _ConvertedData.FirstOrDefault(x => x.UserName == userName);
            var items = import.ConvertedFile;
            bool status = false;
            foreach (var item in items)
            {
                if (item.EAN == currentEan)
                {
                    item.OriginalProductName = item.ProductName;
                    item.OriginalQuantity = item.Quantity;
                    item.OriginalSellingPrice = item.SellingPrice;
                    item.OriginalUnit = item.Unit;
                    item.OriginalEAN = item.EAN;
                    item.EAN = convertedEan;
                    item.ProductName = convertedProductName;
                    item.Quantity = (decimal.Parse(item.Quantity) * conversionQuantity).ToString();
                    item.PurchasePrice = (decimal.Round(decimal.Parse(item.PurchasePrice) / conversionQuantity, 2)).ToString();
                    item.Unit = unit;
                    item.IsConverted = true;
                    status = true;
                }

            }
            import.ConvertedFile = items;
            return status;
        }
    }
}
