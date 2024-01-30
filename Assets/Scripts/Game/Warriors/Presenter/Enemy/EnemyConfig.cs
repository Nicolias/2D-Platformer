namespace EnemyNamespace
{
    public class EnemyConfig
    {
        public EnemyConfig(float maxAttackDistanceByXAxis, float maxAttackDistanceByYAxis, float maxFollowDistance)
        {
            MaxAttackDistanceByXAxis = maxAttackDistanceByXAxis;
            MaxAttackDistanceByYAxis = maxAttackDistanceByYAxis;
            MaxFollowDistance = maxFollowDistance;
        }

        public float MaxAttackDistanceByXAxis { get; }
        public float MaxAttackDistanceByYAxis { get; }
        public float MaxFollowDistance { get; }
    }
}