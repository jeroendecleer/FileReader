using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader.Models
{
    public class Xml: Bestand
    {
        public Xml(string naam) {
            Filename = naam;
            Extensie = ".xml";
        }
    }
}
