using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace UnityHack
{
    public class GUIUnityTypeRanderManager : Dictionary<Type, GUIRenderFunction<object,object>>
    {
        #region 单例内容
        protected static GUIUnityTypeRanderManager _Instance = null;
        public static GUIUnityTypeRanderManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new GUIUnityTypeRanderManager();
                    _Instance._Init();
                }

                return _Instance;
            }
        }

        protected void _Init()
        {
            GUIConsole.Log(string.Format("{0}._Init:Start.", GetType().Name));
            GUIUnityTypeRenderFunction.AddUnityDefaultRenderFunction();
        }

        #endregion


        public new GUIRenderFunction<object,object> this[Type k]
        {
            get
            {
                if (!ContainsKey(k))
                {
                    //返回默认的渲染
                    return base[typeof(UnityEngine.Object)];
                }

                return base[k];
            }
        }
    }
}
