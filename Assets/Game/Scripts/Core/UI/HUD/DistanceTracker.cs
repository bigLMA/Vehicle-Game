using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VehicleGame.Core.Data.Configs;
using VehicleGame.Core.Interfaces;
using Zenject;

namespace VehicleGame.Core.UI.HUD
{
    public class DistanceTracker : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private TextMeshProUGUI _distanceTextField;

        private IVehicle _vehicle;
        private LevelConfig _levelConfig;

        [Inject]
        private void Initialize(IVehicle vehicleBehaviour, LevelConfig levelConfig)
        {
            _vehicle = vehicleBehaviour;
            _levelConfig = levelConfig;
        }

        void Update()
        {
            var vehicleTransform = _vehicle.GetTransform();
            _distanceTextField.text = Mathf.Round(vehicleTransform.position.z).ToString();
            _slider.value = vehicleTransform.position.z / _levelConfig.endPoint.z;
        }
    }
}
