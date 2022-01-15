using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Interfaces
{
    public interface IEnvioPublishRabbitMQ
    {
        public void EnviaPublishRabbit<Entity>(Entity ViewModel);
    }
}
