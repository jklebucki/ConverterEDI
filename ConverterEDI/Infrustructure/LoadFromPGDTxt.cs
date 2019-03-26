﻿using ConverterEDI.Models;
using PGDTxt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConverterEDI.Infrustructure
{
    public class LoadFromPGDTxt
    {
        public bool isError { get; protected set; }
        public LoadFromPGDTxt()
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
                            PKWIUCode = item.Pkwiu,
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