namespace VehicleGame.Core.Events
{
    public class EnemyKilledSignal 
    {
        public readonly int value;

        public EnemyKilledSignal(int value) 
        {
            this.value = value; 
        }
    }
}


