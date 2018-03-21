using System;
using Chapaev.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Chapaev.Core
{
    public class MouseInputHandler : IInputHandler
    {
        public event Action<Vector3> OnDownEvent;
        public event Action<Vector3> OnUpEvent;

        private Vector3 _posDown;
        private Vector3 _posUp;
        
        public void OnDown(Vector3 posDown)
        {
            _posDown = Vector3.zero;
            _posUp = Vector3.zero;
            
            _posDown.x = posDown.x;
            _posDown.z = posDown.y;
            
            if(OnDownEvent != null)
                OnDownEvent(posDown);
        }

        public void OnUp(Vector3 posUp)
        {
            _posUp.x = posUp.x;
            _posUp.z = posUp.y;
            
            if(OnUpEvent != null)
                OnUpEvent(_posUp - _posDown);
        }
    }
}