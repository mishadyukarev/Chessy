internal struct IsActivatedComponent
{
    private bool _isActivated;

    internal bool IsActivated
    {
        get => _isActivated;
        set => _isActivated = value;
    }

    internal void StartFill() => _isActivated = false;
}