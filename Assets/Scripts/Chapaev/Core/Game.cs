using System;
using System.Linq;
using Assets.Scripts.Chapaev.Core;
using Assets.Scripts.Chapaev.UI;
using Assets.Scripts.Chapaev.Values;
using Chapaev.Entities;
using Chapaev.Interfaces;
using UnityEngine;

namespace Chapaev.Core
{
	public class Game : MonoBehaviour
	{
		private GameState _state = GameState.BEGIN;
		
		private IInputHandler _inputHandler;
		private IForceCalculator _forceCalculator;
		private IBoardBuilder _boardBuilder;
		private ISelector _selector;
		private IPusher _pusher;
		private IPushed _pushed;
		private Board _board;
		private TurnSwitcher _turnSwitcher;
		private PlayerAI _playerAI;
		private UI _ui;

		private void Start ()
		{
			_ui = new UI();
			_forceCalculator = new ForceCalculator();
			_selector = new Selector3D();
			_pusher = new Pusher();
			_boardBuilder = new BoardBuilder();
			_inputHandler = new MouseInputHandler();

			_board = _boardBuilder.Build();
			_turnSwitcher = new TurnSwitcher(_board);

			_playerAI = new PlayerAI(
				_inputHandler,
				_board
			);

			InitUIHandler();
			InitInputHandler();
			InitCheckersHandler();
			InitTurnSwitcherHandler();
			InitBoardHandler();
		}

		private void Update()
		{	
			if(_state == GameState.PLAY)
				_turnSwitcher.UpdateState();
			
			if (Input.GetMouseButtonDown(0) && _turnSwitcher.GetActiveColorSide() == _turnSwitcher.PlayerCheckerColor)
			{
				_inputHandler.OnDown(Input.mousePosition);
			}

			if (Input.GetMouseButtonUp(0) && _turnSwitcher.GetActiveColorSide() == _turnSwitcher.PlayerCheckerColor)
			{
				_inputHandler.OnUp(Input.mousePosition);
			}

			_playerAI.Aiming();
		}
		
		private void StartGame()
		{
			_state = GameState.PLAY;

			_ui.Manager.txt_checkers_on_board.text = String.Format(
				"White: {0}; Black: {1}",
				_board.GetCheckersWhiteCount().ToString(),
				_board.GetCheckersBlackCount().ToString()
			);
			
			if(_turnSwitcher.PlayerCheckerColor == CheckerColor.BLACK)
				_playerAI.StartAiming();
		}

		private void InitInputHandler()
		{
			_inputHandler.OnDownEvent += (position) =>
			{
				if(_state == GameState.PLAY)
					_selector.SelectFrom(position);
			};

			_inputHandler.OnUpEvent += (distance) =>
			{
				if (_pushed == null) return;
				if(_turnSwitcher.IsPossibleMakeMove() == false) return;
				if (((MonoBehaviour) _pushed).GetComponent<CheckerBase>().CheckerColor != _turnSwitcher.GetActiveColorSide()) return;
				
				_pusher.SetForce(_forceCalculator.GetForce(distance));
				_pusher.Push(_pushed);
				_pushed = null;
				
				_turnSwitcher.Move();
			};
		}

		private void InitCheckersHandler()
		{
			foreach (var checker in _board.CheckersWhite.Concat(_board.CheckersBlack))
			{
				var checker1 = checker;
				
				checker1.SelectEvent += () =>
				{
					if (checker1.CheckerColor == _turnSwitcher.GetActiveColorSide())
						_pushed = checker1.GetComponent<IPushed>();
				};
				
				checker1.BouncingBorderEvent += () =>
				{	
					if(checker1.CheckerColor != _turnSwitcher.GetActiveColorSide())
						_turnSwitcher.RepeatActiveColorSide();
					
					_board.RemoveChecker(checker1);
					_board.CheckEmpty();
					
					_ui.Manager.txt_checkers_on_board.text = String.Format(
						"White: {0}; Black: {1}",
						_board.GetCheckersWhiteCount().ToString(),
						_board.GetCheckersBlackCount().ToString()
					);
				};
			}
		}

		private void InitTurnSwitcherHandler()
		{
			_turnSwitcher.MoveCompleteEvent += () =>
			{
				_turnSwitcher.TurnActiveColorSide();
				if (_turnSwitcher.GetActiveColorSide() == _turnSwitcher.EnemyCheckerColor)
					_playerAI.StartAiming();
			};
		}

		private void InitBoardHandler()
		{
			_board.CheckersIsEmty += (color) =>
			{
				if (color == _turnSwitcher.EnemyCheckerColor)
					print("win");
				else if (color == _turnSwitcher.PlayerCheckerColor)
					print("loose");

				_state = GameState.GAME_OVER;
			};
		}

		private void InitUIHandler()
		{
			_ui.Manager.btn_select_white.onClick.AddListener(() =>
			{
				if (_state == GameState.BEGIN)
				{
					_turnSwitcher.SetPlayerColor(CheckerColor.WHITE);
					_playerAI.SetColor(CheckerColor.BLACK);
					StartGame();
				}
			});
			_ui.Manager.btn_select_black.onClick.AddListener(() =>
			{
				if (_state == GameState.BEGIN)
				{
					_turnSwitcher.SetPlayerColor(CheckerColor.BLACK);
					_playerAI.SetColor(CheckerColor.WHITE);
					StartGame();
				}
			});
		}
	}
}