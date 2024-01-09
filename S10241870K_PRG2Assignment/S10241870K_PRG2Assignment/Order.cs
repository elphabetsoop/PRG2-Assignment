using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace S10241870K_PRG2Assignment //syn
{
    internal class Order
    {
        public int Id { get; set; }
        public DateTime TimeReceived { get; set; }
        public DateTime? TimeFufilled { get; set; }
        public List<IceCream> IceCreamList { get; set; }
        

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
        public void ModifyIceCream(int i)
        {
            //logic to modify ice cream
        }

        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);
        }

        public void DeleteIceCream(int i)
        {
            //logic to delete ice cream
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
            return $"Id: {Id} \t Time Fufilled: {TimeFufilled}";
        }
    }
}
