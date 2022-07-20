using Chessy.Model.Cell.Unit;
using Chessy.Model.Component;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Model.Entity
{
    public sealed class EntitiesModel
    {
        readonly ResourcesC[] _mistakeEconomyEs = new ResourcesC[(byte)ResourceTypes.End];
        readonly PlayerInfoE[] _forPlayerEs = new PlayerInfoE[(byte)PlayerTypes.End];
        readonly CellEs[] _cellEs;
        readonly byte[][] _cellsAround = new byte[IndexCellsValues.CELLS][];
        readonly byte[,] _cellsByDirect = new byte[IndexCellsValues.CELLS, (byte)DirectTypes.End];
        readonly byte[,] _idxs = new byte[IndexCellsValues.X_AMOUNT, IndexCellsValues.Y_AMOUNT];


        public readonly CommonGameE CommonGameE;
        public readonly WeatherE WeatherE = new();


        public DataFromViewC DataFromViewC => CommonGameE.DataFromViewC;
        public FromResourcesC Resources => CommonGameE.Resources;
        public UpdateAllViewC UpdateAllViewC => CommonGameE.UpdateAllViewC;
        public bool NeedUpdateView
        {
            get => UpdateAllViewC.NeedUpdateView;
            internal set => UpdateAllViewC.NeedUpdateView = value;
        }
        public CommonInfoAboutGameC AboutGameC => CommonGameE.CommonInfoAboutGameC;
        public LessonTypes LessonT
        {
            get => AboutGameC.LessonT;
            internal set => AboutGameC.LessonT = value;
        }

        public MistakeC MistakeC => CommonGameE.MistakeC;
        public ZonesInfoC ZoneInfoC => CommonGameE.ZoneInfoC;
        public CellsC CellsC => CommonGameE.CellsC;
        public byte SelectedCellIdx
        {
            get => CellsC.Selected;
            internal set => CellsC.Selected = value;
        }
        public byte CurrentCellIdx
        {
            get => CellsC.Current;
            internal set => CellsC.Current = value;
        }

        public SelectedUnitC SelectedUnitC => CommonGameE.SelectedUnitC;
        internal RpcPoolC RpcC => CommonGameE.RpcC;






        public RaycastTypes RaycastT { get; internal set; }
        public CellClickTypes CellClickT { get; internal set; }


        public bool IsSelectedCity { get; internal set; }
        public bool HaveTreeUnit { get; internal set; }
        public bool IsActivatedIdxAndXyInfoCells { get; internal set; }
        public int AmountPlantedYoungForests { get; internal set; }


        public MistakeTypes MistakeT
        {
            get => MistakeC.MistakeT;
            internal set => MistakeC.MistakeT = value;
        }
        public float MistakeTimer
        {
            get => MistakeC.Timer;
            internal set => MistakeC.Timer = value;
        }


        public ref PlayerInfoE PlayerInfoE(in PlayerTypes player) => ref _forPlayerEs[(byte)player];
        public ResourcesInInventoryC ResourcesInInventoryC(in PlayerTypes playerT) => PlayerInfoE(playerT).ResourcesInInventoryC;
        public float ResourcesInInventory(in PlayerTypes playerT, in ResourceTypes resT) => ResourcesInInventoryC(playerT).Resources(resT);

        public PawnPeopleInfoC PawnPeopleInfoC(in PlayerTypes playerT) => PlayerInfoE(playerT).PawnInfoC;

        public GodInfoC GodInfoC(in PlayerTypes playerT) => PlayerInfoE(playerT).GodInfoC;

        public PlayerInfoC PlayerInfoC(in PlayerTypes playerT) => PlayerInfoE(playerT).PlayerInfoC;

        public BuildingsInTownInfoC BuildingsInTownInfoC(in PlayerTypes playerT) => PlayerInfoE(playerT).BuildingsInTownInfoC;

        public HowManyToolWeaponsInInventoryC HowManyToolWeaponsInInventoryC(in PlayerTypes playerT) => PlayerInfoE(playerT).HowManyToolWeaponsInInventoryC;
        public int ToolWeaponsInInventor(in PlayerTypes playerT, in LevelTypes levT, in ToolsWeaponsWarriorTypes twT) => HowManyToolWeaponsInInventoryC(playerT).ToolWeapons(twT, levT);
        public Action SoundAction(in ClipTypes clipT) => DataFromViewC.SoundAction(clipT);
        public Action SoundAction(in AbilityTypes abilityT) => DataFromViewC.SoundAction(abilityT);
        public ref ResourcesC MistakeEconomy(in ResourceTypes resT) => ref _mistakeEconomyEs[(byte)resT - 1];


        #region Cells

        public CellEs CellEs(in byte idx) => _cellEs[idx];

        public CellE CellE(in byte cell) => CellEs(cell).CellE;
        public PositionC CellPossitionC(in byte cellIdx) => CellE(cellIdx).PositionC;
        public IsStartedCellC IsStartedCellC(in byte cell) => CellE(cell).IsStartedCellC;
        public XyCellC XyCellC(in byte cellIdx) => CellE(cellIdx).XyCellC;
        public byte XCell(in byte cellIdx) => XyCellC(cellIdx).X;
        public byte YCell(in byte cellIdx) => XyCellC(cellIdx).Y;


        #region Around

        public CellAroundE AroundCellE(in byte cell, in byte cellIdx) => CellEs(cell).AroundCellE(cellIdx);

        public CellAroundC CellAroundC(in byte cellIdx, in byte nextCellIdx) => AroundCellE(cellIdx, nextCellIdx).CellAroundC;
        public DirectTypes DirectionAround(in byte cellIdx, in byte nextCellIdx) => CellAroundC(cellIdx, nextCellIdx).DirectT;

        public byte[] IdxsCellsAround(in byte startCellIdx) => (byte[])_cellsAround[startCellIdx].Clone();
        public byte GetIdxCellByDirectAround(in byte startCellIdx, in DirectTypes dirT) => _cellsByDirect[startCellIdx, (byte)dirT];

        #endregion


        public CloudOnCellE CloudOnCellE(in byte cellIdx) => CellEs(cellIdx).CloudE;
        public WhereViewIdxCellC CloudWhereViewDataOnCellC(in byte cellIdx) => CloudOnCellE(cellIdx).WhereSkinAndWhereDataInfoC;
        public CloudC CloudC(in byte cell) => CloudOnCellE(cell).CloudC;
        public bool IsCenterCloud(in byte cellIdx) => CloudC(cellIdx).IsCenter;
        public ShiftingObjectC CloudShiftingC(in byte cellIdx) => CloudOnCellE(cellIdx).ShiftingC;
        public PositionC CloudPossitionC(in byte cellIdx) => CloudOnCellE(cellIdx).PositionC;


        #region Unit

        public UnitE UnitE(in byte idx) => CellEs(idx).UnitE;

        public UnitOnCellC UnitMainC(in byte idx) => UnitE(idx).MainC;
        public UnitTypes UnitT(in byte idx) => UnitMainC(idx).UnitT;
        internal void SetUnitOnCellT(in byte idx, in UnitTypes unitT) => UnitMainC(idx).UnitT = unitT;
        public PlayerTypes UnitPlayerT(in byte idx) => UnitMainC(idx).PlayerType;
        internal void SetUnitPlayerT(in byte cellIdx, in PlayerTypes playerT) => UnitMainC(cellIdx).PlayerT = playerT;
        public LevelTypes UnitLevelT(in byte idx) => UnitMainC(idx).LevelT;
        public void SetUnitLevelT(in byte idx, in LevelTypes levelT) => UnitMainC(idx).LevelT = levelT;
        public ConditionUnitTypes UnitConditionT(in byte idx) => UnitMainC(idx).ConditionType;
        internal void SetUnitConditionT(in byte cellIdx, in ConditionUnitTypes conditionUnitT) => UnitMainC(cellIdx).ConditionT = conditionUnitT;
        public bool IsRightArcherUnit(in byte idx) => UnitMainC(idx).IsArcherDirectedToRight;
        public double DamageSimpleAttack(in byte cell) => UnitMainC(cell).DamageSimpleAttack;
        public double DamageOnCell(in byte cell) => UnitMainC(cell).DamageOnCell;

        public ShiftingObjectC ShiftingInfoForUnitC(in byte cellIdx) => UnitE(cellIdx).ShiftingInfoForUnitC;

        public WhereViewIdxCellC WhereViewDataUnitC(in byte cellIdx) => UnitE(cellIdx).WhereViewDataUnitC;

        public VisibleToOtherPlayerOrNotC UnitVisibleC(in byte cell) => UnitE(cell).VisibleToOtherPlayerOrNotC;
        public CanSetUnitHereC CanSetUnitHereC(in byte cell) => UnitE(cell).CanSetUnitHereC;
        public WhereUnitCanFireAdultForestC WhereUnitCanFireAdultForestC(in byte cell) => UnitE(cell).WhereUnitCanFireAdultForestC;
        public NeedUpdateViewC UnitNeedUpdateViewC(in byte cell) => UnitE(cell).NeedUpdateViewC;
        public EffectsUnitsRightBarsC EffectsUnitsRightBarsC(in byte cellIdx) => UnitE(cellIdx).EffectsUnitsRightBarsC;

        public HealthC HpUnitC(in byte idx) => UnitE(idx).HealthC;
        public double HpUnit(in byte cell) => HpUnitC(cell).Health;
        public WaterAmountC WaterUnitC(in byte idx) => UnitE(idx).WaterC;
        public double WaterUnit(in byte idx) => WaterUnitC(idx).Water;

        public MainToolWeaponUnitC MainToolWeaponC(in byte idx) => UnitE(idx).MainToolWeaponC;
        public ToolsWeaponsWarriorTypes MainToolWeaponT(in byte cell) => MainToolWeaponC(cell).ToolWeaponT;
        public LevelTypes MainTWLevelT(in byte idx) => MainToolWeaponC(idx).LevelT;

        public ExtraToolWeaponUnitC UnitExtraTWC(in byte idx_cell) => UnitE(idx_cell).ExtraToolWeaponC;
        public ToolsWeaponsWarriorTypes ExtraToolWeaponT(in byte idx) => UnitExtraTWC(idx).ToolWeaponT;
        internal void SetExtraToolWeaponT(in byte idx, in ToolsWeaponsWarriorTypes toolWeaponT) => UnitExtraTWC(idx).ToolWeaponT = toolWeaponT;
        public LevelTypes ExtraTWLevelT(in byte idx) => UnitExtraTWC(idx).LevelT;
        public LevelTypes SetExtraTWLevelT(in byte idx, in LevelTypes levelT) => UnitExtraTWC(idx).LevelT = levelT;
        public void SetExtraTWProtection(in byte idx, in float protection) => UnitExtraTWC(idx).ProtectionShield = protection;

        public ExtractionResourcesWithUnitC ExtactionResourcesWithWarriorC(in byte idx_cell) => UnitE(idx_cell).ExtractionResourcesC;

        WhoLastDiedOnCellC LastDiedE(in byte idx) => UnitE(idx).WhoLastDiedHereC;
        public UnitTypes LastDiedUnitT(in byte idx) => LastDiedE(idx).UnitT;
        internal void SetLastDiedUnitT(in byte idx, in UnitTypes unitT) => LastDiedE(idx).UnitT = unitT;
        public LevelTypes LastDiedLevelT(in byte idx) => LastDiedE(idx).LevelT;
        public LevelTypes SetLastDiedLevelT(in byte idx, in LevelTypes levelT) => LastDiedE(idx).LevelT = levelT;
        public PlayerTypes LastDiedPlayerT(in byte idx) => LastDiedE(idx).PlayerT;
        internal void SetLastDiedPlayerT(in byte cellIdx, in PlayerTypes playerT) => LastDiedE(cellIdx).PlayerT = playerT;

        public WhereUnitCanAttackToEnemyC WhereUnitCanAttackSimpleAttackToEnemyC(in byte cellIdx) => UnitE(cellIdx).WhereCanAttackSimpleAttackToEnemyC;
        public WhereUnitCanAttackToEnemyC WhereUnitCanAttackUniqueAttackToEnemyC(in byte cellIdx) => UnitE(cellIdx).WhereCanAttackUniqueAttackToEnemyC;
        public ButtonsAbilitiesUnitC UnitButtonAbilitiesC(in byte cell) => UnitE(cell).UniqueButtonsC;
        public HowManyDistanceNeedForShiftingUnitC HowManyDistanceNeedForShiftingUnitC(in byte cell) => UnitE(cell).HowManyDistanceNeedForShiftingUnitC;
        public WhereUnitCanShiftC WhereUnitCanShiftC(in byte cellIdx) => UnitE(cellIdx).WhereCanShiftC;
        public CooldownAbilitiesInSecondsC UnitCooldownAbilitiesC(in byte cell) => UnitE(cell).CooldownsC;
        public HasUnitKingEffectHereC HasKingEffectHereC(in byte cellIdx) => UnitE(cellIdx).HasKingEffectHereC;


        #region Effects

        public EffectsUnitC UnitEffectsC(in byte idx_cell) => UnitE(idx_cell).EffectsC;
        public float StunUnit(in byte idx) => UnitEffectsC(idx).StunHowManyUpdatesNeedStay;
        public float ProtectionRainyMagicShield(in byte idx) => UnitEffectsC(idx).ProtectionRainyMagicShield;
        public bool HaveFrozenArrawArcher(in byte cell) => UnitEffectsC(cell).HaveFrozenArrawArcher;

        #endregion

        #endregion


        #region Building

        public BuildingE BuildingE(in byte idx) => CellEs(idx).BuildingE;
        public ref BuildingOnCellC BuildingC(in byte cellIdx) => ref BuildingE(cellIdx).BuildingMainC;
        public BuildingTypes BuildingOnCellT(in byte idx) => BuildingC(idx).BuildingT;
        internal void SetBuildingOnCellT(in byte cellIdx, in BuildingTypes buildingT) => BuildingC(cellIdx).BuildingT = buildingT;
        public bool HaveBuildingOnCell(in byte idxCell) => BuildingOnCellT(idxCell).HaveBuilding();
        public bool IsBuildingOnCell(in byte cellIdx, params BuildingTypes[] buildingTs) => BuildingOnCellT(cellIdx).Is(buildingTs);
        public LevelTypes BuildingLevelT(in byte idx) => BuildingC(idx).LevelT;
        public void SetBuildingLevelT(in byte idx, in LevelTypes levelT) => BuildingC(idx).LevelT = levelT;
        public PlayerTypes BuildingPlayerT(in byte idx) => BuildingC(idx).PlayerT;
        internal void SetBuildingPlayerT(in byte cellIdx, in PlayerTypes playerT) => BuildingC(cellIdx).PlayerT = playerT;
        public ref HealthC BuildingHpC(in byte idx) => ref BuildingE(idx).HealthC;
        public VisibleToOtherPlayerOrNotC BuildingVisibleC(in byte cell) => BuildingE(cell).VisibleToOtherPlayerC;
        public ref BuildingExtractionOnCellC BuildingExtractionC(in byte cellIdx) => ref BuildingE(cellIdx).ExtractionC;
        public float WoodcutterExtract(in byte idx) => BuildingExtractionC(idx).HowManyWoodcutterCanExtractWood;
        public float FarmExtract(in byte idx) => BuildingExtractionC(idx).HowManyFarmCanExtractFood;

        #endregion


        public EnvironmentE EnvironmentE(in byte idx) => CellEs(idx).EnvironmentE;
        public ref ResourcesC YoungForestC(in byte idx) => ref EnvironmentE(idx).YoungForestC;
        public ref ResourcesC AdultForestC(in byte idx) => ref EnvironmentE(idx).AdultForestC;
        public ref ResourcesC MountainC(in byte idx) => ref EnvironmentE(idx).MountainC;
        public ref ResourcesC HillC(in byte idx) => ref EnvironmentE(idx).HillC;
        public ref ResourcesC WaterOnCellC(in byte idx) => ref EnvironmentE(idx).FertilizeC;

        public RiverE RiverE(in byte idx) => CellEs(idx).RiverE;
        public RiverC RiverC(in byte cellIdx) => RiverE(cellIdx).RiverC;
        public RiverTypes RiverT(in byte cell) => RiverC(cell).RiverT;
        public void SetRiverT(in byte cell, in RiverTypes riverT) => RiverC(cell).RiverT = riverT;
        public HaveRiverAroundCellC HaveRiverC(in byte cell) => RiverE(cell).HaveRiverC;

        public EffectE EffectE(in byte idx) => CellEs(idx).EffectE;
        public ref bool HaveFire(in byte idx) => ref EffectE(idx).HaveFire;

        public TrailE TrailE(in byte cell) => CellEs(cell).TrailE;
        public VisibleToOtherPlayerOrNotC TrailVisibleC(in byte cell) => TrailE(cell).VisibleC;
        public HealthTrailC HealthTrail(in byte cell) => TrailE(cell).HealthC;

        public byte GetIdxCellByXy(params byte[] xy) => _idxs[xy[0], xy[1]];


        #endregion


        public EntitiesModel(in DataFromViewC dataFromViewC, in string nameRpcMethod, in List<object> actions, in TestModeTypes testModeT)
        {
            CommonGameE = new CommonGameE(dataFromViewC, testModeT, DateTime.Now, actions, nameRpcMethod);

            for (var playerT = (PlayerTypes)0; playerT < PlayerTypes.End; playerT++)
            {
                _forPlayerEs[(byte)playerT] = new PlayerInfoE();
            }


            _cellEs = new CellEs[IndexCellsValues.CELLS];
            var xys = new List<byte[]>();

            byte idxCell = 0;
            for (byte x = 0; x < IndexCellsValues.X_AMOUNT; x++)
                for (byte y = 0; y < IndexCellsValues.Y_AMOUNT; y++)
                {
                    _idxs[x, y] = idxCell;
                    xys.Add(new byte[] { x, y });
                    idxCell++;
                }

            for (idxCell = 0; idxCell < IndexCellsValues.CELLS; idxCell++)
            {
                _cellEs[idxCell] = new CellEs(dataFromViewC, dataFromViewC.IdCell(idxCell), idxCell, this, xys[idxCell]);
            }




            for (byte startCellIdx_0 = 0; startCellIdx_0 < IndexCellsValues.CELLS; startCellIdx_0++)
            {
                if (CellE(startCellIdx_0).CellC.IsBorder) continue;

                _cellsAround[startCellIdx_0] = new byte[(byte)DirectTypes.End - 1];

                for (byte currentCellIdx_1 = 0; currentCellIdx_1 < IndexCellsValues.CELLS; currentCellIdx_1++)
                {
                    if (CellE(currentCellIdx_1).CellC.IsBorder) continue;

                    for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                    {
                        if(CellAroundC(startCellIdx_0, currentCellIdx_1).DirectT == directT)
                        {
                            if (CellAroundC(startCellIdx_0, currentCellIdx_1).LevelFromCellT == DistanceFromCellTypes.First)
                            {
                                _cellsAround[startCellIdx_0][(byte)directT - 1] = currentCellIdx_1;
                                _cellsByDirect[startCellIdx_0, (byte)directT] = currentCellIdx_1;
                            }
                        }
                    }
                }
            }
        }


        internal void SetResourcesInInventory(in PlayerTypes playerT, in ResourceTypes resT, in float resources) => ResourcesInInventoryC(playerT).Set(resT, resources);

        internal void SetToolWeaponsInInventor(in PlayerTypes playerT, in LevelTypes levT, in ToolsWeaponsWarriorTypes twT, in int amountToolWeapons) => HowManyToolWeaponsInInventoryC(playerT).Set(twT, levT, amountToolWeapons);
        internal void AddToolWeaponsInInventor(in PlayerTypes playerT, in LevelTypes levT, in ToolsWeaponsWarriorTypes twT, in int adding = 1) => HowManyToolWeaponsInInventoryC(playerT).Add(twT, levT, adding);
        internal void SubtractToolWeaponsInInventor(in PlayerTypes playerT, in LevelTypes levT, in ToolsWeaponsWarriorTypes twT, in int subtraction = 1) => HowManyToolWeaponsInInventoryC(playerT).Subtract(twT, levT, subtraction);


        internal void Dispose()
        {
            CommonGameE.Dispose();
            
            CellClickT = default;
            IsSelectedCity = default;
            HaveTreeUnit = default;
            MistakeT = default;
            AmountPlantedYoungForests = default;

            WeatherE.Dispose();

            LessonT = default;

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                CellEs(cellIdxCurrent).Dispose();
            }

            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                PlayerInfoE(playerT).Dispose();
            }
        }
    }
}