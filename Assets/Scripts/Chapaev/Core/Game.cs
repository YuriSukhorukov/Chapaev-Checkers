using System;
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
		private TurnSwitcher _turnSwitcher;
		private ForceLine _forceLine;

//		public CheckerColor ActiveCheckerColor;

		private void Start ()
		{	
			_forceCalculator = new ForceCalculator();
			_selector = new Selector3D();
			_pusher = new Pusher();
			_boardBuilder = new BoardBuilder();
			_inputHandler = new MouseInputHandler();

			_board = _boardBuilder.Build();
			_turnSwitcher = new TurnSwitcher(_board);

			foreach (var checker in _board.CheckersWhite.Concat(_board.CheckersBlack))
			{
				var checker1 = checker;
				checker1.SelectEvent += () =>
				{
					if (checker1.CheckerColor == _turnSwitcher.GetActiveColorSide())
					{
						_pushed = checker1.GetComponent<IPushed>();
						_forceLine.SetBeginPoint(checker1.gameObject.transform.position);
					}
				};
				checker1.BouncingBorderEvent += () =>
				{
					_board.RemoveChecker(checker1);
					
					if(checker1.CheckerColor != _turnSwitcher.GetActiveColorSide())
						_turnSwitcher.RepeatActiveColorSide();
				};
			}

			_inputHandler.OnDownEvent += (position) =>
			{
				_selector.SelectFrom(position);
			};
			_inputHandler.OnUpEvent += PushCheckerWithForce;

			_turnSwitcher.MoveCompleteEvent += () =>
			{
				_turnSwitcher.TurnActiveColorSide();
			};

			//AIClick();
			
			_forceLine = new ForceLine();
		}

		private void Update()
		{	
			_turnSwitcher.UpdateState();
			
			if (Input.GetMouseButtonDown(0))
			{
				_inputHandler.OnDown(Input.mousePosition);
			}

			if (Input.GetMouseButtonUp(0))
			{
				_inputHandler.OnUp(Input.mousePosition);
			}

			if (Input.GetMouseButton(0))
			{
				if (_pushed != null)
					_forceLine.SetEndPoint(Input.mousePosition);
			}
//			ActiveCheckerColor = _turnSwitcher.GetActiveColorSide();
		}

		private void PushCheckerWithForce(Vector3 distance)
		{
			if (_pushed == null) return;
			if(_turnSwitcher.IsPossibleMakeMove() == false) return;
			if (((MonoBehaviour) _pushed).GetComponent<CheckerBase>().CheckerColor != _turnSwitcher.GetActiveColorSide()) return;

			_pusher.SetForce(_forceCalculator.GetForce(distance));
			_pusher.Push(_pushed);
			_turnSwitcher.Move();
			_pushed = null;
			
			_forceLine.Hide();
		}

		private void AIClick()
		{
			_inputHandler.OnDown(Camera.main.WorldToScreenPoint(_board.CheckersWhite[0].transform.position));
			_inputHandler.OnUp(Camera.main.WorldToScreenPoint(_board.CheckersBlack[5].transform.position));
		}
	}
}
