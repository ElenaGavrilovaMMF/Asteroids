using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class SpawnBulletController : BaseController
    {
        private readonly ResourcePath _viewPath = new() {PathResource = Common.PathData.BULLET_SPAWN_PATH};
        private SpawnBulletView _view;
        private Transform _transformParent;
        private List<BulletController> _bulletsList = new();
        private List<GameObject> _spawnPoints = new();
        private int _bulletsCount;
        private int _currentCountIndex;

        private AsteroidsInputSystem _inputSystem;
        private object _isShoot;
        public SpawnBulletController(SpawnBulletView view, AsteroidsInputSystem inputSystem)
        {
            _transformParent = Load();
            _view = view;
            _spawnPoints = _view.SpawnPoints;
            _bulletsCount = _view.BulletsCount;
            _currentCountIndex = _spawnPoints.Count;
            _inputSystem = inputSystem;
            _inputSystem.Player.Fire.performed += ctx => _isShoot = ctx.ReadValueAsObject();


            Spawn();
        }

        public override void Tick()
        {
            Move();

            foreach (BaseController baseController in GetControllers())
            {
                baseController.Tick();
            }
        }

        private Transform Load()
        {
            var obj = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            AddGameObjects(obj);
            obj.transform.position = Vector3.zero;

            return obj.transform;
        }

        private void Spawn()
        {
            for (int i = 0; i < _bulletsCount; i++)
            {
                BulletController bulletController = new BulletController(_transformParent);
                _bulletsList.Add(bulletController);
                AddController(bulletController);
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void Move()
        {

            if (_isShoot != null)
            {
                int listBulletLength = 0;
                foreach (BulletController bulletController in _bulletsList)
                {
                    if (!bulletController.GetBulletStatus())
                    {
                        if (_currentCountIndex > 0)
                        {
                            bulletController.SetActiveStatus(true);
                            _currentCountIndex--;
                            bulletController.SetCurrentPos(_spawnPoints[_currentCountIndex].transform.position);
                            bulletController.SetCurrentRot(_view.transform.parent.rotation);
                        }
                        else
                        {
                            _currentCountIndex = _spawnPoints.Count;
                            break;
                        }
                    }

                    listBulletLength++;
                }

                if (_currentCountIndex != 0 && listBulletLength == _bulletsList.Count)
                {
                    AddNewBulletToPool();
                }
                
                _isShoot = null;
            }
        }

        private void AddNewBulletToPool()
        {
            for (int i = 0; i < _currentCountIndex; i++)
            {
                BulletController bulletController = new BulletController(_transformParent);
                _bulletsList.Add(bulletController);
                bulletController.SetActiveStatus(true);
                bulletController.SetCurrentPos(_spawnPoints[i].transform.position);
                bulletController.SetCurrentRot(_view.transform.parent.rotation);
                AddController(bulletController);
            }

            _currentCountIndex = _spawnPoints.Count;
        }
    }
}
