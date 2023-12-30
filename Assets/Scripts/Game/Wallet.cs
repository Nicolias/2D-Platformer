using System;

public class Wallet
{
    private readonly int _pointPerOneCoin = 1;

    private int _score = 0;

    public int Score => _score;

    public void AddCoin()
    {
        _score += _pointPerOneCoin;
    }
}