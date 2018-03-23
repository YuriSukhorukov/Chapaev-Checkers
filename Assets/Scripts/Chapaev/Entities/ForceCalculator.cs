using Assets.Scripts.Chapaev.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Chapaev.Entities
{
    public class ForceCalculator : IForceCalculator
    {
        private Vector3 _pos1;
        private Vector3 _pos2;

        private const float MaxForceMagnitude = 20;
        private const int K = 10;
		
        public void SetFirstPoint(Vector3 pos)
        {
            _pos1.x = pos.x;
            _pos1.z = pos.y;
        }
		
        public void SetLastPoint(Vector3 pos)
        {
            _pos2.x = pos.x;
            _pos2.z = pos.y;
        }
		
        public Vector3 GetForce()
        {   
            Vector3 force = _pos2 - _pos1 / K;
            force = force.magnitude > MaxForceMagnitude ? force * (MaxForceMagnitude / force.magnitude) : force;

            return force;
        }
        
        public Vector3 GetForce(Vector3 distance)
        {   
            Vector3 force = distance / K;
            force = force.magnitude > MaxForceMagnitude ? force * (MaxForceMagnitude / force.magnitude) : force;

            return force;
        }
		
        public void Reset()
        {
            _pos1 = Vector3.zero;
            _pos2 = Vector3.zero;
        }
    }
}