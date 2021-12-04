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

            ref var hpUnitCell_0 = ref Unit<HpUnitC>(idx_0);
            ref var hpUnit_0 = ref _statUnitF.Get1(idx_0);
            ref var stepUnit_0 = ref _statUnitF.Get2(idx_0);

            ref var condUnit_0 = ref _effUnitF.Get1(idx_0);
            ref var effUnit_0 = ref _effUnitF.Get2(idx_0);

            ref var tw_0 = ref UnitToolWeapon<ToolWeaponC>(idx_0);
            ref var twLevel_0 = ref UnitToolWeapon<LevelC>(idx_0);


            if (hpUnitCell_0.HaveMax)
            {
                if (Unit<StepUnitC>(idx_0).HaveMaxSteps)
                {
                    InvUnitsC.Take(Unit<OwnerC>(idx_0).Owner, UnitTypes.Scout, LevelTypes.First);
                    Unit<UnitCellC>(idx_0).SetScout(UnitTypes.Scout, LevelTypes.First);

                    RpcSys.SoundToGeneral(sender, ClipTypes.ClickToTable);
                }

                else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
        }
    }
}