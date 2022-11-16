using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NS.Veterinary.Api.Models;
using NS.Veterinary.Api.Validations;
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
            ValidateModelState(modelState);
            if (!OperationIsValid())
                return BadRequest(new ResponseApi(false, _notifier.GetNotifications().ToList(), data));
            return Ok(new ResponseApi(true, _notifier.GetNotifications().ToList(), data));
        }

        private void ValidateModelState(ModelStateDictionary modelState)
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
            foreach (var errorMessage in validationResult.Errors)
                Notify(errorMessage.PropertyName, errorMessage.ErrorMessage);
        }

        protected void Notify(string message)
            => _notifier.Handle(new Notification(message));
        protected void Notify(string field, string message)
            => _notifier.Handle(new Notification(field, message));
    }
}
