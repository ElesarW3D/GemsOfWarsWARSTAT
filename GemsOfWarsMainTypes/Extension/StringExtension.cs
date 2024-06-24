namespace GemsOfWarsMainTypes.Extension
{
    public static class StringExtension
    {
        public static string Args(this string str, params object[] args)
        {
            return string.Format(str, args);
        }
    }
}
