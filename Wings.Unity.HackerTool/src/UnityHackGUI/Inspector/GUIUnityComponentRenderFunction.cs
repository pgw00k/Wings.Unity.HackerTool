using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace UnityHack
{
    /// <summary>
    /// Unity默认的一些组件的渲染方法
    /// </summary>
    public class GUIUnityComponentRenderFunction
    {
        public static void AddUnityDefaultRenderFunction()
        {
            //添加默认的绘制控制
            GUIUnityComponentRenderManager.Instance.Add(typeof(UnityEngine.Component), GUIUnityComponentDefault.DefaultRender);

            //添加Unity常见组件的绘制控制
            GUIUnityComponentRenderManager.Instance.Add(typeof(UnityEngine.Transform), GUIUnityTransform.DefaultRender);
        }
    }

    public class GUIUnityComponentDefault
    {
        public static IHackGUIRender[] DefaultRender(UnityEngine.Component _Source)
        {
            FieldInfo[] fis = _Source.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] pis = _Source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            //MemberInfo[] mis = _Source.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);

            int countf = fis.Length;
            int countp = pis.Length;
            IHackGUIRender[] _MemberInfoes = new IHackGUIRender[countf + countp];

            for (int i = 0; i < countf; i++)
            {
                _MemberInfoes[i] = new GUIFieldInfo(fis[i], _Source);
            }

            for (int i = 0; i < countp; i++)
            {
                _MemberInfoes[i + countf] = new GUIPropertyInfo(pis[i], _Source);
            }

            return _MemberInfoes;
        }
    }

    public class GUIUnityTransform
    {
        public static IHackGUIRender[] DefaultRender(UnityEngine.Component _Source)
        {
            IHackGUIRender[] _MemberInfoes = new IHackGUIRender[5];

            PropertyInfo pi = _Source.GetType().GetProperty("position");
            _MemberInfoes[0] = new GUIPropertyInfo(pi, _Source);

            pi = _Source.GetType().GetProperty("localPosition");
            _MemberInfoes[1] = new GUIPropertyInfo(pi, _Source);

            pi = _Source.GetType().GetProperty("rotation");
            _MemberInfoes[2] = new GUIPropertyInfo(pi, _Source);

            pi = _Source.GetType().GetProperty("localRotation");
            _MemberInfoes[3] = new GUIPropertyInfo(pi, _Source);
 
            pi = _Source.GetType().GetProperty("localScale");
            _MemberInfoes[4] = new GUIPropertyInfo(pi, _Source);

            return _MemberInfoes;
        }
    }
}
