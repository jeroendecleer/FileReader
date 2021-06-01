using FileReader.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileReader.Models
{
    public class Xml: BestandTypes
    {
        public Xml() {
            Naam = "Xml";
            Extensie = ".xml";
        }

        public override string[] GetFiles(string[] files) {
            return files.Where(x => x.Contains(Extensie)).ToArray();
        }
    }
}
