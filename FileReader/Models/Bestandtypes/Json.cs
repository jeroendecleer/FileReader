using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileReader.Models
{
    public class Json : BestandTypes
    {
        public Json() {
            Naam = "Json";
            Extensie = ".json";
        }

        public override string[] GetFiles(string[] files) {
            return files.Where(x => x.Contains(Extensie)).ToArray();
        }
    }
}
