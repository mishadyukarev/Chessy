using Chessy.Common.Component;
using Chessy.Model;
using Chessy.Model.View.System;

namespace Chessy.Model
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