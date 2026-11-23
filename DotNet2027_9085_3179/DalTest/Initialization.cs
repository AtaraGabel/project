using DalApi;
using DalFacade.Do;
using DO;


namespace DalTest
{
    public static class Initialization
    {
        private static IDal? s_dal;

        private static void createCustomers()
        {
            if (s_dal is null) return;

            Customer[] customers =
            [
                new Customer { CustomerId = 111111111, CustomerName = "יוסף כהן", Address = "רחוב הרב קוק 12, ירושלים", Phone = "050-1234567" },
            new Customer { CustomerId = 222222222, CustomerName = "דוד לוי", Address = "רחוב רבי עקיבא 45, בני ברק", Phone = "052-2345678" },
            new Customer { CustomerId = 333333333, CustomerName = "אברהם מזרחי", Address = "רחוב ירושלים 8, בית שמש", Phone = "053-3456789" },
            new Customer { CustomerId = 444444444, CustomerName = "משה ישראלי", Address = "רחוב הנביאים 20, מודיעין עילית", Phone = "054-4567890" },
            new Customer { CustomerId = 555555555, CustomerName = "יעקב אברהמי", Address = "רחוב השומר 3, אלעד", Phone = "058-5678901" }
            ];

            foreach (Customer c in customers)
                s_dal.Customer.Create(c);
        }

        private static void createProducts()
        {
            if (s_dal is null) return;

            Product[] products =
            [
                new Product { ProductId = 0, ProductName = "לחם אחיד", Category = "מאפים", Price = 7.5, count = 50 },
            new Product { ProductId = 0, ProductName = "חלב 3%", Category = "חלב", Price = 6.9, count = 40 },
            new Product { ProductId = 0, ProductName = "גבינה צהובה", Category = "חלב", Price = 24.0, count = 25 },
            new Product { ProductId = 0, ProductName = "שמן זית", Category = "מזווה", Price = 32.5, count = 15 },
            new Product { ProductId = 0, ProductName = "אורז בסמטי", Category = "מזווה", Price = 14.9, count = 30 }
            ];

            foreach (Product p in products)
                s_dal.Product.Create(p);
        }

        private static void createSales()
        {
            if (s_dal is null) return;

            List<Product> products = s_dal.Product.ReadAll();
            if (products.Count == 0) return;

            Sale[] sales =
            [
                new Sale { SaleId = 0, ProductId = products[0].ProductId, CountMinSale = 2, CountAllPrice = 12, Every = true, dateStart = DateTime.Now, dateEnd = DateTime.Now.AddDays(14) },
            new Sale { SaleId = 0, ProductId = products[1].ProductId, CountMinSale = 3, CountAllPrice = 18, Every = true, dateStart = DateTime.Now, dateEnd = DateTime.Now.AddDays(10) },
            new Sale { SaleId = 0, ProductId = products[2].ProductId, CountMinSale = 2, CountAllPrice = 42, Every = false, dateStart = DateTime.Now, dateEnd = DateTime.Now.AddDays(7) },
            new Sale { SaleId = 0, ProductId = products[3].ProductId, CountMinSale = 2, CountAllPrice = 55, Every = true, dateStart = DateTime.Now, dateEnd = DateTime.Now.AddDays(21) },
            new Sale { SaleId = 0, ProductId = products[4].ProductId, CountMinSale = 3, CountAllPrice = 38, Every = false, dateStart = DateTime.Now, dateEnd = DateTime.Now.AddDays(5) }
            ];

            foreach (Sale s in sales)
                s_dal.Sale.Create(s);
        }

        public static void Initialize(IDal dal)
        {
            s_dal = dal;
            createCustomers();
            createProducts();
            createSales();
        }
    }
}
