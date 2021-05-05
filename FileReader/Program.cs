using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace FileReader
{
    class Program
    {
        static void Main(string[] args) {
            string bestand = "FileReader.files.tekst.txt";
            
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(bestand)) {
                using (StreamReader reader = new StreamReader(stream)) {
                    string result = reader.ReadToEnd();
                    Console.WriteLine(result);
                }
            }
        }
    }
}
