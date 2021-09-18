using ImplementChallenge.Api.Data;
using ImplementChallenge.Api.Domain;
using ImplementChallenge.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationContext context) : base(context) { }
        public async Task<IEnumerable<Usuario>> ObterTodos()
        {
            return await _DbContext.Usuario.ToListAsync();
        }
    }
}
