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
            Queue<Order> goldOrder = new Queue<Order>();
            Queue<Order> regularOrder = new Queue<Order>();
            List<Order> orderList = new List<Order>();
            List<Customer?> customerList = new List<Customer?>();

            using (StreamReader sr = new StreamReader("orders.csv"))
            {
                //Create an Order object 
                Order order = new Order();


                string? s = sr.ReadLine(); // read the heading
                                           // display the heading
                if (s != null)
                {
                    string[] heading = s.Split(',');
                }
                while ((s = sr.ReadLine()) != null)     // repeat until end of file
                {
                    string[] orderDetails = s.Split(',');
                    int oID = int.Parse(orderDetails[0]);
                    int memberId = int.Parse(orderDetails[1]);
                    DateTime timeReceived = DateTime.Now;

                    order = new Order(oID, timeReceived);


                    Order existingOrder = orderList.Find(o => o.Id == oID);

                    if (existingOrder != null) ////order exists in orderList (ie existing order w same ID exists)
                    {
                        existingOrder.AddIceCream(newIceCream);
                    }
                    else //no existing order in orderList, add order to list & queue
                    {
                        order.AddIceCream(newIceCream);
                        orderList.Add(order);

                        if (string.IsNullOrEmpty(orderDetails[3])) //time fulfilled is blank, pending order
                        {
                            //add pending orders to queue
                            //append to gold queue
                            foreach (Customer gc in customerList)
                            {
                                if (gc.MemberId == memberId)
                                {
                                    goldOrder.Enqueue(order);
                                }
                            }

                            //otherwise append to regular queue
                            if (!goldOrder.Contains(order))
                            {
                                regularOrder.Enqueue(order);
                            }
                        }
                        else //time fulfilled not blank, 
                        {
                            DateTime timeFulfilled = Convert.ToDateTime(orderDetails[3]);
                            order.TimeFulfilled = timeFulfilled;

                            //add order to OrderHistory list, alr fulfilled
                            foreach (Customer c in customerList)
                            {
                                if (c.MemberId == memberId)
                                    c.OrderHistory.Add(order);
                            }
                        }
                    }
                }
            }
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
