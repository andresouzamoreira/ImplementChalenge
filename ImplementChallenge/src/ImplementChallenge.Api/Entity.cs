using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api
{
    public abstract class Entity
    {
        [Ignore]
        public int Id { get; set; }
    }
}
