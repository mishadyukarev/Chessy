
namespace Game.Game
{
    public struct ActionC
    {
        public readonly System.Action Action;

        public ActionC(in System.Action action)
        {
            Action = action;
        }

        public void Invoke() => Action.Invoke();
    }
}