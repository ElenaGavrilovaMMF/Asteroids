using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class CakeSpawnController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = Common.PathData.CAKE_SPAWN_PATH};
        private readonly CakeSpawnView _cakeSpawnView;
        
        private float _spawnTime;
        private int _spawnCount;
        private int _spawnCountMax;
        private List<Transform> _pointSpawn;
        
        private float _tempTime;
        private float _tempCount;
        
        public CakeSpawnController()
        {
            _cakeSpawnView = LoadView();
    
            _spawnTime = _cakeSpawnView.SpawnTime;
            _spawnCount = _cakeSpawnView.SpawnCount;
            _pointSpawn = _cakeSpawnView.PointsSpawn;
            _spawnCountMax = _cakeSpawnView.MaxSpawnCount;
            _tempTime = _spawnTime;
            _tempCount = _spawnCountMax - 1;
    
            SpawnCake();
        }
        
        public override void Tick()
        {
            if (_tempTime < 0 && _tempCount > 0)
            {
                SpawnCake();
                _tempCount--;
                _tempTime = _spawnTime;
            }
    
            if (_tempTime >= 0)
            {
                _tempTime -= 0.01f;
            }
            
            foreach (BaseController cakeController in GetControllers())
            {
                cakeController.Tick();
            }
        }
        
        private CakeSpawnView LoadView()
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            AddGameObjects(objView);
    
            return objView.GetComponent<CakeSpawnView>();
        }
    
        private void SpawnCake()
        {
            
            for (int i = 0; i < _spawnCount; i++)
            {
                int tempSpawnIndex = Random.Range(0, _pointSpawn.Count);
                CakeController cakeController = new CakeController(_pointSpawn[tempSpawnIndex]);
                AddController(cakeController);
            }
        }
    }
}
