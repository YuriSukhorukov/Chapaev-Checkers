using UnityEngine;

namespace Assets.Scripts.Chapaev.Core
{
    public class ForceLine
    {
        private readonly LineRenderer _lineRenderer;
        public ForceLine()
        {
            _lineRenderer = GameObject.Find("Force_Line").GetComponent<LineRenderer>();
        }

        public void SetLine(Vector3 begin, Vector3 end)
        {
            _lineRenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(begin));
            _lineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(end));
        }

        public void SetBeginPoint(Vector3 begin)
        {
            _lineRenderer.SetPosition(0, begin);
        }
        public void SetEndPoint(Vector3 end)
        {
            _lineRenderer.SetPosition(1, end);
        }

        public void Hide()
        {
            _lineRenderer.SetPosition(0, Vector3.zero);
            _lineRenderer.SetPosition(1, Vector3.zero);
        }
    }
}