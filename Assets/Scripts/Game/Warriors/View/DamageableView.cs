using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class DamagableView : MonoBehaviour
{
    public Health Health { get; private set; }

    public void Initialize(Health health)
    {
        Health = health;
    }
}
