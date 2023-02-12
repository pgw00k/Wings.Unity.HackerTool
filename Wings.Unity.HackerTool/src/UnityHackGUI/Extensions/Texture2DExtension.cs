using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEngine
{
    public static class Texture2DExtension
    {
        public static void FillTexture(this Texture2D tex, Color color)
        {
            int y = 0;
            while (y < tex.height)
            {
                int x = 0;
                while (x < tex.width)
                {
                    tex.SetPixel(x, y, color);
                    ++x;
                }
                ++y;
            }
            tex.Apply();
        }
    }
}
