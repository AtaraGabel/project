

namespace Dal
{
    internal static class DataSource
    {
        
        internal static List<Product?> Products { get; } = new();
        internal static List<Sale?> Sales { get; } = new();
        internal static List<Client?> Clients { get; } = new();

        internal static class Config
        {           
            internal const int StartSaleCode = 100;
            private static int _nextSaleCode = StartSaleCode;
            internal static int NextSaleCode => _nextSaleCode++;
        }
    }
}
