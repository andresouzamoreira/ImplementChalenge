using AutoMapper;
using ImplementChallenge.Api.Domain;
using ImplementChallenge.Api.Interfaces;
using ImplementChallenge.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace ImplementChallenge.Api.Controllers
{
    //[Authorize]
    [Route("api/Curtidas")]    
    public class CurtidasController : MainController
    {
        private readonly ICurtidasService _curtidasService;
        private readonly ICurtidasRepository _curtidasRepository;
        private readonly IMapper _mapper;

        public CurtidasController(INotificador notificador, ICurtidasService curtidasService, IMapper mapper, ICurtidasRepository curtidasRepository) : base(notificador)
        {
            _curtidasService = curtidasService;
            _mapper = mapper;
            _curtidasRepository = curtidasRepository;
        }

        
        [HttpPost]        
        public async Task<ActionResult<int>> Post(CurtidasViewModel curtidasViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _curtidasService.Adicionar(_mapper.Map<Curtidas>(curtidasViewModel));

            return  CustomResponse(returTotalCurtidas());
        }

        [HttpGet("TotalCurtidas")]
        public async Task<ActionResult<int>> GetTotalCurtidas()
        {
            return CustomResponse(await returTotalCurtidas());
        }

        private async Task<ActionResult<int>> returTotalCurtidas()
        {
            return await _curtidasRepository.ObterTotalCurtidas();
        }
    }
}
