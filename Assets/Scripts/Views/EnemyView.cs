using UnityEngine;

namespace Asteroids
{
    public class EnemyView : MonoBehaviour
    {
        public float MovementSpeed;
        public float RotationSpeed;
    
        private bool _isBroken = false;
    
        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == Common.TagData.BULLET_TAG || collider.gameObject.tag == Common.TagData.PLAYER_TAG)
            {
                collider.gameObject.SetActive(false);
                _isBroken = true;
            }

            if (collider.gameObject.tag == Common.TagData.BULLET_TAG)
            {
                PointsController.AddPoints(EnemyType.Enemy);
            }
        }
    
        public bool GetStatusBroken()
        {
            return _isBroken;
        }
    }
}
