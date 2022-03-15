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
    public class TreatmentController : EntityController<Treatment, TreatmentViewModel, ITreatmentRepository>
    {
        public TreatmentController(
            INotifier notifier
            , ITreatmentRepository repository
            , IMapper mapper
            , IUnitOfWork unitOfWork) : base(notifier, repository, mapper, unitOfWork)
        {
        }

        [HttpGet("GetByVeterinarianId/{veterinarianId:guid}")]
        public async Task<IEnumerable<TreatmentViewModel>> GetByVeterinarianId(Guid veterinarianId)
          => _mapper.Map<IEnumerable<Treatment>, IEnumerable<TreatmentViewModel>>(await _repository.GetByVeterinarianIdAsync(veterinarianId));

        [HttpGet("GetByAnimalId/{animalId:guid}")]
        public async Task<IEnumerable<TreatmentViewModel>> GetByAnimalId(Guid animalId)
          => _mapper.Map<IEnumerable<Treatment>, IEnumerable<TreatmentViewModel>>(await _repository.GetByAnimalIdAsync(animalId));

        [HttpPost]
        public async Task<ActionResult<ResponseApi>> PostAsync([FromBody] TreatmentViewModel treatmentViewModel)
        {
            treatmentViewModel.ToGenerate();
            var treatment = _mapper.Map<TreatmentViewModel, Treatment>(treatmentViewModel);
            var isValid = await RunEntityValidationAsync(treatment, new TreatmentValidation());
            if (!isValid) return CustomResponse(treatmentViewModel);

            await _repository.RegisterAsync(treatment);
            await SaveChangesAsync();
            return CustomResponse(treatmentViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ResponseApi>> PutAsync([FromBody] TreatmentViewModel treatmentViewModel, Guid id)
        {
            if (treatmentViewModel.Id != id) return NotFound();
            var treatment = _mapper.Map<TreatmentViewModel, Treatment>(treatmentViewModel);
            var isValid = await RunEntityValidationAsync(treatment, new TreatmentValidation());
            if (!isValid) return CustomResponse(treatmentViewModel);

            _repository.Update(treatment);
            await SaveChangesAsync();
            return CustomResponse(treatmentViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ResponseApi>> DeleteAsync(Guid id)
        {
            var treatment = await _repository.GetByIdAsync(id);
            if (treatment == null) return NotFound();

            _repository.Delete(treatment);
            await SaveChangesAsync();
            return CustomResponse();
        }
    }
}
