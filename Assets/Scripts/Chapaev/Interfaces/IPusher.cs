using UnityEngine;

namespace Assets.Scripts.Chapaev.Interfaces
{
    public interface IPusher
    {
        void SetForce(Vector3 force);
        void Push(IPushed pushed);
    }
}