using System.Collections.Generic;
using System.Xml.Serialization;

namespace ProfastXML.Models
{
    public class InputFileModel
    {
        [XmlRoot(ElementName="NAGLOWEK")]	public class NAGLOWEK {
		[XmlElement(ElementName="NR_DOKUMENTU")]
		public string NR_DOKUMENTU { get; set; }
		[XmlElement(ElementName="DATA_DOKUMENTU")]
		public string DATA_DOKUMENTU { get; set; }
		[XmlElement(ElementName="KOD_DOSTAWCY")]
		public string KOD_DOSTAWCY { get; set; }
		[XmlElement(ElementName="KOD_ODBIORCY")]
		public string KOD_ODBIORCY { get; set; }
		[XmlElement(ElementName="ILOSC_POZYCJI")]
		public string ILOSC_POZYCJI { get; set; }
		[XmlElement(ElementName="WART_NET")]
		public string WART_NET { get; set; }
		[XmlElement(ElementName="NIP_DOSTAWCY")]
		public string NIP_DOSTAWCY { get; set; }
	}
	[XmlRoot(ElementName="POZYCJA")]
	public class POZYCJA {
		[XmlElement(ElementName="OPERACJA")]
		public string OPERACJA { get; set; }
		[XmlElement(ElementName="LP")]
		public string LP { get; set; }
		[XmlElement(ElementName="NAZWA")]
		public string NAZWA { get; set; }
		[XmlElement(ElementName="KOD_KRESKOWY")]
		public string KOD_KRESKOWY { get; set; }
		[XmlElement(ElementName="CENA_NET")]
		public string CENA_NET { get; set; }
		[XmlElement(ElementName="PROC_RABAT")]
		public string PROC_RABAT { get; set; }
		[XmlElement(ElementName="PROC_VAT")]
		public string PROC_VAT { get; set; }
		[XmlElement(ElementName="PKWiU")]
		public string PKWiU { get; set; }
		[XmlElement(ElementName="ILOSC")]
		public string ILOSC { get; set; }
	}

	[XmlRoot(ElementName="POZYCJE")]
	public class POZYCJE {
		[XmlElement(ElementName="POZYCJA")]
		public List<POZYCJA> POZYCJA { get; set; }
	}

	[XmlRoot(ElementName="DOKUMENT")]
	public class DOKUMENT {
		[XmlElement(ElementName="NAGLOWEK")]
		public NAGLOWEK NAGLOWEK { get; set; }
		[XmlElement(ElementName="POZYCJE")]
		public POZYCJE POZYCJE { get; set; }
	}
    }
}