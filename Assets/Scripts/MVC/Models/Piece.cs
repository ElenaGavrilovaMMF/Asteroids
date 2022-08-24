using UnityEngine;

namespace Asteroids
{
    public class Piece 
    {
        public float Speed { get; }
        public float RotateAngle { get; }
        public Transform PieceTransform { get; }
        public Vector3 Forward { get; }
        private bool _isActive;
        public bool IsActive
        {
            get
            {
                if (PieceTransform.gameObject.activeSelf != _isActive) _isActive = PieceTransform.gameObject.activeSelf;
            
                return _isActive;
            }
            set
            {
                _isActive = value;
                PieceTransform.gameObject.SetActive(value);
            }
        }

        public Piece(float speed, float rotateAngle, Transform pieceTransform)
        {
            Speed = speed;
            RotateAngle = rotateAngle;
            PieceTransform = pieceTransform;
            Forward = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        }
    }
}
