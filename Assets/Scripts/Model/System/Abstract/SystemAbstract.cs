using Chessy.Model.Component;
using Chessy.Model.Entity;
using Chessy.Model.Values;

namespace Chessy.Model
{
    public abstract class SystemAbstract
    {
        protected readonly EntitiesModel _e;


        #region Cells

        protected readonly CellC[] cellCs = new CellC[IndexCellsValues.CELLS];
        protected readonly PositionCellC[] positionCellCs = new PositionCellC[IndexCellsValues.CELLS];
        protected readonly XyCellC[] xyCellsCs = new XyCellC[IndexCellsValues.CELLS];
        protected readonly IsStartedCellC[] isStartedCellCs = new IsStartedCellC[IndexCellsValues.CELLS];
        protected readonly CellsByDirectAroundC[] cellsByDirectAroundC = new CellsByDirectAroundC[IndexCellsValues.CELLS];
        protected readonly IdxsAroundCellC[] idxsAroundCellCs = new IdxsAroundCellC[IndexCellsValues.CELLS];

        protected readonly CellAroundC[,] cellAroundCs = new CellAroundC[IndexCellsValues.CELLS, IndexCellsValues.CELLS];



        #region Unit

        protected readonly UnitE[] _unitEs = new UnitE[IndexCellsValues.CELLS];

        protected readonly UnitOnCellC[] unitCs = new UnitOnCellC[IndexCellsValues.CELLS];
        protected readonly AttackDamageUnitC[] _unitAttackDamageCs = new AttackDamageUnitC[IndexCellsValues.CELLS];
        protected readonly HealthC[] unitHpCs = new HealthC[IndexCellsValues.CELLS];
        protected readonly WaterAmountC[] _unitWaterCs = new WaterAmountC[IndexCellsValues.CELLS];
        protected readonly EffectsUnitC[] _effectsUnitCs = new EffectsUnitC[IndexCellsValues.CELLS];
        protected readonly ShiftingObjectC[] shiftingUnitCs = new ShiftingObjectC[IndexCellsValues.CELLS];
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


        protected readonly BuildingOnCellC[] buildingCs = new BuildingOnCellC[IndexCellsValues.CELLS];
        protected readonly BuildingExtractionOnCellC[] extractionBuildingCs = new BuildingExtractionOnCellC[IndexCellsValues.CELLS];
        protected readonly VisibleToOtherPlayerOrNotC[] visibleBuildingCs = new VisibleToOtherPlayerOrNotC[IndexCellsValues.CELLS];

        protected readonly EnvironmentC[] environmentCs = new EnvironmentC[IndexCellsValues.CELLS];

        protected readonly CloudC[] cloudCs = new CloudC[IndexCellsValues.CELLS];
        protected readonly WhereViewIdxCellC[] cloudWhereViewDataCs = new WhereViewIdxCellC[IndexCellsValues.CELLS];
        protected readonly ShiftingObjectC[] shiftCloudCs = new ShiftingObjectC[IndexCellsValues.CELLS];

        protected readonly RiverC[] riverCs = new RiverC[IndexCellsValues.CELLS];
        protected readonly HaveRiverAroundCellC[] haveRiverAroundCellCs = new HaveRiverAroundCellC[IndexCellsValues.CELLS];

        protected readonly FireC[] fireCs = new FireC[IndexCellsValues.CELLS];

        protected readonly TrailsHealthOnCellC[] hpTrailCs = new TrailsHealthOnCellC[IndexCellsValues.CELLS];
        protected readonly VisibleToOtherPlayerOrNotC[] visibleTrailCs = new VisibleToOtherPlayerOrNotC[IndexCellsValues.CELLS];

        #endregion


        #region Else

        // Info if player
        protected readonly PlayerInfoE[] playerInfoEs = new PlayerInfoE[(byte)PlayerTypes.End];
        protected readonly PlayerInfoC[] playerInfoCs = new PlayerInfoC[(byte)PlayerTypes.End];
        protected readonly GodInfoC[] godInfoCs = new GodInfoC[(byte)PlayerTypes.End];
        protected readonly PawnPeopleInfoC[] pawnPeopleInfoCs = new PawnPeopleInfoC[(byte)PlayerTypes.End];
        protected readonly BuildingsInTownInfoC[] buildingsInTownInfoCs = new BuildingsInTownInfoC[(byte)PlayerTypes.End];
        protected readonly ResourcesInInventoryC[] resourcesInInventoryCs = new ResourcesInInventoryC[(byte)PlayerTypes.End];
        protected readonly HowManyToolWeaponsInInventoryC[] howManyToolWeaponsInInventoryCs = new HowManyToolWeaponsInInventoryC[(byte)PlayerTypes.End];

        // Game info
        protected readonly CommonInfoAboutGameC aboutGameC;
        protected readonly InputC inputC;
        protected readonly BookC bookC;
        protected readonly SelectedBuildingsInTownC selectedBuildingsInTownC;
        protected readonly SelectedToolWeaponC selectedToolWeaponC;
        protected readonly SettingsC settingsC;
        protected readonly ShopC shopC;
        protected readonly IndexedCellsC indexesCellsC;
        protected readonly RpcPoolC rpcC;
        protected readonly DataFromViewC dataFromViewC;
        protected readonly MistakeC mistakeC;
        protected readonly UpdateAllViewC updateAllViewC;
        protected readonly ZonesInfoC zonesInfoC;
        protected readonly SelectedUnitC selectedUnitC;
        protected readonly FromResourcesC fromResourcesC;

        // Weather
        protected readonly SunC sunC;
        protected readonly WindC windC;

        #endregion


        #region

        protected CellC CellC(in byte cellIdx) => cellCs[cellIdx];
        //protected PositionCellC PositionCellC(in byte cellIdx) => _positionCellCs[cellIdx];
        protected XyCellC XyCellC(in byte cellIdx) => xyCellsCs[cellIdx];
        //protected IsStartedCellC IsStartedCellC(in byte cellIdx) => isStartedCellCs[cellIdx];
        protected IdxsAroundCellC IdxsAroundCellC(in byte cellIdx) => idxsAroundCellCs[cellIdx];
        protected CellsByDirectAroundC CellsByDirectAroundC(in byte cellIdx) => cellsByDirectAroundC[cellIdx];
        //public CellAroundC CellAroundC(in byte cellIdx) => _cellAroundCs[cellIdx];

        protected UnitOnCellC UnitC(in byte cellIdx) => unitCs[cellIdx];
        protected WhereViewIdxCellC UnitViewDataC(in byte cellIdx) => _unitWhereViewDataCs[cellIdx];
        protected MainToolWeaponUnitC UnitMainTWC(in byte cellIdx) => _mainTWC[cellIdx];
        protected ExtraToolWeaponUnitC UnitExtraTWC(in byte cellIdx) => _extraTWC[cellIdx];
        protected AttackDamageUnitC UnitAttackC(in byte cellIdx) => _unitAttackDamageCs[cellIdx];
        protected ButtonsAbilitiesUnitC UnitButtonsC(in byte cellIdx) => _buttonsAbilitiesUnitCs[cellIdx];
        protected ShiftingObjectC UnitShiftC(in byte cellIdx) => shiftingUnitCs[cellIdx];
        protected WaterAmountC UnitWaterC(in byte cellIdx) => _unitWaterCs[cellIdx];
        //protected EffectsUnitC UnitEffectC(in byte cellIdx) => _effectsUnitCs[cellIdx];
        //protected CooldownAbilitiesInSecondsC UnitCooldownC(in byte cellIdx) => _cooldownAbilityCs[cellIdx];
        protected WhereUnitCanShiftC WhereUnitCanShiftC(in byte cellIdx) => _whereUnitCanShiftCs[cellIdx];


        protected BuildingOnCellC BuildingC(in byte cellIdx) => buildingCs[cellIdx];

        protected EnvironmentC EnvironmentC(in byte cellIdx) => environmentCs[cellIdx];

        protected CloudC CloudC(in byte cellIdx) => cloudCs[cellIdx];
        protected WhereViewIdxCellC CloudViewDataC(in byte cellIdx) => cloudWhereViewDataCs[cellIdx];
        protected ShiftingObjectC CloudShiftC(in byte cellIdx) => shiftCloudCs[cellIdx];


        protected FireC FireC(in byte cellIdx) => fireCs[cellIdx];
        protected RiverC RiverC(in byte cellIdx) => riverCs[cellIdx];

        #endregion


        protected PlayerInfoE PlayerInfoE(in PlayerTypes player) => playerInfoEs[(byte)player];
        protected PlayerInfoC PlayerInfoC(in PlayerTypes playerT) => playerInfoCs[(byte)playerT];
        //protected GodInfoC GodInfoC(in PlayerTypes playerT) => godInfoCs[(byte)playerT];
        protected PawnPeopleInfoC PawnPeopleInfoC(in PlayerTypes playerT) => pawnPeopleInfoCs[(byte)playerT];
        //protected BuildingsInTownInfoC BuildingsInTownInfoC(in PlayerTypes playerT) => _buildingsInTownInfoCs[(byte)playerT];
        protected ResourcesInInventoryC ResourcesInInventoryC(in PlayerTypes playerT) => resourcesInInventoryCs[(byte)playerT];
        protected HowManyToolWeaponsInInventoryC ToolWeaponsInInventoryC(in PlayerTypes playerT) => howManyToolWeaponsInInventoryCs[(byte)playerT];


        protected SystemAbstract(EntitiesModel eM)
        {
            _e = eM;

            for (byte cellIdx_0 = 0; cellIdx_0 < IndexCellsValues.CELLS; cellIdx_0++)
            {
                var cellEs = eM.CellEs(cellIdx_0);

                for (byte cellIdx_1 = 0; cellIdx_1 < IndexCellsValues.CELLS; cellIdx_1++)
                {
                    cellAroundCs[cellIdx_0, cellIdx_1] = cellEs.AroundCellE(cellIdx_1).CellAroundC;
                }

                var cellE = cellEs.CellE;
                var unitE = cellEs.UnitE;
                var cloudE = cellEs.CloudE;
                var buildingE = cellEs.BuildingE;
                var riverE = cellEs.RiverE;
                var effectE = cellEs.EffectE;
                var trailE = cellEs.TrailE;
                var environmentE = cellEs.EnvironmentE;


                cellCs[cellIdx_0] = cellE.CellC;
                positionCellCs[cellIdx_0] = cellE.PositionC;
                xyCellsCs[cellIdx_0] = cellE.XyCellC;
                isStartedCellCs[cellIdx_0] = cellE.IsStartedCellC;
                cellsByDirectAroundC[cellIdx_0] = cellE.CellsByDirectAroundC;
                idxsAroundCellCs[cellIdx_0] = cellE.IdxsAroundCellC;

                _unitEs[cellIdx_0] = unitE;
                unitCs[cellIdx_0] = unitE.MainC;
                _unitAttackDamageCs[cellIdx_0] = unitE.AttackDamageC;
                unitHpCs[cellIdx_0] = unitE.HealthC;
                _unitWaterCs[cellIdx_0] = unitE.WaterC;
                _effectsUnitCs[cellIdx_0] = unitE.EffectsC;
                shiftingUnitCs[cellIdx_0] = unitE.ShiftingInfoForUnitC;
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

                buildingCs[cellIdx_0] = buildingE.BuildingMainC;
                extractionBuildingCs[cellIdx_0] = buildingE.ExtractionC;
                visibleBuildingCs[cellIdx_0] = buildingE.VisibleToOtherPlayerC;

                environmentCs[cellIdx_0] = environmentE.EnvironmentC;

                cloudCs[cellIdx_0] = cloudE.CloudC;
                cloudWhereViewDataCs[cellIdx_0] = cloudE.WhereSkinAndWhereDataInfoC;
                shiftCloudCs[cellIdx_0] = cloudE.ShiftingC;

                riverCs[cellIdx_0] = riverE.RiverC;
                haveRiverAroundCellCs[cellIdx_0] = riverE.HaveRiverC;

                fireCs[cellIdx_0] = effectE.FireC;

                hpTrailCs[cellIdx_0] = trailE.HealthC;
                visibleTrailCs[cellIdx_0] = trailE.VisibleC;
            }

            var commonInfoE = eM.CommonGameE;

            aboutGameC = commonInfoE.CommonInfoAboutGameC;
            inputC = commonInfoE.InputC;
            bookC = commonInfoE.BookC;
            selectedBuildingsInTownC = commonInfoE.SelectedBuildingsC;
            selectedToolWeaponC = commonInfoE.SelectedToolWeaponC;
            settingsC = commonInfoE.SettingsC;
            shopC = commonInfoE.ShopC;
            indexesCellsC = commonInfoE.CellsC;
            rpcC = commonInfoE.RpcC;
            dataFromViewC = commonInfoE.DataFromViewC;
            mistakeC = commonInfoE.MistakeC;
            updateAllViewC = commonInfoE.UpdateAllViewC;
            zonesInfoC = commonInfoE.ZoneInfoC;
            selectedUnitC = commonInfoE.SelectedUnitC;
            fromResourcesC = commonInfoE.Resources;

            var weatherE = eM.WeatherE;
            sunC = weatherE.SunC;
            windC = weatherE.WindC;

            for (var playerT = (PlayerTypes)0; playerT < PlayerTypes.End; playerT++)
            {
                var playerTByte = (byte)playerT;

                var playerInfoE = eM.PlayerInfoE(playerTByte);

                playerInfoCs[playerTByte] = playerInfoE.PlayerInfoC;
                playerInfoEs[playerTByte] = playerInfoE;
                godInfoCs[playerTByte] = playerInfoE.GodInfoC;
                pawnPeopleInfoCs[playerTByte] = playerInfoE.PawnInfoC;
                buildingsInTownInfoCs[playerTByte] = playerInfoE.BuildingsInTownInfoC;
                resourcesInInventoryCs[playerTByte] = playerInfoE.ResourcesInInventoryC;
                howManyToolWeaponsInInventoryCs[playerTByte] = playerInfoE.HowManyToolWeaponsInInventoryC;
            }
        }
    }
}