using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using UnityEngine;

namespace Chessy.Game
{
    sealed class TruceMS : SystemModelGameAbs
    {
        public TruceMS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
        }

        internal void Run(in GameModeTC gameModeTC)
        {
            int random;

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                eMG.HaveFire(cell_0) = false;

                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    eMG.CellEs(cell_0).TrailHealthC(dirT).Health = 0;
                }
                

                if (eMG.UnitTC(cell_0).HaveUnit)
                {
                    if (gameModeTC.Is(GameModes.TrainingOff))
                    {
                        if (eMG.UnitPlayerTC(cell_0).Is(PlayerTypes.First))
                        {
                            if (eMG.UnitExtraTWTC(cell_0).HaveToolWeapon)
                            {
                                eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).LevelE(eMG.UnitExtraLevelTC(cell_0).LevelT).ToolWeapons(eMG.UnitExtraTWTC(cell_0).ToolWeaponT)++;
                                eMG.UnitExtraTWTC(cell_0).ToolWeaponT = ToolWeaponTypes.None;
                            }

                            //E.UnitInfoE(E.UnitPlayerTC(cell_0).Player, E.UnitLevelTC(cell_0).Level).HaveInInventor = true;
                            eMG.UnitTC(cell_0).UnitT = UnitTypes.None;
                        }
                    }
                    else
                    {

                        if (eMG.UnitExtraTWTC(cell_0).HaveToolWeapon)
                        {
                            eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).LevelE(eMG.UnitExtraLevelTC(cell_0).LevelT).ToolWeapons(eMG.UnitExtraTWTC(cell_0).ToolWeaponT)++;

                            eMG.UnitExtraTWTC(cell_0).ToolWeaponT = ToolWeaponTypes.None;
                        }

                        //E.UnitInfoE(E.UnitPlayerTC(cell_0).Player, E.UnitLevelTC(cell_0).Level).HaveInInventor = true;
                        eMG.UnitTC(cell_0).UnitT = UnitTypes.None;
                    }
                }


                if (eMG.BuildingTC(cell_0).HaveBuilding)
                {
                    if (eMG.BuildingTC(cell_0).Is(BuildingTypes.Camp))
                    {
                        //Es.WhereBuildingEs.HaveBuild(BuildEs(cell_0).BuildingE, cell_0).HaveBuilding.Have = false;
                        //Es.BuildE(cell_0).BuildingE.Destroy(Es);
                    }
                }

                else
                {
                    if (eMG.YoungForestC(cell_0).HaveAnyResources)
                    {
                        eMG.YoungForestC(cell_0).Resources = 0;

                       // Es.AdultForestC(cell_0).SetRandomResources();
                    }

                    if (!eMG.EnvironmentEs(cell_0).FertilizeC.HaveAnyResources
                        && !eMG.EnvironmentEs(cell_0).MountainC.HaveAnyResources
                        && !eMG.AdultForestC(cell_0).HaveAnyResources)
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