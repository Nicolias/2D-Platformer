using System;

public static class DirectionConst
{
    public static readonly float Left = -1;
    public static readonly float Middle = 0;
    public static readonly float Right = 1;

    public static float GetDistance(float firstPoint, float secondPoint)
    {
        float maxValue = Math.Max(firstPoint, secondPoint);
        float minValue = Math.Min(firstPoint, secondPoint);

        return maxValue - minValue;
    }

    public static float Clamp(float direction)
    {
        if (direction < Middle)
            return Left;
        else if (direction > Middle)
            return Right;
        else 
            return Middle;
    }

    public static DirectionType GetDirectionType(float direction)
    {
        if (direction < Left || direction > Right)
            throw new InvalidOperationException();

        if (direction < Middle)
            return DirectionType.Left;
        if (direction > Middle)
            return DirectionType.Right;
        else
            return DirectionType.Middle;
    }

    public static float GetDirectionValue(DirectionType directionType)
    {
        switch (directionType)
        {
            case DirectionType.Middle:
                return Middle;
            case DirectionType.Left:
                return Left;
            case DirectionType.Right:
                return Right;
        }

        throw new InvalidOperationException();
    }
}