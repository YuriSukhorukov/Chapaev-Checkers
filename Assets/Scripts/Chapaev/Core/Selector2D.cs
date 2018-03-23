using Assets.Scripts.Chapaev.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Chapaev.Core
{
    public class Selector2D : ISelector
    {
        public void SelectFrom(Vector3 coords)
        {
            var hit = Physics2D.Raycast(new Vector2(coords[0], coords[1]), Vector2.zero, 0);
            if (hit)
                hit.transform.gameObject.GetComponent<ISelectable>().Select();
        }
    }
}