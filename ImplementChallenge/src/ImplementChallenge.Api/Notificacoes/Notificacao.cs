using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Notificacoes
{
    public class Notificacao
    {
        public Notificacao(string mensagem)
        {
            Messagem = mensagem;
        }
        public string Messagem { get; set; }
    }
}
