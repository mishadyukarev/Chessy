using Chessy.Model.Component;
using Chessy.Model.Entity;
using Chessy.Model.Values;

namespace Chessy.Model
{
    public abstract class SystemAbstract
    {
        protected readonly EntitiesModel _e;


        #region Cells

        protected readonly CellC[] _cellCs = new CellC[IndexCellsValues.CELLS];
        protected readonly PositionCellC[] _possitionCellCs = new PositionCellC[IndexCellsValues.CELLS];
        protected readonly XyCellC[] _xyCellsCs = new XyCellC[IndexCellsValues.CELLS];
        protected readonly IsStartedCellC[] _isStartedCellCs = new IsStartedCellC[IndexCellsValues.CELLS];
        protected readonly CellsByDirectAroundC[] _cellsByDirectAroundC = new CellsByDirectAroundC[IndexCellsValues.CELLS];
        protected readonly IdxsAroundCellC[] _idxsAroundCellCs = new IdxsAroundCellC[IndexCellsValues.CELLS];

        protected readonly CellAroundC[,] _cellAroundCs = new CellAroundC[IndexCellsValues.CELLS, IndexCellsValues.CELLS];
        


        #region Unit

        protected readonly UnitE[] _unitEs = new UnitE[IndexCellsValues.CELLS];

        protected readonly UnitOnCellC[] _unitCs = new UnitOnCellC[IndexCellsValues.CELLS];
        protected readonly HealthC[] _hpUnitCs = new HealthC[IndexCellsValues.CELLS];
        protected readonly WaterAmountC[] _unitWaterCs = new WaterAmountC[IndexCellsValues.CELLS];
        protected readonly EffectsUnitC[] _effectsUnitCs = new EffectsUnitC[IndexCellsValues.CELLS];
        protected readonly ShiftingObjectC[] _shiftingUnitCs = new ShiftingObjectC[IndexCellsValues.CELLS];
        protected readonly MainToolWeaponUnitC[] _mainTWC = new MainToolWeaponUnitC[IndexCellsValues.CELLS];
        protected readonly ExtraToolWeaponUnitC[] _extraTWC = new ExtraToolWeaponUnitC[IndexCellsValues.CELLS];
        protected readonly HowManyDistanceNeedForShiftingUnitC[] _howManyDistanceNeedForShiftingUnitCs = new HowManyDistanceNeedForShiftingUnitC[IndexCellsValues.CELLS];
        protected readonly WhereUnitCanAttackToEnemyC[] _whereSimpleAttackCs = new WhereUnitCanAttackToEnemyC[IndexCellsValues.CELLS];
        protected readonly WhereUnitCanAttackToEnemyC[] _whereUniqueAttackCs = new WhereUnitCanAttackToEnemyC[IndexCellsValues.CELLS];
        protected readonly bool[][] _whereSimple = new bool[IndexCellsValues.CELLS][];
        protected readonly bool[][] _whereUnque = new bool[IndexCellsValues.CELLS][];
        protected readonly VisibleToOtherPlayerOrNotC[] _unitVisibleCs = new VisibleToOtherPlayerOrNotC[IndexCellsValues.CELLS];
        protected readonly WhereViewIdxCellC[] _unitWhereViewDataCs = new WhereViewIdxCellC[IndexCellsValues.CELLS];
        protected readonly HasUnitKingEffectHereC[] _hasUnitKingEffectHereCs = new HasUnitKingEffectHereC[IndexCellsValues.CELLS];
        protected readonly CooldownAbilitiesInSecondsC[] _cooldownAbilityCs = new CooldownAbilitiesInSecondsC[IndexCellsValues.CELLS];
        protected readonly WhereUnitCanShiftC[] _whereUnitCanShiftCs = new WhereUnitCanShiftC[IndexCellsValues.CELLS];
        protected readonly ButtonsAbilitiesUnitC[] _buttonsAbilitiesUnitCs = new ButtonsAbilitiesUnitC[IndexCellsValues.CELLS];
        protected readonly EffectsUnitsRightBarsC[] _effectsUnitsRightBarsCs = new EffectsUnitsRightBarsC[IndexCellsValues.CELLS];
        protected readonly NeedUpdateViewC[] _updateViewUnitCs = new NeedUpdateViewC[IndexCellsValues.CELLS];
        protected readonly ExtractionResourcesWithUnitC[] _extractionResourcesWithUnitCs = new ExtractionResourcesWithUnitC[IndexCellsValues.CELLS];
        protected readonly WhereUnitCanFireAdultForestC[] _whereUnitCanFireAdultForestCs = new WhereUnitCanFireAdultForestC[IndexCellsValues.CELLS];

        #endregion


        protected readonly BuildingOnCellC[] _buildingCs = new BuildingOnCellC[IndexCellsValues.CELLS];
        protected readonly BuildingExtractionOnCellC[] _extractionBuildingCs = new BuildingExtractionOnCellC[IndexCellsValues.CELLS];
        protected readonly VisibleToOtherPlayerOrNotC[] _visibleBuildingCs = new VisibleToOtherPlayerOrNotC[IndexCellsValues.CELLS];

        protected readonly EnvironmentC[] _environmentCs = new EnvironmentC[IndexCellsValues.CELLS];

        protected readonly CloudOnCellE[] _cloudOnCellEs = new CloudOnCellE[IndexCellsValues.CELLS];
        protected readonly CloudC[] _cloudCs = new CloudC[IndexCellsValues.CELLS];
        protected readonly WhereViewIdxCellC[] _cloudWhereViewDataCs = new WhereViewIdxCellC[IndexCellsValues.CELLS];
        protected readonly ShiftingObjectC[] _shiftCloudCs = new ShiftingObjectC[IndexCellsValues.CELLS];

        protected readonly RiverC[] _riverCs = new RiverC[IndexCellsValues.CELLS];
        protected readonly HaveRiverAroundCellC[] _haveRiverAroundCellCs = new HaveRiverAroundCellC[IndexCellsValues.CELLS];

        protected readonly FireC[] _fireCs = new FireC[IndexCellsValues.CELLS];

        protected readonly HealthTrailC[] _hpTrailCs = new HealthTrailC[IndexCellsValues.CELLS];
        protected readonly VisibleToOtherPlayerOrNotC[] _visibleTrailCs = new VisibleToOtherPlayerOrNotC[IndexCellsValues.CELLS];

        #endregion


        #region Else

        protected readonly CommonInfoAboutGameC _aboutGameC;
        protected readonly InputC _inputC;
        protected readonly BookC _bookC;
        protected readonly SelectedBuildingsInTownC _selectedBuildingsInTownC;
        protected readonly SelectedToolWeaponC _selectedToolWeaponC;
        protected readonly SettingsC _settingsC;
        protected readonly ShopC _shopC;
        protected readonly CellsC _cellsC;
        protected readonly RpcPoolC _rpcC;
        protected readonly DataFromViewC _dataFromViewC;
        protected readonly MistakeC _mistakeC;
        protected readonly UpdateAllViewC _updateAllViewC;
        protected readonly ZonesInfoC _zonesInfoC;
        protected readonly SelectedUnitC _selectedUnitC;
        protected readonly FromResourcesC _fromResourcesC;

        protected readonly WeatherE _weatherE;
        protected readonly SunC _sunC;
        protected readonly WindC _windC;

        protected readonly PlayerInfoE[] _playerInfoEs = new PlayerInfoE[(byte)PlayerTypes.End];
        protected readonly PlayerInfoC[] _playerInfoCs = new PlayerInfoC[(byte)PlayerTypes.End];
        protected readonly GodInfoC[] _godInfoCs = new GodInfoC[(byte)PlayerTypes.End];
        protected readonly PawnPeopleInfoC[] _pawnPeopleInfoCs = new PawnPeopleInfoC[(byte)PlayerTypes.End];
        protected readonly BuildingsInTownInfoC[] _buildingsInTownInfoCs = new BuildingsInTownInfoC[(byte)PlayerTypes.End];
        protected readonly ResourcesInInventoryC[] _resourcesInInventoryCs = new ResourcesInInventoryC[(byte)PlayerTypes.End];
        protected readonly HowManyToolWeaponsInInventoryC[] _howManyToolWeaponsInInventoryCs = new HowManyToolWeaponsInInventoryC[(byte)PlayerTypes.End];

        #endregion


        public PlayerInfoE PlayerInfoE(in PlayerTypes player) => _playerInfoEs[(byte)player];
        public PlayerInfoC PlayerInfoC(in PlayerTypes playerT) => _playerInfoCs[(byte)playerT];
        public GodInfoC GodInfoC(in PlayerTypes playerT) => _godInfoCs[(byte)playerT];
        public PawnPeopleInfoC PawnPeopleInfoC(in PlayerTypes playerT) => _pawnPeopleInfoCs[(byte)playerT];
        public BuildingsInTownInfoC BuildingsInTownInfoC(in PlayerTypes playerT) => _buildingsInTownInfoCs[(byte)playerT];
        public ResourcesInInventoryC ResourcesInInventoryC(in PlayerTypes playerT) => _resourcesInInventoryCs[(byte)playerT];
        public HowManyToolWeaponsInInventoryC ToolWeaponsInInventoryC(in PlayerTypes playerT) => _howManyToolWeaponsInInventoryCs[(byte)playerT];


        protected SystemAbstract(in EntitiesModel eM)
        {
            _e = eM;

            for (byte cellIdx_0 = 0; cellIdx_0 < IndexCellsValues.CELLS; cellIdx_0++)
            {
                var cellEs = _e.CellEs(cellIdx_0);

                for (byte cellIdx_1 = 0; cellIdx_1 < IndexCellsValues.CELLS; cellIdx_1++)
                {
                    _cellAroundCs[cellIdx_0, cellIdx_1] = cellEs.AroundCellE(cellIdx_1).CellAroundC;
                }

                var cellE = cellEs.CellE;
                var unitE = cellEs.UnitE;
                var cloudE = cellEs.CloudE;
                var buildingE = cellEs.BuildingE;
                var riverE = cellEs.RiverE;
                var effectE = cellEs.EffectE;
                var trailE = cellEs.TrailE;
                var environmentE = cellEs.EnvironmentE;


                _cellCs[cellIdx_0] = cellE.CellC;
                _possitionCellCs[cellIdx_0] = cellE.PositionC;
                _xyCellsCs[cellIdx_0] = cellE.XyCellC;
                _isStartedCellCs[cellIdx_0] = cellE.IsStartedCellC;
                _cellsByDirectAroundC[cellIdx_0] = cellE.CellsByDirectAroundC;
                _idxsAroundCellCs[cellIdx_0] = cellE.IdxsAroundCellC;

                _unitEs[cellIdx_0] = unitE;
                _unitCs[cellIdx_0] = unitE.MainC;
                _hpUnitCs[cellIdx_0] = unitE.HealthC;
                _unitWaterCs[cellIdx_0] = unitE.WaterC;
                _effectsUnitCs[cellIdx_0] = unitE.EffectsC;
                _shiftingUnitCs[cellIdx_0] = unitE.ShiftingInfoForUnitC;
                _mainTWC[cellIdx_0] = unitE.MainToolWeaponC;
                _extraTWC[cellIdx_0] = unitE.ExtraToolWeaponC;
                _howManyDistanceNeedForShiftingUnitCs[cellIdx_0] = unitE.HowManyDistanceNeedForShiftingUnitC;
                _whereSimpleAttackCs[cellIdx_0] = unitE.WhereCanAttackSimpleAttackToEnemyC;
                _whereUniqueAttackCs[cellIdx_0] = unitE.WhereCanAttackUniqueAttackToEnemyC;
                _whereSimple[cellIdx_0] = _whereSimpleAttackCs[cellIdx_0].WhereUnitCanAttack;
                _whereUnque[cellIdx_0] = _whereUniqueAttackCs[cellIdx_0].WhereUnitCanAttack;
                _unitVisibleCs[cellIdx_0] = unitE.VisibleToOtherPlayerOrNotC;
                _unitWhereViewDataCs[cellIdx_0] = unitE.WhereViewDataUnitC;
                _hasUnitKingEffectHereCs[cellIdx_0] = unitE.HasKingEffectHereC;
                _cooldownAbilityCs[cellIdx_0] = unitE.CooldownsC;
                _whereUnitCanShiftCs[cellIdx_0] = unitE.WhereCanShiftC;
                _buttonsAbilitiesUnitCs[cellIdx_0] = unitE.UniqueButtonsC;
                _effectsUnitsRightBarsCs[cellIdx_0] = unitE.EffectsUnitsRightBarsC;
                _updateViewUnitCs[cellIdx_0] = unitE.NeedUpdateViewC;
                _extractionResourcesWithUnitCs[cellIdx_0] = unitE.ExtractionResourcesC;
                _whereUnitCanFireAdultForestCs[cellIdx_0] = unitE.WhereUnitCanFireAdultForestC;

                _buildingCs[cellIdx_0] = buildingE.BuildingMainC;
                _extractionBuildingCs[cellIdx_0] = buildingE.ExtractionC;
                _visibleBuildingCs[cellIdx_0] = buildingE.VisibleToOtherPlayerC;

                _environmentCs[cellIdx_0] = environmentE.EnvironmentC;

                _cloudOnCellEs[cellIdx_0] = cloudE;
                _cloudCs[cellIdx_0] = cloudE.CloudC;
                _cloudWhereViewDataCs[cellIdx_0] = cloudE.WhereSkinAndWhereDataInfoC;
                _shiftCloudCs[cellIdx_0] = cloudE.ShiftingC;

                _riverCs[cellIdx_0] = riverE.RiverC;
                _haveRiverAroundCellCs[cellIdx_0] = riverE.HaveRiverC;

                _fireCs[cellIdx_0] = effectE.FireC;

                _hpTrailCs[cellIdx_0] = trailE.HealthC;
                _visibleTrailCs[cellIdx_0] = trailE.VisibleC;
            }

            var commonInfoE = _e.CommonGameE;

            _aboutGameC = commonInfoE.CommonInfoAboutGameC;
            _inputC = commonInfoE.InputC;
            _bookC = commonInfoE.BookC;
            _selectedBuildingsInTownC = commonInfoE.SelectedBuildingsC;
            _selectedToolWeaponC = commonInfoE.SelectedToolWeaponC;
            _settingsC = commonInfoE.SettingsC;
            _shopC = commonInfoE.ShopC;
            _cellsC = commonInfoE.CellsC;
            _rpcC = commonInfoE.RpcC;
            _dataFromViewC = commonInfoE.DataFromViewC;
            _mistakeC = commonInfoE.MistakeC;
            _updateAllViewC = commonInfoE.UpdateAllViewC;
            _zonesInfoC = commonInfoE.ZoneInfoC;
            _selectedUnitC = commonInfoE.SelectedUnitC;
            _fromResourcesC = commonInfoE.Resources;

            _weatherE = _e.WeatherE;
            _sunC = _weatherE.SunC;
            _windC = _weatherE.WindC;

            for (var playerT = (PlayerTypes)0; playerT < PlayerTypes.End; playerT++)
            {
                var playerTByte = (byte)playerT;

                var playerInfoE = eM.PlayerInfoE(playerTByte);

                _playerInfoCs[playerTByte] = playerInfoE.PlayerInfoC;
                _playerInfoEs[playerTByte] = playerInfoE;
                _godInfoCs[playerTByte] = playerInfoE.GodInfoC;
                _pawnPeopleInfoCs[playerTByte] = playerInfoE.PawnInfoC;
                _buildingsInTownInfoCs[playerTByte] = playerInfoE.BuildingsInTownInfoC;
                _resourcesInInventoryCs[playerTByte] = playerInfoE.ResourcesInInventoryC;
                _howManyToolWeaponsInInventoryCs[playerTByte] = playerInfoE.HowManyToolWeaponsInInventoryC;
            }
        }
    }
}