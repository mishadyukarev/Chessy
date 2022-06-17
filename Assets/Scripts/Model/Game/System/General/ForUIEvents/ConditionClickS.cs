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
                    _eMG.RpcPoolEs.ConditionUnitToMaster(_eMG.CellsC.Selected, ConditionUnitTypes.None);
                }
                else
                {
                    _eMG.RpcPoolEs.ConditionUnitToMaster(_eMG.CellsC.Selected, conditionT);
                }
            }
            else _sMG.Mistake(MistakeTypes.NeedWaitQueue);

            _eMG.NeedUpdateView = true;
        }
    }
}