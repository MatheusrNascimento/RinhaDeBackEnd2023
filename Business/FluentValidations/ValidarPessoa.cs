using FluentValidation;
using RinhaDeBackEnd2023.Models;
using RinhaDeBackEnd2023.Models.JsonRequest;
using System.Globalization;

namespace RinhaDeBackEnd2023.Business.FluentValidations
{
    public class ValidateJsonRequest : AbstractValidator<PessoaJsonRequest>
    {
        public ValidateJsonRequest()
        {
            RuleFor(x => x.nome).NotEmpty().NotNull();
            RuleFor(x => x.apelido).NotNull().WithMessage("Informe um apelido").NotEmpty().WithMessage("Apelido não pode ser vazio");

            RuleFor(x => x.stack).Must((tag) => { 
                foreach(string item in tag)
                {
                    if (item.Length > 32 || item.Length == 0)
                        return false;
                }
                return true;
            }).WithMessage("É valido apenas 32 caracteres por tag no campo stack");

             RuleFor(p => p.nascimento)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
            .Must(predicate: BeAValidDate).WithMessage("A data deve estar no formato yyyy-MM-dd.");
        }
        private bool BeAValidDate(string date)
        {
            return DateTime.TryParseExact(
                date,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _
            );
        }
    }
}
