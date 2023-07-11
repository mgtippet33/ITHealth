using AutoMapper;
using ITHealth.Domain.Contracts.Commands;
using Microsoft.AspNetCore.Mvc;

namespace ITHealth.Web.API.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;

        protected readonly IMapper _mapper;

        public BaseApiController(IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        protected TCommand CreateCommand<TCommand, TFormModel>(TFormModel formModel)
            where TCommand : BaseCommandModel
        {
            var initialCommand = Activator.CreateInstance(typeof(TCommand)) as TCommand;
            TCommand command = _mapper.Map(formModel, initialCommand);

            return command;
        }
    }
}
