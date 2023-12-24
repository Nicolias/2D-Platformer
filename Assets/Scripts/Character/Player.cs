using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public MoneyCollector MoneyCollector { get; private set; }
    [field: SerializeField] public CharacterMovement Movement { get; private set; }
}