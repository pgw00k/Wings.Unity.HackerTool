using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityHack
{
    public class GUIConsole : BaseScrollWindow
    {
        public static void Log(string info, string trace = null, int LogLevel = 0)
        {
            GUIConsoleLog.Log(info, trace, LogLevel);
        }

        public static void Log(Exception e)
        {
            GUIConsoleLog.Log(e);
        }

        protected GUIToggleButton _TBtnUnityLog = null;
        public GUIConsole() : base(200, Screen.height - 300, Screen.width - 400, 300, 2, "ConsoleLog")
        {
            _TBtnUnityLog = new GUIToggleButton("UnityLog ON", "UnityLog OFF", UnityLogSwitch);
            _TBtnUnityLog.Click();

            _MinWindowRect = new Rect(_MinWindowRect.x, Screen.height - 50, _MinWindowRect.width, 50);

            System.AppDomain.CurrentDomain.UnhandledException += SystemExceptionCallBack;
        }

        public virtual void Clear()
        {
            GUIConsoleLog.Clear();
        }

        public virtual void SaveToFile()
        {

        }

        protected override void _DrawBeforeScrollContent()
        {
            base._DrawBeforeScrollContent();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(string.Format("Clear({0})", GUIConsoleLog.Count)))
            {
                Clear();
            }

            if (GUILayout.Button("Save"))
            {
                SaveToFile();
            }

            _TBtnUnityLog.Render();

            GUILayout.EndHorizontal();
        }

        protected override void _DrawScrollContent()
        {
            base._DrawScrollContent();

            foreach (GUILogInfoItem item in GUIConsoleLog.Instance)
            {
                if (item == null)
                {
                    continue;
                }
                item.Render();
            }
        }

        protected override void _CheckStatue()
        {
            if (_PreIsMin != IsMin)
            {
                _PreIsMin = IsMin;
                if (_PreIsMin)
                {
                    float ny = _WindowRect.position.y + _WindowRect.height;
                    ny = ny >= Screen.height ? Screen.height - 50 : ny;
                    _MinWindowRect.position = new Vector2(_WindowRect.position.x, ny);
                }
                else
                {
                    float ny = _MinWindowRect.position.y - _WindowRect.height + 50;
                    ny = ny <= 0 ? 0 : ny;
                    _WindowRect.position = new Vector2(_MinWindowRect.position.x, ny);
                }
            }
        }

        public virtual void UnityLogSwitch(bool isLog)
        {
            if (isLog)
            {
                Log("Start listen LogCallBack.");
                UnityEngine.Application.logMessageReceived += UnityLogCallBack;
                Debug.Log("Listen Success!");
            }
            else
            {
                Log("Stop listen LogCallBack.");
                UnityEngine.Application.logMessageReceived -= UnityLogCallBack;
            }
        }

        protected virtual void SystemExceptionCallBack(object sender, UnhandledExceptionEventArgs arg)
        {
            Exception e = arg.ExceptionObject as Exception;
            if(e!=null)
            {
                Log(e);
            }else
            {
                Log("arg.ExceptionObject is" + arg.ExceptionObject.GetType().Name);
            }
        }

        protected virtual void UnityLogCallBack(string info, string trace, LogType type)
        {
            int logLevel = (int)type;
            Log(info, trace, logLevel);
        }
    }
}
