using System;
using UnityEngine;

namespace UnityHack
{
    public class GUITreeItem : GUIFoldoutItem
    {
        #region 公共事件

        public static Action<GUITreeItem> OnClickCommon = null;

        #endregion

        /// <summary>
        /// 源GameObject对象
        /// </summary>
        public GameObject Source;

        /// <summary>
        /// 子对象
        /// </summary>
        public GUITreeItem[] Children;

        public GUITreeItem(GameObject go,int level = 0)
        {
            Source = go;
            TreeLevel = level;
            string prefix = "";
            for(int i = 0; i < TreeLevel; i++)
            {
                prefix += "  ";
            }
            _ExpandName = prefix + "▼";
            _UnexpandName = prefix + "▶";
        }

        public override void Expand()
        {
            base.Expand();
            if (IsExpand && Children == null)
            {
                int count = Source.transform.childCount;
                Children = new GUITreeItem[count];
                for (int i = 0; i < count; i++)
                {
                    Children[i] = new GUITreeItem(Source.transform.GetChild(i).gameObject, TreeLevel + 1);
                }
            }
        }

        public override void ExpandRender()
        {
            base.ExpandRender();
            int count = Children.Length;
            for (int i = 0; i < count; i++)
            {
                Children[i].Render();
            }
        }

        public override void ClickEvent()
        {
            base.ClickEvent();
            OnClickCommon?.Invoke(this);
        }

        protected override void _DrawFirstLineLabel()
        {
            base._DrawFirstLineLabel();
            IsActive = GUILayout.Toggle(IsActive, Source.name, HackGUIStyle.TreeItemButtonStyle);
        }

        public override void Render()
        {
            if(!Source)
            {
                GUILayout.Label("Not exist.");
                return;
            }

            base.Render();
        }
    }

}
