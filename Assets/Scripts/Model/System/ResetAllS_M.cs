using Chessy.Model.Values;

namespace Chessy.Model.Model.System
{
    public sealed partial class SystemsModel : IUpdate
    {

        public void ResetAll()
        {
            _e.IsStartedGame = default;
            _e.Motions = default;
            _e.ZoneInfoC.IsActiveFriend = default;
            _e.ZoneInfoC = default;
            _e.WhoseMovePlayerT = default;
            _e.CellClickT = default;
            _e.IsSelectedCity = default;
            _e.HaveTreeUnit = default;
            _e.MistakeT = default;
            _e.WinnerPlayerT = default;
            _e.CellsC = default;
            _e.CurPlayerIT = default;
            _e.AmountPlantedYoungForests = default;

            _e.WeatherE.WindC = new WindC(default, default);
            _e.WeatherE.SunSideT = default;
            _e.WeatherE.CellIdxCenterCloud = default;

            _e.SelectedE.ToolWeaponC = new SelectedToolWeaponC(default, default);

            _e.LessonT = default;

            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                ClearAllEnvironment(cellIdxCurrent);

                for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++)
                    _e.HealthTrail(cellIdxCurrent).Health(dirT) = 0;

                _e.UnitEs(cellIdxCurrent).ClearEverything();
                _e.SetBuildingOnCellT(cellIdxCurrent, default);
            }

            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                _e.PlayerInfoE(playerT).IsReadyForStartOnlineGame = default;

                _e.PlayerInfoE(playerT).BuildingsInfoC.Clear();

                _e.PlayerInfoE(playerT).AmountFarmsInGame = default;

                _e.PlayerInfoE(playerT).PawnInfoC.PeopleInCity = default;
                _e.PlayerInfoE(playerT).PawnInfoC.MaxAvailable = default;
                _e.PlayerInfoE(playerT).PawnInfoC.AmountInGame = default;

                _e.PlayerInfoE(playerT).KingInfoE.HaveInInventor = default;
                _e.PlayerInfoE(playerT).WoodForBuyHouse = default;
                _e.PlayerInfoE(playerT).IsReadyForStartOnlineGame = default;

                _e.PlayerInfoE(playerT).GodInfoE = default;
                _e.PlayerInfoE(playerT).GodInfoE.HaveHeroInInventor = default;
                _e.PlayerInfoE(playerT).WhereKingEffects.Clear();

                for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
                {
                    for (var twT = (ToolWeaponTypes)1; twT < ToolWeaponTypes.End; twT++)
                    {
                        _e.PlayerInfoE(playerT).LevelE(levT).ToolWeapons(twT) = default;
                    }

                    for (var buildT = (BuildingTypes)1; buildT < BuildingTypes.End; buildT++)
                    {
                        _e.PlayerInfoE(playerT).LevelE(levT).BuildingInfoE(buildT).IdxC.Clear();
                    }
                }

                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    _e.PlayerInfoE(playerT).ResourcesC(resT).Resources = default;
                }
            }
        }
    }
}