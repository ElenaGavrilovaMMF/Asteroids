using UnityEngine;

namespace Asteroids
{
    public class Bullet
    {
        public float Speed { get; }
        public float LifeTime { get; set; }
        public Transform TransformBullet { get; set; }
        private bool _isActive;
        private GameObject _bulletObject;

        public GameObject BulletObject
        {
            get
            {
                return _bulletObject;
            }
            set
            {
                _bulletObject = value;
                TransformBullet = value.transform;
            }
        }

   
        public bool IsActive
        {
            get
            {
                if (BulletObject && BulletObject.activeSelf != _isActive) _isActive = BulletObject.activeSelf;
            
                return _isActive;
            }
            set
            {
                _isActive = value;
                if (BulletObject) BulletObject.SetActive(value);
            }
        }

        public Bullet(float speed, GameObject bulletObject, bool isActive, float lifeTime)
        {
            Speed = speed;
            IsActive = isActive;
            BulletObject = bulletObject;
            LifeTime = lifeTime;
        }
    }
}
