using Chessy.Model.Entity;
using Chessy.View.Component;
using Chessy.View.System;

namespace Chessy.View.UI.System
{
    sealed class KingPassiveVS : SystemViewAbstract
    {
        bool _needActive;
        readonly GameObjectVC _kingPassiveGOC;

        internal KingPassiveVS(in GameObjectVC kingPassiveGOC, in EntitiesModel eM) : base(eM)
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