using UnityEngine;

namespace Asteroids
{
    public class Ship
    {
        public float MovementSpeed { get; }
        public float RotationSpeed { get; }
        public float MovementSpeedUpMax { get; }
        public float SpeedUpStep { get; }
        public float SpeedDownStep { get; }
        public Transform ShipTransform { get; }

        public float CurrentSpeed { get; set; }
    
        private bool _isActive;
        public bool IsActive
        {
            get
            {
                if (ShipTransform && ShipTransform.gameObject.activeSelf != _isActive) _isActive = ShipTransform.gameObject.activeSelf;
            
                return _isActive;
            }
            set
            {
                _isActive = value;
                if (ShipTransform.gameObject.activeSelf) ShipTransform.gameObject.SetActive(value);
            }
        }

        public Ship(Transform shipTransform, float movementSpeed, float rotationSpeed, float speedUpMax, float speedUpStep, float speedDownStep)
        {
            ShipTransform = shipTransform;
            MovementSpeed = movementSpeed;
            RotationSpeed = rotationSpeed;
            MovementSpeedUpMax = speedUpMax;
            SpeedUpStep = speedUpStep;
            SpeedDownStep = speedDownStep;
        }
    }
}
