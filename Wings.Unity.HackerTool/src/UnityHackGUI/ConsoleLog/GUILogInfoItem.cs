using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityHack
{
    /// <summary>
    /// 日志信息
    /// </summary>
    public class GUILogInfoItem : GUIFoldoutItem
    {
        protected string _Trace = "";
        protected int _LogLevel = 0;
        public GUILogInfoItem(string info,string trace = null,int logLevel = 0) : base(info)
        {
            _Trace = trace;
            _LogLevel = logLevel;
        }

        public override void ExpandRender()
        {
            base.ExpandRender();

            if(string.IsNullOrEmpty(_Trace))
            {
                return;
            }
            GUILayout.BeginHorizontal();
            GUILayout.TextArea(_Trace);
            GUILayout.EndHorizontal();
        }
    }
}
