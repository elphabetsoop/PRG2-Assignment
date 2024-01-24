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

namespace S10241870K_PRG2Assignment //Syn Kit
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
            //create an order object 
            Order order = new Order();
            CurrentOrder = order;
            return new Order();
        }


        public bool IsBirthday()
        {
            int today = DateTime.Today.Day;
            int dobDate = Dob.Day;

            int todayMonth = DateTime.Today.Month;
            int dobMonth = Dob.Month;
            if (today == dobDate && todayMonth == dobMonth)
                return true;

            return false;

        }

        public override string ToString()
        {
            return $"Name: {Name} \t Member ID: {MemberId} \t DOB: {Dob.ToString("dd/MM/yyyy")}\t";
        }
    }
}
