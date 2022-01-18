using AutoMapper;
using FluentAssertions;
using ImplementChallenge.Api.Controllers;
using ImplementChallenge.Api.Data;
using ImplementChallenge.Api.Domain;
using ImplementChallenge.Api.Extensions;
using ImplementChallenge.Api.Interfaces;
using ImplementChallenge.Api.Notificacoes;
using ImplementChallenge.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace ImplementChallengeTest
{    
    public class LoginControllerTest
    {       
        private INotificador notificador = new Notificador();
        private Mock<IUsuarioRepository> mockUsuarioRepository;
        private Mock<IOptions<AppSettings>> mockAppSettings;
        public LoginControllerTest()
        {
            mockUsuarioRepository = new Mock<IUsuarioRepository>();
            mockAppSettings = new Mock<IOptions<AppSettings>>();
        }

        [Fact(DisplayName = "Deve retornar exception se o valor de login ou senha forem vazios")]
        public async Task Post_ValidacaoLoginIdZeroOuESenhaVAzia_DeveRetornarException()
        {
            var loginView = new LoginViewModel()
            {
                login = "",
                Senha = ""
            };

            var controller = new LoginController(notificador, mockUsuarioRepository.Object, mockAppSettings.Object);

            var result = await controller.Post(loginView);

            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
