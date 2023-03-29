using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext context;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly UserManager<StoreUser> userManager;

        public DutchSeeder(DutchContext context, IWebHostEnvironment hostEnvironment, UserManager<StoreUser> userManager)
        {
            this.context = context;
            this.hostEnvironment = hostEnvironment;
            this.userManager = userManager;
        }

        public void Seed()
        {
            context.Database.EnsureCreated();

            if (!context.Products.Any())
            {
                var filePath = "C:\\Users\\kajga\\source\\repos\\DutchTreat\\Data\\art.json";
                var json = File.ReadAllText(filePath);

                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);
                context.Products.AddRange(products);
                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "1000",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product=products.First(),
                            Quantity=5,
                            UnitPrice=products.First().Price
                        }
                    }
                };

                context.Orders.Add(order);

                context.SaveChanges();

            }
        }





            public async Task SeedAsync()
        {
            context.Database.EnsureCreated();

            StoreUser user = await userManager.FindByEmailAsync("shawn@yahoo.com");


            if (user == null) 
            {
                user=new StoreUser() 
                {
                    FirstName="Luke",
                    LastName="Shaw",
                    Email= "shawn@yahoo.com",
                    UserName = "shawn@yahoo.com"
                };


                var result = await userManager.CreateAsync(user,"P@ssw0rd!");
                if (!result.Succeeded) 
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }


            }


            if (!context.Products.Any())
            {
                var filePath = "C:\\Users\\kajga\\source\\repos\\DutchTreat\\Data\\art.json";
                var json = File.ReadAllText(filePath);

                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);
                context.Products.AddRange(products);


                var order = await context.Orders.Where(o => o.Id == 1).FirstOrDefaultAsync();

                if (order != null) 
                {
                    order.User=user;
                    order.Items=new List<OrderItem>() 
                    {
                        new OrderItem()
                        {
                            Product=products.First(),
                            Quantity=5,
                            UnitPrice=products.First().Price
                        }
                    };
                }

                context.Orders.Add(order);
                context.SaveChanges();

            }

        }
    }
}