using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct SelIdxC : ISelectedIdx
    {
        public bool IsSelCell => SelIdx<IdxC>().Idx != 0;
    }
}