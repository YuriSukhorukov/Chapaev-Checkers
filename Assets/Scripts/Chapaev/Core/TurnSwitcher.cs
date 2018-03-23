using System;
using Assets.Scripts.Chapaev.Entities;
using Assets.Scripts.Chapaev.Values;

namespace Assets.Scripts.Chapaev.Core
{
    public class TurnSwitcher
    {
        public event Action MoveCompleteEvent;
        
        private Board _board;
        private bool _canMove = true;
        private CheckerColor _activeCheckerColor;
        private bool _repeatMove = false;
        
        public CheckerColor PlayerCheckerColor;
        public CheckerColor EnemyCheckerColor;

        public TurnSwitcher(Board board)
        {
            _board = board;
        }
        
        public void SetPlayerColor(CheckerColor checkerColor)
        {			
            PlayerCheckerColor = checkerColor;
            EnemyCheckerColor = PlayerCheckerColor == CheckerColor.WHITE ? CheckerColor.BLACK : CheckerColor.WHITE;
        }

        public void UpdateState()
        {
            bool noOneCheckerMove = true;
            for (int i = 0; i < _board.CheckersWhite.Count; i++)
            {
                if (Math.Abs(_board.CheckersWhite[i].GetSpeed()) > 0.02f)
                {
                    noOneCheckerMove = false;
                    break;
                }
            }

            for (int i = 0; i < _board.CheckersBlack.Count; i++)
            {
                if (Math.Abs(_board.CheckersBlack[i].GetSpeed()) > 0.02f)
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