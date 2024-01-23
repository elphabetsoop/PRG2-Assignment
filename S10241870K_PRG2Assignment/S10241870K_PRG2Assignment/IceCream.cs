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
            //return "Option: " + Option + "\t" + "Scoop(s): " + Scoops + "\t" + "Flavour(s): " + Flavours + "\t" + "Topping(s): " + Toppings; 
            string flavours = "";
            foreach (Flavour f in Flavours)
            {
                flavours += f.Type + ",";
            }

            string toppings = "";
            foreach (Topping t in Toppings)
            {
                toppings += t.Type + ",";
            }

            //remove last comma
            flavours = flavours.Remove(flavours.Length - 1);


            if (toppings.Length == 0) //no toppings
                return $"{Scoops} Scoop {Option} with {flavours} ice cream";

            toppings = toppings.Remove(toppings.Length - 1);
            return $"{Scoops} Scoop {Option} with {flavours} ice cream, and {toppings} topping";

            
        }
    }
}
