using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Asteroids
{
    public class BaseController : IDisposable
    {
        private List<BaseController> _baseControllers = new();
        private List<GameObject> _gameObjects = new();

        private bool _isDisposed;

        public void Dispose()
        {
            if(_isDisposed) return;

            _isDisposed = true;

            foreach (var baseController in _baseControllers)
            {
                baseController?.Dispose();
            }

            _baseControllers.Clear();
        
            foreach (var cashedGameObject in _gameObjects)
            {
                Object.Destroy(cashedGameObject);
            }

            _gameObjects.Clear();

            OnDispose();
        }
    
        protected void AddController(BaseController baseController)
        {
            _baseControllers.Add(baseController);
        }

        protected void AddGameObjects(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        protected List<BaseController> GetControllers()
        {
            return _baseControllers;
        }

        protected virtual void OnDispose()
        {
        
        }
    
        public virtual void Tick()
        {
        
        }

    }
}
