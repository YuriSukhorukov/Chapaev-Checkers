using System;
using System.Linq;
using Assets.Scripts.Chapaev.Core;
using Assets.Scripts.Chapaev.Values;
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
		private PlayerAI _playerAI;

		private GameState _state = GameState.BEGIN;

//		public CheckerColor ActiveCheckerColor;

		private void Start ()
		{	
			_forceCalculator = new ForceCalculator();
			_selector = new Selector3D();
			_pusher = new Pusher();
			_forceLine = new ForceLine();
			_boardBuilder = new BoardBuilder();
			_inputHandler = new MouseInputHandler();

			_board = _boardBuilder.Build();
			_turnSwitcher = new TurnSwitcher(_board);
			_playerAI = new PlayerAI(_inputHandler, _board);

			_board.CheckersIsEmty += (color) =>
			{
				if (color == CheckerColor.BLACK)
					print("win");
				else if (color == CheckerColor.BLACK)
					print("loose");

				_state = GameState.GAME_OVER;
			};

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
				if (_state == GameState.BEGIN)
					_state = GameState.PLAY;
				if(_state == GameState.PLAY)
					_selector.SelectFrom(position);
			};
			_inputHandler.OnUpEvent += PushCheckerWithForce;

			_turnSwitcher.MoveCompleteEvent += () =>
			{
				_turnSwitcher.TurnActiveColorSide();
				if (_turnSwitcher.GetActiveColorSide() == CheckerColor.BLACK)
				{
					_playerAI.StartAiming();
				}
			};
		}

		private void Update()
		{	
			if(_state == GameState.PLAY)
				_turnSwitcher.UpdateState();
			
			if (Input.GetMouseButtonDown(0) && _turnSwitcher.GetActiveColorSide() == CheckerColor.WHITE)
			{
				_inputHandler.OnDown(Input.mousePosition);
			}

			if (Input.GetMouseButtonUp(0) && _turnSwitcher.GetActiveColorSide() == CheckerColor.WHITE)
			{
				_inputHandler.OnUp(Input.mousePosition);
			}

			if (Input.GetMouseButton(0))
			{
				if (_pushed != null)
					_forceLine.SetEndPoint(Input.mousePosition);
			}
//			ActiveCheckerColor = _turnSwitcher.GetActiveColorSide();
			_playerAI.Aiming();
			//print(_turnSwitcher.GetActiveColorSide());
		}

		private void PushCheckerWithForce(Vector3 distance)
		{
			if (_pushed == null) return;
			if(_turnSwitcher.IsPossibleMakeMove() == false) return;
			if (((MonoBehaviour) _pushed).GetComponent<CheckerBase>().CheckerColor != _turnSwitcher.GetActiveColorSide()) return;

			_pusher.SetForce(_forceCalculator.GetForce(distance));
			_pusher.Push(_pushed);
			_pushed = null;
			_forceLine.Hide();
			_turnSwitcher.Move();
		}

		private void AIClick()
		{
			_inputHandler.OnDown(Camera.main.WorldToScreenPoint(_board.CheckersWhite[0].transform.position));
			_inputHandler.OnUp(Camera.main.WorldToScreenPoint(_board.CheckersBlack[5].transform.position));
		}
	}
}
