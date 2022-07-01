using Chessy.Model.Cell.Unit;
using Chessy.Model.Component;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using System;
using System.Collections.Generic;
namespace Chessy.Model.Entity
{
    public sealed class EntitiesModel
    {
        readonly ResourcesC[] _mistakeEconomyEs = new ResourcesC[(byte)ResourceTypes.End];
        readonly PlayerInfoE[] _forPlayerEs = new PlayerInfoE[(byte)PlayerTypes.End];
        readonly CellEs[] _cellEs;

        public PlayerTypes WinnerPlayerT;
        public PlayerTypes WhoseMovePlayerT;
        public PlayerTypes CurrentPlayerIT;


        public SelectedE SelectedE;


        #region CommonGameE

        public CommonGameE CommonGameE;

        public DataFromViewC DataFromViewC => CommonGameE.DataFromViewC;
        public Resources Resources => CommonGameE.Resources;
        public ref ShopC ShopC => ref CommonGameE.ShopC;
        public ref AdC AdC => ref CommonGameE.AdC;

        public ref UpdateAllViewC UpdateAllViewC => ref CommonGameE.UpdateAllViewC;
        internal ref float ForUpdateViewTimer => ref UpdateAllViewC.ForUpdateViewTimer;
        public bool NeedUpdateView
        {
            get => UpdateAllViewC.NeedUpdateView;
            set => UpdateAllViewC.NeedUpdateView = value;
        }

        public ref SettingsC SettingsC => ref CommonGameE.SettingsC;

        public ref BookC BookC => ref CommonGameE.BookC;
        public PageBookTypes OpenedNowPageBookT
        {
            get => BookC.OpenedNowPageBookT;
            internal set => BookC.OpenedNowPageBookT = value;
        }

        public ref CommonInfoAboutGameC CommonInfoAboutGameC => ref CommonGameE.CommonInfoAboutGameC;
        public TestModeTypes TestModeT => CommonInfoAboutGameC.TestModeT;
        public DateTime StartGameTime => CommonInfoAboutGameC.StartGameTime;
        public LessonTypes LessonT
        {
            get => CommonInfoAboutGameC.LessonT;
            internal set => CommonInfoAboutGameC.LessonT = value;
        }
        public GameModeTypes GameModeT
        {
            get => CommonInfoAboutGameC.GameModeT;
            internal set => CommonInfoAboutGameC.GameModeT = value;
        }
        public SceneTypes SceneT
        {
            get => CommonInfoAboutGameC.SceneT;
            internal set => CommonInfoAboutGameC.SceneT = value;
        }

        public ref MistakeC MistakeC => ref CommonGameE.MistakeC;
        public ref ZonesInfoC ZoneInfoC => ref CommonGameE.ZoneInfoC;
        public ref WhereTeleportC WhereTeleportC => ref CommonGameE.WhereTeleportC;

        public ref MotionC MotionC => ref CommonGameE.MotionC;
        public float MotionTimer
        {
            get => MotionC.Timer;
            internal set => MotionC.Timer = value;
        }
        public int Motions
        {
            get => MotionC.Motions;
            internal set => MotionC.Motions = value;
        }

        public ref CellsC CellsC => ref CommonGameE.CellsC;
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

        public ref SelectedUnitC SelectedUnitC => ref CommonGameE.SelectedUnitC;

        public ref InputC InputC => ref CommonGameE.InputC;
        public bool IsClicked
        {
            get => InputC.IsClicked;
            internal set => InputC.IsClicked = value;
        }

        internal ref RpcPoolC RpcC => ref CommonGameE.RpcC;






        #endregion


        #region WeatherE

        public WeatherE WeatherE;

        public ref WindC WindC => ref WeatherE.WindC;
        public DirectTypes DirectWindT
        {
            get => WindC.DirectT;
            internal set => WindC.DirectT = value;
        }
        public byte SpeedWind
        {
            get => WindC.Speed;
            internal set => WindC.Speed = value;
        }

        public ref SunC SunC => ref WeatherE.SunC;
        public SunSideTypes SunSideT
        {
            get => SunC.SunSideT;
            internal set => SunC.SunSideT = value;
        }

        public ref CloudC CloudC => ref WeatherE.CloudC;
        public byte CenterCloudCellIdx
        {
            get => CloudC.CellIdxCenterCloud;
            internal set => CloudC.CellIdxCenterCloud = value;
        }

        #endregion






        public RaycastTypes RaycastT { get; internal set; }
        public CellClickTypes CellClickT { get; internal set; }
        public bool IsStartedGame { get; internal set; }
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

        public ref PawnPeopleInfoC PawnPeopleInfoC(in PlayerTypes playerT) => ref PlayerInfoE(playerT).PawnInfoC;

        public ref GodInfoC GodInfoC(in PlayerTypes playerT) => ref PlayerInfoE(playerT).GodInfoC;

        public ref PlayerInfoC PlayerInfoC(in PlayerTypes playerT) => ref PlayerInfoE(playerT).PlayerInfoC;

        public BuildingsInTownInfoC BuildingsInTownInfoC(in PlayerTypes playerT) => PlayerInfoE(playerT).BuildingsInTownInfoC;

        public HowManyToolWeaponsInInventoryC HowManyToolWeaponsInInventoryC(in PlayerTypes playerT) => PlayerInfoE(playerT).HowManyToolWeaponsInInventoryC;
        public int ToolWeaponsInInventor(in PlayerTypes playerT, in LevelTypes levT, in ToolWeaponTypes twT) => HowManyToolWeaponsInInventoryC(playerT).ToolWeapons(twT, levT);
        public Action SoundAction(in ClipTypes clipT) => DataFromViewC.SoundAction(clipT);
        public Action SoundAction(in AbilityTypes abilityT) => DataFromViewC.SoundAction(abilityT);
        public ref ResourcesC MistakeEconomy(in ResourceTypes resT) => ref _mistakeEconomyEs[(byte)resT - 1];


        #region Cells

        ref CellEs CellEs(in byte idx) => ref _cellEs[idx];


        public ref CellE CellE(in byte cell) => ref CellEs(cell).CellE;
        public CellC CellC(in byte cellIdx) => CellE(cellIdx).CellC;
        public IsStartedCellC IsStartedCellC(in byte cell) => CellE(cell).IsStartedCellC;
        public IdxCellC IdxCellC(in byte cellIdx) => CellE(cellIdx).IdxCellC;
        public byte IdxCell(in byte cellIdx) => IdxCellC(cellIdx).Idx;
        public XyCellC XyCellC(in byte cellIdx) => CellE(cellIdx).XyCellC;
        public byte XCell(in byte cellIdx) => XyCellC(cellIdx).X;
        public byte YCell(in byte cellIdx) => XyCellC(cellIdx).Y;
        public bool IsBorder(in byte cell) => CellC(cell).IsBorder;
        public int InstanceID(in byte cell) => CellC(cell).InstanceID;


        public AroundCellsE AroundCellsE(in byte cell) => CellEs(cell).AroundCellsEs;


        #region Unit

        public ref UnitE UnitE(in byte idx) => ref CellEs(idx).UnitE;

        public ref UnitOnCellC UnitMainC(in byte idx) => ref UnitE(idx).MainC;
        public UnitTypes UnitT(in byte idx) => UnitMainC(idx).UnitT;
        internal void SetUnitOnCellT(in byte idx, in UnitTypes unitT) => UnitMainC(idx).UnitT = unitT;
        public PlayerTypes UnitPlayerT(in byte idx) => UnitMainC(idx).PlayerT;
        internal void SetUnitPlayerT(in byte cellIdx, in PlayerTypes playerT) => UnitMainC(cellIdx).PlayerT = playerT;
        public LevelTypes UnitLevelT(in byte idx) => UnitMainC(idx).LevelT;
        public void SetUnitLevelT(in byte idx, in LevelTypes levelT) => UnitMainC(idx).LevelT = levelT;
        public ConditionUnitTypes UnitConditionT(in byte idx) => UnitMainC(idx).ConditionT;
        internal void SetUnitConditionT(in byte cellIdx, in ConditionUnitTypes conditionUnitT) => UnitMainC(cellIdx).ConditionT = conditionUnitT;
        public bool IsRightArcherUnit(in byte idx) => UnitMainC(idx).IsArcherDirectedToRight;
        public double DamageAttack(in byte cell) => UnitMainC(cell).DamageSimpleAttack;
        public double DamageOnCell(in byte cell) => UnitMainC(cell).DamageOnCell;

        public VisibleToOtherPlayerOrNotC UnitVisibleC(in byte cell) => UnitE(cell).VisibleToOtherPlayerOrNotC;
        public CanSetUnitHereC CanSetUnitHereC(in byte cell) => UnitE(cell).CanSetUnitHereC;
        public WhereUnitCanFireAdultForestC WhereUnitCanFireAdultForestC(in byte cell) => UnitE(cell).WhereUnitCanFireAdultForestC;
        public ref NeedUpdateViewC UnitNeedUpdateViewC(in byte cell) => ref UnitE(cell).NeedUpdateViewC;
        public EffectsUnitsRightBarsC EffectsUnitsRightBarsC(in byte cellIdx) => UnitE(cellIdx).EffectsUnitsRightBarsC;

        public ref HealthC HpUnitC(in byte idx) => ref UnitE(idx).HealthC;
        public double HpUnit(in byte cell) => HpUnitC(cell).Health;
        public ref EnergyC EnergyUnitC(in byte idx) => ref UnitE(idx).EnergyC;
        public double EnergyUnit(in byte idx) => EnergyUnitC(idx).Energy;
        public ref WaterC WaterUnitC(in byte idx) => ref UnitE(idx).WaterC;
        public double WaterUnit(in byte idx) => WaterUnitC(idx).Water;

        public ref MainToolWeaponUnitC MainToolWeaponE(in byte idx) => ref UnitE(idx).MainToolWeaponC;
        public ToolWeaponTypes MainToolWeaponT(in byte cell) => MainToolWeaponE(cell).ToolWeaponT;
        public void SetMainToolWeaponT(in byte cell, in ToolWeaponTypes toolWeaponT) => MainToolWeaponE(cell).ToolWeaponT = toolWeaponT;
        public LevelTypes MainTWLevelT(in byte idx) => MainToolWeaponE(idx).LevelT;
        internal void SetMainTWLevelT(in byte cellIdx, in LevelTypes levelT) => MainToolWeaponE(cellIdx).LevelT = levelT;

        public ref ExtraToolWeaponUnitC UnitExtraTWE(in byte idx_cell) => ref UnitE(idx_cell).ExtraToolWeaponC;
        public ToolWeaponTypes ExtraToolWeaponT(in byte idx) => UnitExtraTWE(idx).ToolWeaponT;
        internal void SetExtraToolWeaponT(in byte idx, in ToolWeaponTypes toolWeaponT) => UnitExtraTWE(idx).ToolWeaponT = toolWeaponT;
        public LevelTypes ExtraTWLevelT(in byte idx) => UnitExtraTWE(idx).LevelT;
        public LevelTypes SetExtraTWLevelT(in byte idx, in LevelTypes levelT) => UnitExtraTWE(idx).LevelT = levelT;
        public float ExtraTWProtection(in byte idx) => UnitExtraTWE(idx).ProtectionShield;

        public ref ExtractionResourcesWithUnitC ExtactionResourcesWithWarriorC(in byte idx_cell) => ref UnitE(idx_cell).ExtractionResourcesC;
        public float PawnExtractAdultForest(in byte idx) => ExtactionResourcesWithWarriorC(idx).HowManyWarriourCanExtractAdultForest;
        public float PawnExtractHill(in byte idx) => ExtactionResourcesWithWarriorC(idx).HowManyWarriourCanExtractHill;

        ref WhoLastDiedHereC LastDiedE(in byte idx) => ref UnitE(idx).WhoLastDiedHereC;
        public UnitTypes LastDiedUnitT(in byte idx) => LastDiedE(idx).UnitT;
        internal void SetLastDiedUnitT(in byte idx, in UnitTypes unitT) => LastDiedE(idx).UnitT = unitT;
        public LevelTypes LastDiedLevelT(in byte idx) => LastDiedE(idx).LevelT;
        public LevelTypes SetLastDiedLevelT(in byte idx, in LevelTypes levelT) => LastDiedE(idx).LevelT = levelT;
        public PlayerTypes LastDiedPlayerT(in byte idx) => LastDiedE(idx).PlayerT;
        internal void SetLastDiedPlayerT(in byte cellIdx, in PlayerTypes playerT) => LastDiedE(cellIdx).PlayerT = playerT;

        public WhereUnitCanAttackToEnemyC WhereUnitCanAttackSimpleAttackToEnemyC(in byte cellIdx) => UnitE(cellIdx).WhereCanAttackSimpleAttackToEnemyC;
        public WhereUnitCanAttackToEnemyC WhereUnitCanAttackUniqueAttackToEnemyC(in byte cellIdx) => UnitE(cellIdx).WhereCanAttackUniqueAttackToEnemyC;
        public UniqueButtonsC UnitButtonAbilitiesC(in byte cell) => UnitE(cell).UniqueButtonsC;
        public HowManyEnergyNeedForShiftingUnitC HowManyEnergyNeedForShiftingUnitC(in byte cell) => UnitE(cell).HowManyEnergyNeedForShiftingUnitC;
        public WhereUnitCanShiftC WhereUnitCanShiftC(in byte cellIdx) => UnitE(cellIdx).WhereCanShiftC;
        public CooldownAbilitiesC UnitCooldownAbilitiesC(in byte cell) => UnitE(cell).CooldownsC;
        public HasUnitKingEffectHereC HasKingEffectHereC(in byte cellIdx) => UnitE(cellIdx).HasKingEffectHereC;


        #region Effects

        public ref EffectsUnitC UnitEffectsC(in byte idx_cell) => ref UnitE(idx_cell).EffectsC;
        public float StunUnit(in byte idx) => UnitEffectsC(idx).StunHowManyUpdatesNeedStay;
        public float ShieldEffect(in byte idx) => UnitEffectsC(idx).ProtectionRainyMagicShield;
        public int FrozenArrawEffect(in byte cell) => UnitEffectsC(cell).ShootsFrozenArrawArcher;

        #endregion

        #endregion


        #region Building

        public ref BuildingE BuildingE(in byte idx) => ref CellEs(idx).BuildingE;
        public ref BuildingC BuildingC(in byte cellIdx) => ref BuildingE(cellIdx).BuildingMainC;
        public BuildingTypes BuildingOnCellT(in byte idx) => BuildingC(idx).BuildingT;
        internal void SetBuildingOnCellT(in byte cellIdx, in BuildingTypes buildingT) => BuildingC(cellIdx).BuildingT = buildingT;
        public bool HaveBuildingOnCell(in byte idxCell) => BuildingOnCellT(idxCell).HaveBuilding();
        public bool IsBuildingOnCell(in byte cellIdx, params BuildingTypes[] buildingTs) => BuildingOnCellT(cellIdx).Is(buildingTs);
        public LevelTypes BuildingLevelT(in byte idx) => BuildingC(idx).LevelT;
        public void SetBuildingLevelT(in byte idx, in LevelTypes levelT) => BuildingC(idx).LevelT = levelT;
        public PlayerTypes BuildingPlayerT(in byte idx) => BuildingC(idx).PlayerT;
        internal void SetBuildingPlayerT(in byte cellIdx, in PlayerTypes playerT) => BuildingC(cellIdx).PlayerT = playerT;
        public ref HealthC BuildingHpC(in byte idx) => ref BuildingE(idx).HealthC;
        public double BuildingHp(in byte idx) => BuildingHpC(idx).Health;
        public VisibleToOtherPlayerOrNotC BuildingVisibleC(in byte cell) => BuildingE(cell).VisibleToOtherPlayerC;
        public ref BuildingExtractionC BuildingExtractionC(in byte cellIdx) => ref BuildingE(cellIdx).ExtractionC;
        public float WoodcutterExtract(in byte idx) => BuildingExtractionC(idx).HowManyWoodcutterCanExtractWood;
        public float FarmExtract(in byte idx) => BuildingExtractionC(idx).HowManyFarmCanExtractFood;

        #endregion


        public ref EnvironmentE EnvironmentE(in byte idx) => ref CellEs(idx).EnvironmentE;
        public ref ResourcesC YoungForestC(in byte idx) => ref EnvironmentE(idx).YoungForestC;
        public ref ResourcesC AdultForestC(in byte idx) => ref EnvironmentE(idx).AdultForestC;
        public ref ResourcesC MountainC(in byte idx) => ref EnvironmentE(idx).MountainC;
        public ref ResourcesC HillC(in byte idx) => ref EnvironmentE(idx).HillC;
        public ref ResourcesC WaterOnCellC(in byte idx) => ref EnvironmentE(idx).FertilizeC;

        public ref RiverE RiverE(in byte idx) => ref CellEs(idx).RiverE;
        public ref RiverC RiverC(in byte cellIdx) => ref RiverE(cellIdx).RiverC;
        public RiverTypes RiverT(in byte cell) => RiverC(cell).RiverT;
        public void SetRiverT(in byte cell, in RiverTypes riverT) => RiverC(cell).RiverT = riverT;
        public ref HaveRiverAroundCellC HaveRiverC(in byte cell) => ref RiverE(cell).HaveRiverC;

        public ref EffectE EffectE(in byte idx) => ref CellEs(idx).EffectE;
        public ref bool HaveFire(in byte idx) => ref EffectE(idx).HaveFire;

        public ref TrailE TrailE(in byte cell) => ref CellEs(cell).TrailE;
        public VisibleToOtherPlayerOrNotC TrailVisibleC(in byte cell) => TrailE(cell).VisibleC;
        public HealthTrailC HealthTrail(in byte cell) => TrailE(cell).HealthC;


        #region Space

        readonly Dictionary<string, byte> _idxs;

        public const byte XY_FOR_ARRAY = 2;
        public const byte X = 0;
        public const byte Y = 1;

        public byte GetIdxCellByXy(in byte[] xy) => _idxs[xy[X].ToString() + "_" + xy[Y]];

        #endregion


        #endregion


        public EntitiesModel(in DataFromViewC dataFromViewC, in string nameRpcMethod, in List<object> actions, in TestModeTypes testModeT)
        {
            CommonInfoAboutGameC = new CommonInfoAboutGameC(testModeT, DateTime.Now);

            CommonGameE = new CommonGameE(dataFromViewC);

            for (var playerT = (PlayerTypes)0; playerT < PlayerTypes.End; playerT++)
            {
                _forPlayerEs[(byte)playerT] = new PlayerInfoE(true);
            }

            var selectedBuildings = new Dictionary<BuildingTypes, bool>();
            for (var buildingT = BuildingTypes.None + 1; buildingT < BuildingTypes.End; buildingT++) selectedBuildings.Add(buildingT, false);
            SelectedE.BuildingsC = new SelectedBuildingsInTownC(selectedBuildings);

            RpcC = new RpcPoolC(actions, nameRpcMethod);


            _cellEs = new CellEs[StartValues.CELLS];
            _idxs = new Dictionary<string, byte>();
            var xys = new List<byte[]>();

            byte idxCell = 0;
            for (byte x = 0; x < StartValues.X_AMOUNT; x++)
                for (byte y = 0; y < StartValues.Y_AMOUNT; y++)
                {
                    _idxs.Add(x.ToString() + "_" + y, idxCell);
                    xys.Add(new byte[] { x, y });
                    idxCell++;
                }

            for (idxCell = 0; idxCell < StartValues.CELLS; idxCell++)
            {
                _cellEs[idxCell] = new CellEs(dataFromViewC, dataFromViewC.IdCell(idxCell), idxCell, this, xys[idxCell]);
            }
        }


        internal void SetResourcesInInventory(in PlayerTypes playerT, in ResourceTypes resT, in float resources) => ResourcesInInventoryC(playerT).Set(resT, resources);

        internal void SetToolWeaponsInInventor(in PlayerTypes playerT, in LevelTypes levT, in ToolWeaponTypes twT, in int amountToolWeapons) => HowManyToolWeaponsInInventoryC(playerT).Set(twT, levT, amountToolWeapons);
        internal void AddToolWeaponsInInventor(in PlayerTypes playerT, in LevelTypes levT, in ToolWeaponTypes twT, in int adding = 1) => HowManyToolWeaponsInInventoryC(playerT).Add(twT, levT, adding);
        internal void SubtractToolWeaponsInInventor(in PlayerTypes playerT, in LevelTypes levT, in ToolWeaponTypes twT, in int subtraction = 1) => HowManyToolWeaponsInInventoryC(playerT).Subtract(twT, levT, subtraction);

    }
}