using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Chapaev.Values;

namespace Assets.Scripts.Chapaev.Entities
{
    public class Board
    {
        public List<CheckerBase> CheckersWhite { get; private set; }
        public List<CheckerBase> CheckersBlack { get; private set; }

        public event Action<CheckerColor> CheckersIsEmty;
        
        public void AddWhiteFigure(CheckerBase checker)
        {
            if(CheckersWhite == null)
                CheckersWhite = new List<CheckerBase>();
            CheckersWhite.Add(checker);
        }
        
        public void AddWhiteFigures(IEnumerable<CheckerBase> checkers)
        {
            if(CheckersWhite == null)
                CheckersWhite = new List<CheckerBase>();
            CheckersWhite = checkers.ToList();
        }
        
        public void AddBlackFigure(CheckerBase checker)
        {
            if(CheckersBlack == null)
                CheckersBlack = new List<CheckerBase>();
            CheckersBlack.Add(checker);
        }
        
        public void AddBlackFigures(IEnumerable<CheckerBase> checkers)
        {
            if(CheckersWhite == null)
                CheckersWhite = new List<CheckerBase>();
            CheckersBlack = checkers.ToList();
        }

        public void RemoveChecker(CheckerBase checker)
        {   
            if(checker.CheckerColor == CheckerColor.WHITE)
                 CheckersWhite.Remove(checker);
            else if(checker.CheckerColor == CheckerColor.BLACK)
                CheckersBlack.Remove(checker);
            
            checker.gameObject.SetActive(false);

            CheckEmpty();
        }

        public void CheckEmpty()
        {
            if (CheckersWhite.Count == 0)
            {
                if (CheckersIsEmty != null)
                    CheckersIsEmty(CheckerColor.WHITE);
            }
            if (CheckersBlack.Count == 0)
            {
                if (CheckersIsEmty != null)
                    CheckersIsEmty(CheckerColor.BLACK);
            }
        }
        
        public int GetCheckersWhiteCount()
        {
            return GetCheckersCount(CheckersWhite);
        }
        public int GetCheckersBlackCount()
        {
            return GetCheckersCount(CheckersBlack);
        }
        
        private int GetCheckersCount(List<CheckerBase> checkers)
        {
            int count = 0;
            for (int i = 0; i < checkers.Count; i++)
            {
                if (checkers[i].enabled) count++;
            }
            return count;
        }
    }
}