public abstract class AbstractCollectItemView : MonoBehaviour
{
    protected abstract ICollectableItem CollectableItem { get; }

    public event Action PickedUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Collector characterCollector))
            PickUp();
    }

    private void PickUp()
    {
        CollectableItem.PickUp();
        gameObject.SetActive(false);
        PickedUp?.Invoke();
    }
}

public class CoinView : AbstractCollectItemView
{
    private CoinModel _coin;

    protected override ICollectableItem CollectableItem => _coin;

    public void Initialize(Wallet wallet)
    {
        _coin = new CoinModel(wallet);
    }
}

public interface ICollectableItem
{
    public void PickUp();
}

public class CoinModel : ICollectableItem
{
    private Wallet _wallet;

    public CoinModel(Wallet wallet)
    {
        _wallet = wallet;
    }

    public void PickUp()
    {
        _wallet.AddCoin();
    }
}