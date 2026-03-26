using UnityEngine;

namespace VehicleGame.Core.Systems.Camera
{
    public class FaceCamera : MonoBehaviour
    {
        private void LateUpdate()
        {
            if(!gameObject.activeSelf) return;

            var camera = UnityEngine.Camera.main.transform;
            transform.LookAt(transform.position + camera.rotation * Vector3.forward, camera.rotation * Vector3.up);
        }
    }
}