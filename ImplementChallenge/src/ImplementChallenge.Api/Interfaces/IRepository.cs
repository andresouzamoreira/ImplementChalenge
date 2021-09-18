using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Interfaces
{
    public interface IRepository<Tentity> : IDisposable where Tentity : Entity
    {
        Task Adicionar(Tentity entity);
        Task Atualizar(Tentity entity);
        Task Remover(int id);
        Task<List<Tentity>> ObterTodos();
        Task<int> SaveChanges();
    }
}
