using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private MoveAnimation _moveAnimation;

    [field: SerializeField] public MoneyCollector MoneyCollector { get; private set; }
    [field: SerializeField] public JumpMovementFacade Movement { get; private set; }

    private void Awake()
    {
        _moveAnimation.Initialize(Movement);   
    }
}