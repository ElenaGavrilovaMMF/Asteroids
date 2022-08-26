using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;              
using UnityEngine.InputSystem.Controls;

namespace Asteroids
{
    public class ShipView : MonoBehaviour
    {
        public float MovementSpeed;
        public float RotationSpeed;

        public float MovementSpeedUpMax;
        public float SpeedUpStep;
        public float SpeedDownStep;
    }
}

