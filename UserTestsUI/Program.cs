using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserTestsDL;
using UserTestsModels;
namespace UserTestsUI
{
   public  class Program
    {
        static void Main(string[] args)
        {
            

             var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            //setting up my db connections
            string connectionString = configuration.GetConnectionString("game");
            DbContextOptions<UserTestDBContext> options = new DbContextOptionsBuilder<UserTestDBContext>()
            .UseNpgsql(connectionString)
            .Options;
   
            var context = new UserTestDBContext(options);
  
            Console.WriteLine("Hello World!");
        
          
          
        }
    }
}
