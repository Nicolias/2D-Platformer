public class Medkit : ICollectableItem
{
    private readonly IHealabel _health;
    private readonly int _healValue;

    public Medkit(IHealabel health, int healValue)
    {
        _health = health;
        _healValue = healValue;
    }

    public void PickUp()
    {
        _health.Heal(_healValue);
    }
}