namespace VehicleGame.Core.Events
{
    public class CoinsChangedSignal
    {
        public readonly int current;

        public CoinsChangedSignal(int current)
        {
            this.current = current;
        }
    }
}