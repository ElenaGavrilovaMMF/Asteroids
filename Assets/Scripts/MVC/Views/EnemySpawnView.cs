using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class EnemySpawnView : MonoBehaviour
    {
        public int MaxEnemyCount;
        public float SpawnTime;
    
        public List<Transform> PointsSpawn;
    }
}
