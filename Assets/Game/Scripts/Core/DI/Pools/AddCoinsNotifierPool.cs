using UnityEngine;
using VehicleGame.Core.UI.FloatingUI;
using Zenject;

namespace VehicleGame.Core.DI
{
    public class AddCoinsNotifierPool : MonoMemoryPool<Vector3, FloatingUI>
    {
        protected override void Reinitialize(Vector3 position, FloatingUI item)
        {
            base.Reinitialize(position, item);
            item.Construct(position);
        }
    }
}

