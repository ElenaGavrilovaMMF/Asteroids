using UnityEngine;

namespace Asteroids
{
    public static class Common
    {
        public static class CameraData
        {
            public static float MinX => 0f;
            public static float MaxX => Camera.main.pixelWidth; 
            public static float MinY => 0f;
            public static float MaxY => Camera.main.pixelHeight;
        }

        public static class PathData
        {
            public static readonly string MAIN_MENU_PATH = "Prefabs/MainMenu";
            public static readonly string SHIP_PATH = "Prefabs/Ship";
            public static readonly string CAKE_SPAWN_PATH = "Prefabs/CakeSpawn";
            public static readonly string CAKE_PATH = "Prefabs/Cake/Cake";
            public static readonly string ENEMY_SPAWN_PATH = "Prefabs/EnemySpawn";
            public static readonly string ENEMY_PATH = "Prefabs/Enemy";
            public static readonly string BULLET_PATH = "Prefabs/Bullet";
            public static readonly string BULLET_SPAWN_PATH = "Prefabs/BulletSpawn";
            public static readonly string UI_PATH = "Prefabs/UI";
            public static readonly string POINTS_UI_PATH = "Prefabs/PointsUI";
        }

        public static class TagData
        {
            public static readonly string BULLET_TAG = "Bullet";
            public static readonly string ENEMY_TAG = "Enemy";
            public static readonly string CAKE_TAG = "Cake";
            public static readonly string PIECE_TAG = "Piece";
            public static readonly string PLAYER_TAG = "Player";
        }

        public static class ButtonData
        {
            public static readonly string START_BUTTON_NAME = "Start";
            public static readonly string RESTART_BUTTON_NAME = "Restart";
        }
    }

}
