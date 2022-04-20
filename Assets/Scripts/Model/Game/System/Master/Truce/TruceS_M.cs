using Chessy.Common;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class TruceS_M : SystemModel
    {
        const float PEOPLE_AFTER_TRUCE = 15;

        internal TruceS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void Truce()
        {
            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                eMG.PlayerInfoE(playerT).KingInfoE.CellKing = 0;
                eMG.PlayerInfoE(playerT).KingInfoE.HaveInInventor = true;

                eMG.PlayerInfoE(playerT).GodInfoE.HaveHeroInInventor = true;

                eMG.PlayerInfoE(playerT).PawnInfoE.PeopleInCityC.People = PEOPLE_AFTER_TRUCE;
                eMG.PlayerInfoE(playerT).PawnInfoE.PawnsInGame = 0;
            }


            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                eMG.HaveFire(cell_0) = false;

                sMG.MasterSs.TryClearAllTrailsOnCellS.TryDestroy(cell_0);




                if (eMG.UnitTC(cell_0).HaveUnit)
                {
                    if (eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline))
                    {
                        if (eMG.UnitPlayerTC(cell_0).Is(PlayerTypes.First))
                        {
                            if (eMG.ExtraToolWeaponTC(cell_0).HaveToolWeapon)
                            {
                                eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).LevelE(eMG.ExtraTWLevelTC(cell_0).LevelT).ToolWeapons(eMG.ExtraToolWeaponTC(cell_0).ToolWeaponT)++;
                            }

                            sMG.UnitSs.ClearUnit(cell_0);
                        }
                    }
                    else
                    {

                        if (eMG.ExtraToolWeaponTC(cell_0).HaveToolWeapon)
                        {
                            eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).LevelE(eMG.ExtraTWLevelTC(cell_0).LevelT).ToolWeapons(eMG.ExtraToolWeaponTC(cell_0).ToolWeaponT)++;
                        }

                        sMG.UnitSs.ClearUnit(cell_0);
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

                        eMG.AdultForestC(cell_0).SetRandom(EnvironmentValues.MIN_RESOURCES_FOR_SPAWN, EnvironmentValues.MAX_RESOURCES);
                    }
                }
            }
        }
    }
}