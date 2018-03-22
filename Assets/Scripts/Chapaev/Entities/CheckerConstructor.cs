using Assets.Scripts.Chapaev.Values;
using Chapaev.Interfaces;
using UnityEngine;

namespace Chapaev.Entities
{
    public class CheckerConstructor: IConstructor<CheckerBase>
    {
        private IPrefabsRepository CheckerPrefabsRepository { get; set; }
        public CheckerConstructor(IPrefabsRepository checkersPrefabsRepository)
        {
            CheckerPrefabsRepository = checkersPrefabsRepository;
        }
        
        public CheckerBase Construct(Vector3 position, string name)
        {
            CheckerColor checkerColor;
            
            int indexPrefab = 0;
            if (name.Contains("white"))
            {
                indexPrefab = 0;
                checkerColor = CheckerColor.WHITE;
            }
            else if (name.Contains("black"))
            {
                indexPrefab = 1;
                checkerColor = CheckerColor.BLACK;
            }
            else
                checkerColor = CheckerColor.WHITE;


            var figureGo =
                GameObject.Instantiate(CheckerPrefabsRepository.GetPrefab(indexPrefab));

            figureGo.name = name;

            CheckerBase checkerBase = figureGo.GetComponent<CheckerBase>();
            checkerBase.CheckerColor = checkerColor;
            
            return figureGo.GetComponent<CheckerBase>();
        }
        
        public CheckerBase Construct(Vector3 position, GameObject parent, string name)
        {
            int indexPrefab = 0;
            if (name.Contains("white"))
                indexPrefab = 0;
            else if (name.Contains("black"))
                indexPrefab = 1;


            var figureGo =
                GameObject.Instantiate(CheckerPrefabsRepository.GetPrefab(indexPrefab),
                    parent.transform);

            return figureGo.GetComponent<CheckerBase>();
        }
    }
}