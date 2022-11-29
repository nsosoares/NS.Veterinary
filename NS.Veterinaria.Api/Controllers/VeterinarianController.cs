using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NS.Veterinary.Api.Models;
using NS.Veterinary.Api.Validations;
using NS.Veterinary.Api.Interfaces;
using NS.Veterinary.Api.Notifications;
using NS.Veterinary.Api.ViewModels;
using NS.Veterinary.Api.Data.FluentApis;
using ErrorOr;

namespace NS.Veterinary.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class VeterinarianController : EntityBaseController<Veterinarian, VeterinarianViewModel, IVeterinarianRepository>
    {
        public VeterinarianController(
            INotifier notifier
            , IVeterinarianRepository repository
            , IMapper mapper
            , IUnitOfWork unitOfWork) 
            : base(notifier, repository, mapper, unitOfWork)
        {
        }

        [HttpPost]
        public async Task<ActionResult<ResponseApi>> PostAsync([FromBody] VeterinarianViewModel veterinarianViewModel)
        {
            veterinarianViewModel.ToGenerate();
            var veterinarian = _mapper.Map<VeterinarianViewModel, Veterinarian>(veterinarianViewModel);
            var validationResult = await RunEntityValidationAsync(veterinarian, new VeterinarianValidation());
            if (validationResult.IsError) return Problem(validationResult.Errors);

            await _repository.RegisterAsync(veterinarian);
            await SaveChangesAsync();
            return Ok(veterinarianViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ResponseApi>> PutAsync([FromBody] VeterinarianViewModel veterinarianViewModel, Guid id)
        {
            if (veterinarianViewModel.Id != id) return Problem(Error.NotFound());
            var veterinarian = _mapper.Map<VeterinarianViewModel, Veterinarian>(veterinarianViewModel);
            var validationResult = await RunEntityValidationAsync(veterinarian, new VeterinarianValidation());
            if (validationResult.IsError) return Problem(validationResult.Errors);

            _repository.Update(veterinarian);
            await SaveChangesAsync();
            return Ok(veterinarianViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ResponseApi>> DeleteAsync(Guid id)
        {
            var veterinarian = await _repository.GetByIdAsync(id);
            if (veterinarian == null) return Problem(Error.NotFound());

            _repository.Delete(veterinarian);
            await SaveChangesAsync();
            return Ok();
        }
    }
}
