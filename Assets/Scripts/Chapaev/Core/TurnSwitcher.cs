using System;
using Assets.Scripts.Chapaev.Values;
using Chapaev.Entities;
using UnityEngine;

namespace Assets.Scripts.Chapaev.Core
{
    public class TurnSwitcher
    {
        public event Action MoveCompleteEvent;
        private Board _board;
        private bool _canMove = true;
        private CheckerColor _activeCheckerColor;
        private bool _repeatMove = false;

        public TurnSwitcher(Board board)
        {
            _board = board;
        }

        public void UpdateState()
        {
            bool noOneCheckerMove = true;
            for (int i = 0; i < _board.CheckersWhite.Count; i++)
            {
                if (_board.CheckersWhite[i].GetSpeed() != 0)
                {
                    noOneCheckerMove = false;
                    break;
                }
            }

            for (int i = 0; i < _board.CheckersBlack.Count; i++)
            {
                if (_board.CheckersBlack[i].GetSpeed() != 0)
                {
                    noOneCheckerMove = false;
                    break;
                }
            }

            if (_canMove != noOneCheckerMove)
            {
                _canMove = noOneCheckerMove;
                if (MoveCompleteEvent != null)
                    MoveCompleteEvent();
            }
        }

        public void Move()
        {
            _canMove = false;
            _repeatMove = false;
        }

        public void TurnActiveColorSide()
        {
            if(!_repeatMove)
                _activeCheckerColor = _activeCheckerColor == CheckerColor.WHITE ? CheckerColor.BLACK : CheckerColor.WHITE;
        }

        public CheckerColor GetActiveColorSide()
        {
            return _activeCheckerColor;
        }

        public void RepeatActiveColorSide()
        {
            _repeatMove = true;
        }

        public bool IsPossibleMakeMove()
        {
            return _canMove;
        }
    }
}