
﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Schema;
﻿using S10241870K_PRG2Assignment;
using System.Linq;
using System.Runtime.CompilerServices;
using static System.Formats.Asn1.AsnWriter;

namespace S10241870K_PRG2Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Order o1 = new Order(1, DateTime.Now);
            Order o2 = new Order(1, DateTime.Now);

            Console.WriteLine(o1 == o2);


            //HEAD
            //init empty customer & order list
            List<Customer> customerList = new List<Customer>();
            List<Order> orderList = new List<Order>();
            //init valid flavours, toppings, waffle flavours
            List<string> validFlavours = new List<string>
                { "vanilla", "chocolate", "strawberry", "durian", "ube", "sea salt" };
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

            /*// ### TESTING CURRENT ORDER FOR OPN 6 ###
            Customer amelia = new Customer("Amelia", 685582, new DateTime(2000, 03, 12));
            Customer bob = new Customer("Bob", 245718, new DateTime(1966, 11, 01));
            amelia.Rewards = new PointCard(150, 8);
            bob.Rewards = new PointCard(5, 1);
            customerList.Add(amelia); //gold
            customerList.Add(bob); //regular

            amelia.CurrentOrder = new Order(69, DateTime.Now);
            amelia.CurrentOrder.AddIceCream(new Cone(2,
                new List<Flavour> { new Flavour("Durian", true, 1), new Flavour("Chocolate", false, 1) },
                new List<Topping>(), true));
            amelia.CurrentOrder.AddIceCream(new Cup(2,
                new List<Flavour> { new Flavour("Strawberry", false, 1), new Flavour("Chocolate", false, 1) },
                new List<Topping> { new Topping("sprinkles") }));
            amelia.CurrentOrder.AddIceCream(new Waffle(1, new List<Flavour> { new Flavour("Ube", true, 1) },
                new List<Topping> { new Topping("oreos") }, "Pandan"));

            bob.CurrentOrder = new Order(420, DateTime.Now);
            bob.CurrentOrder.AddIceCream(new Cone(2, new List<Flavour> { new Flavour("Durian", true, 1), 
                new Flavour("Chocolate", false, 1) }, new List<Topping>(), true));*/


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


                else if (opn == 3)
                {
                    RegisterCustomer(customerList); 
                }

                else if (opn == 4)
                {
                    CreateCustomerOrder(customerList); 
                }

                else if (opn == 5)
                {
                    DisplayOrderDetails(customerList);
                }
                else if (opn == 6)
                {
                    ModifyOrderDetails(customerList, validFlavours, validToppings, validWaffle);
                }
                else if (opn == 7) //advanced 1
                {
                    //
                }
                else if (opn == 8) //advanced 2
                {
                    //
                }
                else
                {
                    Console.WriteLine("Invalid Option. Please try again.");
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

            while (true)
            {
                try
                {
                    Console.Write("Enter your option: ");
                    int opn = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    return opn;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid option. Please enter a number.");
                }
            }
            
            
            
        } //DisplayMenu(): Syn Kit

        // ### BASIC FEATURES ###
        //opn 1 basic feature 1: Valery 
        static void ListCustomer(List<Customer> customerList)
        {
            //display information of all customers 
            using (StreamReader sr = new StreamReader("customers.csv"))
            {
                int i = 1;
                string? s = sr.ReadLine(); // read the heading
                                           // display the heading
                if (s != null)
                {
                    string[] heading = s.Split(',');
                }

                Console.WriteLine($"{"No.",-5}{"Name",-20}{"Member ID",-15}{"DOB",-15}{"Points",-10}{"PunchCard",-13}{"Tier"}");
                while ((s = sr.ReadLine()) != null)     // repeat until end of file
                {
                    string[] customers = s.Split(',');
                    DateTime date;

                    if (DateTime.TryParse(customers[2], out date))
                    {
                        if (!DateTime.TryParse(customers[2], out date))
                        {
                            Console.WriteLine("Error in parsing DateTime from string."); 
                        }

                        Customer customer = new Customer(customers[0], Convert.ToInt32(customers[1]), date);
                        customerList.Add(customer);
                        PointCard pointCard = new PointCard(Convert.ToInt32(customers[4]), Convert.ToInt32(customers[5]));
                        //pointCard.Tier = customers[3];
                        customer.Rewards = pointCard; //syn: set attribute pointcard, else pointcard not associated (null)
                        Console.WriteLine($"{i,-5}{customer.Name,-20}{customer.MemberId,-15}{customer.Dob.ToString("dd/MM/yyyy"),-15}{pointCard.Points,-10}{pointCard.PunchCard,-13}{pointCard.Tier}");
                        i++; //syn: added counter to display customer number (for opn 5)
                    }  

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

                        Order existingOrder = orderList.Find(o => o.Id == oID);
                        {
                            if (existingOrder != null) ////order exists in orderList (ie existing order w same ID exists)
                            {
                                existingOrder.AddIceCream(iceCream);
                            }
                            else //no existing order in orderList, add order to list & queue
                            {
                                order.AddIceCream(iceCream);
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

                                //add order to OrderHistory list
                                foreach (Customer c in customerList)
                                {
                                    if (c.MemberId == memberId)
                                        c.OrderHistory.Add(order);
                                }
                            }
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
                Console.WriteLine(gold.IceCreamList[0]);
                
                if (gold.IceCreamList.Count > 1)
                {
                    for (int i = 1; i < gold.IceCreamList.Count; i++)
                    {
                        Console.WriteLine($"{" ",-40} {gold.IceCreamList[i]}");
                    }
                }
            }

            Console.WriteLine();

            Console.WriteLine("REGULAR QUEUE");
            Console.WriteLine($"{"Order ID",-15} {"Time received",-25}{"Ice cream(s)",-15}");
            foreach (Order regular in regularOrder)
            {
                Console.Write($"{regular.Id,-15} {regular.TimeReceived,-25}");
                Console.WriteLine(regular.IceCreamList[0]);

                if (regular.IceCreamList.Count > 1)
                {
                    for (int i = 1; i < regular.IceCreamList.Count; i++)
                    {
                        Console.WriteLine($"{" ",-40} {regular.IceCreamList[i]}");
                    }
                }
            }
        } //2: ListCurrentOrders() 

        //opn 3 basic feature 3: Valery 
        static void RegisterCustomer(List<Customer> customerList)
        {
            // prompt user for information 
            Console.WriteLine("Please provide the following: ");
            Console.Write("Name: ");
            string? name = Console.ReadLine();
            Console.Write("ID Number: "); 
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Date Of Birth: ");
            DateTime customerDob;
            if (!DateTime.TryParse(Console.ReadLine(), out customerDob))
            {
                Console.WriteLine("Given date format is invalid. Please try again.");
                return; //to stop the program from further execution 
            }

            //create customer object with the information 
            Customer newCustomer = new Customer(name, id, customerDob);
            customerList.Add(newCustomer);

            //create a Pointcard object 
            int points = 0;
            double totalPrice = 0;
            int punchCard = 0;

            if (newCustomer.CurrentOrder != null && newCustomer.CurrentOrder.IceCreamList != null)
            {
                punchCard = newCustomer.CurrentOrder.IceCreamList.Count;

                foreach (IceCream iceCream in newCustomer.CurrentOrder.IceCreamList)
                {
                    double price = iceCream.CalculatePrice();
                    totalPrice += price;
                }

                points = (int)Math.Floor(totalPrice * 0.72);
            }

            PointCard newPointcard = new PointCard(points, punchCard);

            //assign Pointcard object to the customer 
            newCustomer.Rewards = newPointcard;

            //append customer information to customers file 
            string customerDetails = newCustomer.Name + "," + newCustomer.MemberId + "," + newCustomer.Dob.ToString("dd/MM/yyyy") + "," + newPointcard.Points + "," + newPointcard.PunchCard + "," + newPointcard.Tier; 
            using (StreamWriter sw = new StreamWriter("customers.csv", true))
            {
                sw.WriteLine(customerDetails);
                Console.WriteLine($"Registration of new customer, {name}, successful.");
            }
        } //RegisterCustomer 

        //opn 4 basic feature 4: Valery 
        static void CreateCustomerOrder(List<Customer> customerList)
        {
            //List customers from customers csv 
            using (StreamReader sr = new StreamReader("customers.csv"))
            {
                string? s = sr.ReadLine(); // read the heading
                                           // display the heading
                if (s != null)
                {
                    string[] heading = s.Split(',');
                }
                while ((s = sr.ReadLine()) != null)     // repeat until end of file
                {
                    string[] customers = s.Split(',');
                    DateTime date = DateTime.Parse(customers[2]);

                    Customer customer = new Customer(customers[0], Convert.ToInt32(customers[1]), date);
                    customerList.Add(customer);
                    PointCard pointCard = new PointCard(Convert.ToInt32(customers[4]), Convert.ToInt32(customers[5]));
                    pointCard.Tier = customers[3];
                    Console.WriteLine($"{customer.ToString()}{pointCard.ToString()}");
                }
            }

            //Prompt user to select a customer 
            Console.Write("Name of customer: ");
            string? name = Console.ReadLine();

            foreach (Customer customer in customerList)
            {
                if (customer.Name.ToLower() == name.ToLower())
                {
                    //Create an Order object 
                    DateTime currentDate = DateTime.Now;
                    Order order = new Order(0, currentDate);  //id is not properly done, queue number from the queue created in option 2?? 
                }
            }
            

        } //CreateCustomerOrder 

        //opn 5 basic feature 5: Syn Kit
        static void DisplayOrderDetails(List<Customer> customerList)
        {
            ListCustomer(customerList); //list customers
            Console.Write("Select a customer: ");
            int cNo = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine($"Order History of Customer {cNo}");
            Customer c = customerList[cNo - 1];
            Console.WriteLine(c);
            Console.WriteLine();

            foreach (Order o in c.OrderHistory)
            {
                Console.WriteLine(o);
                foreach (IceCream iC in o.IceCreamList)
                {
                    Console.WriteLine(iC);
                }
                Console.WriteLine();
            }
        } //DisplayOrderDetails()

        //opn 6 basic feature 6: Syn Kit
        static void ModifyOrderDetails(List<Customer> customerList, List<string> validFlavours, List<string> validToppings, List<string> validWaffle)
        {
            ListCustomer(customerList); //list customers
            Console.Write("Select a customer: ");
            int cNo = Convert.ToInt32(Console.ReadLine());
            Customer c = customerList[cNo - 1];

            try
            {
                Order currentOrder = c.CurrentOrder; //retrieve customer's current order

                for (int i = 0; i < currentOrder.IceCreamList.Count; i ++)
                {
                    Console.WriteLine($"{i+1}. {currentOrder.IceCreamList[i]}"); //list all ice cream objs in current order
                }

                while (true)
                {
                    //display mod menu
                    string[] modOpns = { "Modify ice cream", "Add new ice cream", "Remove ice cream" };
                    Console.WriteLine();
                    for (int i = 0; i < modOpns.Length; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {modOpns[i]}");
                    }

                    Console.Write("Select an option: ");
                    int modOpn = Convert.ToInt32(Console.ReadLine());

                    if (modOpn == 1) // choose an existing ice cream object to modify
                    {
                        Console.Write("Choose an existing ice cream to modify: ");
                        int iCNo = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();

                        currentOrder.ModifyIceCream(iCNo -1);
                        break;
                    }
                    else if (modOpn == 2)
                    {
                        IceCream newIceCream = null;

                        //add an entirely new ice cream object to the order
                        Console.Write("Select type: (Cup/Cone/Waffle): ");
                        string opn = Console.ReadLine();

                        Console.Write("Scoops: ");
                        int scoops = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Flavours (separated by comma): ");
                        string[] fL = Console.ReadLine().Split(",");

                        
                        List<Flavour> flavours = new List<Flavour>();

                        foreach (string f in fL)
                        {
                            bool isPremium;
                            if (f != null && (validFlavours.IndexOf(f.ToLower()) != -1)) //check if flavour is valid
                            {
                                if (validFlavours.IndexOf(f.ToLower()) >= 3) //premium
                                    isPremium = true;
                                else
                                    isPremium = false;
                                flavours.Add(new Flavour(f, isPremium, 1));
                            }
                        }

                        Console.Write("Toppings (separated by comma): ");
                        string[] tL = Console.ReadLine().Split(",");
                        List<Topping> toppings = new List<Topping>();

                        foreach (string t in tL)
                        {
                            if (t != null && (validToppings.IndexOf(t.ToLower()) != -1))
                            {
                                toppings.Add(new Topping(t));
                            }
                        }

                        if (opn.ToLower() == "cone")
                        {
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
                            //init cone
                            newIceCream = new Cone(scoops, flavours, toppings, isDipped);
                        }
                        else if (opn.ToLower() == "waffle")
                        {
                            while (true)
                            {
                                Console.Write("Waffle flavour: ");
                                string waffleFlavour = Console.ReadLine();

                                if (validWaffle.IndexOf(waffleFlavour.ToLower()) != -1)
                                {
                                    //init waffle
                                    newIceCream = new Waffle(scoops, flavours, toppings, waffleFlavour);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid waffle flavour.");
                                }
                            }
                        }
                        else if (opn.ToLower() == "cup")
                        {
                            //init cup
                            newIceCream = new Cup(scoops, flavours, toppings);
                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Please try again.");
                        }

                        currentOrder.AddIceCream(newIceCream);
                        break;
                    }
                    else if (modOpn == 3) //choose an existing ice cream object to delete from the order
                    {
                        if (currentOrder.IceCreamList.Count > 1)
                        {
                            Console.Write("Choose an ice cream to remove from order: ");
                            int iCNo = Convert.ToInt32(Console.ReadLine());

                            currentOrder.DeleteIceCream(iCNo - 1);
                        }
                        else
                        {
                            Console.WriteLine("You cannot have 0 ice creams in an order.");
                        }
                        
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                }

                Console.WriteLine("\nUpdated order: ");
                for (int i = 0; i < currentOrder.IceCreamList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {currentOrder.IceCreamList[i]}"); //list all ice cream objs in updated order
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine($"Customer {cNo} does not currently have an order.");
            }
        } //ModifyOrderDetails()







        // ### ADVANCED FEATURES ###

    }
}

