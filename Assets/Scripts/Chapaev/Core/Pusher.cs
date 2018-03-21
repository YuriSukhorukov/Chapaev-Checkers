using Chapaev.Interfaces;
using UnityEngine;

namespace Chapaev.Core
{
    public class Pusher : IPusher
    {
        private Vector3 _force;

        public void SetForce(Vector3 force)
        {
            _force = force;
        }
        public void Push(IPushed pushed)
        {
            pushed.PushIt(_force);
        }
    }
}