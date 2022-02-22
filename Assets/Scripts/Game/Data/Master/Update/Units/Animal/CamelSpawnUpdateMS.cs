﻿using UnityEngine;

namespace Game.Game
{
    sealed class CamelSpawnUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal CamelSpawnUpdateMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            var haveCamel = false;

            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.UnitTC(idx_0).Is(UnitTypes.Camel))
                {
                    haveCamel = true;
                    break;
                }
            }

            if (!haveCamel)
            {
                byte idx_0 = (byte)Random.Range(0,  Start_Values.ALL_CELLS_AMOUNT);

                if (E.CellEs(idx_0).IsActiveParentSelf)
                {
                    if (!E.UnitTC(idx_0).HaveUnit && !E.EnvironmentEs(idx_0).MountainC.HaveAnyResources)
                    {
                        bool haveNearUnit = false;

                        foreach (var cellE in E.CellEs(idx_0).AroundCellEs)
                        {
                            if (E.UnitTC(cellE.IdxC.Idx).HaveUnit)
                            {
                                haveNearUnit = true;
                                break;
                            }
                        }

                        if (!haveNearUnit)
                        {
                            E.UnitTC(idx_0).Unit = UnitTypes.Camel;
                            E.UnitLevelTC(idx_0).Level = LevelTypes.First;
                            E.UnitPlayerTC(idx_0).Player = PlayerTypes.None;
                            E.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;

                            E.UnitHpC(idx_0).Health = CellUnitStatHp_Values.MAX_HP;
                            E.UnitStepC(idx_0).Steps = 1f;
                            E.UnitWaterC(idx_0).Water = 1f;


                            //Es.UnitE(idx_0).SetNew((UnitTypes.Camel, LevelTypes.First, PlayerTypes.None, ConditionUnitTypes.None, false), Es);
                            return;
                        }
                    }
                }
            }
        }
    }
}