using Leopotam.Ecs;
using Game.Common;

namespace Game.Game
{
    public sealed class ScoutOldNewSys : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, StepC> _statUnitF = default;
        private EcsFilter<ConditionC, UnitEffectsC> _effUnitF = default;

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

            ref var tw_0 = ref EntityPool.ToolWeapon<ToolWeaponC>(idx_0);
            ref var twLevel_0 = ref EntityPool.ToolWeapon<LevelC>(idx_0);


            if (hpUnit_0.HaveMaxHp)
            {
                if (stepUnit_0.HaveMaxSteps(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Steps), UnitUpgC.Steps(unit_0.Unit, levUnit_0.Level, ownUnit_0.Owner)))
                {
                    unit_0.CreateScout();

                    if (tw_0.HaveTW)
                    {
                        InvTWC.Add(tw_0.TW, twLevel_0.Level, ownUnit_0.Owner);
                        tw_0.Reset();
                    }


                    levUnit_0.Set(LevelTypes.First);
                    hpUnit_0.SetMax();
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