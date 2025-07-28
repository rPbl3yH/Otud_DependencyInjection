using Game.Meta;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InventoryIsEmptyEvents : MonoBehaviour
{
    [SerializeField] private SceneInventory _sceneInventory;
    [Space]
    [SerializeField] private UnityEvent _isEmpty_Event;
    [SerializeField] private UnityEvent _isNotEmpty_Event;

    private Inventory _inventory;


    private IEnumerator Start()
    {
        _inventory = _sceneInventory.Inventory;
        _inventory.OnCountChanged += OnItemCountChanged;

        yield return null;
        yield return null;

        OnItemCountChanged(_inventory.Count);
    }

    private void OnItemCountChanged(int newCount)
    {
        SetIsEmpty(_inventory.Count == 0);
    }


    private void SetIsEmpty(bool isEmpty)
    {
        if (isEmpty == true)
        {
            _isEmpty_Event?.Invoke();
            return;
        }

        _isNotEmpty_Event?.Invoke();
    }
}
