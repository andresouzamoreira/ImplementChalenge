using ImplementChallenge.Api.Data;
using ImplementChallenge.Api.Domain;
using ImplementChallenge.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Repository
{
    public class CurtidaRepository : Repository<Curtidas>, ICurtidasRepository
    {
        public CurtidaRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
