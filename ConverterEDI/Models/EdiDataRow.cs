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
        public string OriginalProductName { get; set; }
        public string OriginalQuantity { get; set; }
        public string OriginalUnit { get; set; }
        public string OriginalPurchasePrice { get; set; }
        public string OriginalEAN { get; set; }
    }
}
