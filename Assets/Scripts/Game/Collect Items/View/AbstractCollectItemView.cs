using Character;
using System;
using UnityEngine;

public abstract class AbstractCollectItemView : MonoBehaviour
{
    protected abstract ICollectableItem CollectableItem { get; }

    public event Action PickedUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Collector characterCollector))
            PickUp();
    }

    private void PickUp()
    {
        CollectableItem.PickUp();
        gameObject.SetActive(false);
        PickedUp?.Invoke();
    }
}
