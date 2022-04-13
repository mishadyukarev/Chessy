using Chessy.Game.Model.Entity;
using Chessy.Game.View.System;

namespace Chessy.Game
{
    internal class SystemViewCellGameAbs : SystemViewGameAbs
    {
        protected readonly byte _currentCell;

        protected SystemViewCellGameAbs(in byte currentCell, in EntitiesModelGame eMG) : base(eMG)
        {
            _currentCell = currentCell;
        }

        internal override void Sync() { }
    }
}