using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private Transform _placeForUi;
        private MainController _mainController;
        private AsteroidsInputSystem _inputSystem;
        
        private void Awake()
        {
            var profilePlayer = new ProfilePlayer();
            _inputSystem = new AsteroidsInputSystem();

            profilePlayer.CurrentState.Value = GameState.Start;
            _mainController = new MainController(_placeForUi, profilePlayer, _inputSystem);
        }

        private void Update()
        {
            _mainController.Tick();
        }

        private void OnDestroy()
        {
            _mainController?.Dispose();
        }
        
        private void OnEnable()
        {
            if (_inputSystem != null)
            {
                _inputSystem.Player.Enable();
            }
        }
        
        private void OnDisable()
        {
            if (_inputSystem != null)
            {
                _inputSystem.Player.Disable();
            }
        }
    }
}
