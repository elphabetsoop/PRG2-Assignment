namespace S10241870K_PRG2Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int opn = DisplayMenu();

            }
        }

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
        } //DisplayMenu()


        // ### BASIC FEATURES ###


        // ### ADVANCED FEATURES ###
    }
}