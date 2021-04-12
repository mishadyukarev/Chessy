
public struct InputComponent
{
    private bool _isClick;
    private bool _isClickBackMove;

    public bool IsClick => _isClick;
    public bool IsClickBackMove => _isClickBackMove;


    public void SetClick(bool isClick) => _isClick = isClick;
    public void SetClickBackMove(bool isClick) => _isClickBackMove = isClick;
}
