using System.Collections.Generic;

namespace ConverterEDI.Models
{
    public class ConvertedData
    {
        public string ConversionCode { get; set; }
        public string UserName { get; set; }
        public List<EdiDataRow> ConvertedFile { get; set; }

    }
}
