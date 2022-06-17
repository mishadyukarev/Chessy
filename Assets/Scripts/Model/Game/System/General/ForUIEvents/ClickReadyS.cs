using Chessy.Common.Interface;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void ClickReady()
        {
            _eMG.RpcPoolEs.ReadyToMaster();

            _eMG.NeedUpdateView = true;
        }
    }
}