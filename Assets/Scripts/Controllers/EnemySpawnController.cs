using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class EnemySpawnController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = Common.PathData.ENEMY_SPAWN_PATH};
        private readonly EnemySpawnView _enemySpawnView;
        
        private Transform _shipTransform;
        
        private int _maxEnemyCount;
        private float _spawnTime;
    
        private float _tempTime = 0f;
        private int _currentEnemyCount = 0;
    
        private List<Transform> _pointSpawn;
        
        public EnemySpawnController(Transform shipTransform)
        {
            _enemySpawnView = LoadView();
            
            _maxEnemyCount = _enemySpawnView.MaxEnemyCount;
            _spawnTime = _enemySpawnView.SpawnTime;
            _pointSpawn = _enemySpawnView.PointsSpawn;
            
            _shipTransform = shipTransform;
        }
        
        public override void Tick()
        {
            SpawnEnemy();
            
            foreach (BaseController controller in GetControllers())
            {
                controller.Tick();
            }
        }
        
        private EnemySpawnView LoadView()
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            AddGameObjects(objView);
    
            return objView.GetComponent<EnemySpawnView>();
        }
    
        private void SpawnEnemy()
        {
            _tempTime += 0.01f;
            if (_tempTime > _spawnTime && _currentEnemyCount != _maxEnemyCount)
            {
                int tempSpawnIndex = Random.Range(0, _pointSpawn.Count);
                EnemyController enemyController = new EnemyController(_shipTransform, _pointSpawn[tempSpawnIndex]);
                AddController(enemyController);
                
                _tempTime = 0f;
                _currentEnemyCount++;
            }
        }
    }
}
