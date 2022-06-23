using Chessy.Common;
using Chessy.Model.Model.Entity;
using Chessy.Model.Values;

namespace Chessy.Model.Model.System
{
    static class TruceS
    {
        const int PEOPLE_AFTER_TRUCE = 15;

        internal static void ExecuteTruce(this EntitiesModel e, in SystemsModel s)
        {
            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                e.PlayerInfoE(playerT).KingInfoE.CellKing = 0;
                e.PlayerInfoE(playerT).KingInfoE.HaveInInventor = true;

                e.PlayerInfoE(playerT).GodInfoE.HaveHeroInInventor = true;

                e.PlayerInfoE(playerT).PawnInfoC.PeopleInCity = PEOPLE_AFTER_TRUCE;
                e.PlayerInfoE(playerT).PawnInfoC.AmountInGame = 0;
            }


            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                e.HaveFire(cellIdxCurrent) = false;

                s.TryDestroyAllTrailsOnCell(cellIdxCurrent);




                if (e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    if (e.GameModeT.Is(GameModeTypes.TrainingOffline))
                    {
                        if (e.UnitPlayerT(cellIdxCurrent).Is(PlayerTypes.First))
                        {
                            if (e.ExtraToolWeaponT(cellIdxCurrent).HaveToolWeapon())
                            {
                                e.PlayerInfoE(e.UnitPlayerT(cellIdxCurrent)).LevelE(e.ExtraTWLevelT(cellIdxCurrent)).ToolWeapons(e.ExtraToolWeaponT(cellIdxCurrent))++;
                            }

                            e.UnitEs(cellIdxCurrent).ClearEverything();
                        }
                    }
                    else
                    {

                        if (e.ExtraToolWeaponT(cellIdxCurrent).HaveToolWeapon())
                        {
                            e.PlayerInfoE(e.UnitPlayerT(cellIdxCurrent)).LevelE(e.ExtraTWLevelT(cellIdxCurrent)).ToolWeapons(e.ExtraToolWeaponT(cellIdxCurrent))++;
                        }

                        e.UnitEs(cellIdxCurrent).ClearEverything();
                    }
                }


                if (e.BuildingOnCellT(cellIdxCurrent).HaveBuilding())
                {
                    if (e.BuildingOnCellT(cellIdxCurrent).Is(BuildingTypes.Camp))
                    {
                        //Es.WhereBuildingEs.HaveBuild(BuildEs(cell_0).BuildingE, cell_0).HaveBuilding.Have = false;
                        //Es.BuildE(cell_0).BuildingE.Destroy(Es);
                    }
                }

                else
                {
                    if (e.YoungForestC(cellIdxCurrent).HaveAnyResources)
                    {
                        e.YoungForestC(cellIdxCurrent).Resources = 0;

                        e.AdultForestC(cellIdxCurrent).SetRandom(EnvironmentValues.MIN_RESOURCES_FOR_SPAWN, EnvironmentValues.MAX_RESOURCES);
                    }
                }
            }
        }
    }
}