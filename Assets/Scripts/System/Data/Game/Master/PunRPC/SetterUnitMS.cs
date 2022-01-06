using Leopotam.Ecs;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class SetterUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            UnitDoingMC.Get(out var unit);
            IdxDoingMC.Get(out var idx_0);


            ref var env_0 = ref Environment<EnvironmentC>(idx_0);
            ref var fire_0 = ref Fire<HaveEffectC>(idx_0);

            ref var unit_0 = ref Unit<UnitC>(idx_0);
            ref var levUnit_0 = ref Unit<LevelC>(idx_0);
            ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);

            ref var hp_0 = ref Unit<HpC>(idx_0);
            ref var step_0 = ref Unit<StepC>(idx_0);
            ref var water_0 = ref Unit<WaterC>(idx_0);

            ref var cond_0 = ref Unit<ConditionC>(idx_0);
            ref var moveCond_0 = ref Unit<MoveInCondC>(idx_0);
            ref var eff_0 = ref Unit<EffectsC>(idx_0);

            ref var tw_0 = ref UnitTW<ToolWeaponC>(idx_0);


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
                Unit<UnitCellEC>(idx_0).SetNew((unit, levUnit, whoseMove));


                if (unit == UnitTypes.King) PickUpgC.SetHaveUpgrade(whoseMove, true);

                RpcSys.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}