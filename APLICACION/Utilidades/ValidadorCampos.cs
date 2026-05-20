using System.Text.RegularExpressions;

namespace APLICACION.Utilidades
{
    public class ValidadorCampos
    {
        public static void ValidarCampoRequerido(string? campo, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(campo))
                throw new ArgumentException($"El campo {nombreCampo} es requerido.");
        }

        public static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email es requerido.");

            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!regex.IsMatch(email))
                throw new ArgumentException("El formato del email no es válido.");
        }

        public static void ValidarTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                throw new ArgumentException("El teléfono es requerido.");

            var regex = new Regex(@"^\d{7,}$");
            if (!regex.IsMatch(telefono.Replace("-", "").Replace(" ", "")))
                throw new ArgumentException("El formato del teléfono no es válido.");
        }

        public static bool EsCorreoValido(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return false;

            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(correo);
        }

        public static bool EsTelefonoValido(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                return false;

            var regex = new Regex(@"^\d{7,}$");
            return regex.IsMatch(telefono.Replace("-", "").Replace(" ", ""));
        }

        public static bool EsCedulaValida(string cedula)
        {
            return !string.IsNullOrWhiteSpace(cedula) && cedula.Length >= 6;
        }

        public static bool EsCampoRequerido(string? campo)
        {
            return !string.IsNullOrWhiteSpace(campo);
        }

        public static bool EsNumeroPositivo(decimal numero)
        {
            return numero > 0;
        }
    }
}
