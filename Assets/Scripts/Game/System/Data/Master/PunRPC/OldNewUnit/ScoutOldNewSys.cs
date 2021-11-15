using Leopotam.Ecs;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class ScoutOldNewSys : IEcsRunSystem
    {
        private EcsFilter<ForOldNewUnitCom> _forOldNewUnitCom = default;

        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, StepC> _statUnitF = default;
        private EcsFilter<ConditionUnitC, UnitEffectsC> _effUnitF = default;
        private EcsFilter<ToolWeaponC> _twUnitF = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idx_0 = _forOldNewUnitCom.Get1(0).IdxCell;

            ref var unit_0 = ref _unitF.Get1(idx_0);
            ref var levUnitC_0 = ref _unitF.Get2(idx_0);
            ref var ownUnit_0 = ref _unitF.Get3(idx_0);

            ref var hpUnit_0 = ref _statUnitF.Get1(idx_0);
            ref var stepUnit_0 = ref _statUnitF.Get2(idx_0);

            ref var condUnit_0 = ref _effUnitF.Get1(idx_0);
            ref var effUnit_0 = ref _effUnitF.Get2(idx_0);

            ref var twUnitC_0 = ref _twUnitF.Get1(idx_0);


            if (hpUnit_0.HaveMaxHp)
            {
                if (stepUnit_0.HaveMaxSteps(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Steps), UnitStepUpgC.UpgSteps(ownUnit_0.Owner, unit_0.Unit)))
                {
                    InvUnitsC.TakeUnit(ownUnit_0.Owner, UnitTypes.Scout, LevelUnitTypes.First);
                    WhereUnitsC.Remove(ownUnit_0.Owner, unit_0.Unit, levUnitC_0.Level, idx_0);
                    unit_0.Reset();

                    if (twUnitC_0.HaveToolWeap)
                    {
                        InvToolWeapC.AddAmountTools(ownUnit_0.Owner, twUnitC_0.ToolWeapType, twUnitC_0.LevelTWType);
                        twUnitC_0.ToolWeapType = default;
                    }

                    unit_0.Set(_forOldNewUnitCom.Get1(0).UnitType);
                    levUnitC_0.SetLevel(LevelUnitTypes.First);

                    hpUnit_0.SetMaxHp();
                    stepUnit_0.SetMaxSteps(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Steps), UnitStepUpgC.UpgSteps(ownUnit_0.Owner, unit_0.Unit));
                    if (condUnit_0.HaveCondition) condUnit_0.Reset();
                    WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnitC_0.Level, idx_0);

                    RpcSys.SoundToGeneral(sender, ClipTypes.ClickToTable);
                }

                else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
        }
    }
}