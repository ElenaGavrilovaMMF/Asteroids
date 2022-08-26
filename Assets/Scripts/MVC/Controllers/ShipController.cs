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

        private float _vertical = 0f;
        private float _horizontal = 0f;

        private AsteroidsInputSystem _inputSystem;
        private Vector2 _movementAxis;
        
        public ShipController(AsteroidsInputSystem inputSystem)
        {
            _shipView = LoadView();
            
            _inputSystem = inputSystem;
            _inputSystem.Player.Move.performed += ctx => _movementAxis = ctx.ReadValue<Vector2>();
            
            CurrentShip = new Ship(_shipView.transform, _shipView.MovementSpeed, _shipView.RotationSpeed,
                _shipView.MovementSpeedUpMax, _shipView.SpeedUpStep, _shipView.SpeedDownStep);
        }
        
        public override void Tick()
        {
           Move();
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private void Move()
        {
            SetAxis();
            SetCurrentSpeedUp(_vertical);
            
            if (CurrentShip.ShipTransform)
            {
                CurrentShip.ShipTransform.position += CurrentShip.ShipTransform.up *
                                                          (Time.deltaTime * (_vertical * CurrentShip.MovementSpeed +
                                                                             _currentSpeedUp));
                CurrentShip.ShipTransform.Rotate(-Vector3.forward,
                        _horizontal * CurrentShip.RotationSpeed * Time.deltaTime);
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

        private void SetAxis()
        {
            if (_inputSystem.Player.Move.IsPressed())
            {
                _vertical = _movementAxis.y != 0f
                    ? _movementAxis.y / Mathf.Abs(_movementAxis.y)
                    : 0f;
                _horizontal = _movementAxis.x != 0f
                    ? _movementAxis.x / Mathf.Abs(_movementAxis.x)
                    : 0f;
            }
            else
            {
                CheckAxis(ref _vertical);
                CheckAxis(ref _horizontal);
            }
        }

        private void CheckAxis(ref float axis)
        {
            if (axis > 0f)
            {
                axis -= 0.01f;
            }

            if (axis < 0f)
            {
                axis += 0.01f;
            }

            if (axis < 0.1f && axis > -0.1f)
            {
                axis = 0f;
            }
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
