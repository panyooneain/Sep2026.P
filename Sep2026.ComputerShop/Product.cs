
namespace Sep2026.PanYoonEain_ComputerShop
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public void DisplayProduct()
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Product ID   : {ProductId}");
            Console.WriteLine($"Product Name : {ProductName}");
            Console.WriteLine($"Brand        : {Brand}");
            Console.WriteLine($"Price        : {Price:N0} MMK");
            Console.WriteLine($"Quantity     : {Quantity}");
            Console.WriteLine("--------------------------------------");
        }
    }
}
