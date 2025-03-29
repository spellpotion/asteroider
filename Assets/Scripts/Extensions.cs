using UnityEngine;

namespace Asteroider
{
    public static class Extensions
    {
        public static string ToHex(this Color32 color)
        {
            return $"#{color.r:X2}{color.g:X2}{color.b:X2}";
        }
    }
}