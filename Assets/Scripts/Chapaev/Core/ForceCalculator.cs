using Chapaev.Interfaces;
using UnityEngine;

namespace Chapaev.Core
{
    public class ForceCalculator : IForceCalculator
    {
        private Vector3 _pos1;
        private Vector3 _pos2;
		
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
            const float maxForceMagnitude = 10;
            const int k = 10;
            Vector3 force = (_pos2 - _pos1) / k;
            force = force.magnitude > maxForceMagnitude ? force * (maxForceMagnitude / force.magnitude) : force;

            return force;
        }
		
        public void Reset()
        {
            _pos1 = Vector3.zero;
            _pos2 = Vector3.zero;
        }
    }
}