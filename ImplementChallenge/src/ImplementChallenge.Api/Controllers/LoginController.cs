using ImplementChallenge.Api.Extensions;
using ImplementChallenge.Api.Interfaces;
using ImplementChallenge.Api.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Controllers
{
   
    public class LoginController : MainController
    {
        private readonly INotificador _notificador;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly AppSettings _appSettings;

        public LoginController(INotificador notificador, IUsuarioRepository usuarioRepository,IOptions<AppSettings> appSettings) : base(notificador)
        {
            _notificador = notificador;
            _usuarioRepository = usuarioRepository;
            _appSettings = appSettings.Value;
        }

        [HttpPost("api/Login")]
        public async Task<ActionResult> Post(LoginViewModel loginViewModel)
        {            
            if (await IsExisteUsuario(loginViewModel))
                return CustomResponse(GerarJWT());
                
            return BadRequest();
        }

        private async Task<bool> IsExisteUsuario(LoginViewModel loginViewModel)
        {
            return await _usuarioRepository.ExisteUsuario(loginViewModel.login, loginViewModel.Senha);
        }

        private string GerarJWT()
        {
            var tokenHanlder = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHanlder.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            });

            var encodeToken = tokenHanlder.WriteToken(token);

            return encodeToken;
        }
    }
}
