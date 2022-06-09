using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;

namespace Chessy.Game
{
    internal sealed class ResetAllS_M : SystemModel
    {
        internal ResetAllS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {

        }

        public void ResetAll()
        {
            eMG.IsStartedGame = default;
            eMG.MotionsC.Motions = default;
            eMG.ZoneInfoC.IsActiveFriend = default;
            eMG.ZoneInfoC = default;
            eMG.WhoseMovePlayerTC.PlayerT = default;
            eMG.CellClickTC.CellClickT = default;
            eMG.IsSelectedCity = default;
            eMG.HaveTreeUnit = default;
            eMG.MistakeT = default;
            eMG.WinnerPlayerT = default;
            eMG.CellsC = default;
            eMG.CurPlayerIT = default;
            eMG.AmountPlantedYoungForests = default;

            eMG.WeatherE.WindC = new WindC(default, default, default, default);
            eMG.WeatherE.SunSideTC.SunSideT = default;
            eMG.WeatherE.CloudC.Center = default;

            eMG.SelectedE.ToolWeaponC = new SelectedToolWeaponC(default, default);

            eMG.LessonT = default;

            for (byte cellIdx = 0; cellIdx < StartValues.CELLS; cellIdx++)
            {
                sMG.MasterSs.ClearAllEnvironmentS.Clear(cellIdx);

                for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++)
                    eMG.HealthTrail(cellIdx).Health(dirT) = 0;

                sMG.UnitSs.ClearUnit(cellIdx);
                eMG.BuildingTC(cellIdx).BuildingT = default;
            }

            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                eMG.PlayerInfoE(playerT).IsReadyForStartOnlineGame = default;

                eMG.PlayerInfoE(playerT).BuildingsInfoC.Clear();

                eMG.PlayerInfoE(playerT).AmountFarmsInGame = default;

                eMG.PlayerInfoE(playerT).PawnInfoC.PeopleInCity = default;
                eMG.PlayerInfoE(playerT).PawnInfoC.MaxAvailable = default;
                eMG.PlayerInfoE(playerT).PawnInfoC.AmountInGame = default;

                eMG.PlayerInfoE(playerT).KingInfoE.HaveInInventor = default;
                eMG.PlayerInfoE(playerT).WoodForBuyHouse = default;
                eMG.PlayerInfoE(playerT).IsReadyForStartOnlineGame = default;

                eMG.PlayerInfoE(playerT).GodInfoE = default;
                eMG.PlayerInfoE(playerT).GodInfoE.HaveHeroInInventor = default;
                eMG.PlayerInfoE(playerT).WhereKingEffects.Clear();

                for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
                {
                    for (var twT = (ToolWeaponTypes)1; twT < ToolWeaponTypes.End; twT++)
                    {
                        eMG.PlayerInfoE(playerT).LevelE(levT).ToolWeapons(twT) = default;
                    }

                    for (var buildT = (BuildingTypes)1; buildT < BuildingTypes.End; buildT++)
                    {
                        eMG.PlayerInfoE(playerT).LevelE(levT).BuildingInfoE(buildT).IdxC.Clear();
                    }
                }

                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    eMG.PlayerInfoE(playerT).ResourcesC(resT).Resources = default;
                }
            }
        }
    }
}