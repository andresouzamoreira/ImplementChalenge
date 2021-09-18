using ImplementChallenge.Api.Data;
using ImplementChallenge.Api.Domain;
using ImplementChallenge.Api.Interfaces;
using ImplementChallenge.Api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IConfiguration configuration, IUsuarioRepository usuarioRepository)
        {
            _configuration = configuration;
            _usuarioRepository = usuarioRepository;
        }

        //private readonly ApplicationContext _context;
        //public UsuarioController(ApplicationContext context)
        //{
        //    _context = context;
        //}

        //[HttpGet("obter-por-id/{id:int}")]
        //public async Task<ActionResult<string>> Get(int id)
        //{
        //     await
        //     return "value";
        //}

        [HttpPost("inserir")]
        [ProducesResponseType(typeof(Usuario),StatusCodes.Status201Created)]        
        public async Task<ActionResult<Usuario>> Post(Usuario usuario)
        {
            await _usuarioRepository.Adicionar(usuario);
               
            return CreatedAtAction(nameof(Usuario), usuario);
        }
    }
}
