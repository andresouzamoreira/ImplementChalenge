using AutoMapper;
using ImplementChallenge.Api.Data;
using ImplementChallenge.Api.Domain;
using ImplementChallenge.Api.Extensions;
using ImplementChallenge.Api.Interfaces;
using ImplementChallenge.Api.Repository;
using ImplementChallenge.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : MainController
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IEnvioPublishRabbitMQ _envioPublishRabbitMQ;

        public UsuarioController(IMapper mapper,IConfiguration configuration, IUsuarioRepository usuarioRepository, INotificador notificador, IEnvioPublishRabbitMQ envioPublishRabbitMQ) : base(notificador)
        {
            _configuration = configuration;
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _envioPublishRabbitMQ = envioPublishRabbitMQ;
        }

        
        [HttpPost("Cadastrar")]
        [Authorize(Roles = "Gerente")]        
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<UsuarioViewModel>> Post(UsuarioViewModel usuarioViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            #region inserir da fila rabbit

            _envioPublishRabbitMQ.EnviaPublishRabbit(usuarioViewModel);
            
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
            if (!ModelState.IsValid) return CustomResponse(ModelState);

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
            await _usuarioRepository.Remover(id);

            return CustomResponse();
        }
    }
}
