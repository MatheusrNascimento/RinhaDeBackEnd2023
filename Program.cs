using FluentValidation.Results;
using RinhaDeBackEnd2023.Business.FluentValidations;
using RinhaDeBackEnd2023.Models;
using RinhaDeBackEnd2023.Business;
using RinhaDeBackEnd2023.Repository.Interfaces;
using RinhaDeBackEnd2023.Models.DTOs;

namespace RinhaDeBackEnd2023
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.ConfigureMiddlewares();

            var app = builder.Build();
            app.ConfigureApp();
           

            app.MapPost("/person", async (HttpContext context, PersonTRA personTRA, jsonPersonRequest personRequest)  => {

                try
                {
                    ValidationResult result = new ValidateJsonRequest().Validate(personRequest);

                    if (!result.IsValid)
                    {
                        return Results.UnprocessableEntity(new
                        {
                            Errors = result.Errors.Select(e => e.ErrorMessage),
                        });
                    }

                    Person person = Mapper.MapperJsonRequest.MapPersonFromJsonRequest(personRequest);

                    await personTRA.InsertNewPerson(person);

                    context.Response.Headers.Append("Lacation", $"/persons/{person.Id}");

                    return Results.Created();
                }
                catch (Exception ex){
                    return Results.BadRequest(ex.Message);
                }
            });

            app.MapGet("/person/{id}", async (string id, PersonTRA personTRA) =>
            {
                try
                {
                    Person person = await personTRA.GetPersonById(id);

                    if (person is null)
                        return Results.Ok();

                    return Results.Ok(person);
                }
                catch (Exception)
                {
                    return Results.BadRequest();
                }
            });

            app.MapGet("/person/t={t}", async (PersonTRA personTRA, string t) => {
                try
                {
                    IEnumerable<Person> person = await personTRA.GetPersonByTag(t);

                    return Results.Ok(person);
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