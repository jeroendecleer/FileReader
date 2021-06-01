using FileReader.Models.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader.Models
{
    public class Input {
        public Input() {
            Encrypted = false;
            RolbasedSecurity = false;
        }

        public BestandTypes BestandType {get; set;}
        public bool Encrypted { get; set; }
        public bool RolbasedSecurity { get; set; }
        public string GekozenBestand { get; set; }
        public Rol Rol { get; set; }
    }
}
