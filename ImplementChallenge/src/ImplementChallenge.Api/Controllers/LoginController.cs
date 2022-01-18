using ImplementChallenge.Api.Extensions;
using ImplementChallenge.Api.Interfaces;
using ImplementChallenge.Api.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Post(LoginViewModel loginViewModel)
        {
            if (string.IsNullOrEmpty(loginViewModel.login))
                throw new Exception("Login não pode ser vazio ou nulo");

            if (string.IsNullOrEmpty(loginViewModel.Senha))
                throw new Exception("Senha não pode ser vazia ou nula");

            if (await IsExisteUsuario(loginViewModel))
                return CustomResponse(GerarJWT(loginViewModel));

            return CustomResponse(NotFound());
        }

        private async Task<bool> IsExisteUsuario(LoginViewModel loginViewModel)
        {
            return await _usuarioRepository.ExisteUsuario(loginViewModel.login, loginViewModel.Senha);
        }

        private string GerarJWT(LoginViewModel loginViewModel)
        {
            var user =  _usuarioRepository.BuscarUsuario(loginViewModel.login, loginViewModel.Senha);
            var valorClaim = user.Result.ValorClaim;
            var tipoClaim = user.Result.TipoClaim;

            var tokenHanlder = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHanlder.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Role,tipoClaim),    
                    new Claim(ClaimTypes.Role,valorClaim),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)
                }),

                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            });

            var encodeToken =  tokenHanlder.WriteToken(token);

            return encodeToken;
        }
        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
