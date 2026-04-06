namespace VehicleGame.Core.Events
{
    public class ChangeLevelSignal 
    {
        public readonly string _levelName;

        public ChangeLevelSignal(string levelName)
        {
            _levelName = levelName;
        }
    }
}