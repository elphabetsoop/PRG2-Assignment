//==========================================================
// Student Number : S10257905F
// Student Name : Tan Syn Kit
// Partner Name : Tan Yi Jing Valery
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace S10241870K_PRG2Assignment //Syn Kit
{
    internal class Order
    {
        public int Id { get; set; }
        public DateTime TimeReceived { get; set; }
        public DateTime? TimeFulfilled { get; set; }
        public List<IceCream> IceCreamList { get; set; } = new List<IceCream>();
        

        //ctor
        public Order()
        {
            
        }

        public Order(int id, DateTime timeReceiveed)
        {
            Id = id;
            TimeReceived = timeReceiveed;
        }

        //meth
        public void ModifyIceCream(int i) //i == index of ice cream in ice cream list
        {
            List<string> validFlavours = new List<string>
                { "vanilla", "chocolate", "strawberry", "durian", "ube", "sea salt" };
            List<string> validToppings = new List<string> { "sprinkles", "mochi", "sago", "oreos" };
            List<string> validWaffle = new List<string> { "original", "red velvet", "charcoal", "pandan" };

            //logic to modify ice cream
            IceCream modIceCream = IceCreamList[i];
            Console.WriteLine("Enter new ice cream modifications: ");
            Console.Write("Scoops: ");
            int scoops = Convert.ToInt32(Console.ReadLine());

            //mod ice cream
            modIceCream.Scoops = scoops;

            Console.Write("Flavours (separated by comma): ");
            string[] fL = Console.ReadLine().Split(",");

            IceCreamList[i].Flavours.Clear();

            //mod Flavours
            bool isPremium;
            foreach (string f in fL)
            {
                if (f != null && (validFlavours.IndexOf(f.ToLower()) != -1)) //check if flavour is valid
                {
                    if (validFlavours.IndexOf(f.ToLower()) >= 3) //premium
                        isPremium = true;
                    else
                        isPremium = false;

                    modIceCream.Flavours.Add(new Flavour(f, isPremium, 1));
                }
            }

            Console.Write("Toppings (separated by comma): ");
            string[] tL = Console.ReadLine().Split(",");

            //mod Toppings
            IceCreamList[i].Toppings.Clear();
            foreach (string t in tL)
            {
                if (t != null && (validToppings.IndexOf(t.ToLower()) != -1))
                {
                    modIceCream.Toppings.Add(new Topping(t));
                }
            }

            if (modIceCream is Waffle)
            {
                Waffle waffle = (Waffle)modIceCream;

                while (true)
                {
                    Console.Write("Waffle flavour: ");
                    string waffleFlavour = Console.ReadLine();

                    if (validWaffle.IndexOf(waffleFlavour.ToLower()) != -1)
                    {
                        waffle.WaffleFlavour = waffleFlavour;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid waffle flavour.");
                    }
                }
                
            }
            else if (modIceCream is Cone)
            {
                Cone cone = (Cone) modIceCream;
                
                bool isDipped;

                while (true)
                {
                    Console.Write("Dipped cone (y/n): ");
                    string dipped = Console.ReadLine();

                    if (dipped.ToLower() == "y")
                    {
                        isDipped = true;
                        break;
                    }
                    else if (dipped.ToLower() == "n")
                    {
                        isDipped = false;
                        break;
                    }
                    else
                        Console.WriteLine("Please enter 'y' or 'n'");
                }
                cone.Dipped = isDipped;
            }
        }



        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);
        }

        public void DeleteIceCream(int i)
        {
            //logic to delete ice cream
            IceCream delIceCream = IceCreamList[i];
            IceCreamList.Remove(delIceCream);
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (IceCream iceCream in IceCreamList)
            {
                double price = iceCream.CalculatePrice();
                total += price;
            }
            return total;
        }

        public override string ToString()
        {
            if (TimeFulfilled != null)
                return $"Id: {Id} \t Time Received: {TimeReceived} \t Time Fulfilled: {TimeFulfilled} \t";
            
            return $"Id: {Id} \t Time Received: {TimeReceived} \t";
        }
    }
}
