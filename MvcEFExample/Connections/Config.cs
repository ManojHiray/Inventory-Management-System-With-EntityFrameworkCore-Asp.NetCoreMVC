namespace MvcEFExample.Connections
{
    public class Config
    {
        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);

            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:InventoryDatabase");
            return (constring);
        }
    }
}
