using Leopotam.Ecs;
using Game.Common;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class ScoutOldNewSys : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, StepC> _statUnitF = default;
        private EcsFilter<ConditionC, EffectsC> _effUnitF = default;

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

            ref var tw_0 = ref UnitToolWeapon<ToolWeaponC>(idx_0);
            ref var twLevel_0 = ref UnitToolWeapon<LevelC>(idx_0);


            if (hpUnit_0.HaveMax)
            {
                if (UnitStat<UnitStatC>(idx_0).HaveMaxSteps(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Steps), UnitUpgC.Steps(unit_0.Unit, levUnit_0.Level, ownUnit_0.Owner)))
                {
                    var level = Unit<LevelC>(idx_0).Level;
                    var owner = Unit<OwnerC>(idx_0).Owner;

                    InvUnitsC.Take(owner, UnitTypes.Scout, LevelTypes.First);

                    WhereUnitsC.Set(UnitTypes.Scout, level, owner, idx_0, false);
                    unit_0.Clean();



                    if (tw_0.HaveTW)
                    {
                        InvTWC.Add(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Owner);
                        tw_0.Reset();
                    }


                    hpUnit_0.SetMax();
                    UnitStat<UnitStatC>(idx_0).SetMaxSteps(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Steps), UnitUpgC.Steps(unit_0.Unit, levUnit_0.Level, ownUnit_0.Owner));
                    if (condUnit_0.HaveCondition) condUnit_0.Reset();

                    unit_0.SetNew(unit_0.Unit, LevelTypes.First, ownUnit_0.Owner);

                    RpcSys.SoundToGeneral(sender, ClipTypes.ClickToTable);
                }

                else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
        }
    }
}