using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader.Models.Security
{
    public class Admin : Rol
    {
        public Admin() {
            Naam = "Admin";
        }

        public override string[] GetFiles(string[] files) {
            return files;
        }
    }
}
