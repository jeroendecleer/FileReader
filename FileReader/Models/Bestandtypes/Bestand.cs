using FileReader.Models.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FileReader.Models
{
    public abstract class BestandTypes
    {
        public string Naam { get; set; }
        public string Extensie { get; set; }
        public abstract string[] GetFiles(string[] files);
    }
}
