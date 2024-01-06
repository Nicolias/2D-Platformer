public class Medkit
{
    private readonly Health _health;
    private readonly int _healValue;

    public Medkit(Health health, int healValue)
    {
        _health = health;
        _healValue = healValue;
    }

    public void Heal()
    {
        _health.Heal(_healValue);
    }
}