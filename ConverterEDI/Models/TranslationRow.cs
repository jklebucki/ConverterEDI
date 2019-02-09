using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConverterEDI.Models
{
    [Table("TranslationRows")]
    public class TranslationRow
    {
        [Key]
        public int TranslationRowId { get; set; }
        public string SupplierId { get; set; }
        public string SupplierItemCode { get; set; }
        public string SupplierItemDescription { get; set; }
        public string BuyerItemCode { get; set; }
        public string BuyerItemDescription { get; set; }
        public decimal Ratio { get; set; }
        public string SupplierUnitOfMeasure { get; set; }
        public string BuyerUnitOfMeasure { get; set; }
    }
}