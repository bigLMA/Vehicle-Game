namespace VehicleGame.Core.Gameplay.Enemy
{
    public abstract class EnemyState
    {
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Tick(float delTime);
    }
}

