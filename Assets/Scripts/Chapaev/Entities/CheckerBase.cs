using System;
using Chapaev.Interfaces;
using UnityEngine;

namespace Chapaev.Entities
{
    public class CheckerBase : MonoBehaviour, ISelectable
    {
        public event Action SelectEvent;
        public CheckerColor CheckerColor { get; set; }
        
        public void Select()
        {
            if (SelectEvent != null)
                SelectEvent.Invoke();
        }
    }
}
