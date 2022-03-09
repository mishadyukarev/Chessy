using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.Systems.Model.Master.Methods
{
    public struct IceWallSnowyMS
    {
        public IceWallSnowyMS(in byte idx_0, in EntitiesModel _e)
        {
            var ability = AbilityTypes.IceWall;

            if (_e.UnitStepC(idx_0).Steps >= StepValues.BUILDING_ICE_WALL || _e.RiverEs(idx_0).RiverTC.HaveRiverNear)
            {
                if (!_e.BuildingTC(idx_0).HaveBuilding)
                {
                    if (!_e.AdultForestC(idx_0).HaveAnyResources)
                    {
                        _e.AdultForestC(idx_0).Resources = 0;
                        _e.FertilizeC(idx_0).Resources = 0;

                        if (_e.UnitStepC(idx_0).Steps >= StepValues.BUILDING_ICE_WALL)
                        {
                            _e.UnitStepC(idx_0).Steps -= StepValues.BUILDING_ICE_WALL;

                            _e.UnitEs(idx_0).CoolDownC(ability).Cooldown = AbilityCooldownValues.AFTER_ICE_WALL;

                            _e.BuildingMainE(idx_0).Set(BuildingTypes.IceWall, LevelTypes.First, HpValues.MAX, _e.UnitPlayerTC(idx_0).Player);
                        }
                    }
                }
            }
        }
    }
}