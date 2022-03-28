using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class ClickReadyS : SystemModelGameAbs, IClickUI
    {
        public ClickReadyS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Click()
        {
            e.RpcPoolEs.ReadyToMaster();

            e.NeedUpdateView = true;
        }
    }
}