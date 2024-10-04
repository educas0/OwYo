using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OwYO.Models
{
    public class Cliente
    {

        public int IdCliente { get; set; }
        public String Nickname { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }

    }
    public class Cliente_Login
    {
        #region ...propiedades clase modelo Cliente....

        [Required(ErrorMessage = "* Login obligatorio")]
        public String Login { get; set; }

        [Required(ErrorMessage = "* Password obligatoria")]
        [MinLength(4, ErrorMessage = "Se requieren al menos 4 caracteres MIN")]
        [MaxLength(20, ErrorMessage = "la Password no debe tener mas de 20 caracteres")]
        //[RegularExpression("^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{4,}$", ErrorMessage = "la password debe tener al menos una letra min, letra MAYS, numero y simbolo")]
        public String Password { get; set; }

        //public String? HashPasword { get; set; }

        #endregion
    }


}
