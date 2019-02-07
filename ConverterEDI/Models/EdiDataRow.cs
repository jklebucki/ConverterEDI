using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConverterEDI.Models
{
    public class EdiDataRow
    {
        public string EAN { get; set; }
        public string Quantity { get; set; }
        public string PurchasePrice { get; set; }
        public string ProductName { get; set; }
        public string VatRate { get; set; }
        public string PKWIUCode { get; set; }
        public string Unit { get; set; }
        public string ProductCode { get; set; }
        public string StationId { get; set; }
        public string SellingPrice { get; set; }
        public bool IsConverted { get; set; }
    }
}
