using FluentValidation.Results;
using RinhaDeBackEnd2023.Business.FluentValidations;
using RinhaDeBackEnd2023.Models.JsonRequest;
using RinhaDeBackEnd2023.Models;
using RinhaDeBackEnd2023.Repository;
using RinhaDeBackEnd2023.Business;

namespace RinhaDeBackEnd2023
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapPost("/Pessoas", async (HttpContext context, PersonJsonRequest pessoa) =>
            {

                ValidationResult result = new ValidateJsonRequest().Validate(pessoa);

                if (!result.IsValid)
                {
                    return Results.UnprocessableEntity(new
                    {
                        Errors = result.Errors.Select(e => e.ErrorMessage),
                    });
                }

                Person person = Mapper.MapperJsonRequest.MapPersonFromJsonRequest(pessoa);

                await PersonTRA.InsertNewPerson(person);
                

                context.Response.Headers.Append("Lacation", $"/pessoas/{person.Id}");

                return Results.Created();
            });

            app.Run();
        }
    }
}