using UnityEngine;

namespace CharacterSystem
{
    [RequireComponent(typeof(Animator))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private JumpMovementFacade _movement;

        [SerializeField] private int _health;

        [field: SerializeField] public MoneyCollector MoneyCollector { get; private set; }

        public Health Health { get; private set; }

        public void Initialize(UpdateServise updateServise)
        {
            new Imput(_movement, updateServise);
            new MoveAnimation(_movement, GetComponent<Animator>(), transform, updateServise);

            Health = new Health(_health);
        }
    }
}