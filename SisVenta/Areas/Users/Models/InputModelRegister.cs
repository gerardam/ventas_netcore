using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SisVenta.Areas.Users.Models
{
    public class InputModelRegister
    {
        [Required(ErrorMessage = "Nombre obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Apellido obligatorio")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "NID obligatorio")]
        public string NID { get; set; }

        [Required(ErrorMessage = "Telefono obligatorio")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{5})$", ErrorMessage = "<font color='red'>Formato no validos</font>")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email obligatorio")]
        [EmailAddress(ErrorMessage = "Formato no valido")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Contraseña obligatoria")]
        [StringLength(100, ErrorMessage = "La contrasena es de {0}; debe ser al menos {2}", MinimumLength = 8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Rol obligatorio")]
        public string Role { get; set; }

        public string ID { get; set; }
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
