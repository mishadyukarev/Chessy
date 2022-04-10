using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed class DoneClickS : SystemModel
    {
        internal DoneClickS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        public void Click()
        {
            eMG.RpcPoolEs.DoneToMaster();
        }
    }
}