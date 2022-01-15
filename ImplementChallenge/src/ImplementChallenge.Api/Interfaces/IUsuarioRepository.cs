using ImplementChallenge.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        public Task<bool> ExisteUsuario(string usuario, string senha);
        public Task<IEnumerable<Usuario>> OterTodosUsuariosOrdenados();
        public Task<Usuario> BuscarUsuario(string nome, string senha);
        public Task<Usuario> BuscarUsuarioPorId(int ind);

        public Task<IEnumerable<Usuario>> ObterTodosPaginacao(int paginaAtual, int itenPaginas);
    }
}
