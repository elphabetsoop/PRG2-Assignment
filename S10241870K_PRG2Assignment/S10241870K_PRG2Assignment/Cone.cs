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
    internal class Cone : IceCream
    {
        //property
        public bool Dipped { get; set; }

        //constructor 
        public Cone() { }
        public Cone(int scoops, List<Flavour> flavours, List<Topping> toppings, bool dipped):base("Cone", scoops, flavours, toppings)
        {
            Dipped = dipped;
        }

        //methods 
        public override double CalculatePrice()
        {
            double price;
            int premiumPrice = 0; 

            if (Scoops == 1)
            {
                price = 4.00; 
            }
            else if (Scoops == 2)
            {
                price = 5.50; 
            }
            else if (Scoops == 3)
            {
                price = 6.50; 
            }
            else
            {
                price = 0.0; 
            }

            double toppingPrice = Toppings.Count * 1.0; 
            double dippedPrice = Dipped ? 2.00:0.0; //if Dipped is true, give a value of 2, else 0.

            foreach (Flavour f in Flavours) //iterate through flavours, if premium add $2
            {
                if (f.Premium == true)
                {
                    premiumPrice += 2;
                }
            }

            return price + toppingPrice + dippedPrice + premiumPrice; 
        }
        public override string ToString()
        {
            if (Dipped)
                return $"Dipped " + base.ToString();
            
            return base.ToString();
        }
    }
}
