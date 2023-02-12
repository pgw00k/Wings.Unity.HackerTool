using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GenOcean.Common;
using UnityEngine;

namespace UnityHack
{
    public class Program
    {

        protected static HackGUIRenderMain _Instance = null;

        public static void Load()
        {
            Shell.OpenConsole();
            Console.WriteLine("Inject success!");
            FindType("UnityEngine.GameObject,UnityEngine.dll");


            if (_Instance == null)
            {
                GameObject go = new GameObject(typeof(HackGUIRenderMain).Name);
                _Instance = go.AddComponent<HackGUIRenderMain>();
                _Instance.Init();
            }
        }

        public static void FindType(string key)
        {

            //获取需要Hook的类型
            Type type = Type.GetType(key);
            if (type != null)
            {
                Console.WriteLine("Get type " + type.Name);
            }
            else
            {
                Console.WriteLine("Can not find class key");
                return;
            }
        }

    }

    public class HackGUIRenderMain : MonoBehaviour
    {
        protected virtual void Start()
        {
            Console.WriteLine("Hook GUI Start!");
            Init();
        }

        public virtual string Init()
        {
            DontDestroyOnLoad(gameObject);
            return gameObject.name;
        }

        public virtual void OnGUI()
        {
            if(GUILayout.Button("Test"))
            { }
        }
    }
}
