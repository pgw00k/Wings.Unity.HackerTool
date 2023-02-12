using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using UnityEngine;

namespace UnityHack
{

    public class GUIConsoleLogEnumerator: IEnumerator<GUILogInfoItem>
    {
        private List<GUILogInfoItem> _LogInfoes;
        private int _Index;
        private bool disposedValue;

        public GUIConsoleLogEnumerator(List<GUILogInfoItem> list)
        {
            _LogInfoes = list;
            _Index = -1;
        }

        bool IEnumerator.MoveNext()
        {
            _Index++;
            if (_Index < _LogInfoes.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void IEnumerator.Reset()
        {
            _Index = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return _LogInfoes[_Index];
            }
        }

        public GUILogInfoItem Current
        {
            get
            {
                return _LogInfoes[_Index];
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~GUIConsoleLog()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// 记录日志的类
    /// </summary>
    public class GUIConsoleLog:GenOcean.Common.SingletonManagerBase<GUIConsoleLog>,IEnumerable
    {
        protected List<GUILogInfoItem> _LogInfoes = new List<GUILogInfoItem>();

        /// <summary>
        /// 重定义标准输出流
        /// </summary>
        protected GUIStandardConsoleWriter _StandardWriter = new GUIStandardConsoleWriter();


        public GUIConsoleLog()
        {
            Console.SetOut(_StandardWriter);
        }


        protected virtual void _LogInfo(string info, string trace = null, int LogLevel = 0)
        {
            trace += GUITrace.GetStackTraceModelName();
            _LogInfoes.Add(new GUILogInfoItem(info, trace, LogLevel));
        }

        protected virtual void _Clear()
        {
            _LogInfoes.Clear();
        }


        public static void Log(string info, string trace = null, int LogLevel = 0)
        {
            Instance._LogInfo(info, trace, LogLevel);
        }

        public static void Log(Exception e)
        {
            Instance._LogInfo(e.Message, e.StackTrace);
        }

        public static void Clear()
        {
            Instance._Clear();
        }
        /// <summary>
        /// 当前存储的日志总数
        /// </summary>
        public static int Count
        {
            get
            {
                return Instance._LogInfoes.Count;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new GUIConsoleLogEnumerator(_LogInfoes);
        }
    }
}
