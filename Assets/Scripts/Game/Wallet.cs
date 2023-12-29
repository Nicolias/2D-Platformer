using System;

public class Wallet
{
    private readonly int _pointPerOneCoin = 1;

    public int Score { get; private set; }

    public void AddCoin()
    {
        Score += _pointPerOneCoin;
    }
}