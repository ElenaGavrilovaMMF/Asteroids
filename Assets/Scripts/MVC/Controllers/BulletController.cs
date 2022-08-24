using UnityEngine;

namespace Asteroids
{
    public class BulletController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = Common.PathData.BULLET_PATH};
        private readonly BulletView _bulletView;
        private Bullet _currentBullet;
        private float _currentLifeTime;
    
        public BulletController(Transform parent)
        {
            _bulletView = LoadView();
            _currentBullet = new Bullet(_bulletView.Speed, _bulletView.gameObject, _bulletView.gameObject.activeSelf, _bulletView.LifeTime);
            _currentBullet.TransformBullet.SetParent(parent);
            _currentLifeTime = _currentBullet.LifeTime;
        }
        
        public override void Tick()
        {
            Move();
        }
    
        private void Move()
        {
            if (_currentBullet.IsActive)
            {
                if (_currentLifeTime > 0)
                {
                    _currentBullet.TransformBullet.position +=
                        _currentBullet.TransformBullet.up * _currentBullet.Speed * Time.deltaTime;
                    _currentBullet.TransformBullet.position = CameraAction.PortalMovement(_currentBullet.TransformBullet);
                    _currentLifeTime -= 0.01f;
                }
                else
                {
                    _currentBullet.IsActive = false;
                    _currentLifeTime = _currentBullet.LifeTime;
                }
            }
        }
        
        private BulletView LoadView()
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            objView.SetActive(false);
            AddGameObjects(objView);
    
            return objView.GetComponent<BulletView>();
        }
    
        public void SetActiveStatus(bool status)
        {
            _currentBullet.IsActive = status;
        }
    
        public bool GetBulletStatus()
        {
            return _currentBullet.IsActive;
        }
    
        public void SetCurrentPos(Vector3 position)
        {
            _currentBullet.TransformBullet.position = position;
        }
    
        public void SetCurrentRot(Quaternion quaternion)
        {
            _currentBullet.TransformBullet.rotation = quaternion;
        }
    }
}
