using UnityEngine;

namespace Asteroids
{
    public class Cake
    {
        public float Speed { get; }
        public float RotateAngle { get; }
        public GameObject Pieces { get; }
        public Transform CakeTransform { get; }
        private bool _isActive;
        public bool IsActive
        {
            get
            {
                if (CakeTransform.gameObject.activeSelf != _isActive) _isActive = CakeTransform.gameObject.activeSelf;
            
                return _isActive;
            }
            set
            {
                _isActive = value;
                CakeTransform.gameObject.SetActive(value);
            }
        }
    
        public Cake(float speed, float rotateAngle, GameObject pieces, Transform cakeTransform, bool isActive)
        {
            Speed = speed;
            RotateAngle = rotateAngle;
            Pieces = pieces;
            CakeTransform = cakeTransform;
            IsActive = isActive;
        }
    }
}
