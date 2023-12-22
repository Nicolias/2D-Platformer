using System;

public class Wallet
{
    private int _score = 0;

    public int Score => _score;

    public void AddCoin()
    {
        _score += 1;
    }
}