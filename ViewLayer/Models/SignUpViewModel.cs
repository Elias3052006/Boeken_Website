using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewLayer.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Gebruikersnaam is verplicht")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Gebruikersnaam moet tussen 3 en 50 tekens zijn")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Wachtwoord moet minimaal 6 tekens zijn")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "E-mailadres is verplicht")]
        [EmailAddress(ErrorMessage = "Ongeldig e-mailadres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Postcode is verplicht")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Huisnummer is verplicht")]
        [Range(1, int.MaxValue, ErrorMessage = "Huisnummer moet groter zijn dan 0")]
        public int HouseNumber { get; set; }
    }
}

