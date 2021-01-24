using Eshop_UTB.Models.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop_UTB.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(256)]
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [StringLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [UniqueCharacters(6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = RegisterViewModel.ErrorMessagePassword)]
        public string Password { get; set; }
        private const string ErrorMessagePassword = "U need more uniq characters!";
        [Required]
        [Compare(nameof(Password),ErrorMessage = "Password dont match!!")]
        public string RepeatedPassword { get; set; }

        public string [] ErrorsDuringRegister { get; set; } 

    }
}
