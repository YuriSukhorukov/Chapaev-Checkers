using System.Linq;
using Assets.Scripts.Chapaev.Behaviours;
using Assets.Scripts.Chapaev.Entities;
using Assets.Scripts.Chapaev.Interfaces;
using Assets.Scripts.Chapaev.Values;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Chapaev.Core
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
		private UI.Core.UI _ui;

		private void Start ()
		{
			_ui = new UI.Core.UI();
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
			_inputHandler.OnDownEvent += OnDownEventHandler;
			_inputHandler.OnUpEvent += OnUpEventHandler;
			_turnSwitcher.MoveCompleteEvent += MoveCompleteEventHandler;
			_board.CheckersIsEmty += CheckersIsEmtyEventHandler;
			
			foreach (var checker in _board.CheckersWhite.Concat(_board.CheckersBlack))
			{
				var checker1 = checker;
				checker1.SelectEvent += () => SelectEventHandler(checker1);
				checker1.BouncingBorderEvent += () => BouncingBorderEventHandler(checker1);
			}
			
			_ui.SelectYourColourText();
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

			if (_state == GameState.PLAY)
				_playerAI.Aiming();
		}
		
		private void StartGame()
		{
			_state = GameState.PLAY;

			if(_turnSwitcher.PlayerCheckerColor == CheckerColor.BLACK)
				_playerAI.StartAiming();
			
			_ui.SetCheckersCountInText(_board.GetCheckersWhiteCount(), _board.GetCheckersBlackCount());
		}

		void OnDownEventHandler(Vector3 position)
		{
			if(_state == GameState.PLAY)
				_selector.SelectFrom(position);
		}

		void OnUpEventHandler(Vector3 distance)
		{
			if (_pushed == null) return;
			if(_turnSwitcher.IsPossibleMakeMove() == false) return;
			if (((MonoBehaviour) _pushed).GetComponent<CheckerBase>().CheckerColor != _turnSwitcher.GetActiveColorSide()) return;
				
			_pusher.SetForce(_forceCalculator.GetForce(distance));
			_pusher.Push(_pushed);
			_pushed = null;
				
			_turnSwitcher.Move();
		}

		void SelectEventHandler(CheckerBase checker)
		{
			if (checker.CheckerColor == _turnSwitcher.GetActiveColorSide())
				_pushed = checker.GetComponent<IPushed>();
		}

		void BouncingBorderEventHandler(CheckerBase checker)
		{
			if(checker.CheckerColor != _turnSwitcher.GetActiveColorSide())
				_turnSwitcher.RepeatActiveColorSide();
					
			_board.RemoveChecker(checker);
			_board.CheckEmpty();

			_ui.SetCheckersCountInText(_board.GetCheckersWhiteCount(), _board.GetCheckersBlackCount());
		}

		void MoveCompleteEventHandler()
		{
			_turnSwitcher.TurnActiveColorSide();
			if (_turnSwitcher.GetActiveColorSide() == _turnSwitcher.EnemyCheckerColor)
				_playerAI.StartAiming();
				
			if(_turnSwitcher.GetActiveColorSide() == _turnSwitcher.PlayerCheckerColor)
				_ui.CurrentLivePlayer();
			else
				_ui.CurrentAIPlayer();
		}

		void CheckersIsEmtyEventHandler(CheckerColor color)
		{
			if(_state != GameState.PLAY) return;
				
			if (color == _turnSwitcher.EnemyCheckerColor)
				_ui.WinText();
			else if (color == _turnSwitcher.PlayerCheckerColor)
				_ui.LooseText();

			_state = GameState.GAME_OVER;
			_ui.ShowRestartButton();
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
			
			_ui.Manager.btn_restart_scene.onClick.AddListener(() =>
			{
				SceneManager.LoadScene("Game");
			});
		}
	}
}