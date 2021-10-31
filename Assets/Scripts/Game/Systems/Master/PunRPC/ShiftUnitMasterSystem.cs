using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class ShiftUnitMasterSystem : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC, ThirstyUnitC> _cellUnitOthFilt = default;
        private EcsFilter<CellEnvDataC> _cellEnvrDataFilter = default;
        private EcsFilter<CellRiverDataC> _cellRiverFilt = default;


        public void Run()
        {
            var idx_from = ForShiftMasCom.IdxFrom;
            var idx_to = ForShiftMasCom.IdxTo;

            var playerType = WhoseMoveC.WhoseMove;


            if (CellsForShiftCom.HaveIdxCell(playerType, idx_from, idx_to))
            {
                ref var unitC_from = ref _cellUnitFilter.Get1(idx_from);
                ref var levUnitC_from = ref _cellUnitMainFilt.Get2(idx_from);
                ref var ownUnitC_from = ref _cellUnitMainFilt.Get3(idx_from);
                ref var hpUnitC_from = ref _cellUnitFilter.Get2(idx_from);
                ref var stepUnitC_from = ref _cellUnitFilter.Get3(idx_from);
                ref var twUnitC_from = ref _cellUnitOthFilt.Get3(idx_from);
                ref var effUnitC_from = ref _cellUnitOthFilt.Get4(idx_from);
                ref var thirUnitC_from = ref _cellUnitOthFilt.Get5(idx_from);

                ref var unitC_to = ref _cellUnitFilter.Get1(idx_to);
                ref var levUnitC_to = ref _cellUnitMainFilt.Get2(idx_to);
                ref var ownUnitC_to = ref _cellUnitMainFilt.Get3(idx_to);
                ref var hpUnitC_to = ref _cellUnitFilter.Get2(idx_to);
                ref var stepUnitC_to = ref _cellUnitFilter.Get3(idx_to);
                ref var condUnitC_to  = ref _cellUnitOthFilt.Get2(idx_to);
                ref var twUnitC_to = ref _cellUnitOthFilt.Get3(idx_to);
                ref var effUnitC_to = ref _cellUnitOthFilt.Get4(idx_to);
                ref var thirUnitC_to = ref _cellUnitOthFilt.Get5(idx_to);

                ref var envDatC_To = ref _cellEnvrDataFilter.Get1(idx_to);

                ref var riverC_to = ref _cellRiverFilt.Get1(idx_to);


                stepUnitC_from.TakeStepsForDoing(envDatC_To);
                //effUnitC_from.Def(StatTypes.Steps);


                unitC_to.SetUnit(unitC_from.Unit);
                levUnitC_to.SetLevel(levUnitC_from.Level);
                ownUnitC_to.SetOwner(playerType);
                hpUnitC_to.AmountHp = hpUnitC_from.AmountHp;
                stepUnitC_to.StepsAmount = stepUnitC_from.StepsAmount;
                condUnitC_to.DefCondition();
                twUnitC_to.Set(twUnitC_from);
                effUnitC_to.Set(effUnitC_from);
                thirUnitC_to.Set(thirUnitC_from);
                if (riverC_to.HaveNearRiver) thirUnitC_to.SetMaxWater(unitC_to.Unit);
                WhereUnitsC.Add(ownUnitC_to.Owner, unitC_to.Unit, levUnitC_to.Level, idx_to);

                WhereUnitsC.Remove(ownUnitC_from.Owner, unitC_from.Unit, levUnitC_from.Level, idx_from);
                unitC_from.NoneUnit();

                RpcSys.SoundToGeneral(InfoC.Sender(MasGenOthTypes.Master), SoundEffectTypes.ClickToTable);
            }
        }
    }
}