using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SBenReady.Models;

namespace SBenReady.Services
{
    public class DeserializeServiceSbenReady
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