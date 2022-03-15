using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NS.Veterinary.Api.Models;
using NS.Veterinary.Api.Validations;
using NS.Veterinary.Api.Interfaces;
using NS.Veterinary.Api.Notifications;
using NS.Veterinary.Api.ViewModels;

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
        public async Task<ActionResult<ResponseApi>> PostAsync([FromBody] AnimalViewModel animalViewModel)
        {
            animalViewModel.ToGenerate();
            var animal = _mapper.Map<AnimalViewModel, Animal>(animalViewModel);
            var isValid = await RunEntityValidationAsync(animal, new AnimalValidation());
            if (!isValid) return CustomResponse(animalViewModel);

            await _repository.RegisterAsync(animal);
            await SaveChangesAsync();
            return CustomResponse(animalViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ResponseApi>> PutAsync([FromBody] AnimalViewModel animalViewModel, Guid id)
        {
            if(animalViewModel.Id != id) return NotFound();
            var animal = _mapper.Map<AnimalViewModel, Animal>(animalViewModel);
            var isValid = await RunEntityValidationAsync(animal, new AnimalValidation());
            if (!isValid) return CustomResponse(animalViewModel);

            _repository.Update(animal);
            await SaveChangesAsync();
            return CustomResponse(animalViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ResponseApi>> DeleteAsync(Guid id)
        {
            var animal = await _repository.GetByIdAsync(id);
            if(animal == null) return NotFound();

            _repository.Delete(animal);
            await SaveChangesAsync();
            return CustomResponse();
        }
    }
}
