using UnityEngine;

namespace Asteroids
{
    public class CakeController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = Common.PathData.CAKE_PATH};
        private readonly CakeView _cakeView;

        private Cake _currentCake;

        private Vector3 _forward;
        private bool _isCreatePieces = false;
        private Transform _spawnPoint;

        public CakeController(Transform spawnPoint)
        {
            _cakeView = LoadView();
            _currentCake = new Cake(_cakeView.Speed, _cakeView.RotateAngle, _cakeView.Pieces, _cakeView.transform,
                true);
            _spawnPoint = spawnPoint;
            _currentCake.CakeTransform.SetParent(_spawnPoint);
            _currentCake.CakeTransform.localPosition = Vector3.zero;

            _forward = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        }

        public CakeController(bool isPieces, CakeView view)
        {
            _cakeView = view;
            _currentCake = new Cake(_cakeView.Speed, _cakeView.RotateAngle, _cakeView.Pieces, _cakeView.transform,
                true);
            _forward = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        }

        public override void Tick()
        {
            if (!_cakeView.GetStatusBroken())
                Move();
            if (_cakeView.GetStatusBroken() && _currentCake.Pieces)
                CreatePieces();
            if (_cakeView.GetStatusBroken() && !_currentCake.Pieces)
                _currentCake.IsActive = false;

            foreach (BaseController baseController in GetControllers())
            {
                baseController.Tick();
            }
        }

        private CakeView LoadView()
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            AddGameObjects(objView);

            return objView.GetComponent<CakeView>();
        }

        private Transform LoadView(GameObject obj, Vector3 position)
        {
            var objView = Object.Instantiate(obj, position, Quaternion.identity);
            AddGameObjects(objView);

            return objView.transform;
        }

        private void Move()
        {
            if (_currentCake.CakeTransform)
            {
                _currentCake.CakeTransform.position += _forward * _currentCake.Speed * Time.deltaTime;
                _currentCake.CakeTransform.position = CameraAction.PortalMovement(_currentCake.CakeTransform);
                _currentCake.CakeTransform.Rotate(Vector3.forward, _currentCake.RotateAngle);
            }
        }

        private void CreatePieces()
        {
            if (!_isCreatePieces && _currentCake.Pieces)
            {
                Transform piecesTransform =
                    LoadView(_currentCake.Pieces.gameObject, _currentCake.CakeTransform.position);
                piecesTransform.SetParent(_currentCake.CakeTransform.parent);

                foreach (CakeView view in piecesTransform.GetComponentsInChildren<CakeView>())
                {
                    CakeController cakeController = new CakeController(true, view);
                    AddController(cakeController);
                }

                _currentCake.IsActive = false;
                _isCreatePieces = true;
            }
        }
    }
}
