using System;
using Assets.Scripts.Chapaev.Interfaces;
using Assets.Scripts.Chapaev.Values;
using UnityEngine;

namespace Assets.Scripts.Chapaev.Entities
{
    public class CheckerBase : MonoBehaviour, ISelectable
    {
        public event Action SelectEvent;
        public event Action BouncingBorderEvent;
        public CheckerColor CheckerColor { get; set; }
        
        private Rigidbody _rigidbody;

        private void OnEnable()
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();
        }

        public void Select()
        {
            if (SelectEvent != null)
                SelectEvent.Invoke();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.name == "Chess_Board" && other.collider is BoxCollider)
            {
                if (BouncingBorderEvent != null)
                    BouncingBorderEvent();
            }
        }

        public float GetSpeed()
        {
            return _rigidbody.velocity.magnitude;
        }
    }
}
