using UnityEditor;
using UnityEngine;

namespace Chapaev.Interfaces
{
    public interface IPrefabsRepository
    {
        GameObject GetPrefab(int index);
    }
}