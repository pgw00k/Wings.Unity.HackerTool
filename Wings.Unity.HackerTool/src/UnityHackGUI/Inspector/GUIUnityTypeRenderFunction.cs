using System;
using UnityEngine;

namespace UnityHack
{
    public class GUIUnityTypeRenderFunction
    {
        public static void AddUnityDefaultRenderFunction()
        {
            //添加默认的绘制控制
            GUIUnityTypeRanderManager.Instance.Add(typeof(UnityEngine.Object), GUIUnityTypeDefault.DefaultRender);

            //添加Unity常见组件的绘制控制
            GUIUnityTypeRanderManager.Instance.Add(typeof(UnityEngine.Vector3), MathStructRenderFunction.Vector3Render);
            GUIUnityTypeRanderManager.Instance.Add(typeof(UnityEngine.Quaternion), MathStructRenderFunction.QuaternionRender);
        }
    }

    public class GUIUnityTypeDefault
    {
        public static object DefaultRender(object obj)
        {
            GUILayout.Label(obj.GetType().Name);

            return obj;
        }
    }

    public class MathStructRenderFunction
    {
        public static object Vector3Render(object obj)
        {
            Vector3 v3 = (Vector3)obj;
            GUILayout.BeginHorizontal();
            v3.x = Convert.ToSingle(GUILayout.TextField(v3.x.ToString()));
            v3.y = Convert.ToSingle(GUILayout.TextField(v3.y.ToString()));
            v3.z = Convert.ToSingle(GUILayout.TextField(v3.z.ToString()));
            GUILayout.EndHorizontal();
            return v3;
        }

        public static object QuaternionRender(object obj)
        {
            Vector3 v3 = ((Quaternion)obj).eulerAngles;
            GUILayout.BeginHorizontal();
            v3.x = Convert.ToSingle(GUILayout.TextField(v3.x.ToString()));
            v3.y = Convert.ToSingle(GUILayout.TextField(v3.y.ToString()));
            v3.z = Convert.ToSingle(GUILayout.TextField(v3.z.ToString()));
            GUILayout.EndHorizontal();

            return Quaternion.Euler(v3);
        }
    }
}
