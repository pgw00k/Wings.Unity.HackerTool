using System;
using System.Reflection;
using UnityEngine;

namespace UnityHack
{
    public class GUIFieldInfo : GUIMemberInfo<FieldInfo>
    {
        public GUIFieldInfo(FieldInfo info, object target) : base(info, target)
        {
            _MemberType = (HackTypeCode)Type.GetTypeCode(_Source.FieldType);
        }

        protected override void _DrawFirstLineLabel()
        {
            base._DrawFirstLineLabel();
            GUILayout.Label(_Source.FieldType.Name);
        }
    }
}
