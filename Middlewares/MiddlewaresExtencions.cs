using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RinhaDeBackEnd2023.Business;
using RinhaDeBackEnd2023.Repository;
using RinhaDeBackEnd2023.Repository.Interfaces;
using StackExchange.Redis;

namespace RinhaDeBackEnd2023
{
    public static class Middlewares
    {
        public static void ConfigureMiddlewares(this WebApplicationBuilder builder)
        {
            var redis = ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis"));

            builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
            builder.Services.AddSingleton<IRedisCacheRepository, RedisCacheRepository>();
            builder.Services.AddScoped<PersonTRA>();
            builder.Services.AddAuthorization();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

        }

        public static void ConfigureApp(this WebApplication app)
        {
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthorization();
        }

    }
}