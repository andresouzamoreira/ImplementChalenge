using ImplementChallenge.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Interfaces
{
    public interface IUsuarioService : IDisposable
    {
        Task Adicionar(Usuario usuario);
        Task Remover(int id);
        Task Atualizar(Usuario usuario);
    }
}
