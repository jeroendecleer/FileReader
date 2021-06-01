using FileReader.Models;
using FileReader.Models.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace FileReader
{
    class Program
    {
        static void Main(string[] args) {
            var input = new Input();

            List<BestandTypes> bestandstypes = new List<BestandTypes>();
            bestandstypes.Add(new Tekst());
            bestandstypes.Add(new Xml());
            bestandstypes.Add(new Json());

            List<Rol> rollen = new List<Rol>();
            rollen.Add(new Admin());
            rollen.Add(new Gebruiker());

            int keuzefile = -1;
            int keuzebestandtype = -1;
            while (keuzebestandtype != 0) {

                //keuzebestandtype = AskBestandType(bestandstypes);
                Console.WriteLine("Voor Exit druk (0");

                for (int i = 0; i < bestandstypes.Count(); i++) {
                    Console.WriteLine("Voor " + bestandstypes[i].Naam + " druk (" + (i + 1) + ")");
                }

                string keuze = Console.ReadLine();
                var success = Int32.TryParse(keuze, out keuzebestandtype);

                input.BestandType = bestandstypes[keuzebestandtype - 1];

                Console.WriteLine("Wil je het bestand encrypteren? druk (j)");
                string encryptedinput = Console.ReadLine();

                if (encryptedinput.ToLower() == "j") {
                    input.Encrypted = true;
                }
                else {
                    input.Encrypted = false;
                }

                Console.WriteLine("Wil je rol based security gebruiken? druk (j)");
                string rolbasedinput = Console.ReadLine();

                if (rolbasedinput.ToLower() == "j") {
                    input.RolbasedSecurity = true;
                    Console.WriteLine("Selecteer je rol");

                    for (int i = 0; i < rollen.Count(); i++) {
                        Console.WriteLine("Voor " + rollen[i].Naam + " druk (" + i + ")");
                    }
                    int keuzerol = Int32.Parse(Console.ReadLine());
                    input.Rol = rollen[keuzerol];
                }
                else {
                    input.RolbasedSecurity = false;
                }

                if (success == true && keuzebestandtype > 0 && keuzebestandtype <= bestandstypes.Count()) {
                    string[] files = Assembly.GetExecutingAssembly().GetManifestResourceNames();
                    files = input.BestandType.GetFiles(files);
                    //TODO als er geen rol geselecteerd is is er nog een error
                    files = input.Rol.GetFiles(files);
                    for (int i = 0; i < files.Length; i++) {
                        Console.WriteLine("Voor inlezen " + files[i] + " druk (" + i + ")");
                    }

                    string filekeuze = Console.ReadLine();
                    Int32.TryParse(filekeuze, out keuzefile);
                    input.GekozenBestand = files[keuzefile];
                    Console.WriteLine(ReadFile(input));
                }
                else {
                    Console.WriteLine("Gelieve een geldig getal op te geven!");
                    if (keuzebestandtype != 0) {
                        keuzebestandtype = -1;
                    }
                }
            }
        }

        public static int AskBestandType(List<BestandTypes> bestandstypes) {
            int keuzebestandtype = -1;
            bool success = false;

            while(success == false) {
                Console.WriteLine("Voor Exit druk (0");

                for (int i = 0; i < bestandstypes.Count(); i++) {
                    Console.WriteLine("Voor " + bestandstypes[i].Naam + " druk (" + (i + 1) + ")");
                }
                //Console.WriteLine("Voor Exit druk " + (i + 1));

                string keuze = Console.ReadLine();
                //TODO when parse fails is automatically sets out to zero
                success = Int32.TryParse(keuze, out keuzebestandtype);

                if (keuzebestandtype == 0) {
                    Environment.Exit(0);
                }
            }

            

            return keuzebestandtype;
        }

        private static string ReadFile(Input input) {
            try {
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(input.GekozenBestand)) {
                    using (StreamReader reader = new StreamReader(stream)) {
                        string result = reader.ReadToEnd();

                        if (input.Encrypted == true) {
                            result = Reverse(result);
                        }
                        return result;
                    }
                }
            }
            catch (Exception ex) {
                return ex.Message;
            }
        }

        public static string Reverse(string s) {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
