using Chessy.Common.Component;
using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class KingPassiveVS : SystemViewCellGameAbs
    {
        bool _needActive;
        readonly GameObjectVC _kingPassiveGOC;

        internal KingPassiveVS(in GameObjectVC kingPassiveGOC, in byte currentCell, in EntitiesModelGame eMG) : base(currentCell, eMG)
        {
            _kingPassiveGOC = kingPassiveGOC;
        }

        internal override void Sync()
        {
            //_needActive = false;

            //if (_e.HaveKingEffect(_currentCell))
            //{
            //    _needActive = true;
            //}

            //_kingPassiveGOC.SetActive(_needActive);
        }
    }
}