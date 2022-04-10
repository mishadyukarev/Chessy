using Chessy.Common.Entity;
using Chessy.Common.Interface;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed class ClickReadyS : SystemModel, IClickUI
    {
        internal ClickReadyS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        public void Click()
        {
            eMG.RpcPoolEs.ReadyToMaster();

            eMG.NeedUpdateView = true;
        }
    }
}