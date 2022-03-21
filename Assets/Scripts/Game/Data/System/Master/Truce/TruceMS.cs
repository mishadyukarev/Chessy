﻿using Chessy.Common;
using Chessy.Game.Values;
using UnityEngine;

namespace Chessy.Game
{
    sealed class TruceMS : SystemAbstract, IEcsRunSystem
    {
        internal TruceMS(in Chessy.Game.Entity.Model.EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            int random;

            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                E.HaveFire(idx_0) = false;

                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    E.CellEs(idx_0).TrailHealthC(dirT).Health = 0;
                }
                

                if (E.UnitTC(idx_0).HaveUnit)
                {
                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (E.UnitPlayerTC(idx_0).Is(PlayerTypes.First))
                        {
                            if (E.UnitExtraTWTC(idx_0).HaveToolWeapon)
                            {
                                E.PlayerInfoE(E.UnitPlayerTC(idx_0).Player).LevelE(E.UnitExtraLevelTC(idx_0).Level).ToolWeapons(E.UnitExtraTWTC(idx_0).ToolWeapon)++;
                                E.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
                            }

                            //E.UnitInfoE(E.UnitPlayerTC(idx_0).Player, E.UnitLevelTC(idx_0).Level).HaveInInventor = true;
                            E.UnitTC(idx_0).Unit = UnitTypes.None;
                        }
                    }
                    else
                    {

                        if (E.UnitExtraTWTC(idx_0).HaveToolWeapon)
                        {
                            E.PlayerInfoE(E.UnitPlayerTC(idx_0).Player).LevelE(E.UnitExtraLevelTC(idx_0).Level).ToolWeapons(E.UnitExtraTWTC(idx_0).ToolWeapon)++;

                            E.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
                        }

                        //E.UnitInfoE(E.UnitPlayerTC(idx_0).Player, E.UnitLevelTC(idx_0).Level).HaveInInventor = true;
                        E.UnitTC(idx_0).Unit = UnitTypes.None;
                    }
                }


                if (E.BuildingTC(idx_0).HaveBuilding)
                {
                    if (E.BuildingTC(idx_0).Is(BuildingTypes.Camp))
                    {
                        //Es.WhereBuildingEs.HaveBuild(BuildEs(idx_0).BuildingE, idx_0).HaveBuilding.Have = false;
                        //Es.BuildE(idx_0).BuildingE.Destroy(Es);
                    }
                }

                else
                {
                    if (E.YoungForestC(idx_0).HaveAnyResources)
                    {
                        E.YoungForestC(idx_0).Resources = 0;

                       // Es.AdultForestC(idx_0).SetRandomResources();
                    }

                    if (!E.EnvironmentEs(idx_0).FertilizeC.HaveAnyResources
                        && !E.EnvironmentEs(idx_0).MountainC.HaveAnyResources
                        && !E.AdultForestC(idx_0).HaveAnyResources)
                    {
                        random = Random.Range(0, 100);

                        if (random <= 3)
                        {
                            //Es.EnvironmentEs(idx_0).FertilizeC.Resources = CellEnvironmentValues.
                        }
                    }
                }
            }
        }
    }
}