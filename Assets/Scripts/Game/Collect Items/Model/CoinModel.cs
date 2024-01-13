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
