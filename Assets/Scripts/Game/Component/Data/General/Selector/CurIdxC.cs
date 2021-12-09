using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct CurIdxC
    {
        public static bool IsStartDirectToCell => CurIdx<IdxC>().Idx == default;
    }
}