namespace ChainOfResponsibility
{
    public interface IGameHandler
    {
        IGameHandler Handle();
        IGameHandler SetNext(IGameHandler nextHandler);
    }
}
