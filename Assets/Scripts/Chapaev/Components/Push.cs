using Chapaev.Interfaces;
using UnityEngine;

namespace Chapaev.Components
{
    public class Push: MonoBehaviour, IPushed
    {
        private Rigidbody Rigidbody { get; set; }
        public void PushIt(Vector3 force)
        {
            if (Rigidbody == null)
                Rigidbody = GetComponent<Rigidbody>();
            Rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}
