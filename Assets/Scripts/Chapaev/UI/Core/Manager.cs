using System.Linq;
using Chapaev.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Chapaev.UI
{
    public class Manager : MonoBehaviour
    {
        public Button btn_restart_scene;
        public Button btn_select_white;
        public Button btn_select_black;
        public Text txt_checkers_on_board;
        public Text txt_win_loose;
        public Text txt_current_player;

        public void Initialize()
        {
            btn_restart_scene = FindWithMarkerTag(UITag.BUTTON_RESTART).GetComponent<Button>();
            btn_select_white = FindWithMarkerTag(UITag.BUTTON_SELECT_WHITE).GetComponent<Button>();
            btn_select_black = FindWithMarkerTag(UITag.BUTTON_SELECT_BLACK).GetComponent<Button>();
            txt_checkers_on_board = FindWithMarkerTag(UITag.TEXT_CHECKERS_ON_BOARD).GetComponent<Text>();
            txt_win_loose = FindWithMarkerTag(UITag.TEXT_WIN_LOOSE).GetComponent<Text>();
            txt_current_player = FindWithMarkerTag(UITag.TEXT_CURRENT_PLAYER).GetComponent<Text>();
        }

        public UIMarker FindWithMarkerTag(UITag uiTag)
        {
            return GetComponentsInChildren<UIMarker>(true).Single(
                marker => marker.Tag == uiTag).GetComponent<UIMarker>();
        }
    }
}
