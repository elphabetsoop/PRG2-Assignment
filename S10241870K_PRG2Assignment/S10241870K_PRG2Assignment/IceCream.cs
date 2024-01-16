using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10241870K_PRG2Assignment //val
{
    internal abstract class IceCream
    {

        //properties 
        public string Option { get; set; }
        public int Scoops {  get; set; }
        public List<Flavour> Flavours { get; set; }
        public List<Topping> Toppings { get; set; }

        //constructors 
        public IceCream()
        {
            Flavours = new List<Flavour>();
            Toppings = new List<Topping>();
        }
        public IceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
        {
            Option = option;
            Scoops = scoops;
            Flavours = flavours;
            Toppings = toppings;
        }

        //methods 
        public abstract double CalculatePrice(); 
        public override string ToString()
        {
            return "Option: " + Option + "\t" + "Scoop(s): " + Scoops + "\t" + "Flavour(s): " + Flavours + "\t" + "Topping(s): " + Toppings; 
        }
    }
}
