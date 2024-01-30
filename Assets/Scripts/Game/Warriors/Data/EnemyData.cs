using EnemyNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Warriar/Enemy")]
public class EnemyData : WarriarData
{
    [SerializeField] private float _maxAttackDistanceByXAxis;
    [SerializeField] private float _maxAttackDistanceByYAxis;
    [SerializeField] private float _maxFollowDistance;

    public EnemyConfig CreateConfig()
    {
        return new EnemyConfig(_maxAttackDistanceByXAxis, _maxAttackDistanceByYAxis, _maxFollowDistance);
    }
}
