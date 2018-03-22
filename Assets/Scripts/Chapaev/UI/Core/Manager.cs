using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Chapaev.UI
{
    public class Manager : MonoBehaviour
    {
        public Button btn_select_white;
        public Button btn_select_black;

        public void Initialize()
        {
            btn_select_white = FindObjectsOfType<UIMarker>().Single(
                marker => marker.Tag == UITag.BUTTON_SELECT_WHITE
            ).GetComponent<Button>();

            btn_select_black = FindObjectsOfType<UIMarker>().Single(
                marker => marker.Tag == UITag.BUTTON_SELECT_BLACK
            ).GetComponent<Button>();
        }
    }
}
