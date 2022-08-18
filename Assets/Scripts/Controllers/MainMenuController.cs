using UnityEngine;

namespace Asteroids
{
    public class MainMenuController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = Common.PathData.MAIN_MENU_PATH};
        private readonly MainMenuView _view;
        private readonly ProfilePlayer _profilePlayer;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);

            if (profilePlayer.CurrentState.Value == GameState.Start)
            {
                _view.ActiveGameOverInf(false);
                _view.Init(StartGame, Common.ButtonData.START_BUTTON_NAME);
            }

            if (profilePlayer.CurrentState.Value == GameState.End)
            {
                _view.ActiveGameOverInf(true);
                _view.Init(StartGame, Common.ButtonData.RESTART_BUTTON_NAME);
                var finalUiController = new FinalUIController(_view.GetFinalView());
                AddController(finalUiController);
            }
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
            AddGameObjects(objView);

            return objView.GetComponent<MainMenuView>();
        }

        private void StartGame()
        {
            _profilePlayer.CurrentState.Value = GameState.Game;
        }
    }
}