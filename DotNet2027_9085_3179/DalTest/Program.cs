using DalApi;
using DalFacade.Do;
using DO;

namespace DalTest;

internal class Program
{
    private static readonly IDal s_dal;

    private static void Main(string[] args)
    {
        try
        {
            Initialization.Initialize(s_dal);

            int choice;
            do
            {
                choice = PrintMainMenu();
                switch (choice)
                {
                    case 1:
                        CustomerMenu();
                        break;
                    case 2:
                        ProductMenu();
                        break;
                    case 3:
                        SaleMenu();
                        break;
                    case 0:
                        Console.WriteLine("יציאה מהתוכנית.");
                        break;
                    default:
                        Console.WriteLine("בחירה לא תקינה.");
                        break;
                }
            } while (choice != 0);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"שגיאה: {ex.Message}");
        }

    }
    public static int PrintMainMenu()
    {
        Console.WriteLine("\n--- תפריט ראשי ---");
        Console.WriteLine("1. לקוחות (Customer)");
        Console.WriteLine("2. מוצרים (Product)");
        Console.WriteLine("3. מבצעים (Sale)");
        Console.WriteLine("0. יציאה");
        Console.Write("בחר אפשרות: ");
        int.TryParse(Console.ReadLine(), out int choice);
        return choice;
    }

    public static int PrintSubMenu(string item)
    {
        Console.WriteLine($"\n--- תפריט {item} ---");
        Console.WriteLine("1. הוספה (Create)");
        Console.WriteLine("2. קריאת פריט לפי מזהה (Read)");
        Console.WriteLine("3. קריאת כל הפריטים (ReadAll)");
        Console.WriteLine("4. עדכון (Update)");
        Console.WriteLine("5. מחיקה (Delete)");
        Console.WriteLine("0. חזרה לתפריט הראשי");
        Console.Write("בחר פעולה: ");
        int.TryParse(Console.ReadLine(), out int choice);
        return choice;
    }

    private static void CustomerMenu()
    {
        int choice;
        do
        {
            choice = PrintSubMenu("Customer");
            switch (choice)
            {
                case 1: AddCustomer(); break;
                case 2: Read<Customer>(s_dal.Customer); break;
                case 3: ReadAll<Customer>(s_dal.Customer); break;
                case 4: UpdateCustomer(); break;
                case 5: Delete<Customer>(s_dal.Customer); break;
                case 0: break;
                default: Console.WriteLine("בחירה לא תקינה."); break;
            }
        } while (choice != 0);
    }

    private static void ProductMenu()
    {
        int choice;
        do
        {
            choice = PrintSubMenu("Product");
            switch (choice)
            {
                case 1: AddProduct(); break;
                case 2: Read<Product>(s_dal.Product); break;
                case 3: ReadAll<Product>(s_dal.Product); break;
                case 4: UpdateProduct(); break;
                case 5: Delete<Product>(s_dal.Product); break;
                case 0: break;
                default: Console.WriteLine("בחירה לא תקינה."); break;
            }
        } while (choice != 0);
    }

    private static void SaleMenu()
    {
        int choice;
        do
        {
            choice = PrintSubMenu("Sale");
            switch (choice)
            {
                case 1: AddSale(); break;
                case 2: Read<Sale>(s_dal.Sale); break;
                case 3: ReadAll<Sale>(s_dal.Sale); break;
                case 4: UpdateSale(); break;
                case 5: Delete<Sale>(s_dal.Sale); break;
                case 0: break;
                default: Console.WriteLine("בחירה לא תקינה."); break;
            }
        } while (choice != 0);
    }
    private static Customer AskCustomer(int id = 0)
    {
        if (id == 0)
        {
            Console.Write("מזהה לקוח: ");
            int.TryParse(Console.ReadLine(), out id);
        }

        Console.Write("שם לקוח: ");
        string name = Console.ReadLine() ?? string.Empty;

        Console.Write("כתובת: ");
        string address = Console.ReadLine() ?? string.Empty;

        Console.Write("טלפון: ");
        string phone = Console.ReadLine() ?? string.Empty;

        return new Customer
        {
            CustomerId = id,
            CustomerName = name,
            Address = address,
            Phone = phone
        };
    }

    private static Product AskProduct(int id = 0)
    {
        Console.Write("שם מוצר: ");
        string name = Console.ReadLine() ?? string.Empty;

        Console.Write("קטגוריה: ");
        string category = Console.ReadLine() ?? string.Empty;

        Console.Write("מחיר: ");
        double.TryParse(Console.ReadLine(), out double price);

        Console.Write("כמות: ");
        int.TryParse(Console.ReadLine(), out int count);

        return new Product
        {
            ProductId = id,
            ProductName = name,
            Category = category,
            Price = price,
            count = count
        };
    }

    private static Sale AskSale(int id = 0)
    {
        Console.Write("מזהה מוצר: ");
        int.TryParse(Console.ReadLine(), out int productId);

        Console.Write("כמות מינימלית למבצע: ");
        int.TryParse(Console.ReadLine(), out int countMinSale);

        Console.Write("מחיר כולל: ");
        int.TryParse(Console.ReadLine(), out int countAllPrice);

        Console.Write("האם לכלל הלקוחות (true/false): ");
        bool.TryParse(Console.ReadLine(), out bool every);

        Console.Write("תאריך התחלה: ");
        DateTime.TryParse(Console.ReadLine(), out DateTime start);

        Console.Write("תאריך סיום: ");
        DateTime.TryParse(Console.ReadLine(), out DateTime end);

        return new Sale
        {
            SaleId = id,
            ProductId = productId,
            CountMinSale = countMinSale,
            CountAllPrice = countAllPrice,
            Every = every,
            dateStart = start,
            dateEnd = end
        };
    }

    private static void AddCustomer()
    {
        try
        {
            Customer c = AskCustomer();
            int id = s_dal.Customer.Create(c);
            Console.WriteLine($"נוסף לקוח: {id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void AddProduct()
    {
        try
        {
            Product p = AskProduct();
            int id = s_dal.Product.Create(p);
            Console.WriteLine($"נוסף מוצר: {id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void AddSale()
    {
        try
        {
            Sale s = AskSale();
            int id = s_dal.Sale.Create(s);
            Console.WriteLine($"נוסף מבצע: {id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private static void UpdateCustomer()
    {
        try
        {
            Console.Write("מזהה לקוח לעדכון: ");
            int.TryParse(Console.ReadLine(), out int id);
            Customer c = AskCustomer(id);
            s_dal.Customer.Update(c);
            Console.WriteLine("הלקוח עודכן.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void UpdateProduct()
    {
        try
        {
            Console.Write("מזהה מוצר לעדכון: ");
            int.TryParse(Console.ReadLine(), out int id);
            Product p = AskProduct(id);
            s_dal.Product.Update(p);
            Console.WriteLine("המוצר עודכן.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void UpdateSale()
    {
        try
        {
            Console.Write("מזהה מבצע לעדכון: ");
            int.TryParse(Console.ReadLine(), out int id);
            Sale s = AskSale(id);
            s_dal.Sale.Update(s);
            Console.WriteLine("המבצע עודכן.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void Read<T>(ICrud<T> crud)
    {
        try
        {
            Console.Write("מזהה: ");
            int.TryParse(Console.ReadLine(), out int id);
            T? item = crud.Read(id);
            Console.WriteLine(item is not null ? item.ToString() : "לא נמצא פריט.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void ReadAll<T>(ICrud<T> crud)
    {
        try
        {
            foreach (T item in crud.ReadAll())
                Console.WriteLine(item);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void Delete<T>(ICrud<T> crud)
    {
        try
        {
            Console.Write("מזהה למחיקה: ");
            int.TryParse(Console.ReadLine(), out int id);
            crud.Delete(id);
            Console.WriteLine("הפריט נמחק.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
