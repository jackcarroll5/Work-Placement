using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherApp
{
   public class NameService
    {
        private readonly List<string> nameData = new List<string>
        {
          "Anthony","Brian","Cillian","Dave","Edward","Frank","Gary","Harry","Jack",
          "Kevin","Lebron","Michael","Nathan","Peter","Robbie","Sam","Tom","William",
          "Fred","Marcus","Kyle","Chris","Ariana","Billie","Ciara","Danielle","Emer",
          "Francesca","Gabriella","Jessica","Kelly","Lara","Millie","Nadine","Pauline","Rachel",
          "Sarah","Tiffany","Victoria","Stephanie","Lauren","Karen","Jade","Eliza","Georgia"
        };

    public async Task<List<string>> GetNamesAsync(int index, int size)
        {
            await Task.Delay(1500);

            return nameData.Skip(index * size).Take(size).ToList();
        }

    }
}
