public class CoinView : AbstractCollectItemView
{
    private CoinModel _coin;

    protected override ICollectableItem CollectableItem => _coin;

    public void Initialize(Wallet wallet)
    {
        _coin = new CoinModel(wallet);
    }
}
