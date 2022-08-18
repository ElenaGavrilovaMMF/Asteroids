using UnityEngine;

namespace Asteroids
{
	public class CakeView : MonoBehaviour
	{
		public float Speed;
		public float RotateAngle;
		public GameObject Pieces;
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
				if(gameObject.tag == Common.TagData.CAKE_TAG)
					PointsController.AddPoints(EnemyType.Cake);
				if(gameObject.tag == Common.TagData.PIECE_TAG)
					PointsController.AddPoints(EnemyType.Piece);
			}
		}

		public bool GetStatusBroken()
		{
			return _isBroken;
		}
	}

}
