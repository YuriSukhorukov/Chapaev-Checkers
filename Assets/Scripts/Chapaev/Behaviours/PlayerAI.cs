using System.Collections.Generic;
using Assets.Scripts.Chapaev.Entities;
using Assets.Scripts.Chapaev.Interfaces;
using Assets.Scripts.Chapaev.Values;
using UnityEngine;

namespace Assets.Scripts.Chapaev.Behaviours
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

        private List<CheckerBase> _myCheckers;
        private List<CheckerBase> _enemyCheckers;

        public PlayerAI(IInputHandler mouseInputHandler, Board board)
        {
            _inputHandler = mouseInputHandler;
            _board = board;
        }

        public void SetColor(CheckerColor checkerColor)
        {
            switch (checkerColor)
            {
                case CheckerColor.WHITE:
                    _myCheckers = _board.CheckersWhite;
                    _enemyCheckers = _board.CheckersBlack;
                    break;
                case CheckerColor.BLACK:
                    _myCheckers = _board.CheckersBlack;
                    _enemyCheckers = _board.CheckersWhite;
                    break;
            }
        }

        private void SelectPushChecker()
        {
            int index = Random.Range(0, _myCheckers.Count - 1);
            _checkerPush = _myCheckers[index];
            _pointPress = _checkerPush.transform.position;
        }

        private void SelectTargetChecker()
        {
            int index = Random.Range(0, _enemyCheckers.Count - 1);
            _checkerTarget = _enemyCheckers[index];
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