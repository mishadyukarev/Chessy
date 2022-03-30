using Chessy.Common;
using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Chessy.Game.Values;
using UnityEngine;

namespace Chessy.Game
{
    sealed class TruceMS : SystemModelGameAbs
    {
        internal TruceMS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Run(in GameModeTC gameModeTC)
        {
            int random;

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                e.HaveFire(cell_0) = false;

                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    e.CellEs(cell_0).TrailHealthC(dirT).Health = 0;
                }
                

                if (e.UnitTC(cell_0).HaveUnit)
                {
                    if (gameModeTC.Is(GameModes.TrainingOff))
                    {
                        if (e.UnitPlayerTC(cell_0).Is(PlayerTypes.First))
                        {
                            if (e.UnitExtraTWTC(cell_0).HaveToolWeapon)
                            {
                                e.PlayerInfoE(e.UnitPlayerTC(cell_0).Player).LevelE(e.UnitExtraLevelTC(cell_0).Level).ToolWeapons(e.UnitExtraTWTC(cell_0).ToolWeapon)++;
                                e.UnitExtraTWTC(cell_0).ToolWeapon = ToolWeaponTypes.None;
                            }

                            //E.UnitInfoE(E.UnitPlayerTC(cell_0).Player, E.UnitLevelTC(cell_0).Level).HaveInInventor = true;
                            e.UnitTC(cell_0).Unit = UnitTypes.None;
                        }
                    }
                    else
                    {

                        if (e.UnitExtraTWTC(cell_0).HaveToolWeapon)
                        {
                            e.PlayerInfoE(e.UnitPlayerTC(cell_0).Player).LevelE(e.UnitExtraLevelTC(cell_0).Level).ToolWeapons(e.UnitExtraTWTC(cell_0).ToolWeapon)++;

                            e.UnitExtraTWTC(cell_0).ToolWeapon = ToolWeaponTypes.None;
                        }

                        //E.UnitInfoE(E.UnitPlayerTC(cell_0).Player, E.UnitLevelTC(cell_0).Level).HaveInInventor = true;
                        e.UnitTC(cell_0).Unit = UnitTypes.None;
                    }
                }


                if (e.BuildingTC(cell_0).HaveBuilding)
                {
                    if (e.BuildingTC(cell_0).Is(BuildingTypes.Camp))
                    {
                        //Es.WhereBuildingEs.HaveBuild(BuildEs(cell_0).BuildingE, cell_0).HaveBuilding.Have = false;
                        //Es.BuildE(cell_0).BuildingE.Destroy(Es);
                    }
                }

                else
                {
                    if (e.YoungForestC(cell_0).HaveAnyResources)
                    {
                        e.YoungForestC(cell_0).Resources = 0;

                       // Es.AdultForestC(cell_0).SetRandomResources();
                    }

                    if (!e.EnvironmentEs(cell_0).FertilizeC.HaveAnyResources
                        && !e.EnvironmentEs(cell_0).MountainC.HaveAnyResources
                        && !e.AdultForestC(cell_0).HaveAnyResources)
                    {
                        random = Random.Range(0, 100);

                        if (random <= 3)
                        {
                            //Es.EnvironmentEs(cell_0).FertilizeC.Resources = CellEnvironmentValues.
                        }
                    }
                }
            }
        }
    }
}