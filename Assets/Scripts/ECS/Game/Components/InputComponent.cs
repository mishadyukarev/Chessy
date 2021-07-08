
internal struct InputComponent
{
    private bool _isClicked;

    internal bool IsClick => _isClicked;

    internal void StartFill() => _isClicked = default;
    internal void SetIsClicked(bool isClicked) => _isClicked = isClicked;
}
