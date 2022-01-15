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
        public async Task<bool> ExisteUsuario(string usuario, string senha)
        {
            int count = await _DbContext.Usuario.CountAsync(w => w.Nome == usuario && w.Senha == senha);

            return count > 0;

        }

        public async Task<IEnumerable<Usuario>> OterTodosUsuariosOrdenados()
        {
            return await _DbContext.Usuario.AsNoTracking().OrderBy(o => o.Nome).ToListAsync();
        }

        public async Task<Usuario> BuscarUsuario(string nome, string senha)
        {
            var usuario = await _DbContext.Usuario.Where(w=>w.Nome == nome && w.Senha == senha).FirstOrDefaultAsync();

            return usuario;
        }

        public async Task<Usuario> BuscarUsuarioPorId(int id)
        {
            var usuario = await _DbContext.Usuario.Where(w => w.Id == id).FirstOrDefaultAsync();
            return usuario;
        }

        public async Task<IEnumerable<Usuario>> ObterTodosPaginacao(int paginaAtual, int itenPaginas)
        {
            var usuario = await _DbContext.Usuario.Skip((paginaAtual - 1) * itenPaginas).Take(itenPaginas).ToListAsync();

            return usuario;
        }
    }
}
