using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Chapaev.UI
{
    public class Manager : MonoBehaviour
    {
        public Button btn_select_white;
        public Button btn_select_black;
        public Text txt_checkers_on_board;

        public void Initialize()
        {
            btn_select_white = FindWithMarkerTag(UITag.BUTTON_SELECT_WHITE).GetComponent<Button>();
            btn_select_black = FindWithMarkerTag(UITag.BUTTON_SELECT_BLACK).GetComponent<Button>();
            txt_checkers_on_board = FindWithMarkerTag(UITag.TEXT_CHECKERS_ON_BOARD).GetComponent<Text>();
        }

        public UIMarker FindWithMarkerTag(UITag uiTag)
        {
            return FindObjectsOfType<UIMarker>().Single(
                marker => marker.Tag == uiTag).GetComponent<UIMarker>();
        }
    }
}
