namespace DistributionCenter.Api;

using System.Globalization;
using System.Text.RegularExpressions;

public static class Test
{
    public static void Main()
    {
        double x = 1234.000; // Ejemplo de número
        string decimalQuantityOnRegex = "{" + 2 + "}";
        string number = x.ToString(CultureInfo.InvariantCulture); // Asegura que tiene 2 decimales
        bool isValid = Regex.IsMatch(number, $@"^\d+(\.\d{decimalQuantityOnRegex})?$");

        Console.WriteLine(number);
        Console.WriteLine(isValid);
    }
}
