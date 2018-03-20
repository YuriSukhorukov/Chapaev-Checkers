using System;
using Chapaev.Interfaces;
using UnityEngine;

namespace Chapaev.Entities
{
    public class CheckerBase : MonoBehaviour, IPushed, ISelectable
    {
        public event Action SelectEvent;
        
        public void Select()
        {
            if (SelectEvent != null)
                SelectEvent.Invoke();
        }

        public void Push(Vector3 force)
        {
            GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }
}
