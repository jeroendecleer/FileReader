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
            Dictionary<int, string> bestanden = new Dictionary<int, string>();
            bestanden.Add(0, "Exit");
            bestanden.Add(1, ".txt");
            bestanden.Add(2, ".xml");

            int keuzegetal = -1;
            int aantalbestanden = bestanden.Count;
            while (keuzegetal != 0) {
                foreach (KeyValuePair<int, string> entry in bestanden) {
                    Console.WriteLine("Voor " + entry.Value + " druk (" + entry.Key + ")");
                }

                string keuze = Console.ReadLine();

                bool success = Int32.TryParse(keuze, out keuzegetal);
                if (success && keuzegetal >= 0 && keuzegetal < aantalbestanden) {
                    string folder = "FileReader.files.tekst";
                    string bestand = folder + bestanden[keuzegetal];
                    Console.WriteLine(ReadFile(bestand));
                }
                else {
                    Console.WriteLine("Gelieve een geldig getal op te geven!");
                    keuzegetal = -1;
                }
            }
        }

        private static string ReadFile(string bestand) {
            try {
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(bestand)) {
                    using (StreamReader reader = new StreamReader(stream)) {
                        string result = reader.ReadToEnd();
                        return result;
                    }
                }
            }
            catch (Exception ex) {
                return ex.Message;
            }

        }
    }
}
