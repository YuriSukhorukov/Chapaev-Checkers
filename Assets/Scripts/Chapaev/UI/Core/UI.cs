using UnityEngine;

namespace Assets.Scripts.Chapaev.UI
{
    public class UI
    {
        public readonly Manager Manager;
        public UI()
        {
            GameObject canvasGO = GameObject.Find("Canvas");
            Manager = canvasGO.AddComponent<Manager>();
            Manager.Initialize();
        }
    }
}