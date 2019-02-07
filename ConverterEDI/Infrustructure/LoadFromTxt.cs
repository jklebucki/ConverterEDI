using System.Collections.Generic;
using CocaColaTxtEDI.Models;
using ConverterEDI.Models;

namespace ConverterEDI.Infrustructure
{
    public class LoadFromTxt
    {
        public bool isError { get; protected set; }
        public LoadFromTxt()
        {
            isError = false;
        }
        public List<EdiDataRow> Load(List<FlatRow> inputRows)
        {
            List<EdiDataRow> rows = new List<EdiDataRow>();

            try
            {
                if (inputRows.Count > 0)
                {
                    foreach (var item in inputRows)
                    {
                        rows.Add(new EdiDataRow
                        {
                            EAN = item.Ean,
                            Quantity = item.Quantity.Replace(',', '.'),
                            PurchasePrice = item.PurchasePrice.Replace(',', '.'),
                            ProductName = item.ProductName,
                            VatRate = item.VatRate.Split('.')[0],
                            PKWIUCode = "",
                            Unit = item.Unit,
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
}