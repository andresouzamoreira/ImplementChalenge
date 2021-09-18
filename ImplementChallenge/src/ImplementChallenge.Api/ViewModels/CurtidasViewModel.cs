using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.ViewModels
{
    public class CurtidasViewModel
    {        
        public int Id { get; set; }

        public int TotalCurtidas { get; set; }

        [Required(ErrorMessage ="Favor informar a data de atualização")]
        public DateTime DataAtualizacao { get; set; }

        [Required(ErrorMessage = "Id do usuário precisa de um valor")]
        public int IdUsuario { get; set; }
    }
}
