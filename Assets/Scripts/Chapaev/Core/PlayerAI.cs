using Chapaev.Entities;
using Chapaev.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Chapaev.Core
{
    public class PlayerAI
    {
        public Vector3 PointPress;
        public Vector3 PointRelease;

        private readonly Board _board;
        private readonly IInputHandler _inputHandler;
        
        private CheckerBase _checkerPush;
        private CheckerBase _checkerTarget;

        public PlayerAI(IInputHandler mouseInputHandler, Board board)
        {
            _inputHandler = mouseInputHandler;
            _board = board;
        }

        public void Press()
        {
            _inputHandler.OnDown(Camera.main.WorldToScreenPoint(_checkerPush.transform.position));
        }

        public void Release()
        {
            _inputHandler.OnUp(Camera.main.WorldToScreenPoint(_checkerTarget.transform.position));
        }

        public void SelectPushChecker()
        {
            int index = Random.Range(0, _board.CheckersWhite.Count);
            _checkerPush = _board.CheckersBlack[index];
        }

        public void SelectTargetChecker()
        {
            int index = Random.Range(0, _board.CheckersWhite.Count);
            _checkerTarget = _board.CheckersWhite[index];
        }
    }
}