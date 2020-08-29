using UnityEngine;

namespace Guu.Utils
{
    public static class ShaderProps
    {
        // Color Properties
        public static readonly int BOTTOM_COLOR = Shader.PropertyToID("_BottomColor");
        public static readonly int MID_COLOR = Shader.PropertyToID("_MidColor");
        public static readonly int TOP_COLOR = Shader.PropertyToID("_TopColor");
    }
}