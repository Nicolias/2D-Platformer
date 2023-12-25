using System;

public class Wallet
{
    private readonly int _pointPerOneMoney = 1;

    private int _score = 0;

    public int Score => _score;

    public void AddCoin()
    {
        _score += _pointPerOneMoney;
    }
}