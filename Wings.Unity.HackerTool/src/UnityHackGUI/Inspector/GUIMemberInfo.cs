using UnityEngine;
using System;
using System.Reflection;

namespace UnityHack
{
    public enum HackTypeCode 
    {
        Empty = 0,
        Object = 1,
        DBNull = 2,
        Boolean = 3,
        Char = 4,
        SByte = 5,
        Byte = 6,
        Int16 = 7,
        UInt16 = 8,
        Int32 = 9,
        UInt32 = 10,
        Int64 = 11,
        UInt64 = 12,
        Single = 13,
        Double = 14,
        Decimal = 15,
        DateTime = 16,
        String = 18,
        Function = 50,
    }

    public class GUIMemberInfo<T> : GUIFoldoutItem where T: MemberInfo
    {
        protected T _Source;
        protected object _Target;

        protected HackTypeCode _MemberType = HackTypeCode.Object;
        protected TypeCode _TypeCode = TypeCode.Object;

        public GUIMemberInfo(T info, object target)
        {
            _Source = info;
            _Target = target;
            _ExpandName = "▼";
            _UnexpandName = "▶";

        }

        protected override void _DrawFirstLineLabel()
        {
            base._DrawFirstLineLabel();
            GUILayout.Label(_Source.Name);
        }

        public override void ExpandRender()
        {
            base.ExpandRender();
            _RenderDefaultGUI();
        }

        /// <summary>
        /// 绘制基础类型
        /// </summary>
        /// <typeparam name="CommonType"></typeparam>
        /// <param name="info"></param>
        /// <param name="RenderFunction"></param>
        protected virtual void _RenderCommonValue<CommonType>(T info, GUIRenderFunction<CommonType, CommonType> RenderFunction)
        {
        }

        /// <summary>
        /// 绘制默认值
        /// </summary>
        protected virtual void _RenderDefaultValue()
        {
            
        }

        /// <summary>
        /// 绘制Object类型
        /// </summary>
        protected virtual void _RenderObjectValue()
        {
        }

        protected virtual object GetValue()
        {
            return null;
        }

        protected virtual void SetValue(object vlaue)
        {

        }

        protected virtual void _RenderDefaultGUI()
        {
            try
            {
                switch (_TypeCode)
                {
                    case System.TypeCode.Boolean:
                        _RenderCommonValue<bool>(_Source, b => GUILayout.Toggle(b, ""));
                        break;
                    case System.TypeCode.String:
                        _RenderCommonValue<string>(_Source, b => GUILayout.TextField(b ,""));
                        break;
                    case System.TypeCode.Int16:
                        _RenderCommonValue<Int16>(_Source, b => Convert.ToInt16(GUILayout.TextField(b.ToString())));
                        break;
                    case System.TypeCode.Int32:
                        _RenderCommonValue<Int32>(_Source, b => Convert.ToInt32(GUILayout.TextField(b.ToString())));
                        break;
                    case System.TypeCode.Int64:
                        _RenderCommonValue<Int64>(_Source, b => Convert.ToInt64(GUILayout.TextField(b.ToString())));
                        break;
                    case System.TypeCode.Single:
                        _RenderCommonValue<Single>(_Source, b => Convert.ToSingle(GUILayout.TextField(b.ToString())));
                        break;
                    case System.TypeCode.Object:
                        _RenderObjectValue();
                        break;
                    default:
                        _RenderDefaultValue();
                        break;
                }
            }
            catch (Exception e)
            {
                GUIConsole.Log(GetType().Name + "_RenderDefaultGUI:" + e.Message, e.StackTrace);
            }
        }
    }
}
