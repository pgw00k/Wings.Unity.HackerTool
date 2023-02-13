using System;
using System.Reflection;
using UnityEngine;

namespace UnityHack
{
    public class GUIPropertyInfo : GUIMemberInfo<PropertyInfo>
    {
        public GUIPropertyInfo(PropertyInfo info, object target) : base(info, target)
        {
            _MemberType = (HackTypeCode)Type.GetTypeCode(_Source.PropertyType);
            _TypeCode = System.Type.GetTypeCode(_Source.PropertyType);
        }

        protected override void _DrawFirstLineLabel()
        {
            base._DrawFirstLineLabel();
            GUILayout.Label(_Source.PropertyType.Name);
        }

        protected override void _RenderCommonValue<CommonType>(PropertyInfo info, GUIRenderFunction<CommonType, CommonType> RenderFunction)
        {
            base._RenderCommonValue(info, RenderFunction);
#if NET40
            CommonType value = (CommonType)info.GetValue(_Target, null);
            value = RenderFunction.Invoke(value);
            info.SetValue(_Target, value, null);
#elif NET45

            CommonType value = (CommonType)info.GetValue(_Target);
            value = RenderFunction.Invoke(value);
            info.SetValue(_Target, value);

#endif
        }

        protected override void _RenderDefaultValue()
        {
            base._RenderDefaultValue();
            GUILayout.Label(_Source.PropertyType.Name);
        }

        protected override void _RenderObjectValue()
        {
            base._RenderObjectValue();
            object obj = _Source.GetValue(_Target, null);
            obj = GUIUnityTypeRanderManager.Instance[_Source.PropertyType].Invoke(obj);
            _Source.SetValue(_Target, obj,null);
        }
    }
}
