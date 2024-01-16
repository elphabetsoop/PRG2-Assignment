using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10241870K_PRG2Assignment //val
{
    internal class Topping 
    {
        //property 
        public string Type { get; set; }

        //constructor  
        public Topping() { }
        public Topping(string type)
        {
            Type = type;
        }

        //method 
        public override string ToString()
        {
            return "Type: " + Type; 
        }
    }
}
