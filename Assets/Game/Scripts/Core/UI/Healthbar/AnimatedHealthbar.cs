using UnityEngine;
using UnityEngine.UI;
using VehicleGame.Core.Interfaces;

namespace VehicleGame.Core.UI.Healthbar
{
    public class AnimatedHealthbar : MonoBehaviour
    {
        [Header("Images")]
        [SerializeField]
        private Image _primaryImage;
        [SerializeField]
        private Image _secondaryImage;

        protected IHealth _healthComponent;
        [SerializeField]
        private GameObject _target;

        [SerializeField]
        private float _animationSpeed = 3.4f;

        protected virtual void Awake()
        {
            _healthComponent = _target.GetComponent<IHealth>();

            if (_healthComponent == null)
            {
                Debug.LogError("There is no health component on target!");
            }

            _healthComponent.OnHealthChange += HealthComponent_OnHealthChange;
            _healthComponent.OnDeath += ResetHealthBar;
        }

        protected virtual void OnDestroy()
        {
            _healthComponent.OnHealthChange -= HealthComponent_OnHealthChange;
            _healthComponent.OnDeath -= ResetHealthBar;
        }

        private void Update()
        {
            if (!Mathf.Approximately(_secondaryImage.transform.localScale.x, _primaryImage.transform.localScale.x))
            {
                var x = Mathf.Lerp(_secondaryImage.transform.localScale.x,
                    _primaryImage.transform.localScale.x,
                    _animationSpeed * Time.deltaTime);

                _secondaryImage.transform.localScale = new Vector3(x, 1f, 1f);
            }
        }
        public void ResetHealthBar()
        {
            _primaryImage.transform.localScale = Vector3.one;
            _secondaryImage.transform.localScale = Vector3.one;
        }

        private void HealthComponent_OnHealthChange(int current, int max)
        {
            _primaryImage.transform.localScale = new Vector3((float)current / (float)max, 1f, 1f);
        }
    }
}