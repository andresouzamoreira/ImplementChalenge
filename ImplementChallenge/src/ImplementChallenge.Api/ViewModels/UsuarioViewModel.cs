using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.ViewModels
{
    public class UsuarioViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Favor digitar o nome do usuario")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Favor digitar uma senha para o usuário")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "A senha deve ter entre 4 e 10 caracteres")]
        public string Senha { get; set; }
        public string Token { get; set; }
        public string tipoClaim { get; set; }
        public string valorClaim { get; set; }
    }
}
