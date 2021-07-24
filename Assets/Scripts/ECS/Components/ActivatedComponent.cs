internal struct ActivatedComponent
{
    internal bool IsActivated { get; set; }

    internal void StartFill(bool value = default) => IsActivated = value;
    internal void ToggleActivated() => IsActivated = !IsActivated;
}