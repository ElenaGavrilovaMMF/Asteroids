using UnityEngine;

namespace Asteroids
{
    public class MainController : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
    
        private MainMenuController _mainMenuController;
        private GameController _gameController;
        private UIController _uiController;
    
        public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
    
            OnChangeGameState(profilePlayer.CurrentState.Value);
            profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        }
    
        public override void Tick()
        {
            _gameController?.Tick();
            _uiController?.Tick();
        }
        
        protected override void OnDispose()
        {
            AllDispose();
            _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
        }
        
        private void OnChangeGameState(GameState state)
        {
            switch (state)
            {
                case GameState.Start:
                    _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                    _gameController?.Dispose();
                    _uiController?.Dispose();
                    break;
                
                case GameState.Game:
                    _gameController = new GameController(_profilePlayer);
                    _uiController = new UIController(_placeForUi, _gameController.GetShipData(), _gameController.GetLazerData());
                    _mainMenuController?.Dispose();
                    break;
                case GameState.End:
                    _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                    _gameController?.Dispose();
                    _uiController?.Dispose();
                    break;
                default:
                    AllDispose();
                    break;
            }
        }
    
        private void AllDispose()
        {
            _gameController?.Dispose();
            _mainMenuController?.Dispose();
            _uiController?.Dispose();
        }
    }
}
