using Chessy.Game.Entity.Model;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game.Model.System
{
    public sealed class DoneClickS : SystemModelGameAbs
    {
        internal DoneClickS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        public void Click()
        {
            eMG.RpcPoolEs.DoneToMaster();
        }
    }
}