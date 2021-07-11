internal struct ActivatedComponent
{
    private bool _isActivated;

    internal bool IsActivated => _isActivated;

    internal void StartFill(bool value = default) => _isActivated = value;

    internal void SetActivated(bool value) => _isActivated = value;
    internal void ToggleActivated() => _isActivated = !_isActivated;
}