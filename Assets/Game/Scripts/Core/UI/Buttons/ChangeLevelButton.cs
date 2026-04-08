using UnityEngine;
using VehicleGame.Core.Events;
using Zenject;

namespace VehicleGame.Core.UI.Buttons
{
    public class ChangeLevelButton : ButtonBase
    {
        [SerializeField]
        private string _levelName;

        private SignalBus _signalBus;

        [Inject]
        private void Initialize(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void ButtonClickListener()
        {
            _signalBus.Fire(new ChangeLevelSignal(_levelName));
        }
    }
}

