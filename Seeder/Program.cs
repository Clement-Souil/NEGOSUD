
using Seeder;

internal class Program
{
    private static void Main(string[] args)
    {
        var ss = new SeedService();
        ss.SeedDatabase();
    }
}