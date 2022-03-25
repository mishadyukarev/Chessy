using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using UnityEngine;

namespace Chessy.Game
{
    sealed class CamelSpawnUpdateS_M
    {
        public void SpawnCamelUpdate(in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            var haveCamel = false;

            for (byte cell_0 = 0; cell_0 < e.LengthCells; cell_0++)
            {
                if (e.UnitTC(cell_0).Is(UnitTypes.Wolf))
                {
                    haveCamel = true;
                    break;
                }
            }

            if (!haveCamel)
            {
                byte cell_0 = (byte)Random.Range(0,  StartValues.CELLS);

                if (e.CellEs(cell_0).IsActiveParentSelf)
                {
                    if (!e.UnitTC(cell_0).HaveUnit && !e.EnvironmentEs(cell_0).MountainC.HaveAnyResources)
                    {
                        bool haveNearUnit = false;

                        foreach (var cellE in e.CellEs(cell_0).AroundCellEs)
                        {
                            if (e.UnitTC(cellE.IdxC.Idx).HaveUnit)
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            e.UnitTC(cell_0).Unit = UnitTypes.Wolf;
                            e.UnitLevelTC(cell_0).Level = LevelTypes.First;
                            e.UnitPlayerTC(cell_0).Player = PlayerTypes.None;
                            e.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;

                            e.UnitHpC(cell_0).Health = HpValues.MAX;
                            e.UnitStepC(cell_0).Steps = 1f;
                            e.UnitWaterC(cell_0).Water = 1f;

                            e.UnitEffectShield(cell_0).Protection = 0;


                            //Es.UnitE(cell_0).SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ConditionUnitTypes.None, false), Es);
                            return;
                        }
                    }
                }
            }
        }
    }
}