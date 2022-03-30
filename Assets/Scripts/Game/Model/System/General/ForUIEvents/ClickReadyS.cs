using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class ClickReadyS : SystemModelGameAbs, IClickUI
    {
        internal ClickReadyS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        public void Click()
        {
            e.RpcPoolEs.ReadyToMaster();

            e.NeedUpdateView = true;
        }
    }
}