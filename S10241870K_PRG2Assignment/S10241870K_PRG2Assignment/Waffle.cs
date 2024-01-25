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
using static System.Formats.Asn1.AsnWriter;

namespace S10241870K_PRG2Assignment //Valery
{
    internal class Waffle : IceCream
    {

        //property
        public string WaffleFlavour { get; set; }

        //constructor 
        public Waffle() { }
        public Waffle(int scoops, List<Flavour> flavours, List<Topping> toppings, string waffleflavour) : base("Waffle", scoops, flavours, toppings)
        {
            WaffleFlavour = waffleflavour;
        }

        //methods 
        public override double CalculatePrice()
        {
            double price;
            int premiumPrice = 0;

            if (Scoops == 1)
            {
                price = 7.00;
            }
            else if (Scoops == 2)
            {
                price = 8.50;
            }
            else if (Scoops == 3)
            {
                price = 9.50;
            }
            else
            {
                price = 0.0;
            }

            double toppingPrice = Toppings.Count * 1.0;

            foreach (Flavour f in Flavours) //iterate through flavours, if premium add $2
            {
                if (f.Premium == true)
                {
                    premiumPrice += 2;
                }
            }

            if (WaffleFlavour.ToLower() == "red velvet" ||
                WaffleFlavour.ToLower() == "charcoal" ||
                WaffleFlavour.ToLower() == "pandan")
            {
                return price + toppingPrice + premiumPrice + 3.0;
            }
            else
            {
                return price + toppingPrice + premiumPrice;
            }
        }
        public override string ToString()
        {
            return $"{WaffleFlavour} " + base.ToString();
        }
    }
}
