using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware
{
    /// <summary>
    /// author: zk
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Name des Fortbildungsdokuments
        /// </summary>
        public String DocumentName { get; set; }
        public DateTime Date { get; set; }

        /// <summary>
        /// zeigt an, ob das Dokument verpflichtend abzugeben ist
        /// </summary>
        public bool Required { get; set; }
        public String FilePath { get; set; }

        public Document(String name,DateTime date, bool required, String filePath)
        {
            this.DocumentName = name;
            this.Date = date;
            this.Required = required;
            this.FilePath = filePath;
        }

        public Document()
        {
            
        }

    }
}
