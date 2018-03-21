using Chapaev.Interfaces;
using UnityEngine;

namespace Chapaev.Entities
{
    public class CheckerPrefabsRepository : MonoBehaviour, IPrefabsRepository
    {
        [SerializeField] public GameObject[] PrefabsObjects;

        public GameObject GetPrefab(int index)
        {
            return PrefabsObjects[index];
        }
    }
}