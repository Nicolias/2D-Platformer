using System;
using UnityEngine;

public class Attacker : IUpdateable
{
    private readonly int _damage;
    private readonly float _attackCooldown;

    private readonly UpdateServise _updateServise;

    private Health _damageable;

    private float _nextAttackTime;

    public Attacker(int damage, float attackCooldown, UpdateServise updateServise)
    {
        if (updateServise == null)
            throw new ArgumentNullException();

        if (damage <= 0)
            throw new ArgumentOutOfRangeException();

        if (attackCooldown <= 0)
            throw new ArgumentOutOfRangeException();

        _damage = damage;
        _attackCooldown = attackCooldown;
        _updateServise = updateServise;
    }

    public event Action Attacked;

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

        _damageable.Damage(_damage);
        _nextAttackTime = Time.time + _attackCooldown;

        Attacked?.Invoke();
    }
}
