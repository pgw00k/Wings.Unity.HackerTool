using UnityEngine;
using System.Reflection;
using System.IO;
using System;

namespace UnityHack
{
    public class GUIComponent:GUIFoldoutItem
    {
        protected Component _Source;

        protected IHackGUIRender[] _MemberInfoes;

        public GUIComponent(Component com)
        {
            _Source = com;
            _ExpandName ="▼";
            _UnexpandName ="▶";
        }

        public virtual void Refresh()
        {
            _MemberInfoes = GUIUnityComponentRenderManager.Instance[_Source.GetType()].Invoke(_Source);
        }

        public override void Expand()
        {
            Refresh();
        }

        protected override void _DrawFirstLineLabel()
        {
            base._DrawFirstLineLabel();
            GUILayout.Label(_Source.GetType().Name);
        }

        public override void ExpandRender()
        {
            base.ExpandRender();

            try
            {
                foreach (IHackGUIRender i in _MemberInfoes)
                {
                    i.Render();
                }
            }catch(Exception e)
            {
                GUIConsole.Log(string.Format("{0}.ExpandRender:{1}", GetType().Name, e.Message),e.StackTrace);
            }
        }
    }
}
