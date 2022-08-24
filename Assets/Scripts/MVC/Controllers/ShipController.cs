using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class ShipController : BaseController
    {
        public Ship CurrentShip { get; }

        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = Common.PathData.SHIP_PATH};
        private readonly ShipView _shipView;
        private float _currentSpeedUp = 0;

        public ShipController()
        {
            _shipView = LoadView();
            CurrentShip = new Ship(_shipView.transform, _shipView.MovementSpeedUpMax, _shipView.RotationSpeed,
                _shipView.MovementSpeedUpMax, _shipView.SpeedUpStep, _shipView.SpeedDownStep);
        }

        public override void Tick()
        {
            Move();
        }

        private void Move()
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            SetCurrentSpeedUp(vertical);
            if (CurrentShip.ShipTransform)
            {
                CurrentShip.ShipTransform.position += CurrentShip.ShipTransform.up * Time.deltaTime *
                                                      (vertical * CurrentShip.MovementSpeed + _currentSpeedUp);
                CurrentShip.ShipTransform.Rotate(-Vector3.forward,
                    horizontal * CurrentShip.RotationSpeed * Time.deltaTime);
                CurrentShip.ShipTransform.position = CameraAction.PortalMovement(CurrentShip.ShipTransform);
            }
        }

        private void SetCurrentSpeedUp(float vertical)
        {
            if (vertical != 0f)
            {
                if (vertical > 0 && _currentSpeedUp < CurrentShip.MovementSpeedUpMax)
                    _currentSpeedUp += CurrentShip.SpeedUpStep;

                if (vertical < 0 && _currentSpeedUp > -CurrentShip.MovementSpeedUpMax)
                    _currentSpeedUp -= CurrentShip.SpeedUpStep;
            }
            else
            {
                if (_currentSpeedUp > 0f)
                    _currentSpeedUp -= CurrentShip.SpeedDownStep;

                if (_currentSpeedUp < 0f)
                    _currentSpeedUp += CurrentShip.SpeedDownStep;
            }

            CurrentShip.CurrentSpeed = _currentSpeedUp;
        }

        private ShipView LoadView()
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            AddGameObjects(objView);

            return objView.GetComponent<ShipView>();
        }

        public GameObject GetViewObject()
        {
            return _shipView.gameObject;
        }
    }
}
