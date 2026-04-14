using UnityEngine;

namespace VehicleGame.Core.Events
{
    public class DragUpdateEndedSignal
    {
        public readonly Vector3 position;

        public bool success;

        public DragUpdateEndedSignal(Vector3 position)
        {
            this.position = position;
        }
    }
}