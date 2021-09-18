using ImplementChallenge.Api.Interfaces;
using ImplementChallenge.Api.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificador _notificador;
        public MainController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new 
                { 
                    success = true, 
                    data = result 
                });
            }

            return BadRequest(new
            { 
                success = false, 
                errors = _notificador.ObterNotificacoes().Select(s => s.Messagem)
            });
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelstate)
        {
            var errors = modelstate.Values.SelectMany(s => s.Errors);
            foreach (var erro in errors)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificaErro(errorMsg);
            }
        }
        protected void NotificaErro(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));   
        }
    }
}
