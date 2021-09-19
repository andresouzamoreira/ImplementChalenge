using AutoMapper;
using ImplementChallenge.Api.Domain;
using ImplementChallenge.Api.Interfaces;
using ImplementChallenge.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace ImplementChallenge.Api.Controllers
{
    [Authorize]
    [Route("api/Curtidas")]    
    public class CurtidasController : MainController
    {
        private readonly ICurtidasService _curtidasService;
        private readonly IMapper _mapper;

        public CurtidasController(INotificador notificador, ICurtidasService curtidasService, IMapper mapper) : base(notificador)
        {
            _curtidasService = curtidasService;
            _mapper = mapper;
        }

        
        [HttpPost]        
        public async Task<ActionResult<CurtidasViewModel>> Post(CurtidasViewModel curtidasViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _curtidasService.Adicionar(_mapper.Map<Curtidas>(curtidasViewModel));

            return  CustomResponse(curtidasViewModel);
        }
    }
}
