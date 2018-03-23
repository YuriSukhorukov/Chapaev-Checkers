using UnityEngine;

namespace Assets.Scripts.Chapaev.Interfaces
{
    public interface IPrefabsRepository
    {
        GameObject GetPrefab(int index);
    }
}