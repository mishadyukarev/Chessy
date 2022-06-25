using Chessy.Common;
using Chessy.Model;
using Chessy.Model.Values;

namespace Chessy.Model
{
    static class TruceS
    {
        const int PEOPLE_AFTER_TRUCE = 15;

        internal static void ExecuteTruce(this EntitiesModel e)
        {
            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                //e.PlayerInfoE(playerT).KingInfoE.CellKing = 0;
                e.PlayerInfoE(playerT).PlayerInfoC.HaveKingInInventor = true;

                e.PlayerInfoE(playerT).GodInfoC.HaveGodInInventor = true;

                e.PlayerInfoE(playerT).PawnInfoC.PeopleInCity = PEOPLE_AFTER_TRUCE;
                e.PlayerInfoE(playerT).PawnInfoC.AmountInGame = 0;
            }


            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                e.HaveFire(cellIdxCurrent) = false;

                e.TryDestroyAllTrailsOnCell(cellIdxCurrent);




                if (e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    if (e.GameModeT.Is(GameModeTypes.TrainingOffline))
                    {
                        if (e.UnitPlayerT(cellIdxCurrent).Is(PlayerTypes.First))
                        {
                            if (e.ExtraToolWeaponT(cellIdxCurrent).HaveToolWeapon())
                            {
                                e.AddToolWeaponsInInventor(e.UnitPlayerT(cellIdxCurrent), e.ExtraTWLevelT(cellIdxCurrent), e.ExtraToolWeaponT(cellIdxCurrent), 1);
                            }

                            e.UnitE(cellIdxCurrent).ClearEverything();
                        }
                    }
                    else
                    {

                        if (e.ExtraToolWeaponT(cellIdxCurrent).HaveToolWeapon())
                        {
                            e.AddToolWeaponsInInventor(e.UnitPlayerT(cellIdxCurrent), e.ExtraTWLevelT(cellIdxCurrent), e.ExtraToolWeaponT(cellIdxCurrent), 1);
                        }

                        e.UnitE(cellIdxCurrent).ClearEverything();
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