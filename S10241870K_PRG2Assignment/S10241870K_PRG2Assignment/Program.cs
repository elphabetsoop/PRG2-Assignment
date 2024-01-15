using System.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace S10241870K_PRG2Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //init valid flavours, toppings, waffle flavours
            List<string> validFlavours = new List<string>{ "vanilla", "chocolate", "strawberry", "durian", "ube", "sea salt" };
            List<string> validToppings = new List<string>{ "sprinkles", "mochi", "sago", "oreos" };
            List<string> validWaffle = new List<string> { "original", "red velvet", "charcoal", "pandan" };
            
            //init empty customer & order list
            List<Customer> customerList = new List<Customer>();
            List<Order> orderList = new List<Order>();

            while (true)
            {
                int opn = DisplayMenu();
                if (opn == 0)
                {
                    break;
                }
                else if (opn == 1)
                {
                    //
                }
                else if (opn == 2)
                {
                    (Queue<Order>, Queue<Order>) orders = InitOrders(customerList, orderList, validFlavours, validToppings, validWaffle);
                    Queue<Order> goldOrder = orders.Item1;
                    Queue<Order> regularOrder = orders.Item2;
                    ListCurrentOrders(goldOrder, regularOrder);
                }

                Console.WriteLine();

            }
        }

        // ### INITIALISATION ###
        static int DisplayMenu()
        {
            List<string> menuList = new List<string> {
                //basic
                "List all customers", "List all current orders",
                "Register a new customer", "Create a cusomer's order",
                "Display order details of a customer", "Modify order details",
                
                //advanced
                "Process an order and checkout",
                "Display monthly charged amouts breakdown & total charged amounts for the year",

                "Exit"
            };

            Console.WriteLine("----------- MENU ------------");
            for (int i = 0; i < menuList.Count; i++)
            {
                if (i == menuList.Count - 1)
                {
                    Console.WriteLine($"[{i - i}] {menuList[i]}");
                }
                else
                {
                    Console.WriteLine($"[{i + 1}] {menuList[i]}");
                }
            }
            Console.WriteLine("-----------------------------");
            Console.Write("Enter your option: ");
            int opn = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            return opn;
        } //DisplayMenu(): Syn Kit


        // ### BASIC FEATURES ###

        //opn 2: Syn Kit
        static (Queue<Order>, Queue<Order>) InitOrders(List<Customer> customerList, List<Order> orderList, List<string>validFlavours, List<string> validToppings, List<string> validWaffle)
        {
            string orderFile = "orders.csv";
            List<Customer> goldCustomers = new List<Customer>();
            List<Customer> regularCustomer = new List<Customer>();

            Queue<Order> goldOrder = new Queue<Order>();
            Queue<Order> regularOrder = new Queue<Order>();

            //iterate through customerList, filter gold & regular members
            foreach (Customer c in customerList)
            {
                string tier = c.Rewards.Tier; //membership tier: ordinary, silver or gold
                if (tier.ToLower() == "gold")
                {
                    goldCustomers.Add(c);
                }
                else
                {
                    regularCustomer.Add(c);
                }
            }

            //read orderfile & create orders, add to respective queue
            using (StreamReader sr = new StreamReader(orderFile))
            {
                string header = sr.ReadLine(); // Id,MemberId,TimeReceived,TimeFulfilled,Option,Scoops,Dipped,WaffleFlavour,F1,F2,F3,T1,T2,T3,T4
                while (true)
                {
                    string line = sr.ReadLine();
                    if (line != null)
                    {
                        string[] orderInfo = line.Split(",");

                        int oID = int.Parse(orderInfo[0]);
                        int memberId = Convert.ToInt32(orderInfo[1]);
                        DateTime timeReceived = Convert.ToDateTime(orderInfo[2]);
                        DateTime timeFufilled = Convert.ToDateTime(orderInfo[3]);
                        string iceCreamOpn = orderInfo[4];
                        int scoops = Convert.ToInt32(orderInfo[5]);

                        //init flavourList
                        List<Flavour> flavourList = new List<Flavour> { };
                        bool isPremium;
                        for (int i = 8; i <= 10; i++) //flavours 1-3
                        {
                            string f = orderInfo[i];
                            if (f != null && (validFlavours.IndexOf(f.ToLower()) != -1)) //check if flavour is valid
                            {
                                if (validFlavours.IndexOf(f.ToLower()) >= 3) //premium
                                    isPremium = true;
                                else
                                    isPremium = false;

                                flavourList.Add(new Flavour(f, isPremium, 1));
                            }
                        }

                        //init toppingList
                        List<Topping> toppingList = new List<Topping>();
                        for (int i = 11; i <= 14; i++) //toppings 1-4
                        {
                            string t = orderInfo[i];
                            if (t != null && (validToppings.IndexOf(t.ToLower()) != -1))
                            {
                                toppingList.Add(new Topping(t));
                            }
                        }

                        //create ice cream objects

                        if (iceCreamOpn.ToLower() == "cup")
                        {
                            IceCream iceCream = new Cup(scoops, flavourList, toppingList);
                        }
                        else if (iceCreamOpn.ToLower() == "cone")
                        {
                            bool isDipped = Convert.ToBoolean(orderInfo[6]);
                            IceCream iceCream = new Cone(scoops, flavourList, toppingList, isDipped);
                        }
                        else if (iceCreamOpn.ToLower() == "waffle")
                        {
                            string waffleFlavour = orderInfo[7];
                            if (validWaffle.IndexOf(waffleFlavour.ToLower()) != -1)
                            {
                                IceCream iceCream = new Waffle(scoops, flavourList, toppingList, waffleFlavour);
                            }
                            else
                            {
                                Console.WriteLine("Invalid waffle flavour.");
                            }              
                        }

                        //create order, add to queue
                        Order order = new Order(oID, timeReceived);

                        orderList.Add(order);


                        //add to gold queue
                        foreach (Customer gc in goldCustomers)
                        {
                            if (gc.MemberId == memberId)
                            {
                                goldOrder.Enqueue(order);
                            }
                        }

                        //add to regular queue
                        if (!goldOrder.Contains(order))
                        {
                            regularOrder.Enqueue(order);
                        }
                    }
                    else
                        break;
                } 
            }

            return (goldOrder, regularOrder);
        } 

        static void ListCurrentOrders(Queue<Order> goldOrder, Queue<Order> regularOrder)
        {
            Console.WriteLine("GOLD QUEUE");
            Console.WriteLine($"{"Order ID",-15} {"Time received",-20} {"Ice cream(s)",-15}");
            foreach (Order gold in goldOrder) 
            {
                Console.WriteLine($"{ gold.Id, -15} {gold.TimeReceived, -15}");
                foreach (IceCream iC in gold.IceCreamList)
                {
                    Console.WriteLine($"{-35}{iC}");
                }
            }

            Console.WriteLine();

            Console.WriteLine("REGULAR QUEUE");
            Console.WriteLine($"{"Order ID",-15} {"Time received",-20} {"Ice cream(s)",-15}");
            foreach (Order regular in regularOrder)
            {
                Console.WriteLine($"{regular.Id,-15} {regular.TimeReceived,-15}");
                foreach (IceCream iC in regular.IceCreamList)
                {
                    Console.WriteLine($"{-35}{iC}");
                }
            }       
        } //2: ListCurrentOrders() 

        // ### ADVANCED FEATURES ###
    }
}