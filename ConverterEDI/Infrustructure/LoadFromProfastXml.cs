using System.Collections.Generic;
using ConverterEDI.Models;
using static CocaColaToEDI.Models.InputFileModel;
using static ProfastXML.Models.InputFileModel;

public class LoadFromProfastXml
{
    public bool isError { get; protected set; }
    public LoadFromProfastXml()
    {
        isError = false;
    }
    public List<EdiDataRow> Load(DOKUMENT dokument)
    {
        List<EdiDataRow> rows = new List<EdiDataRow>();

        try
        {
            if (dokument.POZYCJE.POZYCJA.Count > 0)
            {
                foreach (var item in dokument.POZYCJE.POZYCJA)
                {
                    rows.Add(new EdiDataRow
                    {
                        EAN = item.KOD_KRESKOWY,
                        Quantity = item.ILOSC.Replace(',', '.'),
                        PurchasePrice = item.CENA_NET.Replace(',', '.'),
                        ProductName = item.NAZWA,
                        VatRate = item.PROC_VAT.Split('.')[0],
                        PKWIUCode = "",
                        Unit = "",
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