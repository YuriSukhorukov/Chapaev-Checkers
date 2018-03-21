using System;
using UnityEngine;

namespace Chapaev.Interfaces
{
    public interface IInputHandler
    {
        event Action<Vector3> OnDownEvent;
        event Action<Vector3> OnUpEvent;
        
        void OnDown(Vector3 posDown);
        void OnUp(Vector3 posUp);
    }
}