using UnityEngine;

namespace Asteroids
{
    public class Enemy
    {
        public float MovementSpeed { get; }
        public float RotationSpeed { get; }
        public Transform EnemyTransform { get; }
    
        private bool _isActive;
        public bool IsActive
        {
            get
            {
                if (EnemyTransform.gameObject.activeSelf != _isActive) _isActive = EnemyTransform.gameObject.activeSelf;
            
                return _isActive;
            }
            set
            {
                _isActive = value;
                EnemyTransform.gameObject.SetActive(value);
            }
        }
    
    
        public Enemy(float movementSpeed, float rotationSpeed, Transform enemyTransform, bool isActive)
        {
            MovementSpeed = movementSpeed;
            RotationSpeed = rotationSpeed;
            EnemyTransform = enemyTransform;
            IsActive = isActive;
        }
    }
}
