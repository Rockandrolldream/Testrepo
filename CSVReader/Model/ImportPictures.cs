using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVReader.Model
{
    public class ImportPictures
    {
        public void Import()
        {
            BallingdatabaseContext context = new BallingdatabaseContext();
            Cereal response = new Cereal();
            List<string> files2fromcsv = new List<string>();
            List<Cereal> cerealsfromdatabase = new List<Cereal>();
            List<Cereal> updatedcollection = new List<Cereal>();
            string[] files = new string[77];

            files = Directory.GetFiles(@"C:\Users\KOM\source\repos\Apiopgave\CSVReader\CerealOpgave");

            // create array 

            //foreach (var item in files)
            //{
            //   var output = item.Substring(item.IndexOf('/') + 1); 
            //    files2fromcsv.Add(output);
            //}


            foreach (var item in context.Cereals)
            {
                Console.WriteLine(item.Name + " " + item.Id); 
                cerealsfromdatabase.Add(item);
            }


            foreach (var item2 in cerealsfromdatabase)
            {

                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Contains(item2.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        item2.Picture = files[i];
                        updatedcollection.Add(item2);
                    }
                }
            }


            foreach (var item3 in updatedcollection)
            {
                context.Update(item3);
                context.SaveChanges();
            }

        }
    }
}
