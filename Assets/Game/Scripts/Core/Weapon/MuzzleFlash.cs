using UnityEngine;

namespace VehicleGame.Core.Weapon
{
    public class MuzzleFlash : MonoBehaviour
    {
        private ParticleSystem _particle;

        private void Awake()
        {
            _particle = GetComponent<ParticleSystem>();
        }

        public void Play()
        {
            _particle.Play();
        }
    }
}