using System.Collections.Generic;
using System.Linq;

namespace Chapaev.Entities
{
    public class Board
    {
        private List<CheckerBase> CheckersWhite { get; set; }
        private List<CheckerBase> CheckersBlack { get; set; }
        
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
    }
}