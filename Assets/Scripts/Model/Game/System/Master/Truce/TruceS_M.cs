using Chessy.Common;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed partial class TruceS : SystemModel
    {
        const int PEOPLE_AFTER_TRUCE = 15;

        internal TruceS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void ExecuteTruce()
        {
            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                _e.PlayerInfoE(playerT).KingInfoE.CellKing = 0;
                _e.PlayerInfoE(playerT).KingInfoE.HaveInInventor = true;

                _e.PlayerInfoE(playerT).GodInfoE.HaveHeroInInventor = true;

                _e.PlayerInfoE(playerT).PawnInfoC.PeopleInCity = PEOPLE_AFTER_TRUCE;
                _e.PlayerInfoE(playerT).PawnInfoC.AmountInGame = 0;
            }


            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _e.HaveFire(cellIdxCurrent) = false;

                _s.TryDestroyAllTrailsOnCell(cellIdxCurrent);




                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    if (_e.Common.GameModeT.Is(GameModeTypes.TrainingOffline))
                    {
                        if (_e.UnitPlayerT(cellIdxCurrent).Is(PlayerTypes.First))
                        {
                            if (_e.ExtraToolWeaponT(cellIdxCurrent).HaveToolWeapon())
                            {
                                _e.PlayerInfoE(_e.UnitPlayerT(cellIdxCurrent)).LevelE(_e.ExtraTWLevelT(cellIdxCurrent)).ToolWeapons(_e.ExtraToolWeaponT(cellIdxCurrent))++;
                            }

                            _e.UnitEs(cellIdxCurrent).ClearEverything();
                        }
                    }
                    else
                    {

                        if (_e.ExtraToolWeaponT(cellIdxCurrent).HaveToolWeapon())
                        {
                            _e.PlayerInfoE(_e.UnitPlayerT(cellIdxCurrent)).LevelE(_e.ExtraTWLevelT(cellIdxCurrent)).ToolWeapons(_e.ExtraToolWeaponT(cellIdxCurrent))++;
                        }

                        _e.UnitEs(cellIdxCurrent).ClearEverything();
                    }
                }


                if (_e.BuildingOnCellT(cellIdxCurrent).HaveBuilding())
                {
                    if (_e.BuildingOnCellT(cellIdxCurrent).Is(BuildingTypes.Camp))
                    {
                        //Es.WhereBuildingEs.HaveBuild(BuildEs(cell_0).BuildingE, cell_0).HaveBuilding.Have = false;
                        //Es.BuildE(cell_0).BuildingE.Destroy(Es);
                    }
                }

                else
                {
                    if (_e.YoungForestC(cellIdxCurrent).HaveAnyResources)
                    {
                        _e.YoungForestC(cellIdxCurrent).Resources = 0;

                        _e.AdultForestC(cellIdxCurrent).SetRandom(EnvironmentValues.MIN_RESOURCES_FOR_SPAWN, EnvironmentValues.MAX_RESOURCES);
                    }
                }
            }
        }
    }
}