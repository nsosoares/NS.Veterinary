using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NS.Veterinary.Api.Helpers;
using NS.Veterinary.Api.Models;
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
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public async Task<IEnumerable<TViewModel>> GetAsync()
            =>  _mapper.Map<IEnumerable<TEntity>, IEnumerable<TViewModel>>(await _repository.GetAllAsync());

        [HttpGet("{id:guid}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public async Task<TViewModel> GetByIdAsync(Guid id)
            => _mapper.Map<TEntity, TViewModel>(await _repository.GetByIdAsync(id));

        //[HttpGet("error")]
        //public async Task<TViewModel> ErrorTest()
        //    => throw new Exception("Ocorreu um erro");
        //[HttpPost("error")]
        //public async Task<TViewModel> ErrorTestPost()
        //    => throw new Exception("Ocorreu um erro");


        //[HttpGet("errortratado")]
        //public ActionResult<ResponseApi> ErrorTratado()
        //{
        //    _notifier.Handle(new Notification("Teste"));
        //    return CustomResponse();
        //}

        //[HttpPost("errortratado")]
        //public ActionResult<ResponseApi> ErrorTratadoPost()
        //{
        //    _notifier.Handle(new Notification("Teste"));
        //    return CustomResponse("aa");
        //}

        //[HttpGet("errorTeste")]
        //public ActionResult Teste()
        //{
        //    _notifier.Handle(new Notification("Teste"));
        //    return Problem();
        //}

        protected async Task SaveChangesAsync()
        {
            if (await _unitOfWork.CommitAsync()) return;
            Notify(ErrorMessage.GetErrorMessageCommit());
        }
    }
}
