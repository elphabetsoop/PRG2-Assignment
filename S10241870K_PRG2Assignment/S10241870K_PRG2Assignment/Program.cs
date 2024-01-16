namespace S10241870K_PRG2Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Customer> customerList = new List<Customer>();
            List<Order> orderList = new List<Order>();

            ListCustomer(customerList); 
        } //end of main 
        
        //basic feature 1. 
        static void ListCustomer(List<Customer> customerList)
        {
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
        } //ListCustomer 
    }
}