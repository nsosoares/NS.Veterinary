using FluentValidation;
using NS.Veterinary.Api.Helpers;
using NS.Veterinary.Api.Models;

namespace NS.Veterinary.Api.Validations
{
    public class AnimalValidation : EntityBaseValidation<Animal>
    {
        protected override void Validate()
        {
            base.Validate();
            _validateAge();
            _validateRace();
            _validateSize();
        }

        private void _validateAge()
        {
            RuleFor(animal => animal.Age)
                              .NotNull().WithMessage(ErrorMessage.GetErrorMessageIsEmptyOrNull("idade"));
        }

        private void _validateRace()
        {
            RuleFor(animal => animal.Race)
                .NotEmpty().WithMessage(ErrorMessage.GetErrorMessageIsEmptyOrNull("Raça"))
                            .NotNull().WithMessage(ErrorMessage.GetErrorMessageIsEmptyOrNull("Raça"))
                            .MaximumLength(50).WithMessage(ErrorMessage.GetErrorMessageMaxLenght("Raça", 50))
                            .MinimumLength(3).WithMessage(ErrorMessage.GetErrorMessageMinLenght("Raça", 3));
        }

        private void _validateSize()
        {
            RuleFor(animal => animal.Size)
                             .NotNull().WithMessage(ErrorMessage.GetErrorMessageIsEmptyOrNull("Porte"));
        }
    }
}
