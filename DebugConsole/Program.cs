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

            serviceManager.AddDataService(new ArtistDataService(context));
            serviceManager.AddDataService(new GenreDataService(context));
            serviceManager.AddDataService(new AlbumDataService(context));

            serviceManager.InitSpace();

            foreach(var a in serviceManager.modelSpace)
            {
                Console.WriteLine("Variables:");
                foreach (var b in a.Value.variables)
                {
                    Console.WriteLine($"{b.Key} {b.Value}");
                }

                Console.WriteLine("\nExternal keys:");
                foreach(var b in a.Value.externalKeys)
                {
                    Console.WriteLine($"{b.Key} {b.Value}");
                }

                Console.WriteLine("\n");
            }
        }
    }
}