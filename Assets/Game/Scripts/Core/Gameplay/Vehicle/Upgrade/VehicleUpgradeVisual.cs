using System.Linq;
using UnityEngine;
using VehicleGame.Core.Events;
using Zenject;

namespace VehicleGame.Core.Gameplay.Vehicle
{
    public class VehicleUpgradeVisual : MonoBehaviour
    {
        [SerializeField]
        private string _key;

        [SerializeField]
        private Material _selectedMaterial;

        private Material _startMaterial;
        private MeshRenderer _meshRenderer;

        private SignalBus _signalBus;

        [Inject]
        private void Initialize(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _signalBus.Subscribe<DragUpdateStartedSignal>(OnDragStarted);
            _signalBus.Subscribe<DragUpdateEndedSignal>(OnDragEnded);
            //gameObject.SetActive(false);
            enabled = false;

            _meshRenderer = GetComponent<MeshRenderer>();
            _startMaterial = _meshRenderer.material;
        }

        public void OnDragStarted(DragUpdateStartedSignal signal)
        {
            if(_key.Equals(signal.key))
            {
               // gameObject.SetActive(true);
               enabled = true;
                _meshRenderer.material = _selectedMaterial;
            }
        }

        public void OnDragEnded(DragUpdateEndedSignal signal)
        {
            if(gameObject.activeSelf)
            {
                if(Physics.OverlapSphere(transform.position, 2f).Any(x=>x.gameObject.TryGetComponent<VehicleUpgrader>(out var comp)))
                {
                    _meshRenderer.material = _startMaterial;
                    signal.success = true;
                    enabled = false;
                    return;
                }
            }

            signal.success = false;
        }
    }
}

