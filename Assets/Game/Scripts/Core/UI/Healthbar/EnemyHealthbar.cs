using UnityEngine;
using VehicleGame.Core.UI.Healthbar;

namespace VehicleGame.Core.UI
{
    public class EnemyHealthbar : AnimatedHealthbar
    {
        [SerializeField]
        private GameObject _visual;

        private void OnEnable()
        {
            _healthComponent.OnHealthChange += EnemyDamaged;
            _healthComponent.OnDeath += EnemyDead;
            _visual.SetActive(false);
        }

        private void OnDisable()
        {
            _healthComponent.OnHealthChange -= EnemyDamaged;
            _healthComponent.OnDeath -= EnemyDead;
            _visual.SetActive(false);
        }

        private void EnemyDamaged(int current, int max)
        {
            // Activate on first damage recieved
            if (!_visual.activeSelf)
                if (current < max)
                    _visual.SetActive(true);
        }

        private void EnemyDead()
        {
            _visual.SetActive(false);
        }
    }
}
