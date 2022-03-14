using FluentValidation;
using NS.Veterinaria.Api.Helpers;
using NS.Veterinaria.Api.Models;

namespace NS.Veterinaria.Api.Validations
{
    public class TreatmentValidation : EntityValidation<Treatment>
    {
        protected override void Validate()
        {
            _validateVeterinary();
            _validateAnimal();
            _validateRecipe();
        }

        private void _validateVeterinary()
        {
            RuleFor(treatment => treatment.VeterinarianId)
                            .NotNull().WithMessage(ErrorMessage.GetErrorMessageIsEmptyOrNull("médico veterinario"));
        }

        private void _validateAnimal()
        {
            RuleFor(treatment => treatment.AnimalId)
                            .NotNull().WithMessage(ErrorMessage.GetErrorMessageIsEmptyOrNull("animal"));
        }

        private void _validateRecipe()
        {
            RuleFor(treatment => treatment.Recipe)
                            .MaximumLength(250).WithMessage(ErrorMessage.GetErrorMessageMaxLenght("receita", 250));
        }
    }
}
