using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Domain
{
    public class Usuario : Entity
    {             
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Token { get; set; }
        public string TipoClaim { get; set; }
        public string ValorClaim { get; set; }
    }
}
