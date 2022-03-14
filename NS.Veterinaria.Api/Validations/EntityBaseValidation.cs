using FluentValidation;
using NS.Veterinaria.Api.Helpers;
using NS.Veterinaria.Api.Models;

namespace NS.Veterinaria.Api.Validations
{
    public abstract class EntityBaseValidation<TEntityBase> : EntityValidation<TEntityBase> 
        where TEntityBase : EntityBase
    {
        protected override void Validate()
        {
            _validateName();
        }

        private void _validateName()
        {
            RuleFor(entityBase => entityBase.Name)
                            .NotEmpty().WithMessage(ErrorMessage.GetErrorMessageIsEmptyOrNull("nome"))
                            .NotNull().WithMessage(ErrorMessage.GetErrorMessageIsEmptyOrNull("nome"))
                            .MaximumLength(50).WithMessage(ErrorMessage.GetErrorMessageMaxLenght("nome", 50))
                            .MinimumLength(3).WithMessage(ErrorMessage.GetErrorMessageMinLenght("nome", 3));
        }
    }
}
