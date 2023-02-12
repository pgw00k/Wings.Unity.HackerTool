using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityHack
{
    /// <summary>
    /// 使用GUI进行场景树状图的绘制
    /// </summary>
    public class GUIHierarchy : BaseScrollWindow
    {
        protected Dictionary<int, GUITreeItem> _GameObjects = new Dictionary<int, GUITreeItem>();
        public Scene Scene;

        protected GUITreeItem _CurrentItem = null;

        public static Action<GameObject> OnItemChanged = null;

        protected string sParams = "";

        /// <summary>
        /// 刷新列表
        /// </summary>
        protected void _Refresh(Predicate<GameObject> FuncIsAddToList)
        {
            _GameObjects.Clear();
            GameObject[] gos = GameObject.FindObjectsOfType<GameObject>();
            try
            {
                foreach (GameObject go in gos)
                {
                    if (FuncIsAddToList(go))
                    {
                        GUITreeItem treeItem = new GUITreeItem(go, 0);
                        _GameObjects.Add(go.GetInstanceID(), treeItem);
                    }
                }
            }
            catch (Exception e)
            {
                GUIConsole.Log(string.Format("{0}._Refresh:{1}", GetType().Name, e.Message),e.StackTrace);
            }
        }

        public void Refresh()
        {
            _Refresh(go => !go.transform.parent);
        }


        public void Search(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                GUIConsole.Log("Null params");
                return;
            }

            string[] args = str.Split(':');
            if (args == null || args.Length < 2)
            {
                args = new string[] { "n", str };
            }

            switch (args[0].ToLower())
            {
                case "n":
                    _Refresh(go => go.name.ToLower().Contains(args[1].ToLower()));
                    break;
                case "t":
                    _Refresh(go =>
                    {
                        foreach (Component cp in go.GetComponents<Component>())
                        {
                            if (cp.GetType().Name.ToLower().Contains(args[1].ToLower()))
                            {
                                return true;
                            }
                        }
                        return false;
                    });
                    GUIConsole.Log(string.Format("Find {0} Gameobjects by {1}", _GameObjects.Count, str));
                    break;
            }
        }

        public GUIHierarchy() : base(0, 0, 200, Screen.height, 0, "Hierarchy")
        {
            Refresh();
            GUITreeItem.OnClickCommon += _OnItemClick;
        }

        protected virtual void _OnItemClick(GUITreeItem newItem)
        {
            if (_CurrentItem != newItem)
            {
                if (_CurrentItem != null)
                {
                    _CurrentItem.IsActive = false;
                }

                _CurrentItem = newItem;
                OnItemChanged?.Invoke(newItem.Source);
            }
            _CurrentItem.IsActive = true;
        }

        protected override void _DrawBeforeScrollContent()
        {
            base._DrawBeforeScrollContent();
            if (GUILayout.Button("Refresh"))
            {
                Refresh();
            }

            GUILayout.BeginHorizontal();
            sParams = GUILayout.TextArea(sParams);
            if (GUILayout.Button("S"))
            {
                Search(sParams);
            }
            GUILayout.EndHorizontal();
        }

        protected override void _DrawScrollContent()
        {
            foreach (KeyValuePair<int, GUITreeItem> p in _GameObjects)
            {
                p.Value.Render();
            }
        }
    }
}
