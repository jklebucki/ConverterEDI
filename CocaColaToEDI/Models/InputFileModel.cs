using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace CocaColaToEDI.Models
{
    public class InputFileModel
    {
        [XmlRoot(ElementName = "Delivery")]
        public class Delivery
        {
            [XmlElement(ElementName = "DeliveryDate")]
            public string DeliveryDate { get; set; }
            [XmlElement(ElementName = "DespatchNumber")]
            public string DespatchNumber { get; set; }
            [XmlElement(ElementName = "DespatchDate")]
            public string DespatchDate { get; set; }
        }

        [XmlRoot(ElementName = "Invoice-Header")]
        public class InvoiceHeader
        {
            [XmlElement(ElementName = "InvoiceNumber")]
            public string InvoiceNumber { get; set; }
            [XmlElement(ElementName = "InvoiceDate")]
            public string InvoiceDate { get; set; }
            [XmlElement(ElementName = "SalesDate")]
            public string SalesDate { get; set; }
            [XmlElement(ElementName = "InvoiceCurrency")]
            public string InvoiceCurrency { get; set; }
            [XmlElement(ElementName = "InvoicePaymentDueDate")]
            public string InvoicePaymentDueDate { get; set; }
            [XmlElement(ElementName = "InvoicePaymentTerms")]
            public string InvoicePaymentTerms { get; set; }
            [XmlElement(ElementName = "AccountNumber")]
            public string AccountNumber { get; set; }
            [XmlElement(ElementName = "Delivery")]
            public Delivery Delivery { get; set; }
        }

        [XmlRoot(ElementName = "Buyer")]
        public class Buyer
        {
            [XmlElement(ElementName = "Name")]
            public string Name { get; set; }
            [XmlElement(ElementName = "StreetAndNumber")]
            public string StreetAndNumber { get; set; }
            [XmlElement(ElementName = "CityName")]
            public string CityName { get; set; }
            [XmlElement(ElementName = "PostalCode")]
            public string PostalCode { get; set; }
            [XmlElement(ElementName = "TaxID")]
            public string TaxID { get; set; }
        }

        [XmlRoot(ElementName = "Payer")]
        public class Payer
        {
            [XmlElement(ElementName = "Name")]
            public string Name { get; set; }
            [XmlElement(ElementName = "StreetAndNumber")]
            public string StreetAndNumber { get; set; }
            [XmlElement(ElementName = "CityName")]
            public string CityName { get; set; }
            [XmlElement(ElementName = "PostalCode")]
            public string PostalCode { get; set; }
            [XmlElement(ElementName = "TaxID")]
            public string TaxID { get; set; }
        }

        [XmlRoot(ElementName = "Seller")]
        public class Seller
        {
            [XmlElement(ElementName = "Name")]
            public string Name { get; set; }
            [XmlElement(ElementName = "StreetAndNumber")]
            public string StreetAndNumber { get; set; }
            [XmlElement(ElementName = "PostalCode")]
            public string PostalCode { get; set; }
            [XmlElement(ElementName = "TaxID")]
            public string TaxID { get; set; }
        }

        [XmlRoot(ElementName = "Receiver")]
        public class Receiver
        {
            [XmlElement(ElementName = "ILN")]
            public string ILN { get; set; }
            [XmlElement(ElementName = "Name")]
            public string Name { get; set; }
            [XmlElement(ElementName = "StreetAndNumber")]
            public string StreetAndNumber { get; set; }
            [XmlElement(ElementName = "PostalCode")]
            public string PostalCode { get; set; }
        }

        [XmlRoot(ElementName = "Invoice-Parties")]
        public class InvoiceParties
        {
            [XmlElement(ElementName = "Buyer")]
            public Buyer Buyer { get; set; }
            [XmlElement(ElementName = "Payer")]
            public Payer Payer { get; set; }
            [XmlElement(ElementName = "Seller")]
            public Seller Seller { get; set; }
            [XmlElement(ElementName = "Receiver")]
            public Receiver Receiver { get; set; }
        }

        [XmlRoot(ElementName = "Line-Delivery")]
        public class LineDelivery
        {
            [XmlElement(ElementName = "DespatchNumber")]
            public string DespatchNumber { get; set; }
            [XmlElement(ElementName = "OrderLineNumber")]
            public string OrderLineNumber { get; set; }
        }

        [XmlRoot(ElementName = "Line-Item")]
        public class LineItem
        {
            [XmlElement(ElementName = "LineNumber")]
            public string LineNumber { get; set; }
            [XmlElement(ElementName = "EAN")]
            public string EAN { get; set; }
            [XmlElement(ElementName = "BuyerItemCode")]
            public string BuyerItemCode { get; set; }
            [XmlElement(ElementName = "SupplierItemCode")]
            public string SupplierItemCode { get; set; }
            [XmlElement(ElementName = "ItemDescription")]
            public string ItemDescription { get; set; }
            [XmlElement(ElementName = "InvoiceQuantity")]
            public string InvoiceQuantity { get; set; }
            [XmlElement(ElementName = "UnitOfMeasure")]
            public string UnitOfMeasure { get; set; }
            [XmlElement(ElementName = "InvoiceUnitNetPrice")]
            public string InvoiceUnitNetPrice { get; set; }
            [XmlElement(ElementName = "TaxRate")]
            public string TaxRate { get; set; }
            [XmlElement(ElementName = "TaxCategoryCode")]
            public string TaxCategoryCode { get; set; }
            [XmlElement(ElementName = "TaxAmount")]
            public string TaxAmount { get; set; }
            [XmlElement(ElementName = "NetAmount")]
            public string NetAmount { get; set; }
        }

        [XmlRoot(ElementName = "Line")]
        public class Line
        {
            [XmlElement(ElementName = "Line-Item")]
            public LineItem LineItem { get; set; }
            [XmlElement(ElementName = "Line-Allowances")]
            public string LineAllowances { get; set; }
        }

        [XmlRoot(ElementName = "Invoice-Lines")]
        public class InvoiceLines
        {
            [XmlElement(ElementName = "Line")]
            public List<Line> Line { get; set; }
        }

        [XmlRoot(ElementName = "Tax-Summary-Line")]
        public class TaxSummaryLine
        {
            [XmlElement(ElementName = "TaxRate")]
            public string TaxRate { get; set; }
            [XmlElement(ElementName = "TaxCategoryCode")]
            public string TaxCategoryCode { get; set; }
            [XmlElement(ElementName = "TaxAmount")]
            public string TaxAmount { get; set; }
            [XmlElement(ElementName = "TaxableBasis")]
            public string TaxableBasis { get; set; }
            [XmlElement(ElementName = "TaxableAmount")]
            public string TaxableAmount { get; set; }
            [XmlElement(ElementName = "GrossAmount")]
            public string GrossAmount { get; set; }
        }

        [XmlRoot(ElementName = "Tax-Summary")]
        public class TaxSummary
        {
            [XmlElement(ElementName = "Tax-Summary-Line")]
            public List<TaxSummaryLine> TaxSummaryLine { get; set; }
        }

        [XmlRoot(ElementName = "Deposit-Summary")]
        public class DepositSummary
        {
            [XmlElement(ElementName = "DepositAmount")]
            public string DepositAmount { get; set; }
            [XmlElement(ElementName = "CorrectionTotalGrossAmount")]
            public string CorrectionTotalGrossAmount { get; set; }
        }

        [XmlRoot(ElementName = "Invoice-Summary")]
        public class InvoiceSummary
        {
            [XmlElement(ElementName = "TotalLines")]
            public string TotalLines { get; set; }
            [XmlElement(ElementName = "TotalNetAmount")]
            public string TotalNetAmount { get; set; }
            [XmlElement(ElementName = "TotalTaxableBasis")]
            public string TotalTaxableBasis { get; set; }
            [XmlElement(ElementName = "TaxAmount")]
            public string TaxAmount { get; set; }
            [XmlElement(ElementName = "TotalTaxAmount")]
            public string TotalTaxAmount { get; set; }
            [XmlElement(ElementName = "TotalGrossAmount")]
            public string TotalGrossAmount { get; set; }
            [XmlElement(ElementName = "Tax-Summary")]
            public TaxSummary TaxSummary { get; set; }
            [XmlElement(ElementName = "Deposit-Summary")]
            public DepositSummary DepositSummary { get; set; }
        }

        [XmlRoot(ElementName = "Document-Invoice")]
        public class DocumentInvoice
        {
            [XmlElement(ElementName = "Invoice-Header")]
            public InvoiceHeader InvoiceHeader { get; set; }
            [XmlElement(ElementName = "Invoice-Parties")]
            public InvoiceParties InvoiceParties { get; set; }
            [XmlElement(ElementName = "Line-Delivery")]
            public LineDelivery LineDelivery { get; set; }
            [XmlElement(ElementName = "Invoice-Lines")]
            public InvoiceLines InvoiceLines { get; set; }
            [XmlElement(ElementName = "Invoice-Summary")]
            public InvoiceSummary InvoiceSummary { get; set; }
        }

    }
}
