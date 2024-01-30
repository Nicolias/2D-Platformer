using UnityEngine;

public class MedkitView : AbstractCollectItemView
{
    [SerializeField] private int _healValue;

    private Medkit _model;

    protected override ICollectableItem CollectableItem => _model;

    public void Initialize(IHealabel health)
    {
        _model = new Medkit(health, _healValue);
    }
}
