
using BLL;
using DAL;
using DTO;
using IBL;
using IDAL;

namespace Grocery
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost",
                    builder => builder
                        .WithOrigins("http://localhost:3000")  
                        .AllowAnyMethod()  
                        .AllowAnyHeader()); 
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IUserBL, UserBL>();
            builder.Services.AddScoped<IUserDal,UserDal>();
            builder.Services.AddScoped<ISupplierBL,SupplierBL>();
            builder.Services.AddScoped<ISupplierDal, SupplierDal>();
            builder.Services.AddScoped<IItemBL, ItemBL>();
            builder.Services.AddScoped<IItemDal, ItemDal>();
            builder.Services.AddScoped<IMerchandiseBL, MerchandiseBL>();
            builder.Services.AddScoped<IMerchandiseDal, MerchandiseDal>();
            builder .Services.AddScoped<IOrderDetailBL,OrderDetailsBL>();
            builder.Services.AddScoped<IOrderDetailsDal,OrderDetailDal>();
            builder.Services.AddScoped<IItemInOrderBL, ItemInOrderBL>();
            builder.Services.AddScoped<IItemInOrderDal, ItemInOrderDal>();
            builder.Services.AddScoped<ISupplierAndmerchandiseBL , SupplierAndmerchandiseBL>();
            builder.Services.AddScoped<ISupplierAndmerchandiseDal, SupplierAndmerchandiseDal>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowLocalhost");
            app.MapControllers();

            app.Run();
        }
    }
}
