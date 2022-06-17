namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void DoneReadyClick()
        {
            _eMG.RpcPoolEs.DoneToMaster();
        }
    }
}