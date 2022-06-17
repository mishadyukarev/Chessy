using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {

        public void ResetAll()
        {
            _eMG.IsStartedGame = default;
            _eMG.MotionsC.Motions = default;
            _eMG.ZoneInfoC.IsActiveFriend = default;
            _eMG.ZoneInfoC = default;
            _eMG.WhoseMovePlayerTC.PlayerT = default;
            _eMG.CellClickTC.CellClickT = default;
            _eMG.IsSelectedCity = default;
            _eMG.HaveTreeUnit = default;
            _eMG.MistakeT = default;
            _eMG.WinnerPlayerT = default;
            _eMG.CellsC = default;
            _eMG.CurPlayerIT = default;
            _eMG.AmountPlantedYoungForests = default;

            _eMG.WeatherE.WindC = new WindC(default, default, default, default);
            _eMG.WeatherE.SunSideTC.SunSideT = default;
            _eMG.WeatherE.CloudC.Center = default;

            _eMG.SelectedE.ToolWeaponC = new SelectedToolWeaponC(default, default);

            _eMG.LessonT = default;

            for (byte cellIdx = 0; cellIdx < StartValues.CELLS; cellIdx++)
            {
                ClearAllEnvironment(cellIdx);

                for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++)
                    _eMG.HealthTrail(cellIdx).Health(dirT) = 0;

                UnitSs.ClearUnit(cellIdx);
                _eMG.BuildingTC(cellIdx).BuildingT = default;
            }

            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                _eMG.PlayerInfoE(playerT).IsReadyForStartOnlineGame = default;

                _eMG.PlayerInfoE(playerT).BuildingsInfoC.Clear();

                _eMG.PlayerInfoE(playerT).AmountFarmsInGame = default;

                _eMG.PlayerInfoE(playerT).PawnInfoC.PeopleInCity = default;
                _eMG.PlayerInfoE(playerT).PawnInfoC.MaxAvailable = default;
                _eMG.PlayerInfoE(playerT).PawnInfoC.AmountInGame = default;

                _eMG.PlayerInfoE(playerT).KingInfoE.HaveInInventor = default;
                _eMG.PlayerInfoE(playerT).WoodForBuyHouse = default;
                _eMG.PlayerInfoE(playerT).IsReadyForStartOnlineGame = default;

                _eMG.PlayerInfoE(playerT).GodInfoE = default;
                _eMG.PlayerInfoE(playerT).GodInfoE.HaveHeroInInventor = default;
                _eMG.PlayerInfoE(playerT).WhereKingEffects.Clear();

                for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
                {
                    for (var twT = (ToolWeaponTypes)1; twT < ToolWeaponTypes.End; twT++)
                    {
                        _eMG.PlayerInfoE(playerT).LevelE(levT).ToolWeapons(twT) = default;
                    }

                    for (var buildT = (BuildingTypes)1; buildT < BuildingTypes.End; buildT++)
                    {
                        _eMG.PlayerInfoE(playerT).LevelE(levT).BuildingInfoE(buildT).IdxC.Clear();
                    }
                }

                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    _eMG.PlayerInfoE(playerT).ResourcesC(resT).Resources = default;
                }
            }
        }
    }
}