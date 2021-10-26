// Versión 1.1

using Foolproof;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debe ingresar un RUT")]
        [DisplayName("Rut")]
        [RegularExpression(@"^[0-9]+-[0-9kK]{1}$", ErrorMessage = "Ingrese un RUT válido")]
        public string Rut { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [DisplayName("Contraseña")]
        public string Clave { get; set; }
    }

    public class CambioPasswordViewModel
    {
        [Required(ErrorMessage = "Debe ingresar su contraseña actual")]
        [DataType(DataType.Password)]
        [DisplayName("Contraseña Actual")]
        public string ClaveActual { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña nueva")]
        [StringLength(50, ErrorMessage = "La contraseña debe contener entre 5 y 50 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [NotEqualTo("ClaveActual", ErrorMessage = "La contraseña nueva no puede ser igual a la actual")]
        [DisplayName("Contraseña Nueva")]
        [RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{5,20})$", ErrorMessage = "Debe ingresar una clave que contenga letras y números")]
        public string ClaveNueva { get; set; }

        [Required(ErrorMessage = "Debe confirmar una contraseña nueva")]
        [StringLength(50, ErrorMessage = "La contraseña debe contener entre 5 y 50 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [DisplayName("Confirmar Contraseña Nueva")]
        [Compare("ClaveNueva", ErrorMessage = "Las contraseñas ingresadas no coinciden")]
        [RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{5,20})$", ErrorMessage = "Debe ingresar una clave que contenga letras y números")]
        public string ClaveConfirmacion { get; set; }
    }

    public class RestablecerPasswordViewModel
    {
        public string RutRestablece { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña nueva")]
        [StringLength(50, ErrorMessage = "La contraseña debe contener entre 5 y 50 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [DisplayName("Contraseña Nueva")]
        [RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{5,20})$", ErrorMessage = "Debe ingresar una clave que contenga letras y números")]
        public string ClaveNueva { get; set; }

        [Required(ErrorMessage = "Debe confirmar una contraseña nueva")]
        [StringLength(50, ErrorMessage = "La contraseña debe contener entre 5 y 50 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [DisplayName("Confirmar Contraseña Nueva")]
        [Compare("ClaveNueva", ErrorMessage = "Las contraseñas ingresadas no coinciden")]
        [RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{5,20})$", ErrorMessage = "Debe ingresar una clave que contenga letras y números")]
        public string ClaveConfirmacion { get; set; }
    }
}
