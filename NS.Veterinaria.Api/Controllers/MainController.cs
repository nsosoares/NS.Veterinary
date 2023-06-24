using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NS.Veterinary.Api.Models;
using NS.Veterinary.Api.Validations;
using NS.Veterinary.Api.Notifications;
using NS.Veterinary.Api.ViewModels;
using ErrorOr;
using FluentValidation.Results;

namespace NS.Veterinary.Api.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public abstract class MainController : ControllerBase
    {
        protected readonly INotifier _notifier;
        protected MainController(INotifier notifier)
        {
            _notifier = notifier;
        }

        [ProducesErrorResponseType(typeof(ProblemDetails))]
        protected ActionResult Problem(List<Error> errors)
        {
            var firstError = errors.FirstOrDefault();
            var statusCode = firstError.Type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(statusCode: (int)statusCode, title: firstError.Description);
        }

        [ProducesErrorResponseType(typeof(ProblemDetails))]
        protected ActionResult Problem(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };
            return Problem(statusCode: statusCode, title: error.Description);
        }

        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        protected ActionResult Problem()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if(!OperationIsValid())
                return Problem(_notifier.GetNotifications().ToList());
            return Ok();
        }

        protected bool OperationIsValid() => !_notifier.HasNotification();

        [Obsolete]
        protected async Task<ErrorOr<Success>> RunEntityValidationAsync<TEntity, TValidation>(TEntity entity, TValidation validation) 
            where TEntity : Entity
            where TValidation : EntityValidation<TEntity>
        {
            await validation.RunValidationAsync(entity);
            if (validation.ValidationResult.IsValid) return Result.Success;
            return validation.ValidationResult.Errors
                .Select(error => Error.Validation(code: error.ErrorCode, description: error.ErrorMessage))
                .ToList();
        }
        protected async Task<ValidationResult> EntityValidationAsync<TEntity, TValidation>(TEntity entity, TValidation validation)
           where TEntity : Entity
           where TValidation : EntityValidation<TEntity>
        {
            await validation.RunValidationAsync(entity);
            return validation.ValidationResult;
        }

        protected void Notify(Error error)
            => _notifier.Handle(error);
    }
}
