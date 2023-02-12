using UnityEngine;

namespace UnityHack
{
    /// <summary>
    /// 内容区域自带滑动条的窗口
    /// </summary>
    public class BaseScrollWindow:BaseHackWindow
    {

        public Vector2 ScrollViewV2 = Vector2.zero;

        public BaseScrollWindow(int x, int y, int w, int h, int id, string winName) :
            base(x,y,w,h,id,winName)
        {
        }

        protected virtual void _DrawScrollContent()
        {

        }

        protected virtual void _DrawBeforeScrollContent()
        {

        }

        protected virtual void _DrawAfterScrollContent()
        {

        }

        protected override void _DrawContent()
        {
            base._DrawContent();

            _DrawBeforeScrollContent();
            ScrollViewV2 = GUILayout.BeginScrollView(ScrollViewV2);
            _DrawScrollContent();
            GUILayout.EndScrollView();
            _DrawAfterScrollContent();
        }
    }
}
