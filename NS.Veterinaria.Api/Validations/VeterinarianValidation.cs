using FluentValidation;
using NS.Veterinaria.Api.Helpers;
using NS.Veterinaria.Api.Models;

namespace NS.Veterinaria.Api.Validations
{
    public class VeterinarianValidation : EntityBaseValidation<Veterinarian>
    {
        protected override void Validate()
        {
            base.Validate();
            _validateCrmv();
        }

        private void _validateCrmv()
        {
            RuleFor(veterinarian => veterinarian.Crmv)
                           .NotEmpty().WithMessage(ErrorMessage.GetErrorMessageIsEmptyOrNull("CRMV"))
                           .NotNull().WithMessage(ErrorMessage.GetErrorMessageIsEmptyOrNull("CRMV"))
                           .MaximumLength(50).WithMessage(ErrorMessage.GetErrorMessageMaxLenght("CRMV", 50))
                           .MinimumLength(3).WithMessage(ErrorMessage.GetErrorMessageMinLenght("CRMV", 3));
        }
    }
}
