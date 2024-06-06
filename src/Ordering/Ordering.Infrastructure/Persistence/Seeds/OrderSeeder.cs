using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;

namespace Ordering.Infrastructure.Persistence.Seeds
{
    public class OrderSeeder
    {
        public static async Task Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(
                new Order()
                {
                    Id = 1,
                    UserName = "manavarrp@gmail.com",
                    FirstName = "Mario",
                    LastName = "Navarro",
                    EmailAddress = "manavarrp@gmail.com",
                    Address = "Robledo",
                    TotalPrice = 100,
                    City = "Medallin",
                    CVV = "Test",
                    CardName = "Test",
                    CardNumber = "Test",
                    Expiration = "Test",
                    PaymentMethod = 1,
                    CreatedBy = "Test",
                    CreatedDate = DateTime.Now,
                    PhoneNumber = "01700000000",
                    State = "Test",
                    ZipCode = "Test"
                }
                );
        }
    }
}
