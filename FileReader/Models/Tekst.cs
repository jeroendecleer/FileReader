using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader.Models
{
    public class Tekst : Bestand
    {
        public Tekst(string naam) {
            Filename = naam;
            Extensie = ".txt";
        }
        public override string Encrypt() {
            return "";
        }
    }
}
