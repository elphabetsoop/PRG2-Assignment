using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace S10241870K_PRG2Assignment //val
{
    internal class Waffle : IceCream 
    {

        //property
        public string WaffleFlavour { get; set; }

        //constructor 
        public Waffle() { }
        public Waffle(int scoops, List<Flavour> flavours, List<Topping> toppings, string waffleflavour):base("Waffle", scoops, flavours, toppings)
        {
            WaffleFlavour = waffleflavour; 
        }

        //methods 
        public override double CalculatePrice()
        {
            double price;

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

            return price + toppingPrice; 
        }
        public override string ToString()
        {
            return $"{WaffleFlavour} " + base.ToString(); 
        }
    }
}
