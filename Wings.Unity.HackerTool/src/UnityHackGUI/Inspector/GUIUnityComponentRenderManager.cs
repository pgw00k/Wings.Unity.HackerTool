using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityHack
{


    /// <summary>
    /// 管理GameObject上的组件（Component）在GUIInspector中的渲染模式
    /// </summary>
    public class GUIUnityComponentRenderManager : Dictionary<Type, GUIRenderFunction<UnityEngine.Component, IHackGUIRender[]>>
    {
        #region 单例内容
        protected static GUIUnityComponentRenderManager _Instance = null;
        public static GUIUnityComponentRenderManager Instance
        {
            get
            {
                if(_Instance == null)
                {
                    _Instance = new GUIUnityComponentRenderManager();
                    _Instance._Init();
                }

                return _Instance;
            }
        }

        protected void _Init()
        {
            GUIConsole.Log(string.Format("{0}._Init:Start.", GetType().Name));
            GUIUnityComponentRenderFunction.AddUnityDefaultRenderFunction();
        }

        #endregion

        public new GUIRenderFunction<UnityEngine.Component, IHackGUIRender[]> this[Type k]
        {
            get
            {
                if(!ContainsKey(k))
                {
                    //返回默认的渲染
                    return base[typeof(UnityEngine.Component)];
                }

                return base[k];
            }
        }
    }
}
