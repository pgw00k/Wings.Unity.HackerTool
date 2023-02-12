using UnityEngine;

namespace UnityHack
{
    public class GUIHackToolArea : IHackGUIRender
    {
        protected Rect _Rect = new Rect(200, 0, Screen.width - 400, Screen.height - 300);
        protected Rect _MainWindowRect = new Rect(200, Screen.height - 350, 50, 50);

        protected bool _IsRender = true;

        public Vector2 ScrollViewV2 = Vector2.zero;
        public virtual void Render()
        {
            GUILayout.BeginArea(_Rect);
            _MainWindowRect = GUI.Window(4, _MainWindowRect, _MainWindow,"Tool");
            ScrollViewV2 = GUILayout.BeginScrollView(ScrollViewV2);
            if(_IsRender)
            {
                _DrawScrollContent();
            }
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        public virtual void _DrawScrollContent()
        {    
        }

        protected void _MainWindow(int windowId)
        {
            if (GUILayout.Button("T"))
            {
                _IsRender = !_IsRender;
            }

            GUI.DragWindow();
        }
    }
}
