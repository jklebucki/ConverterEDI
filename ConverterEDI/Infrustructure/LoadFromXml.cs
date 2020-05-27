using ConverterEDI.Models;
using System.Collections.Generic;
using static CocaColaToEDI.Models.InputFileModel;

public class LoadFromXml
{
    public bool isError { get; protected set; }
    public LoadFromXml()
    {
        isError = false;
    }
    public List<EdiDataRow> Load(DocumentInvoice _documentInvoice)
    {
        List<EdiDataRow> rows = new List<EdiDataRow>();

        try
        {
            if (_documentInvoice.InvoiceLines.Line.Count > 0)
            {
                foreach (var item in _documentInvoice.InvoiceLines.Line)
                {
                    rows.Add(new EdiDataRow
                    {
                        EAN = item.LineItem.EAN,
                        Quantity = item.LineItem.InvoiceQuantity.Replace(',', '.'),
                        PurchasePrice = item.LineItem.InvoiceUnitNetPrice.Replace(',', '.'),
                        ProductName = item.LineItem.ItemDescription,
                        VatRate = item.LineItem.TaxRate.Split('.')[0],
                        PKWIUCode = "",
                        Unit = item.LineItem.UnitOfMeasure,
                        ProductCode = "",
                        StationId = "",
                        SellingPrice = "",
                        IsConverted = false
                    });

                }
            }
        }
        catch
        {
            isError = true;
        }

        return rows;
    }
}