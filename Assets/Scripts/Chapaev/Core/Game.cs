using Chapaev.Behaviours;
using Chapaev.Entities;
using Chapaev.Interfaces;
using UnityEngine;

namespace Chapaev.Core
{
	public class Game : MonoBehaviour
	{
		private ISelector _selector;
		private IPusher _pusher;
		private IPushed _pushed;
		private IForceCalculator _forceCalculator;

		private void Start () {
			_forceCalculator = new ForceCalculator();
			_selector = new Selector3D();
			_pusher = new Pusher();

			foreach (var checker in FindObjectsOfType<CheckerBase>())
			{
				var checker1 = checker;
				checker1.SelectEvent += () =>
				{
					_pushed = checker1.GetComponent<IPushed>(); 
				};
			}
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				_forceCalculator.SetFirstPoint(Input.mousePosition);	
				_selector.SelectFrom(Input.mousePosition);
			}

			if (Input.GetMouseButtonUp(0))
			{
				if(_pushed == null) return;
				
				_forceCalculator.SetLastPoint(Input.mousePosition);
				
				_pusher.SetForce(_forceCalculator.GetForce());
				_pusher.Push(_pushed);

				_pushed = null;
			}
		}
	}
}
