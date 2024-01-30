using System;
using UnityEngine;

public class Attacker : IUpdateable
{
    private readonly int _damage;
    private readonly float _attackCooldown;

    private readonly UpdateServise _updateServise;

    private IDamagable _damageable;

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

    public void StartAttack(IDamagable damageable)
    {
        if (damageable == null) throw new ArgumentNullException();

        _damageable = damageable;
        _damageable.Died += OnTargetDied;

        Attack();
        _updateServise.AddToUpdate(this);
    }

    public void StopAttack()
    {
        if (_damageable != null)
            _damageable.Died -= OnTargetDied;

        _damageable = null;
        _updateServise.RemoveFromUpdate(this);
    }

    private void Attack()
    {
        if (_damageable != null)
        {
            _damageable.Damage(_damage);
            _nextAttackTime = Time.time + _attackCooldown;

            Attacked?.Invoke();
        }
    }

    private void OnTargetDied()
    {
        StopAttack();
    }
}
