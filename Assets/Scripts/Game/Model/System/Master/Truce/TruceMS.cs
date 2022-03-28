using Chessy.Common;
using Chessy.Game.Entity.Model;
using Chessy.Game.Values;
using UnityEngine;

namespace Chessy.Game
{
    sealed class TruceMS : SystemModelGameAbs
    {
        internal TruceMS(in EntitiesModelGame eMGame) : base(eMGame)
        {

        }

        public void Run(in GameModeTC gameModeTC)
        {
            int random;

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                eMGame.HaveFire(cell_0) = false;

                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    eMGame.CellEs(cell_0).TrailHealthC(dirT).Health = 0;
                }
                

                if (eMGame.UnitTC(cell_0).HaveUnit)
                {
                    if (gameModeTC.Is(GameModes.TrainingOff))
                    {
                        if (eMGame.UnitPlayerTC(cell_0).Is(PlayerTypes.First))
                        {
                            if (eMGame.UnitExtraTWTC(cell_0).HaveToolWeapon)
                            {
                                eMGame.PlayerInfoE(eMGame.UnitPlayerTC(cell_0).Player).LevelE(eMGame.UnitExtraLevelTC(cell_0).Level).ToolWeapons(eMGame.UnitExtraTWTC(cell_0).ToolWeapon)++;
                                eMGame.UnitExtraTWTC(cell_0).ToolWeapon = ToolWeaponTypes.None;
                            }

                            //E.UnitInfoE(E.UnitPlayerTC(cell_0).Player, E.UnitLevelTC(cell_0).Level).HaveInInventor = true;
                            eMGame.UnitTC(cell_0).Unit = UnitTypes.None;
                        }
                    }
                    else
                    {

                        if (eMGame.UnitExtraTWTC(cell_0).HaveToolWeapon)
                        {
                            eMGame.PlayerInfoE(eMGame.UnitPlayerTC(cell_0).Player).LevelE(eMGame.UnitExtraLevelTC(cell_0).Level).ToolWeapons(eMGame.UnitExtraTWTC(cell_0).ToolWeapon)++;

                            eMGame.UnitExtraTWTC(cell_0).ToolWeapon = ToolWeaponTypes.None;
                        }

                        //E.UnitInfoE(E.UnitPlayerTC(cell_0).Player, E.UnitLevelTC(cell_0).Level).HaveInInventor = true;
                        eMGame.UnitTC(cell_0).Unit = UnitTypes.None;
                    }
                }


                if (eMGame.BuildingTC(cell_0).HaveBuilding)
                {
                    if (eMGame.BuildingTC(cell_0).Is(BuildingTypes.Camp))
                    {
                        //Es.WhereBuildingEs.HaveBuild(BuildEs(cell_0).BuildingE, cell_0).HaveBuilding.Have = false;
                        //Es.BuildE(cell_0).BuildingE.Destroy(Es);
                    }
                }

                else
                {
                    if (eMGame.YoungForestC(cell_0).HaveAnyResources)
                    {
                        eMGame.YoungForestC(cell_0).Resources = 0;

                       // Es.AdultForestC(cell_0).SetRandomResources();
                    }

                    if (!eMGame.EnvironmentEs(cell_0).FertilizeC.HaveAnyResources
                        && !eMGame.EnvironmentEs(cell_0).MountainC.HaveAnyResources
                        && !eMGame.AdultForestC(cell_0).HaveAnyResources)
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