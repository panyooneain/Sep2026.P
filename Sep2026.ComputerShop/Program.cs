
using System;

namespace Sep2026.PanYoonEain_ComputerShop
{
    class Program
    {
        static ProductService productService = new ProductService();

        static void Main(string[] args)
        {
            bool running = true;

            // Test Database Connection
            try
            {
                using (var con = DbConnection.GetConnection())
                {
                    con.Open();
                    Console.WriteLine("Database connected successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database connection failed!");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;
            }

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("======================================");
                Console.WriteLine("  COMPUTER SHOP MANAGEMENT SYSTEM");
                Console.WriteLine("======================================");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. View All Products");
                Console.WriteLine("3. Search Product");
                Console.WriteLine("4. Update Product");
                Console.WriteLine("5. Delete Product");
                Console.WriteLine("6. Sell Product");
                Console.WriteLine("7. Exit");
                Console.WriteLine("======================================");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        AddProduct();
                        break;

                    case "2":
                        ViewProducts();
                        break;

                    case "3":
                        SearchProduct();
                        break;

                    case "4":
                        UpdateProduct();
                        break;

                    case "5":
                        DeleteProduct();
                        break;

                    case "6":
                        SellProduct();
                        break;

                    case "7":
                        running = false;
                        Console.WriteLine("Thank you for using the system!");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        // ADD PRODUCT
        static void AddProduct()
        {
            Console.WriteLine("\n=== Add Product ===");

            Console.Write("Product Name: ");
            string name = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Product name is required.");
                Pause();
                return;
            }

            Console.Write("Brand: ");
            string brand = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(brand))
            {
                Console.WriteLine("Brand is required.");
                Pause();
                return;
            }

            Console.Write("Price (MMK): ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0)
            {
                Console.WriteLine("Enter a valid price greater than zero.");
                Pause();
                return;
            }

            Console.Write("Quantity: ");

            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 0)
            {
                Console.WriteLine("Enter a valid quantity.");
                Pause();
                return;
            }

            try
            {
                Product product = new Product
                {
                    ProductName = name,
                    Brand = brand,
                    Price = price,
                    Quantity = quantity
                };

                int newId = productService.AddProduct(product);

                Console.WriteLine("Product added successfully!");
                Console.WriteLine($"Product ID: {newId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding product: " + ex.Message);
            }

            Pause();
        }

        // VIEW ALL PRODUCTS
        static void ViewProducts()
        {
            Console.WriteLine("\n=== All Products ===");

            try
            {
                List<Product> products = productService.GetAllProducts();

                if (products.Count == 0)
                {
                    Console.WriteLine("No products available.");
                }
                else
                {
                    foreach (Product product in products)
                    {
                        product.DisplayProduct();
                    }

                    Console.WriteLine($"Total Product Types: {products.Count}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error viewing products: " + ex.Message);
            }

            Pause();
        }

        // SEARCH PRODUCT
        static void SearchProduct()
        {
            Console.WriteLine("\n=== Search Product ===");
            Console.Write("Enter Product ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Product ID.");
                Pause();
                return;
            }

            try
            {
                Product? product = productService.GetProductById(id);

                if (product == null)
                {
                    Console.WriteLine("Product not found.");
                }
                else
                {
                    product.DisplayProduct();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error searching product: " + ex.Message);
            }

            Pause();
        }

        // UPDATE PRODUCT
        static void UpdateProduct()
        {
            Console.WriteLine("\n=== Update Product ===");
            Console.Write("Enter Product ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Product ID.");
                Pause();
                return;
            }

            try
            {
                Product? product = productService.GetProductById(id);

                if (product == null)
                {
                    Console.WriteLine("Product not found.");
                    Pause();
                    return;
                }

                product.DisplayProduct();

                Console.Write("New Product Name: ");
                string name = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Product name is required.");
                    Pause();
                    return;
                }

                Console.Write("New Brand: ");
                string brand = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(brand))
                {
                    Console.WriteLine("Brand is required.");
                    Pause();
                    return;
                }

                Console.Write("New Price (MMK): ");

                if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0)
                {
                    Console.WriteLine("Invalid price.");
                    Pause();
                    return;
                }

                Console.Write("New Quantity: ");

                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 0)
                {
                    Console.WriteLine("Invalid quantity.");
                    Pause();
                    return;
                }

                product.ProductName = name;
                product.Brand = brand;
                product.Price = price;
                product.Quantity = quantity;

                bool updated = productService.UpdateProduct(product);

                Console.WriteLine(updated
                    ? "Product updated successfully!"
                    : "Product update failed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating product: " + ex.Message);
            }

            Pause();
        }

        // DELETE PRODUCT
        static void DeleteProduct()
        {
            Console.WriteLine("\n=== Delete Product ===");
            Console.Write("Enter Product ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Product ID.");
                Pause();
                return;
            }

            try
            {
                Product? product = productService.GetProductById(id);

                if (product == null)
                {
                    Console.WriteLine("Product not found.");
                }
                else
                {
                    product.DisplayProduct();

                    Console.Write("Confirm delete? (Y/N): ");
                    string confirm = Console.ReadLine() ?? "";

                    if (confirm.Equals("Y", StringComparison.OrdinalIgnoreCase))
                    {
                        bool deleted = productService.DeleteProduct(id);

                        Console.WriteLine(deleted
                            ? "Product deleted successfully!"
                            : "Product delete failed.");
                    }
                    else
                    {
                        Console.WriteLine("Delete cancelled.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting product: " + ex.Message);
            }

            Pause();
        }

        // SELL PRODUCT
        static void SellProduct()
        {
            Console.WriteLine("\n=== Sell Product ===");
            Console.Write("Enter Product ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Product ID.");
                Pause();
                return;
            }

            try
            {
                Product? product = productService.GetProductById(id);

                if (product == null)
                {
                    Console.WriteLine("Product not found.");
                    Pause();
                    return;
                }

                product.DisplayProduct();

                Console.Write("Quantity to sell: ");

                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                {
                    Console.WriteLine("Enter a valid quantity.");
                    Pause();
                    return;
                }

                if (quantity > product.Quantity)
                {
                    Console.WriteLine("Not enough stock available.");
                    Pause();
                    return;
                }

                decimal total = product.Price * quantity;

                bool sold = productService.SellProduct(id, quantity);

                if (!sold)
                {
                    Console.WriteLine("Sale failed. Please check the available stock.");
                    Pause();
                    return;
                }

                Console.WriteLine("\n========== SALES RECEIPT ==========");
                Console.WriteLine($"Product: {product.ProductName}");
                Console.WriteLine($"Quantity Sold: {quantity}");
                Console.WriteLine($"Unit Price: {product.Price:N0} MMK");
                Console.WriteLine($"Total: {total:N0} MMK");
                Console.WriteLine($"Remaining Stock: {product.Quantity - quantity}");
                Console.WriteLine("===================================");
                Console.WriteLine("Sale completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error selling product: " + ex.Message);
            }

            Pause();
        }

        // PAUSE
        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }
    }
}
