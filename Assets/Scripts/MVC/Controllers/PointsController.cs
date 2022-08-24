using TMPro;
using UnityEngine;

namespace Asteroids
{
    public class PointsController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = Common.PathData.POINTS_UI_PATH};
        private readonly PointsView _view;
    
        private static int _pointsCount;
        private TMP_Text _points;
        private static int _enemyPoint;
        private static int _cakePoint;
        private static int _piecePoint;
    
        public PointsController(Transform placeForUi)
        {
            _view = LoadView(placeForUi);
            
            _points = _view.Points;
            _enemyPoint = _view.EnemyPoint;
            _cakePoint = _view.CakePoint;
            _piecePoint = _view.PieceePoint;
        }
        
        public override void Tick()
        {
            _points.text = _pointsCount.ToString();
        }
    
        protected override void OnDispose()
        {
            _pointsCount = 0;
        }
    
        private PointsView LoadView(Transform placeForUi)
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
            AddGameObjects(objView);
    
            return objView.GetComponent<PointsView>();
        }
    
        public static void AddPoints(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.Cake:
                    _pointsCount += _cakePoint;
                    break;
                
                case EnemyType.Piece:
                    _pointsCount += _piecePoint;
                    break;
                
                case EnemyType.Enemy:
                    _pointsCount += _enemyPoint;
                    break;
                default:
                    break;
            }
        }
    
        public static int GetPoints()
        {
            return _pointsCount;
        }
    
    }
}
