using UnityEngine;
namespace ChainOfResponsibility
{
    public abstract class GameHandler : MonoBehaviour, IGameHandler
    {
        private IGameHandler _nextHandler;
        public IGameHandler SetNext(IGameHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual IGameHandler Handle()
        {
            if (_nextHandler != null)
            {
                _nextHandler.Handle();
            }
            return _nextHandler;
        }
    }
}
