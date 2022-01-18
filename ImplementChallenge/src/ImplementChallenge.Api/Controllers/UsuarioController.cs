using AutoMapper;
using ImplementChallenge.Api.Domain;
using ImplementChallenge.Api.Interfaces;
using ImplementChallenge.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : MainController
    {        
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IFila _fila;

        public UsuarioController(IMapper mapper, IUsuarioRepository usuarioRepository, INotificador notificador, IFila fila) : base(notificador)
        {            
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _fila = fila;
        }

        
        [HttpPost("Cadastrar")]
        [Authorize(Roles = "Gerente")]        
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<UsuarioViewModel>> Post(UsuarioViewModel usuarioViewModel)
        {
            if (usuarioViewModel.Id != 0)
                throw new System.Exception("Valor [ID] não pode ser preenchido para usuario no momento de inserir");

            #region inserir da fila rabbit

            _fila.Adicionar(usuarioViewModel);          
            #endregion


            await _usuarioRepository.Adicionar(_mapper.Map<Usuario>(usuarioViewModel));

            return CustomResponse(usuarioViewModel);
        }

        [HttpGet("ObterTodos")]
        [AllowAnonymous]
        public async Task<IEnumerable<UsuarioViewModel>> ObterTodos()
        {
            //var usuario = new UsuarioViewModel { Id = 1, Nome = "Andre", Senha = "1231", tipoClaim = "geretne", Token = "215", valorClaim = "tipo" };
            //var lista = new List<UsuarioViewModel>();
            //lista.Add(usuario);
            return _mapper.Map<IEnumerable<UsuarioViewModel>>(await _usuarioRepository.ObterTodos());
            //return teste.ToList();
        }

        [HttpGet("ObterTodosPaginacao")]
        [AllowAnonymous]
        public async Task<IEnumerable<UsuarioViewModel>> ObterTodosPaginacao(int paginaAtual, int itenPaginas)
        {
            //var usuario = new UsuarioViewModel { Id = 1, Nome = "Andre", Senha = "1231", tipoClaim = "geretne", Token = "215", valorClaim = "tipo" };
            //var lista = new List<UsuarioViewModel>();
            //lista.Add(usuario);
            return _mapper.Map<IEnumerable<UsuarioViewModel>>(await _usuarioRepository.ObterTodosPaginacao(paginaAtual,itenPaginas));
            //return teste.ToList();
        }

        [HttpGet("ObterUsuarioPorId/{id}")]
        [AllowAnonymous]
        public async Task<UsuarioViewModel> ObterPorId(int id)
        {
            return _mapper.Map<UsuarioViewModel>(await _usuarioRepository.BuscarUsuarioPorId(id));
        }

        [HttpPut("Atualizar")]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioViewModel>> Put(UsuarioViewModel usuarioViewModel)
        {
            if (usuarioViewModel.Id == 0)
                throw new System.Exception("Id do usuário não pode ser zero");
            if (string.IsNullOrEmpty(usuarioViewModel.Nome))
                throw new System.Exception("O nome do usuário não pode ser vazio");
            

            await _usuarioRepository.Atualizar(_mapper.Map<Usuario>(usuarioViewModel));

            return CustomResponse(usuarioViewModel);
        }

        [HttpDelete("Deletar/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Usuario),StatusCodes.Status200OK)]
        public async Task<ActionResult<UsuarioViewModel>> Delete(int id)
        {
            if (id == 0)
                throw new System.Exception("O valor do id não pode ser zero");

            await _usuarioRepository.Remover(id);

            return CustomResponse();
        }
    }
}
