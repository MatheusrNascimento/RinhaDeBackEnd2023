using FluentValidation;
using FluentValidation.Results;
using RinhaDeBackEnd2023.Models.JsonRequest;

namespace RinhaDeBackEnd2023.Business
{
    public class ValidateJsonRequest : AbstractValidator<PersonJsonRequest>
    {
        public ValidationResult ValidatePersonJsonRequest(PersonJsonRequest person)
        {
            ValidateJsonRequest response = new ValidateJsonRequest();

            RuleFor(x => x.nome).NotNull().NotEmpty();
            RuleFor(x => x.apelido).NotNull().NotEmpty();
            RuleFor(x => x.stack).NotNull().NotEmpty();
            RuleFor(x => x.nascimento)
            .Must(data => DateTime.TryParseExact(data, "yyyy/MM/dd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out _))
            .WithMessage("A data deve estar no formato dd/MM/yyyy.");

            return response.Validate(person);
        }
    }
}
