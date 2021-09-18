using FluentValidation;
using ImplementChallenge.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Validations
{
    public class CurtidasValidation : AbstractValidator<Curtidas>
    {
        public CurtidasValidation()
        {
            RuleFor(c => c.DataAtualizacao)
                .NotEmpty().WithMessage("O campo data de atualização não pode ser vazio");

            RuleFor(c => c.TotalCurtidas)
                .NotEmpty().WithMessage("O campo Total de curtidas não pode ser enviado como vazio");

            RuleFor(c => c.IdUsuario)
                .NotNull().WithMessage("O campo IdUsuario não pode ser vazio");
        }
    }
}
