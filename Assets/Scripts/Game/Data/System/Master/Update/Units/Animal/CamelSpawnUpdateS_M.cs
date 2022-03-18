using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using UnityEngine;

namespace Chessy.Game
{
    static class CamelSpawnUpdateS_M
    {
        public static void SpawnCamelUpdate(this EntitiesModel e)
        {
            var haveCamel = false;

            for (byte idx_0 = 0; idx_0 < e.LengthCells; idx_0++)
            {
                if (e.UnitTC(idx_0).Is(UnitTypes.Camel))
                {
                    haveCamel = true;
                    break;
                }
            }

            if (!haveCamel)
            {
                byte idx_0 = (byte)Random.Range(0,  StartValues.CELLS);

                if (e.CellEs(idx_0).IsActiveParentSelf)
                {
                    if (!e.UnitTC(idx_0).HaveUnit && !e.EnvironmentEs(idx_0).MountainC.HaveAnyResources)
                    {
                        bool haveNearUnit = false;

                        foreach (var cellE in e.CellEs(idx_0).AroundCellEs)
                        {
                            if (e.UnitTC(cellE.IdxC.Idx).HaveUnit)
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            e.UnitTC(idx_0).Unit = UnitTypes.Camel;
                            e.UnitLevelTC(idx_0).Level = LevelTypes.First;
                            e.UnitPlayerTC(idx_0).Player = PlayerTypes.None;
                            e.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;

                            e.UnitHpC(idx_0).Health = HpValues.MAX;
                            e.UnitStepC(idx_0).Steps = 1f;
                            e.UnitWaterC(idx_0).Water = 1f;

                            e.UnitEffectShield(idx_0).Protection = 0;


                            //Es.UnitE(idx_0).SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ConditionUnitTypes.None, false), Es);
                            return;
                        }
                    }
                }
            }
        }
    }
}