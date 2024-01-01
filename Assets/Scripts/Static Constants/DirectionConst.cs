using System;

public static class DirectionConst
{
    public static readonly float Left = -1;
    public static readonly float Right = 1;

    public static float GetDirectionNormolized(float direction)
    {
        if (direction < 0)
            return Left;
        else if (direction > 0)
            return Right;
        else 
            return 0;
    }

    public static float GetDistance(float firstPoint, float secondPoint)
    {
        float maxValue = Math.Max(firstPoint, secondPoint);
        float minValue = Math.Min(firstPoint, secondPoint);

        return maxValue - minValue;
    }
}