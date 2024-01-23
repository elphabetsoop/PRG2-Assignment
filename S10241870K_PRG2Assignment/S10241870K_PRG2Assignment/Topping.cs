//==========================================================
// Student Number : S10257905F
// Student Name : Tan Syn Kit
// Partner Name : Tan Yi Jing Valery
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10241870K_PRG2Assignment //Valery
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
