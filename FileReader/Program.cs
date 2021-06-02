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

                keuzebestandtype = AskBestandType(bestandstypes);
                input.BestandType = bestandstypes[keuzebestandtype];
                input.Encrypted = AskJaNee("Wil je het bestand encrypteren?");
                input.RolbasedSecurity = AskJaNee("Wil je rol based security gebruiken ?");

                if (input.RolbasedSecurity == true) {
                    input.Rol = AskRol(rollen);
                }
                else {
                    input.RolbasedSecurity = false;
                    input.Rol = null;
                }

                string[] files = Assembly.GetExecutingAssembly().GetManifestResourceNames();
                files = input.BestandType.GetFiles(files);
                if (input.Rol != null) {
                    files = input.Rol.GetFiles(files);
                }
                for (int i = 0; i < files.Length; i++) {
                    Console.WriteLine("Voor inlezen " + files[i] + " druk (" + i + ")");
                }

                string filekeuze = Console.ReadLine();
                Int32.TryParse(filekeuze, out keuzefile);
                input.GekozenBestand = files[keuzefile];
                Console.WriteLine(ReadFile(input));

            }
        }

        private static int AskBestandType(List<BestandTypes> bestandstypes) {
            int keuzebestandtype = -1;
            bool success = false;

            while (success == false) {
                for (int i = 0; i < bestandstypes.Count(); i++) {
                    Console.WriteLine("Voor " + bestandstypes[i].Naam + " druk (" + (i) + ")");
                }
                Console.WriteLine("Voor Exit druk (" + bestandstypes.Count() + ")");

                string keuze = Console.ReadLine();
                success = Int32.TryParse(keuze, out keuzebestandtype);

                if (success == true && (keuzebestandtype < 0 || keuzebestandtype > bestandstypes.Count())) {
                    success = false;
                }

                if (keuzebestandtype == bestandstypes.Count()) {
                    Environment.Exit(0);
                }
            }

            return keuzebestandtype;
        }
        private static Rol AskRol(List<Rol> rollen) {
            bool success = false;
            int keuzerolgetal = -1;
            while (success == false) {
                Console.WriteLine("Selecteer je rol");

                for (int i = 0; i < rollen.Count(); i++) {
                    Console.WriteLine("Voor " + rollen[i].Naam + " druk (" + i + ")");
                }
                string keuzerol = Console.ReadLine();
                success = Int32.TryParse(keuzerol, out keuzerolgetal);

                if (success == true && (keuzerolgetal < 0 || keuzerolgetal >= rollen.Count())) {
                    success = false;
                }
                else if (success == true && keuzerolgetal >= 0 && keuzerolgetal < rollen.Count()) {
                    return rollen[keuzerolgetal];
                }
            }
            return null;
        }

        private static bool AskJaNee(string vraag) {
            string input = "";
            while (input != "j") {
                Console.WriteLine(vraag + " druk (j) of (n)");
                input = Console.ReadLine();

                if (input.ToLower() == "j") {
                    return true;
                }
                else if (input.ToLower() == "n") {
                    return false;
                }
            }
            return false;
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
