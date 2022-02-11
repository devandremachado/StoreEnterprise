using System.Linq;

namespace Store.Shared.Core.Utils.Extensions
{
    public static class StringExtension
    {
        public static string OnlyNumbers(this string value, string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }
    }
}
