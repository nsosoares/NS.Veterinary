using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NS.Veterinaria.Api.Models;
using NS.Veterinary.Api.Interfaces;
using NS.Veterinary.Api.Notifications;
using NS.Veterinary.Api.ViewModels;

namespace NS.Veterinary.Api.Controllers
{
    public abstract class EntityBaseController<TEntityBase, TEntityBaseViewModel, TRepository>
        : EntityController<TEntityBase, TEntityBaseViewModel, TRepository>
        where TEntityBase : EntityBase
        where TEntityBaseViewModel : EntityBaseViewModel
        where TRepository : IRepository<TEntityBase>
    {
        protected EntityBaseController(
            INotifier notifier
            , TRepository repository
            , IMapper mapper
            , IUnitOfWork unitOfWork) : base(notifier, repository, mapper, unitOfWork)
        {
        }

        [HttpGet("GetByName/{name}")]
        public async Task<IEnumerable<TEntityBaseViewModel>> GetByNameAsync(string name = "")
        {
            if (name.Length == 0) 
                return _mapper.Map<IEnumerable<TEntityBase>, IEnumerable<TEntityBaseViewModel>>(await _repository.GetAllAsync());
            var entities = await _repository.QueryAsync(entityBase => entityBase.Name.Trim().ToLower().Contains(name.ToLower().Trim()));
            return _mapper.Map<IEnumerable<TEntityBase>, IEnumerable<TEntityBaseViewModel>>(entities);
        }
    }
}
