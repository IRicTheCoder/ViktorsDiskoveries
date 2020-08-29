using Guu.Utils;
using UnityEngine;
// ReSharper disable StringLiteralTypo

namespace VikDisk.Chapter1
{
    public static partial class Colors
    {
        /// <summary>
        /// Colors of all the slimes
        /// 
        /// Color order:
        /// 0 - Bottom
        /// 1 - Mid
        /// 2 - Top
        /// 3 - Ammo
        /// </summary>
        public static class Slimes
        {
            // Bottom, Mid, Top, Ammo
            public static readonly Color[] ALBINO = ColorUtils.FromHexArray("ffdbe8", "ffdbe8", "ffdbe8", "ffdbe8");
            public static readonly Color[] GLITCH = ColorUtils.FromHexArray("e64499", "912b66", "0cb485", "ff42bd");

            // TODO: Make these right
            public static readonly Color[] DREAM = ColorUtils.FromHexArray("ffdbe8", "ffdbe8", "ffdbe8", "ffdbe8");
            public static readonly Color[] TARR = ColorUtils.FromHexArray("ffdbe8", "ffdbe8", "ffdbe8", "ffdbe8");
        }
    }
}