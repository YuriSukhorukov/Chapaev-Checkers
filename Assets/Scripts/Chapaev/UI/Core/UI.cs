using System;
using UnityEngine;

namespace Assets.Scripts.Chapaev.UI
{
    public class UI
    {
        public readonly Manager Manager;
        public UI()
        {
            GameObject canvasGO = GameObject.Find("Canvas");
            Manager = canvasGO.AddComponent<Manager>();
            Manager.Initialize();
        }

        public void SetCheckersCountInText(int whiteCheckersCount, int blackCheckersCount)
        {
            Manager.txt_checkers_on_board.text = String.Format(
                "White: {0}; Black: {1}",
                whiteCheckersCount.ToString(),
                blackCheckersCount.ToString()
            );
        }

        public void SelectYourColourText()
        {
            Manager.txt_checkers_on_board.text = "select your color";
        }

        public void WinText()
        {
            Manager.txt_win_loose.text = "YOU WIN!";
        }
        public void LooseText()
        {
            Manager.txt_win_loose.text = "YOU LOOSE!";
        }

        public void ShowRestartButton()
        {
            Manager.btn_restart_scene.gameObject.SetActive(true);
        }
    }
}