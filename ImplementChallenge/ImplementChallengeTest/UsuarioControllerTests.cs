using AutoMapper;
using FluentAssertions;
using ImplementChallenge.Api.Controllers;
using ImplementChallenge.Api.Data;
using ImplementChallenge.Api.Domain;
using ImplementChallenge.Api.Interfaces;
using ImplementChallenge.Api.Notificacoes;
using ImplementChallenge.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace ImplementChallengeTest
{
    public class UsuarioControllerTests
    {
        private Mock<IMapper> mockAutoMapper;
        private Mock<IUsuarioRepository> mockUsuarioRepository;        
        private Mock<IFila> mockFila;
        private INotificador notificador = new Notificador();

        public UsuarioControllerTests()
        {
            mockAutoMapper = new Mock<IMapper>();
            mockUsuarioRepository = new Mock<IUsuarioRepository>();
            mockFila = new Mock<IFila>();
        }


        [Fact(DisplayName = "Post Deve retornar exception se o valor do id for diferente de zero")]
        public async Task Post_ValidacaoUsuarioIdZero_DeveRetornarException()
        {
            //arrange = preparar
            var invalidViewModel = new UsuarioViewModel()
            {
                Id = 0,
                Nome = ""
            };

            var usuario = new Usuario()
            {
                Id = 0,
                Nome = ""
            };

            mockAutoMapper.Setup(_mapper => _mapper.Map<Usuario>(It.IsAny<UsuarioViewModel>())).Returns(usuario);

            mockUsuarioRepository.Setup(ur => ur.Adicionar(usuario)).Returns(Task.FromResult(1));

            mockUsuarioRepository.Setup(ur => ur.Adicionar(It.IsAny<Usuario>())).Verifiable(); // valido quantas vezes foi chamado qual parametro foi chamado
            //setup ele espera um predicado
            //It.Asny <- receber qualuer objeto no caso aqui seri ao usuário.
            //Verifiable quando quero saber se passou no método mas não quero retornar nada.
            //Caso contrário devo colcoar um return para ele retornar um valor e usar ele posteriormente 
            // Task.FromResult(1) < porque ele retonra uma task
            // setup -> Especifica uma configuração no tipo simulado para uma chamada
            //BeOfType -> Afirma que o objeto é do tipo especificado
            mockFila.Setup(f => f.Adicionar(usuario));

            var controller = new UsuarioController(mockAutoMapper.Object, mockUsuarioRepository.Object, notificador, mockFila.Object);

            var result = await controller.Post(invalidViewModel);
            
            result.Result.Should().BeOfType<OkObjectResult>();
            
        }

        [Fact(DisplayName = "Delete Deve exception se o valor do id for zero")]
        public async Task Delete_ValidacaoUsuarioIdZero_DeveRetornarException()
        {
            var usuario = new Usuario()
            {
                Id = 0,
                Nome = ""
            };

            mockUsuarioRepository.Setup(ur => ur.Remover(usuario.Id)).Returns(Task.FromResult(1));

            var controller = new UsuarioController(mockAutoMapper.Object, mockUsuarioRepository.Object, notificador, mockFila.Object);

            var result = await controller.Delete(usuario.Id);

            result.Result.Should().BeOfType<OkObjectResult>();

        }

        [Fact(DisplayName = "Atualizar deve retornar exception se o valor do id for zero ou se o nome for vazio")]
        public async Task Put_ValidacaoUsuarioIdZeroNomeVAzio_DeveRetornarException()
        {
            //arrange = preparar
            var invalidViewModel = new UsuarioViewModel()
            {
                Id = 0,
                Nome = ""
            };

            var usuario = new Usuario()
            {
                Id = 0,
                Nome = ""
            };

            mockAutoMapper.Setup(_mapper => _mapper.Map<Usuario>(It.IsAny<UsuarioViewModel>())).Returns(usuario);

            mockUsuarioRepository.Setup(ur => ur.Atualizar(usuario)).Returns(Task.FromResult(1));

            mockUsuarioRepository.Setup(ur => ur.Atualizar(It.IsAny<Usuario>())).Verifiable(); // valido quantas vezes foi chamado qual parametro foi chamado

            var controller = new UsuarioController(mockAutoMapper.Object, mockUsuarioRepository.Object, notificador, mockFila.Object);

            var result = await controller.Put(invalidViewModel);

            result.Result.Should().BeOfType<OkObjectResult>();

        }
    }
}
