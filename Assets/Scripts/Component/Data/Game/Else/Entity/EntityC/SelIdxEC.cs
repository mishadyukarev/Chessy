using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct SelIdxEC : ISelectedIdx
    {
        public bool IsSelCell => SelIdx<IdxC>().Idx != 0;
    }
}