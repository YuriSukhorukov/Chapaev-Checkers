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
		private PlayerAI _playerAI;

		private GameState _state = GameState.BEGIN;

		public CheckerColor PlayerCheckerColor;
		public CheckerColor EnemyCheckerColor;

		private void Start ()
		{
			EnemyCheckerColor = PlayerCheckerColor == CheckerColor.WHITE ? CheckerColor.BLACK : CheckerColor.WHITE;
			
			_forceCalculator = new ForceCalculator();
			_selector = new Selector3D();
			_pusher = new Pusher();
			_boardBuilder = new BoardBuilder();
			_inputHandler = new MouseInputHandler();

			_board = _boardBuilder.Build();
			_turnSwitcher = new TurnSwitcher(_board);

			_playerAI = new PlayerAI(
				_inputHandler,
				_board,
				PlayerCheckerColor == CheckerColor.WHITE ? CheckerColor.BLACK : CheckerColor.WHITE
			);

			_board.CheckersIsEmty += (color) =>
			{
				if (color == PlayerCheckerColor)
					print("win");
				else if (color == EnemyCheckerColor)
					print("loose");

				_state = GameState.GAME_OVER;
			};

			_inputHandler.OnDownEvent += (position) =>
			{
				if (_state == GameState.BEGIN)
					_state = GameState.PLAY;
				if(_state == GameState.PLAY)
					_selector.SelectFrom(position);
			};
			
			_inputHandler.OnUpEvent += PushCheckerWithForce;
			
			foreach (var checker in _board.CheckersWhite.Concat(_board.CheckersBlack))
			{
				var checker1 = checker;
				checker1.SelectEvent += () =>
				{
					if (checker1.CheckerColor == _turnSwitcher.GetActiveColorSide())
					{
						_pushed = checker1.GetComponent<IPushed>();
						print(checker1.gameObject.transform.position);
					}
				};
				checker1.BouncingBorderEvent += () =>
				{
					_board.RemoveChecker(checker1);
					
					if(checker1.CheckerColor != _turnSwitcher.GetActiveColorSide())
						_turnSwitcher.RepeatActiveColorSide();
				};
			}

			_turnSwitcher.MoveCompleteEvent += () =>
			{
				_turnSwitcher.TurnActiveColorSide();
				if (_turnSwitcher.GetActiveColorSide() == EnemyCheckerColor)
				{
					_playerAI.StartAiming();
				}
			};
			
			if(PlayerCheckerColor == CheckerColor.BLACK)
				_playerAI.StartAiming();
		}

		private void Update()
		{	
			if(_state == GameState.PLAY)
				_turnSwitcher.UpdateState();
			
			if (Input.GetMouseButtonDown(0) && _turnSwitcher.GetActiveColorSide() == PlayerCheckerColor)
			{
				_inputHandler.OnDown(Input.mousePosition);
			}

			if (Input.GetMouseButtonUp(0) && _turnSwitcher.GetActiveColorSide() == PlayerCheckerColor)
			{
				_inputHandler.OnUp(Input.mousePosition);
			}

			_playerAI.Aiming();
		}

		private void PushCheckerWithForce(Vector3 distance)
		{
			if (_pushed == null) return;
			if(_turnSwitcher.IsPossibleMakeMove() == false) return;
			if (((MonoBehaviour) _pushed).GetComponent<CheckerBase>().CheckerColor != _turnSwitcher.GetActiveColorSide()) return;

			_pusher.SetForce(_forceCalculator.GetForce(distance));
			_pusher.Push(_pushed);
			_pushed = null;
			_turnSwitcher.Move();
		}
	}
}
