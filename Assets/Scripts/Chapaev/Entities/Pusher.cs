using Assets.Scripts.Chapaev.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Chapaev.Entities
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