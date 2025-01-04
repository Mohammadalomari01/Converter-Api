using Converter.Core.Common;
using Converter.Core.Repository;
using Converter.Core.Services;
using Converter.Infra.Common;
using Converter.Infra.Services;
using Microsoft.EntityFrameworkCore;
using Aspose.Words; // Add this to include Aspose.Words namespace

namespace Converter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //License license = new License();
            //license.SetLicense("Aspose.Words.lic"); 
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //DBContext 
            builder.Services.AddScoped<IDbContext, Converter.Infra.Common.DbContext>();
            builder.Services.AddScoped<IRoleRepository, Converter.Infra.Repository.RoleRepository>();
            builder.Services.AddScoped<IUserLoginRepository, Converter.Infra.Repository.UserLoginRepository>();
            builder.Services.AddScoped<IUsersRepository, Converter.Infra.Repository.UsersRepository>();
            builder.Services.AddScoped<ICONVERSIONHISTORYRepository, Converter.Infra.Repository.CONVERSIONHISTORYRepository>();

            //Services
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<ICONVERSIONHISTORYService, CONVERSIONHISTORYService>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IUserLoginService, UserLoginService>();

            // Add services to the container.
            builder.Services.AddControllers();
            // Register HttpClient for dependency injection
            builder.Services.AddHttpClient();
            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", policy =>
                {
                    policy.WithOrigins("http://localhost:4200") // Angular development server URL
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            var app = builder.Build();
            app.UseCors("AllowAngularApp");
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
