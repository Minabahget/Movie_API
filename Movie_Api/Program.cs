
using Microsoft.EntityFrameworkCore;
using Movie_Api.DTO;
using Movie_Api.Models;
using Movie_Api.Repository;

namespace Movie_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(option => option.AddPolicy("MyPolicy", corsPolicy =>
            {
                corsPolicy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();

            }));
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<MovieDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
            builder.Services.AddScoped<CategoryRepoService>();
            builder.Services.AddScoped<MovieRepoService>();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseAuthorization();

            app.UseCors("MyPolicy");
            app.MapControllers();



            app.Run();
        }
    }
}
