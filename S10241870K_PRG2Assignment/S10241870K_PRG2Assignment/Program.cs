//==========================================================
// Student Number : S10257905F
// Student Name : Tan Syn Kit
// Partner Name : Tan Yi Jing Valery
//==========================================================

using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Schema;
﻿using S10241870K_PRG2Assignment;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using static System.Formats.Asn1.AsnWriter;

namespace S10241870K_PRG2Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ### INITIALISATION ###

            //init empty customer & order list
            List<Customer?> customerList = new List<Customer?>();
            List<Order> orderList = new List<Order>();

            //init valid flavours, toppings, waffle flavours
            List<string> validFlavours = new List<string>
                { "vanilla", "chocolate", "strawberry", "durian", "ube", "sea salt" };
            List<string> validToppings = new List<string> { "sprinkles", "mochi", "sago", "oreos" };
            List<string> validWaffle = new List<string> { "original", "red velvet", "charcoal", "pandan" };

            //init customer list, order list
            InitCustomer(customerList);
            (Queue<Order>, Queue<Order>) orders = InitOrders(customerList, orderList, validFlavours, validToppings, validWaffle);

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
                    ListCurrentOrders(orders.Item1, orders.Item2);
                }
                else if (opn == 3)
                {
                    RegisterCustomer(customerList); 
                }
                else if (opn == 4)
                {
                    CreateCustomerOrder(orders.Item1, orders.Item2, customerList, orderList, validFlavours, validWaffle, validToppings); 
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
                    ProcessOrderAndCheckout(orders.Item1, orders.Item2, customerList);
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
        static void InitCustomer(List<Customer> customerList)
        {
            //create customer objects
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
                    DateTime date;

                    if (DateTime.TryParse(customers[2], out date))
                    {
                        if (!DateTime.TryParse(customers[2], out date))
                        {
                            Console.WriteLine("Error in parsing DateTime from string.");
                        }

                        Customer? customer = new Customer(customers[0], Convert.ToInt32(customers[1]), date);
                        customerList.Add(customer);
                        PointCard pointCard = new PointCard(Convert.ToInt32(customers[4]), Convert.ToInt32(customers[5]));
                        //pointCard.Tier = customers[3];
                        customer.Rewards = pointCard; //syn: set attribute pointcard, else pointcard not associated (null)
                    }
                }
            }
        } //InitCustomer(): Valery
        static void ListCustomer(List<Customer?> customerList)
        {
            //display information of all customers 
            Console.WriteLine($"{"No.",-5}{"Name",-20}{"Member ID",-15}{"DOB",-15}{"Points",-10}{"PunchCard",-13}{"Tier"}");
            int i = 1;

            foreach (Customer customer in customerList)
            {
                Console.WriteLine($"{i,-5}{customer.Name,-20}{customer.MemberId,-15}{customer.Dob.ToString("dd/MM/yyyy"),-15}{customer.Rewards.Points,-10}{customer.Rewards.PunchCard,-13}{customer.Rewards.Tier}");
                i++; //syn: added counter to display customer number (for opn 5)
            }
        } //ListCustomer(): Valery


        //opn 2 basic feature 2: Syn Kit
        static (Queue<Order>, Queue<Order>) InitOrders(List<Customer?> customerList, List<Order> orderList, List<string> validFlavours, List<string> validToppings, List<string> validWaffle)
        {
            string orderFile = "orders.csv";

            Queue<Order> goldOrder = new Queue<Order>();
            Queue<Order> regularOrder = new Queue<Order>();

            //iterate through customerList, filter gold & regular members
            try
            {
                //read orderfile & create orders, add to respective queue if time fulfilled is null
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

                            string iceCreamOpn = orderInfo[4];
                            int scoops = Convert.ToInt32(orderInfo[5]);

                            //init flavourList
                            List<Flavour> flavourList = new List<Flavour>();
                            bool isPremium;
                            for (int i = 8; i <= 10; i++) //flavours 1-3
                            {
                                string f = orderInfo[i];
                                if (!string.IsNullOrEmpty(f) &&
                                    (validFlavours.IndexOf(f.ToLower()) != -1)) //check if flavour is valid
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
                                if (!string.IsNullOrEmpty(t) && (validToppings.IndexOf(t.ToLower()) != -1))
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

                            if (existingOrder != null) //order exists in orderList (ie existing order w same ID exists)
                            {
                                existingOrder.AddIceCream(iceCream);
                            }
                            else //no existing order in orderList, add order to list & queue
                            {
                                order.AddIceCream(iceCream);
                                orderList.Add(order);

                                if (string.IsNullOrEmpty(orderInfo[3])) //time fulfilled is blank, pending order
                                {
                                    foreach (Customer c in customerList)
                                    {
                                        //Console.WriteLine(c + $"\t{c.Rewards}");
                                        if (c.MemberId == memberId)
                                        {
                                            //add order to respective queues
                                            if (c.Rewards.Tier.ToLower() == "gold")
                                            {
                                                goldOrder.Enqueue(order);
                                            }
                                            else // Ordinary or Silver
                                                regularOrder.Enqueue(order);

                                            //add order to customer's CurrentOrder
                                            c.CurrentOrder = order;
                                        }
                                    }
                                }
                                else //time fulfilled not blank, 
                                {
                                    DateTime timeFulfilled = Convert.ToDateTime(orderInfo[3]);
                                    order.TimeFulfilled = timeFulfilled;

                                    //add order to OrderHistory list, alr fulfilled
                                    foreach (Customer? c in customerList)
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
                
            }
            catch (NullReferenceException nEx)
            {
                Console.WriteLine("Customer List is empty");
                Console.WriteLine($"{nEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            
            return (goldOrder, regularOrder); //returns tuple: 2 queues
        } //InitOrders(): Syn Kit

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
        } //2: ListCurrentOrders(): Syn Kit

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

            //assign Pointcard object to the customer 
            PointCard newPointcard = new PointCard(0, 0);
            newCustomer.Rewards = newPointcard;

            //append customer information to customers file 
            string customerDetails = newCustomer.Name + "," + newCustomer.MemberId + "," + newCustomer.Dob.ToString("dd/MM/yyyy") + "," + newPointcard.Tier + "," + newPointcard.Points + "," + newPointcard.PunchCard;
            using (StreamWriter sw = new StreamWriter("customers.csv", true))
            {
                sw.WriteLine(customerDetails);
                Console.WriteLine($"Registration of new customer, {name}, successful.");
            }
        } //RegisterCustomer(): Valery

        //opn 4 basic feature 4: Valery 
        static void CreateCustomerOrder(Queue<Order> goldOrder, Queue<Order> regularOrder, List<Customer> customerList, List<Order> orderList, List<string> validFlavours, List<string> validWaffle, List<string> validToppings)
        {
            //List customers from customers csv 
            ListCustomer(customerList);

            try
            {
                //Prompt user to select a customer 
                Console.Write("\nCustomer's no.: ");
                int cusNo = Convert.ToInt32(Console.ReadLine());

                if (cusNo < 1 || cusNo > customerList.Count)
                {
                    throw new IndexOutOfRangeException($"Please enter a customer number, from 1 to {customerList.Count}");
                }

                Customer customerChosen = customerList[cusNo - 1]; //retrieve selected customer 

                DateTime timeReceived = DateTime.Now;
                int newOrderId = orderList.Count;
                Order newOrder = customerChosen.MakeOrder(); //init MakeOrder() and retrieve order obj 
                newOrder.TimeReceived = timeReceived;
                newOrder.Id = newOrderId;

                while (true)
                {

                    IceCreamMenu(); // init IceCreamMenu method to showcase menu

                    //prompt user to enter their ice cream order, retrieve ice cream obj 
                    IceCream newIceCream = CreateIceCream(validFlavours, validToppings, validWaffle);

                    // append the new order to the orders.csv file


                    newOrder.AddIceCream(newIceCream);  //init AddIceCream() to add ice cream obj to the icecream list 

                    //prompt the user if they would like to add another ice cream to their order 
                    Console.Write("Would you like to add another ice cream to your order? (Y/N) ");
                    string? nextIceCream = Console.ReadLine();

                    if (nextIceCream.ToLower() == "y")
                    {
                        newIceCream = CreateIceCream(validFlavours, validToppings, validWaffle);
                    } //repeat the steps 
                    else if (nextIceCream.ToLower() == "n")
                    {
                        break;
                    } //continue to the next steps if they do not want another ice cream 

                    //display message to indicate order has been made successfully 
                    Console.WriteLine("Order has been made successfully!");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }//CreateCustomerOrder(): Valery

        static IceCream? CreateIceCream(List<string> validFlavours, List<string> validToppings, List<string> validWaffle)
        {
            IceCream newIceCream = null; //init null IceCream object
            List<string> validOpns = new List<string> { "cone", "cup", "waffle" };

            while (true)
            {
                try
                {
                    //add an entirely new ice cream object to the order
                    Console.Write("Select type: (Cup/Cone/Waffle): ");
                    string opn = Console.ReadLine();


                    if (validOpns.Find(op => op == opn.ToLower()) == null) //invalid option
                    {
                        throw new ArgumentException("Please enter a valid option.");
                    }

                    Console.Write("Scoops: ");
                    int scoops = Convert.ToInt32(Console.ReadLine());

                    if (scoops < 0 || scoops > 3)
                    {
                        throw new ArgumentOutOfRangeException(nameof(scoops), "Number of scoops must be between 1 and 3.");
                    }

                    Console.Write("Flavours (separated by comma): ");
                    string flavourChoice = Console.ReadLine();
                    string[] fL = flavourChoice.Split(",");

                    if (string.IsNullOrEmpty(flavourChoice) || fL.Length != scoops)
                    {
                        throw new ArgumentOutOfRangeException(nameof(flavourChoice),
                            $"Number of flavours must be equals to number of scoops ({scoops}).");
                    }

                    List<Flavour> flavours = new List<Flavour>();

                    foreach (string f in fL)
                    {
                        bool isPremium;
                        if (!string.IsNullOrEmpty(f) &&
                            (validFlavours.IndexOf(f.ToLower()) != -1)) //check if flavour is valid
                        {
                            if (validFlavours.IndexOf(f.ToLower()) >= 3) //premium
                                isPremium = true;
                            else
                                isPremium = false;
                            flavours.Add(new Flavour(f, isPremium, 1));
                        }
                    }

                    Console.Write("Toppings (separated by comma): ");
                    string toppingChoice = Console.ReadLine();
                    string[] tL = toppingChoice.Split(",");
                    List<Topping> toppings = new List<Topping>();

                    if (tL.Length > 4)
                    {
                        throw new ArgumentOutOfRangeException(nameof(toppingChoice),
                            "Number of toppings must be less than or equals to 4");
                    }

                    foreach (string t in tL)
                    {
                        if (!string.IsNullOrEmpty(t) && (validToppings.IndexOf(t.ToLower()) != -1))
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

                    Console.WriteLine(newIceCream);
                    return newIceCream;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }



        } //CreateIceCream(): Syn Kit

        static void IceCreamMenu()
        {
            //options, scoops and add ons 
            Console.WriteLine("\nICE CREAM MENU:");
            Console.WriteLine($"{"Options:",-15}{"Scoops:",-17}{"Add Ons:"}"); 
            Console.WriteLine($"{"",-15}{"Single $4",-17}{"Toppings (+$1)"}");
            Console.WriteLine($"{"Cup",-15}{"Double $5.50"}");
            Console.WriteLine($"{"",-15}{"Triple $6.50"}");
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine($"{"",-15}{"Single $4",-17}{"Toppings (+$1)"}");
            Console.WriteLine($"{"Cone",-15}{"Double $5.50",-17}{"Chocolate-dipped cone (+$2)"}");
            Console.WriteLine($"{"",-15}{"Triple $6.50"}");
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine($"{"",-15}{"Single $7",-17}{"Toppings (+$1)"}");
            Console.WriteLine($"{"Waffle",-15}{"Double $8.50",-17}{"Red velvet, charcoal, or pandan waffle (+$3)"}");
            Console.WriteLine($"{"",-15}{"Triple $9.50"}");
            //ice cream flavours 
            Console.WriteLine("\nIce Cream Flavours:");
            Console.WriteLine($"{"Regular flavours:",-20}{"Premium Flavours (+$2 per scoop):"}");
            Console.WriteLine($"{"Vanilla",-20}{"Durian"}");
            Console.WriteLine($"{"Chocolate",-20}{"Ube"}");
            Console.WriteLine($"{"Strawberry",-20}{"Sea salt"}");
            //toppings 
            Console.WriteLine("\nToppings (+$1 each):");
            Console.WriteLine("Sprinkles");
            Console.WriteLine("Mochi");
            Console.WriteLine("Sago");
            Console.WriteLine("Oreos");
        } //IceCreamMenu(): Valery

        //opn 5 basic feature 5: Syn Kit
        static void DisplayOrderDetails(List<Customer?> customerList)
        {
            ListCustomer(customerList); //list customers
            while (true)
            {
                try
                {
                    Console.Write("Select a customer: ");
                    int cNo = Convert.ToInt32(Console.ReadLine());

                    if (cNo < 1 || cNo > customerList.Count)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    Console.WriteLine();
                    Console.WriteLine($"Order History of Customer {cNo}");
                    Customer? c = customerList[cNo - 1];
                    Console.WriteLine(c);
                    Console.WriteLine();

                    if (c.OrderHistory.Count != 0)
                    {
                        foreach (Order o in c.OrderHistory)
                        {
                            Console.WriteLine(o);
                            foreach (IceCream iC in o.IceCreamList)
                            {
                                Console.WriteLine(iC);
                            }

                            Console.WriteLine();
                        }
                    }
                    else //OrderHistory is empty
                    {
                        Console.WriteLine($"Customer {cNo} does not have any order history.");
                    }
                    
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a number.");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine($"Please enter a number from 1 to {customerList.Count}.");
                }
            }
        } //DisplayOrderDetails(): Syn Kit

        //opn 6 basic feature 6: Syn Kit
        static void ModifyOrderDetails(List<Customer?> customerList, List<string> validFlavours, List<string> validToppings, List<string> validWaffle)
        {
            ListCustomer(customerList); //list customers


            try
            {
                Console.Write("Select a customer: ");
                int cNo = Convert.ToInt32(Console.ReadLine());

                if (cNo < 0 || cNo > customerList.Count)
                {
                    throw new IndexOutOfRangeException($"Please enter a customer number, from 1 to {customerList.Count}");
                }

                Customer? c = customerList[cNo - 1];

                if (c.CurrentOrder == null)
                {
                    throw new NullReferenceException($"Customer {cNo} does not currently have an order.");
                }

                Order? currentOrder = c.CurrentOrder; //retrieve customer's current order

                for (int i = 0; i < currentOrder.IceCreamList.Count; i++)
                {
                    Console.WriteLine(
                        $"{i + 1}. {currentOrder.IceCreamList[i]}"); //list all ice cream objs in current order
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
                        IceCreamMenu();
                        Console.WriteLine();
                        currentOrder.ModifyIceCream(iCNo - 1);
                        break;
                    }
                    else if (modOpn == 2)
                    {
                        IceCreamMenu();

                        IceCream newIceCream = CreateIceCream(validFlavours, validToppings, validWaffle);
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
                    Console.WriteLine(
                        $"{i + 1}. {currentOrder.IceCreamList[i]}"); //list all ice cream objs in updated order
                }
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine($"Please enter a valid option.");
                Console.WriteLine(formatEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } //ModifyOrderDetails(): Syn Kit


        




        // ### ADVANCED FEATURES ###
        //opn 7 advanced feature a): Syn Kit
        static void ProcessOrderAndCheckout(Queue<Order> goldOrder, Queue<Order> regularOrder, List<Customer?> customerList)
        {
            Order checkout = null;

            // ### display
            //dequeue
            if (goldOrder != null) //process gold queue
            {
                checkout = goldOrder.Peek();
                goldOrder.Dequeue();
                
            }
            else //goldQueue is empty (null), process regular queue
            {
                checkout = regularOrder.Peek();
                regularOrder.Dequeue();
            }

            //display all ice creams in order
            Console.WriteLine("Order Details: ");
            foreach (IceCream iC in checkout.IceCreamList)
            {
                Console.WriteLine(iC);
            }

            //display total bill amt
            double totalBill = checkout.CalculateTotal();
            Console.WriteLine($"\nTotal bill amount: {totalBill:C}");

            //display membership status & points
            int cId = checkout.Id;
            Customer? customer = null;

            foreach (Customer? c in customerList)
            {
                if (c.CurrentOrder.Id == cId)
                {
                    customer = c;
                    break;
                }
            }

            if (customer != null)
            {
                Console.WriteLine($"Membership status:\n{customer.Rewards}");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
            

            // ### process order
            double finalBill = totalBill;
            Console.WriteLine(customer.IsBirthday());
            //check birthday
            if (customer.IsBirthday()) //true
            { 
                IceCream freeIceCream = new Cup(); //random null IceCream Cup instance, only for processing
                foreach (IceCream iC in checkout.IceCreamList)
                {
                    double iCPrice = iC.CalculatePrice();

                    if (iCPrice > freeIceCream.CalculatePrice())
                    {
                        freeIceCream = iC;
                    }
                }
                finalBill -= freeIceCream.CalculatePrice();            
            }


            //increment punchcard
            customer.Rewards.Punch();
            if (customer.Rewards.PunchCard == 0) //this is their 11th ice cream
            {
                IceCream firstIceCream = checkout.IceCreamList[0];
                double priceFirstIC = firstIceCream.CalculatePrice();
                finalBill -= priceFirstIC;
                customer.Rewards.PunchCard += (checkout.IceCreamList.Count - 1); //-1 since alr "redeemed" one free ice cream
            }
            else
            {
                customer.Rewards.PunchCard += checkout.IceCreamList.Count; //add number of ice creams ordered to punchcard
            }

            if (customer.Rewards.PunchCard > 10) //set punchcard to 10 if value exceeds 10
            {
                customer.Rewards.PunchCard = 10;
            }

            //redeem points       
            int currentPoints = customer.Rewards.Points;
            
            //offset bill with points depending on tier
            if (customer.Rewards.Tier.ToLower() == "silver" || customer.Rewards.Tier.ToLower() == "gold")
            {
                int pointsRequired = (int)Math.Floor(finalBill / 0.02);
                if (pointsRequired < currentPoints)
                {
                    Console.Write($"You have {currentPoints} points. Would you like to redeem {pointsRequired} points to offset your bill? (y/n): ");
                    string isRedeeming = Console.ReadLine();

                    if (isRedeeming.ToLower() == "y")
                    {
                        customer.Rewards.Points -= pointsRequired;
                        finalBill = 0;
                        Console.WriteLine($"Points redeemed. You have {customer.Rewards.Points} points left.");
                    }
                }
            }

            Console.WriteLine($"Final bill: {finalBill:C}");


            // ### make payment
            Console.Write("Enter any key to make payment: ");
            if (Console.ReadKey() != null)
            {
                //earn points
                int pointsEarned = (int)Math.Floor(finalBill * 0.72);
                customer.Rewards.Points += pointsEarned;

                DateTime timeFulfilled = DateTime.Now;
                Console.WriteLine($"Order fulfilled at {timeFulfilled}");
                Console.WriteLine("Have a nice day!");

                checkout.TimeFulfilled = timeFulfilled;
                customer.OrderHistory.Add(checkout); //add to customer order history
            }
        } //ProcessOrderAndCheckout(): Syn Kit

    }
}
