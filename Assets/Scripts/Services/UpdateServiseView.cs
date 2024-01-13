using UnityEngine;

public class UpdateServiseView : MonoBehaviour
{
    public UpdateServise UpdateServise { get; private set; }

    public void Initialize()
    {
        UpdateServise = new UpdateServise();
    }

    private void Update()
    {
        (UpdateServise as IUpdateable).Update(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        (UpdateServise as IFixedUpdateable).Update(Time.fixedDeltaTime);
    }
}