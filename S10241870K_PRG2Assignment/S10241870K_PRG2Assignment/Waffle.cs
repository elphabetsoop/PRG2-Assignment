using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10241870K_PRG2Assignment //val
{
    internal class Waffle
    {
        public string WaffleFlavour { get; set; }
        public Waffle() { }
        public Waffle(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, string waffleflavour):base(option, scoops, flavours, toppings)
        {
            WaffleFlavour = waffleflavour; 
        }
        public override double CalculatePrice()
        {

        }
        public override string ToString()
        {
            return base.ToString(); 
        }
    }
}
