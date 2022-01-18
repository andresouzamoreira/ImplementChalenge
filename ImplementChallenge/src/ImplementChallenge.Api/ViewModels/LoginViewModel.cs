using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Favor informar o login do usuário", AllowEmptyStrings = false)]
        public string login { get; set; }
        [Required(ErrorMessage = "Favor informar a senha do usuário")]
        public string Senha { get; set; }
    }
}
