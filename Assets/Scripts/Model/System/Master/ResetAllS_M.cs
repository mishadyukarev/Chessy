using Chessy.Model.Values;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
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
            _e.CurrentPlayerIT = default;
            _e.AmountPlantedYoungForests = default;

            _e.WindC = new WindC();
            _e.SunC.SunSideT = default;
            _e.CloudC.CellIdxCenterCloud = default;

            _e.SelectedE.ToolWeaponC = new SelectedToolWeaponC(default, default);

            _e.LessonT = default;

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                ClearAllEnvironment(cellIdxCurrent);

                for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++)
                    _e.HealthTrail(cellIdxCurrent).Health(dirT) = 0;

                _e.UnitE(cellIdxCurrent).ClearEverything();
                _e.SetBuildingOnCellT(cellIdxCurrent, default);
            }

            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                _e.PlayerInfoC(playerT).IsReadyForStartOnlineGame = default;

                _e.BuildingsInTownInfoC(playerT).Clear();

                _e.PlayerInfoC(playerT).AmountFarmsInGame = default;

                _e.PawnPeopleInfoC(playerT).PeopleInCity = default;
                //_e.PawnPeopleInfoC(playerT).MaxAvailable = default;
                _e.PawnPeopleInfoC(playerT).AmountInGame = default;

                _e.PlayerInfoC(playerT).HaveKingInInventor = default;
                _e.PlayerInfoC(playerT).WoodForBuyHouse = default;
                _e.PlayerInfoC(playerT).IsReadyForStartOnlineGame = default;

                _e.GodInfoC(playerT) = default;
                _e.GodInfoC(playerT).HaveGodInInventor = default;
                //_e.PlayerInfoE(playerT).WhereKingEffects.Clear();

                for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
                {
                    for (var twT = (ToolsWeaponsWarriorTypes)1; twT < ToolsWeaponsWarriorTypes.End; twT++)
                    {
                        _e.SetToolWeaponsInInventor(playerT, levT, twT, default);
                    }
                }

                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    _e.SetResourcesInInventory(playerT, resT, default);
                }
            }
        }
    }
}