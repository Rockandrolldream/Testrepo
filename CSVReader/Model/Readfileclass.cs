using CSVReader.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVReader.@class
{
    public class Readfileclass
    {
        public void Read()
        {
            List<string> name = new List<string>();
            List<string> mfr = new List<string>();
            List<string> type = new List<string>();
            List<string> calories = new List<string>();
            List<string> protein = new List<string>();
            List<string> fat = new List<string>();
            List<string> sodium = new List<string>();
            List<string> fiber = new List<string>();
            List<string> carbo = new List<string>();
            List<string> sugars = new List<string>();
            List<string> potass = new List<string>();
            List<string> vitamins = new List<string>();
            List<string> shelf = new List<string>();
            List<string> weight = new List<string>();
            List<string> cups = new List<string>();
            List<string> rating = new List<string>();
            List<int> ratingint = new List<int>();
            List<Cereal> cereals = new List<Cereal>();
            List<Cereal> cerealsfromdatabase = new List<Cereal>();
            BallingdatabaseContext ballingdatabaseContext = new BallingdatabaseContext();

            using (var reader = new StreamReader(@"C:\Users\KOM\source\repos\Apiopgave\CSVReader\Csvfile\cereal_unclean.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    name.Add(values[0]);
                    mfr.Add(values[1]);
                    type.Add(values[2]);
                    calories.Add(values[3]);
                    protein.Add(values[4]);
                    fat.Add(values[5]);
                    sodium.Add(values[6]);
                    fiber.Add(values[7]);
                    carbo.Add(values[8]);
                    sugars.Add(values[9]);
                    potass.Add(values[10]);
                    vitamins.Add(values[11]);
                    shelf.Add(values[12]);
                    weight.Add(values[13]);
                    cups.Add(values[14]);
                    rating.Add(values[15]);

                }
            }

            name.RemoveAt(0);
            name.RemoveAt(0);
            mfr.RemoveAt(0);
            mfr.RemoveAt(0);
            type.RemoveAt(0);
            type.RemoveAt(0);
            calories.RemoveAt(0);
            calories.RemoveAt(0);
            protein.RemoveAt(0);
            protein.RemoveAt(0);
            fat.RemoveAt(0);
            fat.RemoveAt(0);
            sodium.RemoveAt(0);
            sodium.RemoveAt(0);
            fiber.RemoveAt(0);
            fiber.RemoveAt(0);
            carbo.RemoveAt(0);
            carbo.RemoveAt(0);
            sugars.RemoveAt(0);
            sugars.RemoveAt(0);
            potass.RemoveAt(0);
            potass.RemoveAt(0);
            vitamins.RemoveAt(0);
            vitamins.RemoveAt(0);
            shelf.RemoveAt(0);
            shelf.RemoveAt(0);
            weight.RemoveAt(0);
            weight.RemoveAt(0);
            cups.RemoveAt(0);
            cups.RemoveAt(0);
            rating.RemoveAt(0);
            rating.RemoveAt(0);

            foreach (var item in rating)
            {
              var newfloat = item.Replace(".", "");
              var output = int.Parse(newfloat);
                ratingint.Add(output);
            }

             List<int> listintcalories = calories.Select(s => int.Parse(s)).ToList();
            List<int> listintprotein = protein.Select(s => int.Parse(s)).ToList();
            List<int> listintfat = fat.Select(s => int.Parse(s)).ToList();
            List<int> listintsodium = sodium.Select(s => int.Parse(s)).ToList();
            List<Double> listdoublefibers = fiber.Select(s => double.Parse(s)).ToList();
            List<Double> listdoublecarbo = carbo.Select(s => Double.Parse(s)).ToList();
            List<int> listintsugar = sugars.Select(s => int.Parse(s)).ToList();
            List<int> listintpotass = potass.Select(s => int.Parse(s)).ToList();
            List<int> listintvitamins = vitamins.Select(s => int.Parse(s)).ToList();
            List<int> listintshelf = shelf.Select(s => int.Parse(s)).ToList();
            List<Double> listdoubleweight = weight.Select(s => double.Parse(s)).ToList();
            List<Double> listdoublecups = cups.Select(s => Double.Parse(s)).ToList();

            for (int i = 0; i < listintcalories.Count; i++)
            {
                Cereal cereal = new Cereal() { Name = name[i], Mfr = mfr[i], Type = type[i], Calories = listintcalories[i], Protein = listintprotein[i], Fat = listintfat[i], Sodium = listintsodium[i], Fiber = listdoublefibers[i], Carbo = listdoublecarbo[i], Sugars = listintsugar[i], Potass = listintpotass[i], Vitamins = listintvitamins[i], Shelf = listintshelf[i], Weight = listdoubleweight[i], Cups = listdoublecups[i], Rating = ratingint[i] };

                cereals.Add(cereal);

            }



            foreach (var item in ballingdatabaseContext.Cereals)
            {
                ballingdatabaseContext.Cereals.Remove(item);

            }
            ballingdatabaseContext.SaveChanges();
            foreach (var item in cereals)
            {
                ballingdatabaseContext.Add(item);

            }
            ballingdatabaseContext.SaveChanges();
        }
    }
}
