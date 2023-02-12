using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityHack
{
    public class Program
    {
        protected static HackGUIRenderMain _Instance = null;
        public static void Load()
        {
            if(_Instance == null)
            {
                GameObject go = new GameObject(typeof(HackGUIRenderMain).Name);
                _Instance = go.AddComponent<HackGUIRenderMain>();
            }
        }

        public static void UnLoad()
        {
            if (_Instance != null)
            {
                GameObject.Destroy(_Instance.gameObject);
            }
        }
    }

    public class HackGUIRenderMain : MonoBehaviour
    {

        protected GUIHierarchy _Hierarchy;
        protected GUIInspector _Inspector;
        protected GUIConsole _Console;
        protected GUIHackToolArea _ToolArea;

        protected virtual void Start()
        {
            //GUIConsole.Log("Hack Start!");
            DontDestroyOnLoad(gameObject);
            HackGUIStyle.Init();
            _Console = new GUIConsole();
            _ToolArea = new GUIHackToolArea();
            _Hierarchy = new GUIHierarchy();
            _Inspector = new GUIInspector();
        }

        public virtual void OnGUI()
        {
            _Console?.Render();
            _ToolArea?.Render();
            _Hierarchy?.Render();
            _Inspector?.Render();
        }
    }
}
