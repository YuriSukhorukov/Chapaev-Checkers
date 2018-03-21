using UnityEngine;

namespace Chapaev.Interfaces
{
    public interface IConstructor<out T> where T : class
    {
        T Construct(Vector3 position, string name);
        T Construct(Vector3 position, GameObject parent, string name);
    }
}