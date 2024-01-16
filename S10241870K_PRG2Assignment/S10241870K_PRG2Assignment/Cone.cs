using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10241870K_PRG2Assignment //val
{
    internal class Cone : IceCream
    {
        public bool Dipped { get; set; }
        public Cone() { }
        public Cone(int scoops, List<Flavour> flavours, List<Topping> toppings, bool dipped):base("Cone", scoops, flavours, toppings)
        {
            Dipped = dipped;
        }
        public override double CalculatePrice()
        {
            double price; 

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

            return price + toppingPrice + dippedPrice; 
        }
        public override string ToString()
        {
            if (Dipped)
                return $"Dipped " + base.ToString();
            
            return base.ToString();
        }
    }
}
