using ImplementChallenge.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Interfaces
{
    public interface ICurtidasService : IDisposable
    {
        Task<bool> Adicionar(Curtidas curtidas);
        Task<bool> Remover(int id);
        Task<bool> Atualizar(Curtidas curtidas);
    }
}
