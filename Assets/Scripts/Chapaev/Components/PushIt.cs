using Chapaev.Interfaces;
using UnityEngine;

namespace Chapaev.Components
{
    public class PushIt: MonoBehaviour, IPushed {
        public void Push(Vector3 force)
        {
            GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }
}
