using System.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace S10241870K_PRG2Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //HEAD
            //init empty customer & order list
            List<Customer> customerList = new List<Customer>();
            List<Order> orderList = new List<Order>();
            //init valid flavours, toppings, waffle flavours
            List<string> validFlavours = new List<string> { "vanilla", "chocolate", "strawberry", "durian", "ube", "sea salt" };
            List<string> validToppings = new List<string> { "sprinkles", "mochi", "sago", "oreos" };
            List<string> validWaffle = new List<string> { "original", "red velvet", "charcoal", "pandan" };

            /*// ### TESTING CUSTOMER LIST FOR OPN 2
            Customer amelia = new Customer("Amelia", 685582, new DateTime(2000, 03, 12));
            Customer bob = new Customer("Bob", 245718, new DateTime(1966, 11, 01));
            amelia.Rewards = new PointCard(150, 8);

            bob.Rewards = new PointCard(5, 1);
            Console.WriteLine(amelia.Rewards.Tier);
            Console.WriteLine(bob.Rewards.Tier);

            customerList.Add(amelia); //gold
            customerList.Add(bob); //regular*/

            while (true)
            {
                int opn = DisplayMenu();
                if (opn == 0)
                {
                    break;
                }
                else if (opn == 1)
                {
                    ListCustomer(customerList);
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
        } //end of main 

        // ### INITIALISATION ###

        static int DisplayMenu()
        {
            List<string> menuList = new List<string> {
                //basic
                "List all customers", "List all current orders",
                "Register a new customer", "Create a customer's order",
                "Display order details of a customer", "Modify order details",
                
                //advanced
                "Process an order and checkout",
                "Display monthly charged amounts breakdown & total charged amounts for the year",

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
        //opn 1 basic feature 1: Valery 
        static void ListCustomer(List<Customer> customerList)
        {
            using (StreamReader sr = new StreamReader("customers.csv"))
            {
                int i = 1;
                string? s = sr.ReadLine(); // read the heading
                                           // display the heading
                if (s != null)
                {
                    string[] heading = s.Split(',');
                }

                Console.WriteLine($"{"No.",-5}{"Name",-20}{"Member ID",-15}{"DOB",-15}{"Points",-10}{"PunchCard",-10}{"Tier"}");
                while ((s = sr.ReadLine()) != null)     // repeat until end of file
                {
                    string[] customers = s.Split(',');
                    DateTime date = DateTime.Parse(customers[2]);

                    Customer customer = new Customer(customers[0], Convert.ToInt32(customers[1]), date);
                    customerList.Add(customer);
                    PointCard pointCard = new PointCard(Convert.ToInt32(customers[4]), Convert.ToInt32(customers[5]));
                    //pointCard.Tier = customers[3];
                    customer.Rewards = pointCard; //syn: set attribute pointcard, else pointcard not associated (null)
                    //Console.WriteLine($"{i} \t {customer.ToString()}{pointCard.ToString()}");
                    Console.WriteLine($"{i,-5}{customer.Name,-20}{customer.MemberId,-15}{customer.Dob.ToString("dd/MM/yyyy"),-15}{pointCard.Points,-10}{pointCard.PunchCard,-10}{pointCard.Tier}");
                    i++; //syn: added counter to display customer number (for opn 5)
                }
            }
        } //ListCustomer 


        //opn 2 basic feature 2: Syn Kit
        static (Queue<Order>, Queue<Order>) InitOrders(List<Customer> customerList, List<Order> orderList, List<string> validFlavours, List<string> validToppings, List<string> validWaffle)
        {
            string orderFile = "orders.csv";
            List<Customer> goldCustomers = new List<Customer>();
            List<Customer> regularCustomers = new List<Customer>();

            Queue<Order> goldOrder = new Queue<Order>();
            Queue<Order> regularOrder = new Queue<Order>();

            //iterate through customerList, filter gold & regular members
            foreach (Customer c in customerList)
            {
                //Console.WriteLine(c);
                string tier = c.Rewards.Tier; //membership tier: ordinary, silver or gold
                if (tier.ToLower() == "gold")
                {
                    goldCustomers.Add(c);
                }
                else
                {
                    regularCustomers.Add(c);
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
                        IceCream? iceCream = null;
                        switch (iceCreamOpn.ToLower())
                        {
                            case "cup":
                                {
                                    iceCream = new Cup(scoops, flavourList, toppingList);
                                    break;
                                }
                            case "cone":
                                {
                                    bool isDipped = Convert.ToBoolean(orderInfo[6]);
                                    iceCream = new Cone(scoops, flavourList, toppingList, isDipped);
                                    break;
                                }
                            case "waffle":
                                {
                                    string waffleFlavour = orderInfo[7];
                                    if (validWaffle.IndexOf(waffleFlavour.ToLower()) != -1)
                                    {
                                        iceCream = new Waffle(scoops, flavourList, toppingList, waffleFlavour);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid waffle flavour.");
                                    }

                                    break;
                                }
                        }
                        Order order = new Order(oID, timeReceived);

                        //create order, add to queue
                        if (!orderList.Contains(order))
                        {
                            order.AddIceCream(iceCream);
                            orderList.Add(order);
                        }
                        else //order exists in orderList (ie existing order w same ID exists)
                        {
                            if (iceCream != null) order.AddIceCream(iceCream); //add ice cream to IceCreamList in Order
                            break;
                        }

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

            return (goldOrder, regularOrder); //returns tuple: 2 queues
        }

        static void ListCurrentOrders(Queue<Order> goldOrder, Queue<Order> regularOrder)
        {
            Console.WriteLine("GOLD QUEUE");
            Console.WriteLine($"{"Order ID",-15} {"Time received",-25}{"Ice cream(s)",-15}");
            foreach (Order gold in goldOrder)
            {
                Console.Write($"{gold.Id,-15} {gold.TimeReceived,-25}");
                foreach (IceCream iC in gold.IceCreamList)
                {
                    Console.WriteLine($"{iC}");
                }
            }

            Console.WriteLine();

            Console.WriteLine("REGULAR QUEUE");
            Console.WriteLine($"{"Order ID",-15} {"Time received",-25}{"Ice cream(s)",-15}");
            foreach (Order regular in regularOrder)
            {
                Console.Write($"{regular.Id,-15} {regular.TimeReceived,-25}");
                foreach (IceCream iC in regular.IceCreamList)
                {
                    Console.WriteLine($"{iC}");
                }
            }
        } //2: ListCurrentOrders() 

        //opn 5 basic feature 5: Syn Kit
        static void DisplayOrderDetails(List<Customer> customerList)
        {
            ListCustomer(customerList); //list customers
            Console.Write("Select a customer: ");
        }









        // ### ADVANCED FEATURES ###
    }
}
        
