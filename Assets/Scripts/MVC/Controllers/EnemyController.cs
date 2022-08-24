using UnityEngine;

namespace Asteroids
{
    public class EnemyController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = Common.PathData.ENEMY_PATH};
        private readonly EnemyView _enemyView;
        private Enemy _currentEnemy;
    
        private Transform _shipTransform;
        private Transform _spawnPoint;
    
        public EnemyController(Transform shipTransform, Transform spawnPoint)
        {
            _enemyView = LoadView();
            _currentEnemy = new Enemy(_enemyView.MovementSpeed, _enemyView.RotationSpeed, _enemyView.transform, true);
            _shipTransform = shipTransform;
            _spawnPoint = spawnPoint;
            
            _currentEnemy.EnemyTransform.SetParent(_spawnPoint);
            _currentEnemy.EnemyTransform.localPosition = Vector3.zero;
        }
        
        public override void Tick()
        {
            Move();
            Die();
        }
        
        private EnemyView LoadView()
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            AddGameObjects(objView);
    
            return objView.GetComponent<EnemyView>();
        }
    
        private void Move()
        {
            if (_currentEnemy.EnemyTransform && _shipTransform && !_enemyView.GetStatusBroken())
            {
                _currentEnemy.EnemyTransform.position +=
                    (_shipTransform.position - _currentEnemy.EnemyTransform.position).normalized * _currentEnemy.MovementSpeed * Time.deltaTime;
                
                _currentEnemy.EnemyTransform.Rotate(Vector3.forward, _currentEnemy.RotationSpeed);
            }
        }
    
        private void Die()
        {
            if (_enemyView.GetStatusBroken())
            {
                _currentEnemy.IsActive = false;
            }
        }
    }
}
