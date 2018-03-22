using Chapaev.Entities;
using Chapaev.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Chapaev.Core
{
    public class PlayerAI
    {
        private Vector3 _aim;

        private Vector3 _pointPress;
        private Vector3 _pointRelease;

        private readonly Board _board;
        private readonly IInputHandler _inputHandler;
        
        private CheckerBase _checkerPush;
        private CheckerBase _checkerTarget;

        private float _t = 0.0f;
        private bool _aimingEnabled;

        public PlayerAI(IInputHandler mouseInputHandler, Board board)
        {
            _inputHandler = mouseInputHandler;
            _board = board;
        }

        private void SelectPushChecker()
        {
            int index = Random.Range(0, _board.CheckersWhite.Count - 1);
            _checkerPush = _board.CheckersBlack[index];
            _pointPress = _checkerPush.transform.position;
        }

        private void SelectTargetChecker()
        {
            int index = Random.Range(0, _board.CheckersWhite.Count - 1);
            _checkerTarget = _board.CheckersWhite[index];
            _pointRelease = _checkerTarget.transform.position;
        }

        private void Press()
        {
            _aimingEnabled = true;
            _inputHandler.OnDown(Camera.main.WorldToScreenPoint(_checkerPush.transform.position));
        }

        private void Release()
        {
            _aimingEnabled = false;
            _inputHandler.OnUp(Camera.main.WorldToScreenPoint(_checkerTarget.transform.position));
        }
        
        
        public void Aiming()
        {
            if (!_aimingEnabled) return;
            
            _t += 0.5f * Time.deltaTime;
            _aim = Vector3.Slerp(_pointPress, _pointRelease, _t);
            if (_aim == _pointRelease)
            {
                _t = 0;
                _aimingEnabled = false;
                Release();
            }
        }

        public void StartAiming()
        {
            SelectPushChecker();
            SelectTargetChecker();
            Press();
        }
    }
}