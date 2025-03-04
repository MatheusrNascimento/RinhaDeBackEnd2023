using FluentValidation.Results;
using RinhaDeBackEnd2023.Business.FluentValidations;
using RinhaDeBackEnd2023.Models;
using RinhaDeBackEnd2023.Business;
using StackExchange.Redis;
using RinhaDeBackEnd2023.Repository.Interfaces;
using RinhaDeBackEnd2023.Repository;
using RinhaDeBackEnd2023.Models.DTOs;

namespace RinhaDeBackEnd2023
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var redis =  ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis"));

            // Add services to the container.
            builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
            builder.Services.AddSingleton<IRedisCacheRepository, RedisCacheRepository>();
            builder.Services.AddSingleton<PessoaTRA>();
            builder.Services.AddAuthorization();


            // Add services to the container.

            var app = builder.Build();
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapPost("/pessoas", async (HttpContext context, PessoaDTO pessoa, IRedisCacheRepository _cache)  => {

                try
                {
                    ValidationResult result = new ValidateJsonRequest().Validate(pessoa);

                    if (!result.IsValid)
                    {
                        return Results.UnprocessableEntity(new
                        {
                            Errors = result.Errors.Select(e => e.ErrorMessage),
                        });
                    }

                    Pessoa person = Mapper.MapperJsonRequest.MapPersonFromJsonRequest(pessoa);

                    await new PessoaTRA(_cache).InsertNewPerson(person);

                    context.Response.Headers.Append("Lacation", $"/pessoas/{person.Id}");

                    return Results.Created();
                }
                catch (Exception ex){
                    return Results.BadRequest(ex.Message);
                }
            });

            app.MapGet("/pessoas/{id}", async (string id, IRedisCacheRepository _cache) =>
            {
                try
                {
                    Pessoa person = await new PessoaTRA(_cache).GetPersonById(id);

                    if (person is null)
                        return Results.Ok();

                    return Results.Ok(person);
                }
                catch (Exception)
                {
                    return Results.BadRequest();
                }
            });

            app.MapGet("/pessoas/t={t}", async ( IRedisCacheRepository _cache, string t) => {
                try
                {
                    IEnumerable<Pessoa> pessoa = await new PessoaTRA(_cache).GetPersonByTag(t);

                    return Results.Ok(pessoa);
                }
                catch (Exception)
                {
                    return Results.BadRequest();
                }
            });

            app.MapGet("/ping", () => Results.Ok("pong"));

            app.Run();
        }
    }
}