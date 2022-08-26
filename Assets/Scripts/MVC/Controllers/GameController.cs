using UnityEngine;

namespace Asteroids
{
    public class GameController : BaseController
    {
        private ShipController _shipController;
        private CakeSpawnController _cakeSpawnController;
        private EnemySpawnController _enemySpawnController;
        private SpawnBulletController _spawnBulletController;
        private SpawnLazerController _lazerController;
        private ProfilePlayer _profilePlayer;
        
        public GameController(ProfilePlayer profilePlayer, AsteroidsInputSystem _inputSystem)
        {
            _profilePlayer = profilePlayer;
            
            _shipController = new ShipController(_inputSystem);
            _cakeSpawnController = new CakeSpawnController();
            _enemySpawnController = new EnemySpawnController(_shipController.GetViewObject().transform);
            _spawnBulletController = new SpawnBulletController(_shipController.GetViewObject().GetComponentInChildren<SpawnBulletView>(), _inputSystem);
            _lazerController = new SpawnLazerController(_shipController.GetViewObject().GetComponentInChildren<SpawnLazerView>(), _inputSystem);
            AddController(_shipController);
            AddController(_cakeSpawnController);
            AddController(_enemySpawnController);
            AddController(_spawnBulletController);
            AddController(_lazerController);
        }

        public override void Tick()
        {
            if (_profilePlayer.CurrentState.Value == GameState.Game)
            {
                _shipController.Tick();
                _cakeSpawnController.Tick();
                _enemySpawnController.Tick();
                _spawnBulletController.Tick();
                _lazerController.Tick();
            }
            
            if (!GetShipData().IsActive && _profilePlayer.CurrentState.Value != GameState.End)
            {
                EndGame();
            }
        }
        
        public Ship GetShipData()
        {
            return _shipController.CurrentShip;
        }
    
        public Lazer GetLazerData()
        {
            return _lazerController.CurrentLazer;
        }
        
        private void EndGame()
        {
            _profilePlayer.CurrentState.Value = GameState.End;
        }
    }
}
