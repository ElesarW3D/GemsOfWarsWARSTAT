namespace GemsOfWarsMainTypes.Extension
{
    public static class IntExtension
    {
        public static string GetName(this int value)
        {
            //return value.ToString();
            if (value <= 0)
            {
                return "0";
            }
            else if (value <= 5)
            {
                return "|";
            }
            else if (value <= 10)
            {
                return "||";
            }
            else if (value <= 25)
            {
                return "|||";
            }
            else if (value <= 100)
            {
                return "||||";
            }
            return "|||||";
        }
    }
}
