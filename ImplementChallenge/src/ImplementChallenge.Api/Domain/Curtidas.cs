using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Domain
{
    public class Curtidas
    {
        public int Id { get; set; }
        public int TotalCurtidas { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int IdUsuario { get; set; }
    }
}
