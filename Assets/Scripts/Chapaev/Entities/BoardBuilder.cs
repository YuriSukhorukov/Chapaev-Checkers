using System.Collections.Generic;
using System.Linq;
using Chapaev.Interfaces;
using Chapaev.Entities;
using UnityEngine;

namespace Chapaev.Entities
{
    public class BoardBuilder : IBoardBuilder
    {
        public Board Build()
        {
            var board = new Board();
            var checkersPrefabsRepository = GameObject.Find("CheckerPrefabsRepository").GetComponent<CheckerPrefabsRepository>();
            IConstructor<CheckerBase> figureConstructor = new CheckerConstructor(checkersPrefabsRepository);
            
            var figuresWhite = CreateCheckers(8, figureConstructor, "checker_white");
            var figuresBlack = CreateCheckers(8, figureConstructor, "checker_black");
            
            board.AddWhiteFigures(figuresWhite);
            board.AddBlackFigures(figuresBlack);
            
            Arrange(figuresWhite.ToList());
            Arrange(figuresBlack.ToList());

            return board;
        }
        
        private static IEnumerable<CheckerBase> CreateCheckers(int count, IConstructor<CheckerBase> constructor, string name)
        {
            var figures = new List<CheckerBase>();
			
            for (var i = 0; i < count; i++)
            {
                figures.Add(
                    constructor.Construct(
                        new Vector2(0, 0),
                        name + "-" + i
                    )
                );
            }

            return figures;
        }

        private static void Arrange(IList<CheckerBase> checkers)
        {
            for (var i = 0; i < checkers.Count; i++)
            {
                var pos = checkers[i].transform.position;
                pos.x += i * 1.25f;
                checkers[i].transform.position = pos;
            }
        }
    }
}