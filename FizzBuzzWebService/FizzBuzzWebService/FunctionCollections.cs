using System.IO;

namespace FizzBuzzWebService
{
    static class FunctionCollections
    {
        public static void WriteToAFile(string sequence)
        {
            string fileName = "C:\\Users\\capol_000\\Source\\Repos\\FizzBuzzWebService\\list-registry.txt";
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine(sequence);
            }
        }
    }
}