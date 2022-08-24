using UnityEngine;

namespace Asteroids
{
    public class FinalUIController : BaseController
    {
        private readonly MainMenuView _view;
        private readonly FinalUIView _finalUIView;

        public FinalUIController(FinalUIView finalUIView)
        {
            _finalUIView = finalUIView;
            _finalUIView.FinalPoints.text = PointsController.GetPoints().ToString();
        }
    } 
}
