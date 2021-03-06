using ImplementChallenge.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        public Task<IEnumerable<Usuario>> ObterTodos();
    }
}
