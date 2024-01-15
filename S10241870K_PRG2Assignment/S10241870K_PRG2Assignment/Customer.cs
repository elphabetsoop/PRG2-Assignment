using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10241870K_PRG2Assignment //syn
{
    internal class Customer
    {
        public string Name { get; set; }
        public int MemberId { get; set; }
        public DateTime Dob { get; set; }
        public Order CurrentOrder { get; set; } //attribute Order.cs
        public List<Order> OrderHistory { get; set; } = new List<Order>(); //attribute Order.cs

        public PointCard Rewards { get; set; } //attribute PointCard

        //ctor

        public Customer()
        {
            
        }

        public Customer(string n, int mID, DateTime dob)
        {
            Name = n;
            MemberId = mID;
            Dob = dob;
        }

        public Order MakeOrder()
        {
            //logic

            // ### START code to prevent ide complaining
            //remove dur actl implementation         
            return new Order();
            // ### END code to prevent ide complaining
        }

        public bool IsBirthday()
        {
            if (DateTime.Today == Dob)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"Name: {Name} \t Member ID: {MemberId} \t DOB: {Dob}s";
        }
    }
}
