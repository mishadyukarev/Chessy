﻿using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct CurIdxC : ICurrectIdx
    {
        public bool IsStartDirectToCell => CurIdx<IdxC>().Idx == default;
    }
}