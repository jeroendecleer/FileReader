using FileReader.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileReader.Models
{
    public class Tekst : BestandTypes
    {
        public Tekst() {
            Naam = "Tekst";
            Extensie = ".txt";
        }

        public override string[] GetFiles(string[] files) {
            return files.Where(x => x.Contains(Extensie)).ToArray();
        }
    }
}
