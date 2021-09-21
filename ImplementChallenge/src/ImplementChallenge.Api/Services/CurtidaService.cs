using ImplementChallenge.Api.Domain;
using ImplementChallenge.Api.Interfaces;
using ImplementChallenge.Api.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Services
{
    public class CurtidaService : BaseService, ICurtidasService
    {
        private readonly ICurtidasRepository _curtidasRepository;        
        
        public CurtidaService(ICurtidasRepository curtidasRepository,INotificador notificador)
            : base(notificador)
        {
            _curtidasRepository = curtidasRepository;
        }

        public async Task<bool> Adicionar(Curtidas curtidas)
        {
            if (!ExecutarValidacao(new CurtidasValidation(), curtidas)) 
                return false;

            await _curtidasRepository.Adicionar(curtidas);

            return true;
        }

        public async Task<bool> Atualizar(Curtidas curtidas)
        {
            if (!ExecutarValidacao(new CurtidasValidation(), curtidas))
                return false;

            await _curtidasRepository.Atualizar(curtidas);

            return true;
        }

        public void Dispose()
        {
            _curtidasRepository?.Dispose();
        }


        public Task<bool> Remover(int id)
        {
            throw new NotImplementedException();
        }
    }
}
