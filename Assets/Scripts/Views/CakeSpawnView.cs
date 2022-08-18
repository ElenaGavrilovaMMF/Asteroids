using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class CakeSpawnView : MonoBehaviour
    {
        public CakeView CakeView;
        public float SpawnTime;
        public int SpawnCount;
        public int MaxSpawnCount;
        public List<Transform> PointsSpawn;
    }
}
