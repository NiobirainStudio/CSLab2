using DB;
using DB.Services;
using DB.Models;
using System.Reflection;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            var context = new AppDbContext();
            var serviceManager = new ServiceManager();
            serviceManager.AddDataService(new GenreDataService(context));


            foreach(var e in serviceManager.GetCurrentService().typesInfo)
            {
                Console.WriteLine(e);
            }
        }
    }
}