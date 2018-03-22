using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chapaev.Entities
{
    public class Board
    {
        public List<CheckerBase> CheckersWhite { get; private set; }
        public List<CheckerBase> CheckersBlack { get; private set; }
        
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
            
            Object.Destroy(checker.gameObject);
        }
    }
}