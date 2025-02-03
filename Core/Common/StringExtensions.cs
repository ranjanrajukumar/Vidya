namespace Vidya.Core.Common
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyOrWhitespace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}
