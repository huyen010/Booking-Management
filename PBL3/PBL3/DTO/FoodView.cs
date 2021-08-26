using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.DTO
{
    class FoodView
    {
        public int IDFood { get; set; }
        public string NameFood { get; set; }
        public string NameFCategory { get; set; }
        public double Price { get; set; }
        public string Material { get; set; }
        
        public FoodView()
        {
        }
    }
}
