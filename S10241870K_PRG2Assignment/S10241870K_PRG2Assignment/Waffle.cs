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
        public string WaffleFlavour { get; set; }
        public Waffle() { }
        public Waffle(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, string waffleflavour):base(option, scoops, flavours, toppings)
        {
            WaffleFlavour = waffleflavour; 
        }
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
            return base.ToString(); 
        }
    }
}
