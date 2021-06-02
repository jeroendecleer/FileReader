using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader.Models.Security
{
    public abstract class Rol
    {
        public string Naam { get; set; }
        public abstract string[] GetFiles(string[] files);
    }
}
