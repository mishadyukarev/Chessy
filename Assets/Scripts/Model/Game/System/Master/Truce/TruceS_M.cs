using Chessy.Common;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        const int PEOPLE_AFTER_TRUCE = 15;

        internal void ExecuteTruce()
        {
            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                _eMG.PlayerInfoE(playerT).KingInfoE.CellKing = 0;
                _eMG.PlayerInfoE(playerT).KingInfoE.HaveInInventor = true;

                _eMG.PlayerInfoE(playerT).GodInfoE.HaveHeroInInventor = true;

                _eMG.PlayerInfoE(playerT).PawnInfoC.PeopleInCity = PEOPLE_AFTER_TRUCE;
                _eMG.PlayerInfoE(playerT).PawnInfoC.AmountInGame = 0;
            }


            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                _eMG.HaveFire(cell_0) = false;

                TryDestroyAllTrailsOnCell(cell_0);




                if (_eMG.UnitTC(cell_0).HaveUnit)
                {
                    if (_eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline))
                    {
                        if (_eMG.UnitPlayerTC(cell_0).Is(PlayerTypes.First))
                        {
                            if (_eMG.ExtraToolWeaponTC(cell_0).HaveToolWeapon)
                            {
                                _eMG.PlayerInfoE(_eMG.UnitPlayerTC(cell_0).PlayerT).LevelE(_eMG.ExtraTWLevelTC(cell_0).LevelT).ToolWeapons(_eMG.ExtraToolWeaponTC(cell_0).ToolWeaponT)++;
                            }

                            UnitSs.ClearUnit(cell_0);
                        }
                    }
                    else
                    {

                        if (_eMG.ExtraToolWeaponTC(cell_0).HaveToolWeapon)
                        {
                            _eMG.PlayerInfoE(_eMG.UnitPlayerTC(cell_0).PlayerT).LevelE(_eMG.ExtraTWLevelTC(cell_0).LevelT).ToolWeapons(_eMG.ExtraToolWeaponTC(cell_0).ToolWeaponT)++;
                        }

                        UnitSs.ClearUnit(cell_0);
                    }
                }


                if (_eMG.BuildingTC(cell_0).HaveBuilding)
                {
                    if (_eMG.BuildingTC(cell_0).Is(BuildingTypes.Camp))
                    {
                        //Es.WhereBuildingEs.HaveBuild(BuildEs(cell_0).BuildingE, cell_0).HaveBuilding.Have = false;
                        //Es.BuildE(cell_0).BuildingE.Destroy(Es);
                    }
                }

                else
                {
                    if (_eMG.YoungForestC(cell_0).HaveAnyResources)
                    {
                        _eMG.YoungForestC(cell_0).Resources = 0;

                        _eMG.AdultForestC(cell_0).SetRandom(EnvironmentValues.MIN_RESOURCES_FOR_SPAWN, EnvironmentValues.MAX_RESOURCES);
                    }
                }
            }
        }
    }
}