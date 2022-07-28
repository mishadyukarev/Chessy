using Photon.Pun;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void Click(in ConditionUnitTypes conditionT)
        {
            if (_unitCs[IndexesCellsC.Selected].ConditionT == conditionT)
            {
                _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TrySetConditionUnitOnCellM), ConditionUnitTypes.None, IndexesCellsC.Selected });
            }
            else
            {
                _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TrySetConditionUnitOnCellM), conditionT, IndexesCellsC.Selected });
            }

            _updateAllViewC.NeedUpdateView = true;
        }
    }
}