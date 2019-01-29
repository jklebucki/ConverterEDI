using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static CocaColaToEDI.Models.InputFileModel;

namespace CocaColaToEDI.Services
{
    public class DeserializeService
    {
        public bool IsError { get; protected set; }
        public string ExeptionMessage { get; protected set; }

        public async Task<DocumentInvoice> ImportStream(Stream fileStream)
        {
            DocumentInvoice _documentInvoice = new DocumentInvoice();
            try
            {
                using (Stream sr = fileStream)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(DocumentInvoice));
                    _documentInvoice = (DocumentInvoice)serializer.Deserialize(sr);
                }
                IsError = false;
            }
            catch (Exception ex)
            {
                IsError = true;
                ExeptionMessage = ex.Message;
            }
            return await Task.FromResult(_documentInvoice);
        }
    }
}
