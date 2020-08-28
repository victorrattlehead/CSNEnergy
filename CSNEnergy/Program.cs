using BLL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSNEnergy
{
    class Program
    {
        static void Main(string[] args)
        {
            var store = new Store();
            string catalogAsJson = string.Empty;

            using (StreamReader streamReader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "catalogFile.json")))
            {
                catalogAsJson = streamReader.ReadToEnd();
            }
            store.Import(catalogAsJson);
            Console.WriteLine(store.Buy("J.K Rowling - Goblet Of fire", "Isaac Asimov - Foundation"));
            //Console.WriteLine(store.Buy("Ayn Rand - FountainHead", "Robin Hobb - Assassin Apprentice", "Robin Hobb - Assassin Apprentice", "J.K Rowling - Goblet Of fire", "J.K Rowling - Goblet Of fire", "Isaac Asimov - Robot series", "Isaac Asimov - Foundation"));

        }
    }
}
//, "Isaac Asimov - Foundation"