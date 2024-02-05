using System;

public class DetecterHandler
{
    private WarriarPresenter _presenter;
    private AbstractDetector _detector;

    public DetecterHandler(WarriarPresenter presenter, AbstractDetector detector)
    {
        if (presenter == null) throw new ArgumentNullException();
        if (detector == null) throw new ArgumentNullException();

        _presenter = presenter;
        _detector = detector;
    }

    public void Enable()
    {
        _detector.Detected += OnDetected;
        _detector.Lost += OnLost;
    }

    public void Disable()
    {
        _detector.Detected -= OnDetected;
        _detector.Lost -= OnLost;
    }

    private void OnDetected(IDamagable damagable)
    {
        _presenter.Detected(damagable);
    }

    private void OnLost(IDamagable damagable)
    {
        _presenter.Lost();
    }
}