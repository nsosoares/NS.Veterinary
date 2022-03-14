using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NS.Veterinaria.Api.Models;
using NS.Veterinaria.Api.Validations;
using NS.Veterinary.Api.Notifications;
using NS.Veterinary.Api.ViewModels;

namespace NS.Veterinary.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected readonly INotifier _notifier;
        protected MainController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected ActionResult<ResponseApi> CustomResponse(ModelStateDictionary modelState, object data = null)
        {
            _validateModelState(modelState);
            if (!OperationIsValid())
                return BadRequest(new ResponseApi(false, _notifier.GetNotifications().ToList(), data));
            return Ok(new ResponseApi(true, _notifier.GetNotifications().ToList(), data));
        }

        private void _validateModelState(ModelStateDictionary modelState)
        {
            var errorsInModelState = modelState.Values.SelectMany(value => value.Errors);
            foreach (var error in errorsInModelState)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected ActionResult<ResponseApi> CustomResponse(object data = null)
        {
            if (!OperationIsValid())
                return BadRequest(new ResponseApi(false, _notifier.GetNotifications().ToList(), data));
            return Ok(new ResponseApi(true, _notifier.GetNotifications().ToList(), data));
        }

        protected bool OperationIsValid() => !_notifier.HasNotification();

        protected async Task<bool> RunEntityValidationAsync<TEntity, TValidation>(TEntity entity, TValidation validation) 
            where TEntity : Entity
            where TValidation : EntityValidation<TEntity>
        {
            var isValid = await validation.RunValidationAsync(entity);
            if (isValid) return true;

            NotifyValidationsErrors(validation.ValidationResult);
            return false;
        }

        protected void NotifyValidationsErrors(ValidationResult validationResult)
        {
            var errorsMessages = validationResult.Errors.Select(error => error.ErrorMessage);
            foreach (var errorMessage in errorsMessages)
                Notify(errorMessage);
        }

        protected void Notify(string message)
            => _notifier.Handle(new Notification(message));
    }
}
