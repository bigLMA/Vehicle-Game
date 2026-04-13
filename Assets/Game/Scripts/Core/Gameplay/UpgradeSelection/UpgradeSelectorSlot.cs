using System;
using UnityEngine;

public class UpgradeSelectorSlot : MonoBehaviour
{
    [SerializeField]
    private int _slotIndex;

    public event Action<int> OnClick;

    private void OnMouseDown()
    {
        OnClick?.Invoke(_slotIndex);
    }
}
