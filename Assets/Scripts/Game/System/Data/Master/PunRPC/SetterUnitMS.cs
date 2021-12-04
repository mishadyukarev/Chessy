using Leopotam.Ecs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class SetterUnitMS : IEcsRunSystem
    {
        private EcsFilter<EnvC> _envF = default;
        private EcsFilter<FireC> _fireF = default;

        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, StepC, WaterC> _statUnitF = default;
        private EcsFilter<ConditionC, MoveInCondC, EffectsC> _effUnitF = default;
        private EcsFilter<ToolWeaponC> _twUnitF = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            UnitDoingMC.Get(out var unit);
            IdxDoingMC.Get(out var idx_0);


            ref var env_0 = ref _envF.Get1(idx_0);
            ref var fire_0 = ref _fireF.Get1(idx_0);

            ref var unit_0 = ref _unitF.Get1(idx_0);
            ref var levUnit_0 = ref _unitF.Get2(idx_0);
            ref var ownUnit_0 = ref _unitF.Get3(idx_0);

            ref var hp_0 = ref _statUnitF.Get1(idx_0);
            ref var step_0 = ref _statUnitF.Get2(idx_0);
            ref var water_0 = ref _statUnitF.Get3(idx_0);

            ref var cond_0 = ref _effUnitF.Get1(idx_0);
            ref var moveCond_0 = ref _effUnitF.Get2(idx_0);
            ref var eff_0 = ref _effUnitF.Get3(idx_0);

            ref var tw_0 = ref _twUnitF.Get1(idx_0);


            var whoseMove = WhoseMoveC.WhoseMove;


            if (SetUnitCellsC.HaveIdxCell(whoseMove, idx_0))
            {
                var levUnit = LevelTypes.None;

                if (InvUnitsC.Have(unit, LevelTypes.Second, whoseMove))
                {
                    InvUnitsC.Take(whoseMove, unit, LevelTypes.Second);
                    levUnit  = LevelTypes.Second;
                }
                else
                {
                    InvUnitsC.Take(whoseMove, unit, LevelTypes.First);
                    levUnit = LevelTypes.First;
                }
                Unit<UnitCellC>(idx_0).SetNew(unit, levUnit, whoseMove);


                UnitToolWeapon<UnitTWCellC>(idx_0).Reset();

                eff_0.DefAllEffects();
                Unit<HpUnitC>(idx_0).SetMax();
                if (cond_0.HaveCondition) cond_0.Reset();  
                moveCond_0.ResetAll();
                Unit<StepUnitC>(idx_0).SetMaxSteps();
                Unit<WaterUnitC>(idx_0).SetMaxWater();

      

                if (unit == UnitTypes.King) PickUpgC.SetHaveUpgrade(whoseMove, true);

                RpcSys.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}