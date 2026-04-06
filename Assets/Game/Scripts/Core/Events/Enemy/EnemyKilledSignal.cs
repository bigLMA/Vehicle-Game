using UnityEngine;

namespace VehicleGame.Core.Events
{
    public class EnemyKilledSignal 
    {
        public readonly int value;
        public readonly Vector3 deathCoordinate;

        public EnemyKilledSignal(int value, Vector3 deathCoordinate) 
        {
            this.deathCoordinate = deathCoordinate;
            this.value = value; 
        }
    }
}


