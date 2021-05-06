using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace FileReader
{
    class Program
    {
        static void Main(string[] args) {
            bool encrypted = false;
            bool adminrole = false;

            Dictionary<int, string> bestanden = new Dictionary<int, string>();
            bestanden.Add(0, "Exit");
            bestanden.Add(1, "tekst.txt");
            bestanden.Add(2, "tekst.xml");

            int keuzegetal = -1;
            int aantalbestanden = bestanden.Count;
            while (keuzegetal != 0) {
                foreach (KeyValuePair<int, string> entry in bestanden) {
                    Console.WriteLine("Voor " + entry.Value + " druk (" + entry.Key + ")");
                }

                string keuze = Console.ReadLine();

                Console.WriteLine("Wil je het bestand encrypteren? druk (j)");
                string encryptedinput = Console.ReadLine();

                if (encryptedinput.ToLower() == "j") {
                    encrypted = true;
                }
                else {
                    encrypted = false;
                }

                bool success = Int32.TryParse(keuze, out keuzegetal);
                if (success && keuzegetal >= 0 && keuzegetal < aantalbestanden) {
                    string folder = "FileReader.files.";
                    string bestand = folder + bestanden[keuzegetal];
                    Console.WriteLine(ReadFile(bestand, keuzegetal, encrypted));
                }
                else {
                    Console.WriteLine("Gelieve een geldig getal op te geven!");
                    keuzegetal = -1;
                }
            }
        }
        private static string ReadFile(string bestand, int keuzegetal, bool encrypted) {
            try {
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(bestand)) {
                    using (StreamReader reader = new StreamReader(stream)) {
                        string result = reader.ReadToEnd();

                        if(encrypted == true) {
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
