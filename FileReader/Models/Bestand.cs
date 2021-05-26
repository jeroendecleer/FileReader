using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader.Models
{
    public abstract class Bestand
    {

        public string Filename { get; set; }
        public string Extensie { get; set; }
        public bool Encrypted { get; set; }
        public bool Rolbasedsecurity { get; set; }

        public virtual string Encrypt() {
            return "";
        }
    }
}
