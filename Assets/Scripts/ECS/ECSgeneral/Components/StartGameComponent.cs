internal struct StartGameComponent
{
    private bool _isStartedGame;

    internal bool IsStartedGame
    {
        get { return _isStartedGame; }
        set { _isStartedGame = value; }
    }
}
