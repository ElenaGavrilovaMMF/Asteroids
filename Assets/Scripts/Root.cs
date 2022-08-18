using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private Transform _placeForUi;
        private MainController _mainController;

        private void Awake()
        {
            var profilePlayer = new ProfilePlayer();
            profilePlayer.CurrentState.Value = GameState.Start;
            _mainController = new MainController(_placeForUi, profilePlayer);
        }

        private void Update()
        {
            _mainController.Tick();
        }

        private void OnDestroy()
        {
            _mainController?.Dispose();
        }
    }
}
