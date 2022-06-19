using Photon.Pun;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        public void Click(in ConditionUnitTypes conditionT)
        {
            if (_eMG.CurPlayerITC.Is(_eMG.WhoseMovePlayerTC.PlayerT))
            {
                if (_eMG.UnitConditionTC(_eMG.CellsC.Selected).Is(conditionT))
                {
                    _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.TrySetConditionUnitOnCellM), ConditionUnitTypes.None, _eMG.CellsC.Selected });
                }
                else
                {
                    _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, RpcTarget.MasterClient, new object[] { nameof(_sMG.TrySetConditionUnitOnCellM), conditionT, _eMG.CellsC.Selected });
                }
            }
            else _sMG.Mistake(MistakeTypes.NeedWaitQueue);

            _eMG.NeedUpdateView = true;
        }
    }
}