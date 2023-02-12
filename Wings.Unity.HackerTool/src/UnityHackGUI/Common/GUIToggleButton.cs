using System;
using UnityEngine;

namespace UnityHack
{
    /// <summary>
    /// 简单的开关按钮
    /// </summary>
    public class GUIToggleButton : IHackGUIRender
    {
        protected string _OFFName;
        protected string _ONName;
        protected Action<bool> _ClickEvent;
        protected bool _IsOn = false;
        
        /// <summary>
        /// 开关是否已经打开
        /// <para>要改变这个值直接调用Click函数</para>
        /// </summary>
        public bool IsOn
        {
            get
            {
                return _IsOn;
            }
        }

        public GUIToggleButton(string on, string off, Action<bool> click=null)
        {
            _OFFName = off;
            _ONName = on;
            _ClickEvent = click;
        }

        public virtual void Click()
        {
            _IsOn = !_IsOn;
            _ClickEvent?.Invoke(_IsOn);
        }


        public virtual void Render()
        {
            if(GUILayout.Button(_IsOn?_ONName:_OFFName))
            {
                Click();
            }
        }

    }
}
