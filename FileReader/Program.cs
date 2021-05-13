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
            bool encrypted = false;
            bool rolbasedsecurity = false;

            Dictionary<int, string> bestanden = new Dictionary<int, string>();
            bestanden.Add(0, "Exit");
            bestanden.Add(1, ".txt");
            bestanden.Add(2, ".xml");
            bestanden.Add(3, ".json");

            int keuzebestandtype = -1;
            int keuzerol = -1;

            int aantalbestanden = bestanden.Count;
            while (keuzebestandtype != 0) {
                foreach (KeyValuePair<int, string> entry in bestanden) {
                    Console.WriteLine("Voor " + entry.Value + " druk (" + entry.Key + ")");
                }
                string keuze = Console.ReadLine();
                bool success = Int32.TryParse(keuze, out keuzebestandtype);

                Console.WriteLine("Wil je het bestand encrypteren? druk (j)");
                string encryptedinput = Console.ReadLine();

                if (encryptedinput.ToLower() == "j") {
                    encrypted = true;
                }
                else {
                    encrypted = false;
                }

                Console.WriteLine("Wil je rol based security gebruiken? druk (j)");
                string rolbasedinput = Console.ReadLine();

                if (rolbasedinput.ToLower() == "j") {
                    rolbasedsecurity = true;
                    Console.WriteLine("Selecteer je rol");

                    foreach (Rol rol in (Rol[])Enum.GetValues(typeof(Rol))) {
                        Console.WriteLine("Voor " + rol + " druk (" + (int)rol + ")");
                    }
                    keuzerol = Int32.Parse(Console.ReadLine());
                }
                else {
                    rolbasedsecurity = false;
                }

                if (success && keuzebestandtype >= 0 && keuzebestandtype < aantalbestanden) {
                    string[] files = Assembly.GetExecutingAssembly().GetManifestResourceNames();
                    string bestandtype = bestanden[keuzebestandtype];
                    files = files.Where(x => x.Contains(bestandtype)).ToArray();
                    if (rolbasedsecurity == true) {
                        if (keuzerol != (int)Rol.Admin) {
                            files = files.Where(x => x.Contains(Enum.GetName(typeof(Rol), keuzerol).ToLower())).ToArray();
                        }
                    }

                    for (int i = 0; i < files.Length; i++) {
                        Console.WriteLine("Voor inlezen " + files[i] + " druk (" + i + ")");
                    }

                    int keuzebestand = Int32.Parse(Console.ReadLine());
                    string bestand = files[keuzebestand];
                    Console.WriteLine(ReadFile(bestand, encrypted));
                }
                else {
                    Console.WriteLine("Gelieve een geldig getal op te geven!");
                    keuzebestandtype = -1;
                }
            }
        }
        private static string ReadFile(string bestand, bool encrypted) {
            try {
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(bestand)) {
                    using (StreamReader reader = new StreamReader(stream)) {
                        string result = reader.ReadToEnd();

                        if (encrypted == true) {
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

    public enum Rol
    {
        Admin = 1,
        Gebruiker = 2,
    }

}
