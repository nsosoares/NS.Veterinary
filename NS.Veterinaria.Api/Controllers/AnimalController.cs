using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NS.Veterinary.Api.Models;
using NS.Veterinary.Api.Validations;
using NS.Veterinary.Api.Interfaces;
using NS.Veterinary.Api.Notifications;
using NS.Veterinary.Api.ViewModels;
using ErrorOr;
using NS.Veterinary.Api.Data.FluentApis;
using System.Linq;
using NS.Veterinary.Api.Extensions;

namespace NS.Veterinary.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AnimalController : EntityBaseController<Animal, AnimalViewModel, IAnimalRepository>
    {
        public AnimalController(INotifier notifier, IAnimalRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
            : base(notifier, repository, mapper, unitOfWork)
        {
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] AnimalViewModel animalViewModel)
        {
            animalViewModel.ToGenerate();
            var animal = _mapper.Map<AnimalViewModel, Animal>(animalViewModel);
            var validationResult = await EntityValidationAsync(animal, new AnimalValidation());
            if (!validationResult.IsValid) return ValidationProblem(new ValidationProblemDetails(validationResult.Errors.ToDictionary()));

            await _repository.RegisterAsync(animal);
            await SaveChangesAsync();
            return Ok(animalViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> PutAsync([FromBody] AnimalViewModel animalViewModel, Guid id)
        {
            if(animalViewModel.Id != id) return Problem(Error.NotFound());
            var animal = _mapper.Map<AnimalViewModel, Animal>(animalViewModel);
            var validationResult = await EntityValidationAsync(animal, new AnimalValidation());
            if (!validationResult.IsValid) return ValidationProblem(new ValidationProblemDetails(validationResult.Errors.ToDictionary()));

            _repository.Update(animal);
            await SaveChangesAsync();
            return Ok(animalViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var animal = await _repository.GetByIdAsync(id);
            if(animal == null) return Problem(Error.NotFound());

            _repository.Delete(animal);
            await SaveChangesAsync();
            return Ok();
        }
    }
}
