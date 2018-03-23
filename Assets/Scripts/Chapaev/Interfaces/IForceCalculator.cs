using UnityEngine;

namespace Assets.Scripts.Chapaev.Interfaces
{
    public interface IForceCalculator
    {
        void SetFirstPoint(Vector3 pos);
        void SetLastPoint(Vector3 pos);
        Vector3 GetForce();
        Vector3 GetForce(Vector3 dist);
    }
}