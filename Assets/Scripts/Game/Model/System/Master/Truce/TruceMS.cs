using Chessy.Common;
using Chessy.Game.Entity.Model;
using Chessy.Game.Values;
using UnityEngine;

namespace Chessy.Game
{
    struct TruceMS
    {
        public void Run(in GameModeTC gameModeTC, in EntitiesModelGame e)
        {
            int random;

            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                e.HaveFire(idx_0) = false;

                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    e.CellEs(idx_0).TrailHealthC(dirT).Health = 0;
                }
                

                if (e.UnitTC(idx_0).HaveUnit)
                {
                    if (gameModeTC.Is(GameModes.TrainingOff))
                    {
                        if (e.UnitPlayerTC(idx_0).Is(PlayerTypes.First))
                        {
                            if (e.UnitExtraTWTC(idx_0).HaveToolWeapon)
                            {
                                e.PlayerInfoE(e.UnitPlayerTC(idx_0).Player).LevelE(e.UnitExtraLevelTC(idx_0).Level).ToolWeapons(e.UnitExtraTWTC(idx_0).ToolWeapon)++;
                                e.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
                            }

                            //E.UnitInfoE(E.UnitPlayerTC(idx_0).Player, E.UnitLevelTC(idx_0).Level).HaveInInventor = true;
                            e.UnitTC(idx_0).Unit = UnitTypes.None;
                        }
                    }
                    else
                    {

                        if (e.UnitExtraTWTC(idx_0).HaveToolWeapon)
                        {
                            e.PlayerInfoE(e.UnitPlayerTC(idx_0).Player).LevelE(e.UnitExtraLevelTC(idx_0).Level).ToolWeapons(e.UnitExtraTWTC(idx_0).ToolWeapon)++;

                            e.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
                        }

                        //E.UnitInfoE(E.UnitPlayerTC(idx_0).Player, E.UnitLevelTC(idx_0).Level).HaveInInventor = true;
                        e.UnitTC(idx_0).Unit = UnitTypes.None;
                    }
                }


                if (e.BuildingTC(idx_0).HaveBuilding)
                {
                    if (e.BuildingTC(idx_0).Is(BuildingTypes.Camp))
                    {
                        //Es.WhereBuildingEs.HaveBuild(BuildEs(idx_0).BuildingE, idx_0).HaveBuilding.Have = false;
                        //Es.BuildE(idx_0).BuildingE.Destroy(Es);
                    }
                }

                else
                {
                    if (e.YoungForestC(idx_0).HaveAnyResources)
                    {
                        e.YoungForestC(idx_0).Resources = 0;

                       // Es.AdultForestC(idx_0).SetRandomResources();
                    }

                    if (!e.EnvironmentEs(idx_0).FertilizeC.HaveAnyResources
                        && !e.EnvironmentEs(idx_0).MountainC.HaveAnyResources
                        && !e.AdultForestC(idx_0).HaveAnyResources)
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