using Assets.Scripts.Chapaev.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Chapaev.Core
{
    public class Selector3D: ISelector
    {
        public void SelectFrom(Vector3 coords)
        {
            RaycastHit hit; 
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(coords[0], coords[1], coords[2]));
            Physics.Raycast(ray, out hit, 100.0f);

            if (hit.collider == null) return;
            if (hit.collider.gameObject.GetComponent<ISelectable>() == null) return;
                hit.collider.gameObject.GetComponent<ISelectable>().Select();
        }
    }
}