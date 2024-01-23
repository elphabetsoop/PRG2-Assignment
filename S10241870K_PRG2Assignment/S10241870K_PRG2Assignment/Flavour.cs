//==========================================================
// Student Number : S10257905F
// Student Name : Tan Syn Kit
// Partner Name : Tan Yi Jing Valery
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace S10241870K_PRG2Assignment //Valery
{
    internal class Flavour
    {

        //properties 
        public string Type { get; set; }
        public bool Premium { get; set; }
        public int Quantity { get; set; }

        //constructor 
        public Flavour() { }    
        public Flavour(string type, bool premium, int quantity)
        {
            Type = type;
            Premium = premium;
            Quantity = quantity;
        }

        //method 
        public override string ToString()
        {
            return "Type: " + Type + "\t" + "Quantity: " + Quantity; 
        }
    }
}
