using UnityEngine;
using TMPro;

namespace Asteroids
{
    public class UIController : BaseController
    {
        private PointsController _pointsController;

        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = Common.PathData.UI_PATH};
        private readonly UIView _view;

        private TMP_Text _position;
        private TMP_Text _angle;
        private TMP_Text _speed;
        private TMP_Text _lazerCount;
        private TMP_Text _lazerTime;

        private Ship _shipData;
        private Lazer _lazerData;

        public UIController(Transform placeForUi, Ship shipData, Lazer lazerData)
        {
            _pointsController = new PointsController(placeForUi);

            _view = LoadView(placeForUi);

            _position = _view.Position;
            _angle = _view.Angle;
            _speed = _view.Speed;
            _lazerCount = _view.LazerCount;
            _lazerTime = _view.LazerTime;

            _shipData = shipData;
            _lazerData = lazerData;
        }

        public override void Tick()
        {
            _pointsController.Tick();

            if (_shipData.ShipTransform)
            {
                _position.text = new Vector2(RoundToFloat(_shipData.ShipTransform.position.x, 2),
                    RoundToFloat(_shipData.ShipTransform.position.y, 2)).ToString();
                _angle.text = Mathf.Round(_shipData.ShipTransform.eulerAngles.z).ToString();
                _speed.text = RoundToFloat(_shipData.CurrentSpeed, 2).ToString();
            }

            _lazerCount.text = _lazerData.CurrentCountBattery.ToString();
            _lazerTime.text = RoundToFloat(_lazerData.CurrentBatteryReloadTime, 2).ToString();
        }

        protected override void OnDispose()
        {
            _pointsController.Dispose();
        }

        private UIView LoadView(Transform placeForUi)
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
            AddGameObjects(objView);

            return objView.GetComponent<UIView>();
        }

        private float RoundToFloat(float number, int countTail)
        {
            return Mathf.Round(number * Mathf.Pow(10, countTail)) * Mathf.Pow(10, -countTail);
        }
    }
}
