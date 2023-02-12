using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityHack
{
    /******************************************************************
     * 创建人：HTL
     * 创建时间：2015-06-03 19:54:49
     * 说明： 获取出错时的堆栈调用方法列表
     * Huangyuan413026@163.com
     *******************************************************************/
    public class GUITrace
    {
        /// <summary>
        /// @Author:      HTL
        /// @Email:       Huangyuan413026@163.com
        /// @DateTime:    2015-06-03 19:54:49
        /// @Description: 获取当前堆栈的上级调用方法列表,直到最终调用者,只会返回调用的各方法,而不会返回具体的出错行数，可参考：微软真是个十足的混蛋啊！让我们跟踪Exception到行把！（不明真相群众请入） 
        /// </summary>
        /// <returns></returns>
        public static string GetStackTraceModelName()
        {
            //当前堆栈信息
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
            System.Diagnostics.StackFrame[] sfs = st.GetFrames();
            string _fullName = string.Empty , _fileName = string.Empty;
            for (int i = 1; i < sfs.Length; ++i)
            {
                //非用户代码,系统方法及后面的都是系统调用，不获取用户代码调用结束
                if (System.Diagnostics.StackFrame.OFFSET_UNKNOWN == sfs[i].GetILOffset()) break;
                System.Reflection.MethodBase _method = sfs[i].GetMethod();//方法名称
                int ln = sfs[i].GetFileLineNumber();//没有PDB文件的情况下将始终返回0
                int cn = sfs[i].GetFileColumnNumber();//没有PDB文件的情况下将始终返回0
                _fileName = sfs[i].GetFileName();
                _fullName += $"{_method.DeclaringType.Name}.{_method.Name}:{_fileName},{ln},{cn}\n";
            }
            st = null;
            sfs = null;
            return _fullName;
        }
    }
}
