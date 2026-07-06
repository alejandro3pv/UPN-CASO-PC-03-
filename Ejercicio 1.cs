using System;
using System.Text.RegularExpressions;

public class ValidadorComprobante
{
    public static bool ValidarComprobanteElectronico(string numero)
    {
        if (string.IsNullOrEmpty(numero))
            return false;

        // Formato esperado: YXXX-XXXXXXXX
        // Y  -> 'B' (Boleta) o 'F' (Factura)
        // XXX -> 3 digitos que completan la serie de 4 caracteres
        // XXXXXXXX -> 8 digitos del correlativo
        string patron = @"^[BF]\d{3}-\d{8}$";

        return Regex.IsMatch(numero, patron);
    }

    public static void Main()
    {
        Console.WriteLine(ValidarComprobanteElectronico("B123-00032123")); // true
        Console.WriteLine(ValidarComprobanteElectronico("A121-32121"));    // false
    }
}
