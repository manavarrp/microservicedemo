using Dapper;
using Discount.Grpc.Models;
using Microsoft.AspNetCore.Server.IIS.Core;
using Npgsql;

namespace Discount.Grpc.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly IConfiguration _configuration;

        public CouponRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Coupon> GetDiscount(string productId)
        {
            var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductId=@ProductId", new { ProductId  = productId});
            if (coupon == null) 
            {
                return new Coupon() { Amount = 0, ProductName = "Sin Descuento" };
            }
            return coupon;

        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
            // Verificar si ya existe un cupón para el producto proporcionado
            var existingCoupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
                "SELECT * FROM Coupon WHERE ProductId = @ProductId",
                new { ProductId = coupon.ProductId });
            if (existingCoupon != null)
            {
                throw new InvalidOperationException("Ya existe un descuento para este producto.");
            }
            var recordsAffected = await connection.ExecuteAsync
                ("INSERT INTO Coupon(ProductId,ProductName,Description,Amount) VALUES(@ProductId,@ProductName,@Description,@Amount)",
                new {ProductId=coupon.ProductId, ProductName=coupon.ProductName, Description=coupon.Description, Amount= coupon.Amount});
            if (recordsAffected > 0)
            {
              
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
            var recordsAffected = await connection.ExecuteAsync
                ("UPDATE  Coupon SET ProductId=@ProductId,ProductName = @ProductName,Description =@Description,Amount = @Amount WHERE ProductId=@ProductId",
                new { ProductId = coupon.ProductId, ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
            if (recordsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteDiscount(string productId)
        {
            var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
            var recordsAffected = await connection.ExecuteAsync
               ("DELETE FROM Coupon WHERE ProductId=@ProductId ", new { ProductId = productId});
            if (recordsAffected > 0)
            {
                return true;
            }
            return false;
        }


        
    }
}
