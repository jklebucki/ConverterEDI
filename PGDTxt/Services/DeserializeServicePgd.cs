using PGDTxt.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PGDTxt.Services
{
    public class DeserializeServicePgd
    {
        public bool IsError { get; protected set; }
        public string ExeptionMessage { get; protected set; }

        public async Task<List<FlatRow>> ImportStream(Stream fileStream)
        {
            List<FlatRow> rows = new List<FlatRow>();
            try
            {
                using (StreamReader sr = new StreamReader(fileStream))
                {
                    while (sr.Peek() >= 0)
                    {
                        var row = sr.ReadLine();
                        if (row.Contains("Linia:"))
                        {
                            row = row.Replace("Linia:", "");
                            var splitedRow = row.Split('}');
                            rows.Add(new FlatRow
                            {
                                Ean = splitedRow[1].Replace("Kod{", ""),
                                ProductName = splitedRow[0].Replace("Nazwa{", ""),
                                Quantity = splitedRow[7].Replace("Ilosc{", ""),
                                PurchasePrice = splitedRow[8].Replace("Cena{n", "").Replace(',', '.'),
                                Unit = splitedRow[3].Replace("Jm{", ""),
                                VatRate = splitedRow[3].Replace("Vat{", ""),
                                Pkwiu = splitedRow[6].Replace("PKWIU{", ""),
                            });
                        }

                    }
                }
                IsError = false;
            }
            catch (Exception ex)
            {
                IsError = true;
                ExeptionMessage = ex.Message;
            }
            return await Task.FromResult(rows);
        }
    }
}
