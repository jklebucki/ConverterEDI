using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CocaColaTxtEDI.Models;

namespace CocaColaTxtEDI.Services
{
    public class DeserializeServiceTxt
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
                        var row = sr.ReadLine().Split(':');
                        rows.Add(new FlatRow
                        {
                            Field1 = row[0],
                            Field2 = row[1],
                            Ean = row[2],
                            Field4 = row[3],
                            ProductName = row[4],
                            Field6 = row[5],
                            PurchasePrice = row[6],
                            Field8 = row[7],
                            Unit = row[8],
                            Quantity = row[9],
                            VatRate = row[10]
                        });
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