using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Domain
{
    public class Usuario : Entity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Token { get; set; }
    }
}
