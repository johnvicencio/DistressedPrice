/*
 Author: John Vicencio
 Date: 8/18/2022
 Description: Calculate pricing with distressing options

 Blueprint:
    - Create a function that calculates pricing
        1. Price = Price either "add", "deduct", or no change based on distressing pricing
        2. Second priced and price higher than $300, add 1% each
        3. function accepts base price, products to add, distressing pricing
   - Create a list of products with distressing prices
   - Call the function to and display the price
 */
namespace DistressedPricing
{
    internal class DistressedItem
    {
        public string? ProductId { get; set; }
        public string? Operation { get; set; }
        public int Percentage { get; set; }
    }
    internal class Program
    {
        // Method to calculate price
        static double PricingCalculation(double price, List<string> productList, List<DistressedItem> distressedItems)
        {
            // Variable initialize and assignments
            var result = 0.00;
            var index = 0;
            var products = productList;
            var tempPrice = 0.00;

            // Calculate each products one by one
            foreach (var product in products)
            {
                // Get each product item
                var item = distressedItems.Where(d => d.ProductId == product).FirstOrDefault();

                // Determine pricing based on distressing operation
                if (item != null)
                {
                    index++;
                    if (item.Operation == "add")
                    {
                        tempPrice = price + ((price / 100) * item.Percentage);
                        if(index > 1 && tempPrice >= 300.00)
                        {
                            result += tempPrice + ((tempPrice / 100) * 1);
                        } 
                        else
                        {
                            result += tempPrice;
                        }
  
                    }
                    else if (item.Operation == "deduct")
                    {
                        tempPrice = price - ((price / 100) * item.Percentage);
                        if (index > 1 && tempPrice >= 300.00)
                        {
                            result += tempPrice + ((tempPrice / 100) * 1);
                        }
                        else
                        {
                            result += tempPrice;
                        }
                    }
                    else
                    {
                        result += price;
                    }
                    
                }
                
            }

            return result;

        }
        static void Main(string[] args)
        {
            // Assign a base price
            double basePrice = 10.00;

            // List of the products 

            List<DistressedItem> distressedItems = new List<DistressedItem>
            {
                new DistressedItem{ ProductId = "UNIFIN", Operation = "deduct", Percentage = 5  },
                new DistressedItem{ ProductId = "SBI", Operation = "add", Percentage = 10  },
                new DistressedItem{ ProductId = "PBI", Operation = "add", Percentage = 10  },
                new DistressedItem{ ProductId = "FINNTSP", Operation = "add", Percentage = 20  },
                new DistressedItem{ ProductId = "FININTAC", Operation = "add", Percentage = 30  },
                new DistressedItem{ ProductId = "ACIB", Operation = "add", Percentage = 20  },
                new DistressedItem{ ProductId = "PRIME/SV", Operation = "no change", Percentage = 0  },
                new DistressedItem{ ProductId = "PRIME", Operation = "no change", Percentage = 0  },
                new DistressedItem{ ProductId = "EASED", Operation = "add", Percentage = 3  },
                new DistressedItem{ ProductId = "DENTS", Operation = "add", Percentage = 3  },
                new DistressedItem{ ProductId = "SPLITS", Operation = "add", Percentage = 3  },
                new DistressedItem{ ProductId = "WORMHOLES", Operation = "add", Percentage = 3  },
                new DistressedItem{ ProductId = "RASP", Operation = "add", Percentage = 3  },
                new DistressedItem{ ProductId = "SPATTER", Operation = "add", Percentage = 3  },
                new DistressedItem{ ProductId = "COWTAIL", Operation = "add", Percentage = 3  },
                new DistressedItem{ ProductId = "WBRUSH", Operation = "add", Percentage = 8  },
            };

            // Sample product list to get price(s)
            var productList = new List<string>();
            productList.Add("SBI");
            productList.Add("UNIFIN");

            // Get the total price
            double price = PricingCalculation(basePrice, productList, distressedItems);

            // Display price
            Console.WriteLine(price);
        }
    }
}