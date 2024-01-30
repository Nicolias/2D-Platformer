using System;

public abstract class WarriarPresenter : IToggleable
{
    private readonly WarriarModel _model;
    private readonly WarriarView _view;

    protected Attacker Attacker { get; }

    public WarriarPresenter(WarriarView view, WarriarModel model, Attacker attacker)
    {
        if (view == null) throw new ArgumentNullException();
        if (model == null) throw new ArgumentNullException();
        if (attacker == null) throw new ArgumentNullException();

        _view = view;
        _model = model;
        Attacker = attacker;
    }

    public void Enable()
    {
        _model.Enable();

        Attacker.Attacked += OnAttacked;
        _model.Died += OnDied;

        OnEnabled();
    }

    public void Disable()
    {
        _model.Disable();

        Attacker.Attacked -= OnAttacked;
        _model.Died -= OnDied;
        Attacker.StopAttack();

        OnDisabled();
    }

    protected virtual void OnEnabled() { }

    protected virtual void OnDisabled() { }

    public void Detected(IDamagable damagable)
    {
        if (damagable == null) throw new ArgumentNullException();

        OnTargetDetected(damagable);
    }
    
    public void Lost()
    {
        OnTargetLost();
    }

    public void Damage(int value)
    {
        _model.Damage(value);
    }

    protected abstract void OnTargetDetected(IDamagable damagable);

    protected abstract void OnTargetLost();

    private void OnAttacked()
    {
        _view.ShowAttackAnimation();
    }

    private void OnDied()
    {
        _view.Diy();
        Disable();
    }
}
