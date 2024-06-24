using System;

namespace GemsOfWarsMainTypes.SubType
{
    [Flags]
    public enum ColorUnits
    {
        None = 0,
        Red = 1,    
        Brown = 2,
        Purple = 4,
        Yellow = 8,
        Green = 16,
        Blue = 32,
        All = Blue | Green | Yellow | Purple | Brown | Red,
    }
}
