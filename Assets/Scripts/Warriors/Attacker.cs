using System;
using UnityEngine;

public class Attacker : IUpdateable
{
    private readonly int _damage;
    private readonly float _attackCooldown;
    private readonly UpdateServise _updateServise;
    private readonly Animator _selfAnimator;

    private float _nextAttackTime;
    private Health _damageable;

    public Attacker(int damage, float attackCooldown, Animator selfAnimator, UpdateServise updateServise)
    {
        _damage = damage;
        _attackCooldown = attackCooldown;
        _updateServise = updateServise;

        _selfAnimator = selfAnimator;
    }

    void IUpdateable.Update(float timeBetweenFrame)
    {
        if (_nextAttackTime <= Time.time)
            Attack();
    }

    public void StartAttack(Health health)
    {
        _damageable = health;

        Attack();
        _updateServise.AddToUpdate(this);
    }

    public void StopAttack()
    {
        _damageable = null;
        _updateServise.RemoveFromUpdate(this);
    }

    private void Attack()
    {
        if (_damageable.Value == 0)
        {
            StopAttack();
            return;
        }

        _damageable.TakeDamage(_damage);
        _nextAttackTime = Time.time + _attackCooldown;

        _selfAnimator.SetTrigger(AnimatorData.Params.Attack);
    }
}
