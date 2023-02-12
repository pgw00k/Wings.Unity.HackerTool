using UnityEngine;

namespace UnityHack
{
    /// <summary>
    /// 窗口最小化的方式
    /// </summary>
    public enum WindowMinType
    {
        ToTop=0,
        ToBottom=1,
        ToLeft=2,
        ToRight=3,
    }

    public class BaseHackWindow : IHackGUIRender
    {
        protected Rect _WindowRect;
        protected Rect _MinWindowRect;
        protected Rect _DefaultWindowRect;

        protected int _WinId;
        protected string _WinName;

        protected bool _PreIsMin = false;
      
        public bool IsLock = false;
        public bool IsMin = false;

        public Rect WindowRect
        {
            get
            {
                return _WindowRect;
            }
        }

        public int WinId
        {
            get
            {
                return _WinId;
            }
        }

        public string name
        {
            get
            {
                return _WinName;
            }
        }


        public BaseHackWindow(int x,int y,int w,int h,int id,string winName)
        {
            _DefaultWindowRect = new Rect(x, y, w, h);
            _WindowRect = new Rect(x, y, w, h);
            _WinId = id;
            _WinName = winName;
            _MinWindowRect = new Rect(x, y, w, 50);
        }

        public virtual void Render()
        {
            if(IsMin)
            {
                _MinWindowRect = GUI.Window(_WinId, _MinWindowRect, _MainWindow, _WinName);
            }else
            {
                _WindowRect = GUI.Window(_WinId, _WindowRect, _MainWindow, _WinName);
            }

        }

        protected virtual void _DrawSetting()
        {
            IsLock = GUILayout.Toggle(IsLock, "L", HackGUIStyle.WindowLockToggleStyle);
            IsMin = GUILayout.Toggle(IsMin, "Min", HackGUIStyle.WindowLockToggleStyle);
            if(GUILayout.Button("D", HackGUIStyle.WindowLockToggleStyle))
            {
                IsMin = false;
                IsLock = false;
                _WindowRect = new Rect(_DefaultWindowRect);
            }
        }

        protected virtual void _CheckStatue()
        {
            if(_PreIsMin !=IsMin)
            {
                _PreIsMin = IsMin;
                if(_PreIsMin)
                {
                    _MinWindowRect.position = _WindowRect.position;
                }else
                {
                    _WindowRect.position = _MinWindowRect.position;
                }
            }
        }
        protected virtual void _DrawContent()
        {
        }

        protected virtual void _MainWindow(int windowId)
        {
            GUILayout.BeginHorizontal();
            _DrawSetting();
            GUILayout.EndHorizontal();

            _CheckStatue();

            if (!IsMin)
            {
                _DrawContent();
            }

            if (!IsLock)
            {
                GUI.DragWindow();
            }
        }
    }
}
