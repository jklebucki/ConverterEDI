using GalicjaTxt.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GalicjaTxt.Services
{
    public class DeserializeServiceGalicjaTxt
    {
        public bool IsError { get; protected set; }
        public string ExeptionMessage { get; protected set; }

        public DeserializeServiceGalicjaTxt()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public async Task<List<FlatRow>> ImportStream(Stream fileStream)
        {
            List<FlatRow> rows = new List<FlatRow>();
            try
            {
                using (StreamReader sr = new StreamReader(fileStream, Encoding.GetEncoding(1250)))
                {
                    while (sr.Peek() >= 0)
                    {
                        var row = sr.ReadLine();
                        if (row.Length > 0)
                        {
                            var splitedRow = row.Split(';');
                            rows.Add(new FlatRow
                            {
                                Ean = splitedRow[0],
                                ProductName = splitedRow[3],
                                Quantity = splitedRow[1],
                                PurchasePrice = splitedRow[2],
                                Unit = splitedRow[6],
                                VatRate = splitedRow[4],
                                Pkwiu = splitedRow[5]
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
