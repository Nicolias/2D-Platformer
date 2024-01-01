using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace CharacterSystem
{
    [RequireComponent(typeof(Animator))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private JumpMovementFacade _movement;

        [SerializeField] private int _startHealthValue;

        private MoveAnimation _moveAnimation;

        [field: SerializeField] public MoneyCollector MoneyCollector { get; private set; }

        public Health Health { get; private set; }

        public event UnityAction Dead;

        public void Initialize(UpdateServise updateServise)
        {
            new Input(_movement, updateServise);
            _movement.Initialize(updateServise);
            _moveAnimation = new MoveAnimation(_movement, GetComponent<Animator>(), transform, updateServise);

            Health = new Health(_startHealthValue);
            Health.Diying += OnDiy;
        }

        private void OnDiy()
        {
            Health.Diying -= OnDiy;
            StartCoroutine(ShowDeadAnimation());
        }

        private IEnumerator ShowDeadAnimation()
        {
            _moveAnimation.Animator.SetTrigger(AnimatorData.Params.Diy);
            yield return new WaitForSeconds(1);

            _moveAnimation.Dispose();
            Dead?.Invoke();
            Destroy(gameObject);
        }
    }
}