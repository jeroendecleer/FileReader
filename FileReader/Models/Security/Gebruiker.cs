using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileReader.Models.Security
{
    public class Gebruiker : Rol
    {
        public Gebruiker() {
            Naam = "Gebruiker";
        }

        public override string[] GetFiles(string[] files) {
            return files.Where(x => x.Contains(Naam)).ToArray();
        }
    }
}
