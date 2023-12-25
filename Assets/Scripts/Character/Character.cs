using UnityEngine;

[RequireComponent(typeof(MoveAnimation))]
public class Character : MonoBehaviour
{
    [field: SerializeField] public MoneyCollector MoneyCollector { get; private set; }
    [field: SerializeField] public AbstractPhisicsMovement Movement { get; private set; }

    private MoveAnimation _moveAnimation;

    private void Awake()
    {
        _moveAnimation = GetComponent<MoveAnimation>();

        _moveAnimation.Initialize(Movement);   
    }
}