using Photon.Pun;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void Click(in ConditionUnitTypes conditionT)
        {
            if (unitCs[indexesCellsC.Selected].ConditionT == conditionT)
            {
                rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.TrySetConditionUnitOnCellM), ConditionUnitTypes.None, indexesCellsC.Selected });
            }
            else
            {
                rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.TrySetConditionUnitOnCellM), conditionT, indexesCellsC.Selected });
            }

            updateAllViewC.NeedUpdateView = true;
        }
    }
}