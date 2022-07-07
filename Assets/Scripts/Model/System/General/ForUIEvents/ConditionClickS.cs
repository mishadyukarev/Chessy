using Photon.Pun;
namespace Chessy.Model.System
{
    public sealed partial class ForButtonsSystemsModel
    {
        public void Click(in ConditionUnitTypes conditionT)
        {
            //if (_e.CurrentPlayerIT.Is(_e.WhoseMovePlayerT))
            //{
                if (_e.UnitConditionT(_e.SelectedCellIdx).Is(conditionT))
                {
                    _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TrySetConditionUnitOnCellM), ConditionUnitTypes.None, _e.SelectedCellIdx });
                }
                else
                {
                    _e.RpcC.Action0(_e.RpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TrySetConditionUnitOnCellM), conditionT, _e.SelectedCellIdx });
                }
            //}
            //else _s.Mistake(MistakeTypes.NeedWaitQueue);

            _e.NeedUpdateView = true;
        }
    }
}