using CarrefourMagnat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CarrefourMagnat.Services
{
    public class DeserializeServiceMagnat
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
                        if (row.Length >= 7 && row.Substring(0, 7) == "%TOW|% ")
                        {
                            row = row.Replace("%TOW|% ", "");
                            var splitedRow = row.Split('|');
                            decimal price = 0.0M;
                            try
                            {
                                var x = splitedRow[5].Substring(1, splitedRow[5].Length - 1).Replace('.', ',');
                                var amount = decimal.Parse(x);
                                var quantity = decimal.Parse(splitedRow[2].Substring(1, splitedRow[2].Length - 1).Replace('.', ','));
                                price = decimal.Round((amount / quantity), 2);
                            }
                            catch
                            {
                                price = 0;
                            }
                            rows.Add(new FlatRow
                            {
                                Ean = splitedRow[0],
                                ProductName = splitedRow[4].Substring(1, splitedRow[4].Length - 1),
                                Quantity = splitedRow[2].Substring(1, splitedRow[2].Length - 1),
                                PurchasePrice = price.ToString().Replace(',', '.'),
                                Unit = splitedRow[10].Substring(1, splitedRow[10].Length - 1),
                                VatRate = splitedRow[9].Substring(1, splitedRow[9].Length - 1),
                                Pkwiu = splitedRow[12].Substring(1, splitedRow[12].Length - 1)
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
