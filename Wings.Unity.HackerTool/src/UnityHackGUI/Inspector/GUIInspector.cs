using UnityEngine;

namespace UnityHack
{
    public class GUIInspector : BaseScrollWindow
    {
        protected GameObject _CurrentGameObject;

        protected GUIComponent[] GUIComponents;

        public GUIInspector() : base(Screen.width - 200,0 , 200, Screen.height, 1, "Inspector")
        {
            GUIHierarchy.OnItemChanged += _OnItemChanged;
        }

        /// <summary>
        /// 当前展示对象发生改变时
        /// </summary>
        /// <param name="go"></param>
        protected virtual void _OnItemChanged(GameObject go)
        {
            _CurrentGameObject = go;
            Refresh();
        }

        public virtual void Refresh()
        {
            Component[] coms = _CurrentGameObject.GetComponents<Component>();
            int count = coms.Length;
            GUIComponents = new GUIComponent[count];

            for(int i = 0;i<count;i++)
            {
                GUIComponents[i] = new GUIComponent(coms[i]);
            }
        }

        protected override void _DrawContent()
        {
            if(GUIComponents==null||GUIComponents.Length<0)
            {
                return;
            }
            base._DrawContent();
        }

        protected override void _DrawScrollContent()
        {
            base._DrawScrollContent();
            foreach (GUIComponent com in GUIComponents)
            {
                com.Render();
            }
        }
    }
}
