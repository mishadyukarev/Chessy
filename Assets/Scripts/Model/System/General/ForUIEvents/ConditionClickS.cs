using Photon.Pun;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void Click(in ConditionUnitTypes conditionT)
        {
            if (_unitCs[_cellsC.Selected].ConditionT == conditionT)
            {
                _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TrySetConditionUnitOnCellM), ConditionUnitTypes.None, _cellsC.Selected });
            }
            else
            {
                _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TrySetConditionUnitOnCellM), conditionT, _cellsC.Selected });
            }

            _updateAllViewC.NeedUpdateView = true;
        }
    }
}