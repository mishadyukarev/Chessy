using Leopotam.Ecs;
using Game.Common;

namespace Game.Game
{
    public sealed class ScoutOldNewSys : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, StepC> _statUnitF = default;
        private EcsFilter<ConditionUnitC, UnitEffectsC> _effUnitF = default;
        private EcsFilter<ToolWeaponC> _twUnitF = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            IdxDoingMC.Get(out var idx_0);
            UnitDoingMC.Get(out var unit);


            ref var unit_0 = ref _unitF.Get1(idx_0);
            ref var levUnit_0 = ref _unitF.Get2(idx_0);
            ref var ownUnit_0 = ref _unitF.Get3(idx_0);

            ref var hpUnit_0 = ref _statUnitF.Get1(idx_0);
            ref var stepUnit_0 = ref _statUnitF.Get2(idx_0);

            ref var condUnit_0 = ref _effUnitF.Get1(idx_0);
            ref var effUnit_0 = ref _effUnitF.Get2(idx_0);

            ref var twUnitC_0 = ref _twUnitF.Get1(idx_0);


            if (hpUnit_0.HaveMaxHp)
            {
                if (stepUnit_0.HaveMaxSteps(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Steps), UnitUpgC.Steps(unit_0.Unit, levUnit_0.Level, ownUnit_0.Owner)))
                {
                    InvUnitsC.Take(ownUnit_0.Owner, UnitTypes.Scout, LevelTypes.First);
                    unit_0.Clean(levUnit_0.Level, ownUnit_0.Owner);

                    if (twUnitC_0.HaveTW)
                    {
                        InvTWC.Add(twUnitC_0.TW, twUnitC_0.Level, ownUnit_0.Owner);
                        twUnitC_0.TW = default;
                    }


                    levUnit_0.SetLevel(LevelTypes.First);
                    hpUnit_0.SetMaxHp();
                    stepUnit_0.SetMaxSteps(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Steps), UnitUpgC.Steps(unit_0.Unit, levUnit_0.Level, ownUnit_0.Owner));
                    if (condUnit_0.HaveCondition) condUnit_0.Reset();

                    unit_0.SetNew(unit_0.Unit, levUnit_0.Level, ownUnit_0.Owner);

                    RpcSys.SoundToGeneral(sender, ClipTypes.ClickToTable);
                }

                else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
        }
    }
}