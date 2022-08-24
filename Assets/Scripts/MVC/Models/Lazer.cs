using UnityEngine;

namespace Asteroids
{
    public class Lazer
    {
        public float LifeTime { get; }
        public Transform LazerTransform { get; }
        public int CountBattery { get; }
        public int CurrentCountBattery { get; set; }
        public float BatteryLifeTime { get; }
        public float BatteryReloadTime { get; }
        public float CurrentBatteryReloadTime { get; set; }


        public Lazer( float timeLife,  Transform transform, int countBattery, float batteryLifeTime, float batteryReloadTime)
        {
            LifeTime = timeLife;
            LazerTransform = transform;
            CountBattery = countBattery;
            BatteryLifeTime = batteryLifeTime;
            BatteryReloadTime = batteryReloadTime;
        }
    }
}
