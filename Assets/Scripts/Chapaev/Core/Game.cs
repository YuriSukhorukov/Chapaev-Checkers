using System.Linq;
using Assets.Scripts.Chapaev.Core;
using Chapaev.Entities;
using Chapaev.Interfaces;
using UnityEngine;

namespace Chapaev.Core
{
	public class Game : MonoBehaviour
	{
		private IInputHandler _inputHandler;
		private IForceCalculator _forceCalculator;
		private IBoardBuilder _boardBuilder;
		private ISelector _selector;
		private IPusher _pusher;
		private IPushed _pushed;
		private Board _board;

		public CheckerColor ActiveCheckerColor;

		private void Start () {
			_forceCalculator = new ForceCalculator();
			_selector = new Selector3D();
			_pusher = new Pusher();
			_boardBuilder = new BoardBuilder();
			_inputHandler = new MouseInputHandler();

			_board = _boardBuilder.Build();

			foreach (var checker in _board.CheckersWhite.Concat(_board.CheckersBlack))
			{
				var checker1 = checker;
				checker1.SelectEvent += () => { _pushed = checker1.GetComponent<IPushed>(); };
			}

			_inputHandler.OnDownEvent += (position) => _selector.SelectFrom(position);
			_inputHandler.OnUpEvent += (distance) =>
			{
				_pusher.SetForce(_forceCalculator.GetForce(distance));
				MakeMove();
			};

			AIClick();
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				print(Input.mousePosition);
				_inputHandler.OnDown(Input.mousePosition);
			}

			if (Input.GetMouseButtonUp(0))
			{
				_inputHandler.OnUp(Input.mousePosition);
			}
		}
		
		private void MakeMove()
		{	
			if(_pushed == null) return;
			
			if(((MonoBehaviour) _pushed).GetComponent<CheckerBase>().CheckerColor != ActiveCheckerColor) return;
			
			ActiveCheckerColor = ActiveCheckerColor == CheckerColor.WHITE ? CheckerColor.BLACK : CheckerColor.WHITE;
			
			_pusher.Push(_pushed);
			_pushed = null;
		}

		private void AIClick()
		{
			_inputHandler.OnDown(Camera.main.WorldToScreenPoint(_board.CheckersWhite[0].transform.position));
			_inputHandler.OnUp(Camera.main.WorldToScreenPoint(_board.CheckersBlack[5].transform.position));
		}
	}
}
