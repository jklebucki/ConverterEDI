using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static ProfastXML.Models.InputFileModel;

namespace ProfastXML.Services
{
    public class DeserializeServiceXml
    {
        public bool IsError { get; protected set; }
        public string ExeptionMessage { get; protected set; }

        public async Task<DOKUMENT> ImportStream(Stream fileStream)
        {
            DOKUMENT dokument = new DOKUMENT();
            try
            {
                using (Stream sr = fileStream)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(DOKUMENT));
                    dokument = (DOKUMENT)serializer.Deserialize(sr);
                }
                IsError = false;
            }
            catch (Exception ex)
            {
                IsError = true;
                ExeptionMessage = ex.Message;
            }
            return await Task.FromResult(dokument);
        }
    }
}