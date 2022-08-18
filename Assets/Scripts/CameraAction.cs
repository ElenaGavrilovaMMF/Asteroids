using UnityEngine;

namespace Asteroids
{
    public class CameraAction
    {
        public static Vector3 PortalMovement(Transform transform)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            float posX = screenPos.x;
            float posY = screenPos.y;
        
            if (posX < Common.CameraData.MinX) posX = Common.CameraData.MaxX;
            if (posX > Common.CameraData.MaxX) posX = Common.CameraData.MinX;
            if (posY < Common.CameraData.MinY) posY = Common.CameraData.MaxY;
            if (posY > Common.CameraData.MaxY) posY = Common.CameraData.MinY;
            
            return Camera.main.ScreenToWorldPoint(new Vector3(posX, posY, screenPos.z));
        }
    }
}

