using Guu.Utils;
using UnityEngine;
// ReSharper disable StringLiteralTypo

namespace VikDisk.Chapter1
{
    public static partial class Colors
    {
        /// <summary>
        /// Colors of all the largos
        /// 
        /// Color order:
        /// 0 - Bottom
        /// 1 - Mid
        /// 2 - Top
        /// 3 - Ammo
        /// </summary>
        public static class Largos
        {
            // Bottom, Mid, Top, Ammo
            public static readonly Color[] ELECTRIC = ColorUtils.FromHexArray("e64499", "912b66", "0cb485", "42dcff");
        }
    }
}