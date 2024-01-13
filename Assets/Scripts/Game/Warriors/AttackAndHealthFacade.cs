using UnityEngine;

public class AttackAndHealthFacade
{
    private readonly int _health;

    private readonly int _damage;
    private readonly int _attackCoolDown;

    private readonly IDieable _dieable;

    public AttackAndHealthFacade(UpdateServise updateServise, IDieable dieable, int health, int damage, int attackCoolDown)
    {
        _dieable = dieable;

        Health = new Health(health);
        Attacker = new Attacker(damage, attackCoolDown, updateServise);

        Health.Over += OnHealthOver;
    }

    public Attacker Attacker { get; private set; }
    public Health Health { get; private set; }


    private void Dispose()
    {
        Health.Over -= OnHealthOver;
    }

    private void OnHealthOver()
    {
        Attacker.StopAttack();
        _dieable.Die();
    }
}