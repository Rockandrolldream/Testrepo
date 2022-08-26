using CSVReader.@class;
using CSVReader.Model;
using System.Linq;

namespace CSVReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
           

            Readfileclass readfileclass = new Readfileclass();
            readfileclass.Read();

            ImportPictures importPictures = new ImportPictures();
            importPictures.Import();

        }
    }
}