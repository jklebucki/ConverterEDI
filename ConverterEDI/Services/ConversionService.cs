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

        public bool ChangeStatus(string ean, string supplierId, string userName)
        {
            var import = _ConvertedData.FirstOrDefault(x => x.UserName == userName);
            var items = import.ConvertedFile;
            bool status = false;
            foreach (var item in items)
            {
                if (item.EAN == ean)
                {
                    item.IsConverted = true;
                    status = true;
                }

            }
            import.ConvertedFile = items;
            return status;
        }

        public bool Convert(string currentEan, string convertedEan, decimal conversionQuantity, string userName, string convertedProductName)
        {
            var import = _ConvertedData.FirstOrDefault(x => x.UserName == userName);
            var items = import.ConvertedFile;
            bool status = false;
            foreach (var item in items)
            {
                if (item.EAN == currentEan)
                {
                    item.EAN = convertedEan;
                    item.ProductName = convertedProductName;
                    item.Quantity = (decimal.Parse(item.Quantity) * conversionQuantity).ToString();
                    item.PurchasePrice = (decimal.Round(decimal.Parse(item.PurchasePrice) / conversionQuantity, 2)).ToString();
                    item.IsConverted = true;
                    status = true;
                }

            }
            import.ConvertedFile = items;
            return status;
        }
    }
}
