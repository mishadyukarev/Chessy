using Photon.Pun;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void Click(in ConditionUnitTypes conditionT)
        {
            if (_e.CurPlayerIT.Is(_e.WhoseMovePlayerT))
            {
                if (_e.UnitConditionT(_e.CellsC.Selected).Is(conditionT))
                {
                    _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TrySetConditionUnitOnCellM), ConditionUnitTypes.None, _e.CellsC.Selected });
                }
                else
                {
                    _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TrySetConditionUnitOnCellM), conditionT, _e.CellsC.Selected });
                }
            }
            else _s.Mistake(MistakeTypes.NeedWaitQueue);

            _e.NeedUpdateView = true;
        }
    }
}