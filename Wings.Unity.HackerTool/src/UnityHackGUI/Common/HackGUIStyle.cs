using UnityEngine;

namespace UnityHack
{
    public class HackGUIStyle
    {
        public static Texture2D BG;

        public static GUIStyle TreeItemToggleStyle;
        public static GUIStyle TreeItemButtonStyle;
        public static GUIStyle WindowLockToggleStyle;

        public static void Init()
        {
            BG = new Texture2D(128, 128);
            BG.FillTexture(Color.blue);

            TreeItemToggleStyle = new GUIStyle("Label")
            {
                alignment = TextAnchor.UpperLeft,
            };


            TreeItemButtonStyle = new GUIStyle("Label")
            {
                active = new GUIStyleState() { background = BG, textColor = Color.white },
                onActive = new GUIStyleState() { background = BG, textColor = Color.white },
                onNormal = new GUIStyleState() { background = BG, textColor = Color.white },
                alignment = TextAnchor.UpperLeft,
            };

            WindowLockToggleStyle = new GUIStyle("Label")
            {
                onNormal = new GUIStyleState() { background = BG, textColor = Color.white },
                alignment = TextAnchor.MiddleCenter,
            };
        }



    }
}
