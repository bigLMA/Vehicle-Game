using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace VehicleGame.Core.UI.FloatingUI
{
    public class FloatingUI : MonoBehaviour
    {
        [SerializeField]
        private float _lifetime = 3f;

        [SerializeField]
        private float floatSpeed = 15f;

        public event Action<FloatingUI> OnTimerOver;

        private float _timer;


        public async void Launch()
        {
            try
            {
                while (_timer > 0f)
                {
                    transform.position += new Vector3(0f, floatSpeed * Time.deltaTime);
                    await UniTask.Yield();
                    _timer -= Time.deltaTime;
                }

                OnTimerOver?.Invoke(this);
            }
            catch(Exception e)
            {
                print(e.Message);
            }
        }

        public void Construct(Vector3 postion)
        {
            _timer = _lifetime;
            transform.position = postion;
            Launch();
        }
    }
}