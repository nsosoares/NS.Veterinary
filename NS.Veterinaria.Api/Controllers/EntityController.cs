using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NS.Veterinaria.Api.Helpers;
using NS.Veterinaria.Api.Models;
using NS.Veterinary.Api.Interfaces;
using NS.Veterinary.Api.Notifications;
using NS.Veterinary.Api.ViewModels;

namespace NS.Veterinary.Api.Controllers
{
    public abstract class EntityController<TEntity, TViewModel, TRepository> 
        : MainController 
        where TEntity : Entity
        where TViewModel : EntityViewModel
        where TRepository : IRepository<TEntity>

    {
        protected readonly TRepository _repository;
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;


        protected EntityController(
            INotifier notifier
            , TRepository repository
            , IMapper mapper
            , IUnitOfWork unitOfWork) : base(notifier)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<TViewModel>> GetAsync()
            =>  _mapper.Map<IEnumerable<TEntity>, IEnumerable<TViewModel>>(await _repository.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<TViewModel> GetByIdAsync(Guid id)
            => _mapper.Map<TEntity, TViewModel>(await _repository.GetByIdAsync(id));

        protected async Task SaveChangesAsync()
        {
            if (await _unitOfWork.CommitAsync()) return;
            Notify(ErrorMessage.GetErrorMessageCommit());
        }
    }
}
