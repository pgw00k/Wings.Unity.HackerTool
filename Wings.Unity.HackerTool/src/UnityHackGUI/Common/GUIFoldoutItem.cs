using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityHack
{
    public class GUIFoldoutItem : IHackGUIRender, IHackGUIFoldout
    {
        /// <summary>
        /// 点击事件
        /// </summary>
        public Action<GUIFoldoutItem> OnClick = null;

        /// <summary>
        /// 记录上一次是否展开
        /// </summary>
        protected bool _PreIsExpand = false;

        /// <summary>
        /// 记录上一次是否激活
        /// </summary>
        protected bool _PreIsActive = false;

        protected string _ExpandName;
        protected string _UnexpandName;

        protected bool _IsExpand = false;


        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpand
        {
            get
            {
                return _IsExpand;
            }
            set
            {
                _IsExpand = value;
            }
        }

        /// <summary>
        /// 是否选中激活
        /// </summary>
        public bool IsActive = false;

        /// <summary>
        /// 树结构层级
        /// </summary>
        public int TreeLevel = 0;

        public GUIFoldoutItem()
        {
        }

        public GUIFoldoutItem(string ueName,string eName = null)
        {
            _UnexpandName = ueName;
            _ExpandName = eName ?? ueName;
        }

        /// <summary>
        /// 展开
        /// </summary>
        public virtual void Expand()
        {
        }


        /// <summary>
        /// 收回
        /// </summary>
        public virtual void Unexpand()
        {
        }

        /// <summary>
        /// 展开时的持续渲染
        /// <para>一般来说，为了性能考虑，使用Bool值来判断然后在Expand将需要渲染的元素添加到对应的管理区域中</para>
        /// </summary>
        public virtual void ExpandRender()
        {
        }

        public virtual void ClickEvent()
        {
            OnClick?.Invoke(this);
        }

        /// <summary>
        /// 绘制收缩栏的默认行（一直显示）
        /// </summary>
        protected virtual void _DrawFirstLineLabel()
        {
            IsExpand = GUILayout.Toggle(IsExpand, IsExpand ? _ExpandName : _UnexpandName, HackGUIStyle.TreeItemToggleStyle);
        }

        public virtual void Render()
        {
            GUILayout.BeginHorizontal();
            _DrawFirstLineLabel();
            GUILayout.EndHorizontal();

            if (IsExpand != _PreIsExpand)
            {
                _PreIsExpand = IsExpand;
                Expand();
            }

            if (IsActive != _PreIsActive)
            {
                _PreIsActive = IsActive;
                ClickEvent();
            }

            if (IsExpand)
            {
                ExpandRender();
            }
        }
    }
}
