using System.Globalization;
using System.Net.Mail;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Utils
{
    public class Utilidades
    {
        //Función para la validación de un email
        public static bool EmailValido(string email)
        {
            try
            {
                var mail = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Función para convertir una cadena a formato "Título" (cada palabra con mayúscula inicial)
        public static string TitleCase(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(texto.ToLower());
        }

        // Función para comprobar si un IBAN es válido
        public static bool ValidarIBAN(string iban)
        {
            if (string.IsNullOrWhiteSpace(iban))
                return false;

            iban = iban.Replace(" ", "").ToUpper();
            if (!Regex.IsMatch(iban, "^[A-Z]{2}\\d{2}[A-Z0-9]{1,30}$"))
                return false;

            string ibanReordenado = iban.Substring(4) + iban.Substring(0, 4);
            string ibanNumerico = string.Concat(ibanReordenado.Select(c => char.IsLetter(c) ? (c - 'A' + 10).ToString() : c.ToString()));

            return BigInteger.Parse(ibanNumerico) % 97 == 1;
        }

        // Función para calcular la letra del DNI español
        public static char CalcularLetraDNI(int numeroDNI)
        {
            if (numeroDNI < 0 || numeroDNI > 99999999)
                throw new ArgumentOutOfRangeException(nameof(numeroDNI), "El número de DNI debe estar entre 0 y 99.999.999");

            string letras = "TRWAGMYFPDXBNJZSQVHLCKE";
            return letras[numeroDNI % 23];
        }


    }
}
