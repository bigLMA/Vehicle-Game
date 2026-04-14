namespace VehicleGame.Core.Events
{
    public class DragUpdateStartedSignal
    {
        public readonly string key;

        public DragUpdateStartedSignal(string key)
        {
            this.key = key;
        }
    }
}