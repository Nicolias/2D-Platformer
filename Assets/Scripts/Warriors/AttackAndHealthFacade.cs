using System.Collections;
using UnityEngine;

public class AttackAndHealthFacade : MonoBehaviour
{
    [SerializeField] private int _health;

    [SerializeField] private int _damage;
    [SerializeField] private int _attackCoolDown;

    [SerializeField] private float _maxAttackDistanceByXAxis;
    [SerializeField] private float _maxAttackDistanceByYAxis;

    private IDieable _dieable;

    public Attacker Attacker { get; private set; }
    public Health Health { get; private set; }

    public void Initialize(UpdateServise updateServise, IDieable dieable)
    {
        _dieable = dieable;

        Attacker = new Attacker(_damage, _attackCoolDown, dieable.MoveAnimation.Animator, updateServise);
        Health = new Health(_health);
        Health.Dieing += OnDiy;
    }

    private void OnDestroy()
    {
        Health.Dieing -= OnDiy;
    }

    private void OnDiy()
    {
        Attacker.StopAttack();
        Health.Dieing -= OnDiy;
        StartCoroutine(ShowDeadAnimation());
    }

    private IEnumerator ShowDeadAnimation()
    {
        _dieable.MoveAnimation.Animator.SetTrigger(AnimatorData.Params.Diy);
        yield return new WaitForSeconds(1);

        _dieable.MoveAnimation.Dispose();
        _dieable.Die();
    }
}