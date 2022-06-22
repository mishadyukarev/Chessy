using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Game.Entity;
using Chessy.Game.Enum;
using Chessy.Game.Model.Component;
using Chessy.Game.Model.Entity.Cell.Unit;
using Chessy.Game.Values;
using System;
using System.Collections.Generic;

namespace Chessy.Game.Model.Entity
{
    public sealed class EntitiesModelGame
    {
        readonly ResourcesC[] _mistakeEconomyEs = new ResourcesC[(byte)ResourceTypes.End];
        readonly PlayerInfoEs[] _forPlayerEs = new PlayerInfoEs[(byte)PlayerTypes.End];
        readonly CellEs[] _cellEs;


        public readonly EntitiesModelCommon Common;


        internal float ForUpdateViewTimer;

        public readonly DataFromViewC DataFromViewC;
        public ZonesInfoC ZoneInfoC;
        public WhereTeleportC WhereTeleportC;

        public TimerC MotionTimerC;
        public MotionsC MotionsC;
        public PlayerTypes WinnerPlayerTC;
        public CellsC CellsC;
        public PlayerTypes WhoseMovePlayerT;
        public PlayerTypes CurPlayerIT;

        public readonly Resources Resources;
        public MistakeC MistakeC;
        internal RpcPoolC RpcC;
        public WeatherE WeatherE;
        public SelectedE SelectedE;
        public SelectedUnitE SelectedUnitE;



        public RaycastTypes RaycastT { get; internal set; }
        public CellClickTypes CellClickT { get; internal set; }
        public LessonTypes LessonT { get; internal set; }
        public bool NeedUpdateView
        {
            get => Common.NeedUpdateView;
            set => Common.NeedUpdateView = value;
        }
        public bool IsStartedGame { get; internal set; }
        public bool IsSelectedCity { get; internal set; }
        public bool HaveTreeUnit { get; internal set; }
        public bool IsClicked { get; internal set; }
        public bool IsActivatedIdxAndXyInfoCells { get; internal set; }
        public int AmountPlantedYoungForests { get; internal set; }
        public float MotionTimer
        {
            get => MotionTimerC.Timer;
            internal set => MotionTimerC.Timer = value;
        }
        public int Motions
        {
            get => MotionsC.Motions;
            internal set => MotionsC.Motions = value;
        }
        public PlayerTypes WinnerPlayerT
        {
            get => WinnerPlayerTC;
            internal set => WinnerPlayerTC = value;
        }
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
        public GameModeTypes GameModeT
        {
            get => Common.GameModeT;
            internal set => Common.GameModeT = value;
        }


        public ref PlayerInfoEs PlayerInfoE(in PlayerTypes player) => ref _forPlayerEs[(byte)player];
        public ref ResourcesC ResourcesC(in PlayerTypes playerT, in ResourceTypes resT) => ref PlayerInfoE(playerT).ResourcesC(resT);
        public ref int ToolWeaponsC(in PlayerTypes playerT, in LevelTypes levT, in ToolWeaponTypes twT) => ref PlayerInfoE(playerT).LevelE(levT).ToolWeapons(twT);
        public ref PlayerLevelInfoE UnitInfoE(in PlayerTypes playerT, in LevelTypes levT) => ref PlayerInfoE(playerT).LevelE(levT);
        public ref PlayerLevelBuildingInfoE BuildingsInfo(in PlayerTypes playerT, in LevelTypes levT, in BuildingTypes buildT) => ref PlayerInfoE(playerT).LevelE(levT).BuildingInfoE(buildT);
        public Action SoundAction(in ClipTypes clipT) => DataFromViewC.SoundAction(clipT);
        public Action SoundAction(in AbilityTypes abilityT) => DataFromViewC.SoundAction(abilityT);
        public ref ResourcesC MistakeEconomy(in ResourceTypes resT) => ref _mistakeEconomyEs[(byte)resT - 1];


        #region Cells

        ref CellEs CellEs(in byte idx) => ref _cellEs[idx];


        public ref CellE CellE(in byte cell) => ref CellEs(cell).CellE;
        public IsStartedCellC IsStartedCellC(in byte cell) => CellE(cell).IsStartedCellC;
        public bool IsBorder(in byte cell) => CellE(cell).IsBorder;
        public XyCellC XyCellC(in byte cell) => CellE(cell).XyC;
        public int InstanceID(in byte cell) => CellE(cell).InstanceID;


        public AroundCellsE AroundCellsE(in byte cell) => CellEs(cell).AroundCellsEs;


        #region Unit

        public UnitEs UnitEs(in byte idx) => CellEs(idx).UnitEs;

        public UnitMainE UnitMainE(in byte idx) => UnitEs(idx).MainE;
        public UnitTypes UnitT(in byte idx) => UnitMainE(idx).UnitT;
        internal void SetUnitOnCellT(in byte idx, in UnitTypes unitT) => UnitMainE(idx).UnitT = unitT;
        public PlayerTypes UnitPlayerT(in byte idx) => UnitMainE(idx).PlayerT;
        internal void SetUnitPlayerT(in byte cellIdx, in PlayerTypes playerT) => UnitMainE(cellIdx).PlayerT = playerT;
        public LevelTypes UnitLevelT(in byte idx) => UnitMainE(idx).LevelT;
        public void SetUnitLevelT(in byte idx, in LevelTypes levelT) => UnitMainE(idx).LevelT = levelT;
        public ConditionUnitTypes UnitConditionT(in byte idx) => UnitMainE(idx).ConditionT;
        internal void SetUnitConditionT(in byte cellIdx, in ConditionUnitTypes conditionUnitT) => UnitMainE(cellIdx).ConditionT = conditionUnitT;
        public ref IsRightArcherC UnitIsRightArcherC(in byte idx) => ref UnitMainE(idx).IsRightArcherC;
        public bool IsRightArcherUnit(in byte idx) => UnitIsRightArcherC(idx).IsRight;
        public VisibleC UnitVisibleC(in byte cell) => UnitMainE(cell).VisibleC;
        public CanSetUnitHereC CanSetUnitHereC(in byte cell) => UnitMainE(cell).CanSetUnitHereC;
        public IdxsCellsC UnitForArsonC(in byte cell) => UnitMainE(cell).ForArson;
        public ref NeedUpdateViewC UnitNeedUpdateViewC(in byte cell) => ref UnitMainE(cell).NeedUpdateViewC;

        public UnitStatsE StatsUnitE(in byte idx_cell) => UnitEs(idx_cell).StatsE;
        public ref HealthC HpUnitC(in byte idx) => ref StatsUnitE(idx).HealthC;
        public double HpUnit(in byte cell) => HpUnitC(cell).Health;
        public ref StepsC StepUnitC(in byte idx) => ref StatsUnitE(idx).StepC;
        public double StepUnit(in byte idx) => StepUnitC(idx).Steps;
        public ref WaterC WaterUnitC(in byte idx) => ref StatsUnitE(idx).WaterC;
        public double WaterUnit(in byte idx) => WaterUnitC(idx).Water;
        public ref DamageC DamageAttackC(in byte idx) => ref StatsUnitE(idx).DamageSimpleAttackC;
        public double DamageAttack(in byte cell) => DamageAttackC(cell).Damage;
        public ref DamageC DamageOnCellC(in byte idx) => ref StatsUnitE(idx).DamageOnCellC;
        public double DamageOnCell(in byte cell) => DamageOnCellC(cell).Damage;

        public ref ToolWeaponMainUnitC MainToolWeaponE(in byte idx) => ref UnitEs(idx).MainToolWeaponE;
        public ToolWeaponTypes MainToolWeaponT(in byte cell) => MainToolWeaponE(cell).ToolWeaponT;
        public void SetMainToolWeaponT(in byte cell, in ToolWeaponTypes toolWeaponT) => MainToolWeaponE(cell).ToolWeaponT = toolWeaponT;
        public LevelTypes MainTWLevelT(in byte idx) => MainToolWeaponE(idx).LevelT;
        internal void SetMainTWLevelT(in byte cellIdx, in LevelTypes levelT) => MainToolWeaponE(cellIdx).LevelT = levelT;

        public ref ExtraToolWeaponE UnitExtraTWE(in byte idx_cell) => ref UnitEs(idx_cell).ExtraToolWeaponE;
        public ToolWeaponTypes ExtraToolWeaponT(in byte idx) => UnitExtraTWE(idx).ToolWeaponT;
        internal void SetExtraToolWeaponT(in byte idx, in ToolWeaponTypes toolWeaponT) => UnitExtraTWE(idx).ToolWeaponT = toolWeaponT;
        public LevelTypes ExtraTWLevelT(in byte idx) => UnitExtraTWE(idx).LevelT;
        public LevelTypes SetExtraTWLevelT(in byte idx, in LevelTypes levelT) => UnitExtraTWE(idx).LevelT = levelT;
        public ref ProtectionC ExtraTWProtectionC(in byte idx) => ref UnitExtraTWE(idx).ProtectionC;
        public float ExtraTWProtection(in byte idx) => ExtraTWProtectionC(idx).Protection;

        ref CellUnitExtractE UnitExtactE(in byte idx_cell) => ref UnitEs(idx_cell).ExtractE;
        public ref ResourcesC PawnExtractAdultForestC(in byte idx) => ref UnitExtactE(idx).PawnExtractAdultForestE;
        public float PawnExtractAdultForest(in byte idx) => PawnExtractAdultForestC(idx).Resources;
        public ref ResourcesC PawnExtractHillC(in byte idx) => ref UnitExtactE(idx).PawnExtractHillE;
        public float PawnExtractHill(in byte idx) => PawnExtractHillC(idx).Resources;

        ref WhoLastDiedHereE LastDiedE(in byte idx) => ref UnitEs(idx).WhoLastDiedHereE;
        public UnitTypes LastDiedUnitT(in byte idx) => LastDiedE(idx).UnitT;
        internal void SetLastDiedUnitT(in byte idx, in UnitTypes unitT) => LastDiedE(idx).UnitT = unitT;
        public LevelTypes LastDiedLevelT(in byte idx) => LastDiedE(idx).LevelT;
        public LevelTypes SetLastDiedLevelT(in byte idx, in LevelTypes levelT) => LastDiedE(idx).LevelT = levelT;
        public PlayerTypes LastDiedPlayerT(in byte idx) => LastDiedE(idx).PlayerT;
        internal void SetLastDiedPlayerT(in byte cellIdx, in PlayerTypes playerT) => LastDiedE(cellIdx).PlayerT = playerT;

        ref UnitAttackE UnitAttackE(in byte cell) => ref UnitEs(cell).AttackE;
        public IdxsCellsC AttackSimpleCellsC(in byte cell) => UnitAttackE(cell).Simple;
        public IdxsCellsC AttackUniqueCellsC(in byte cell) => UnitAttackE(cell).Unique;

        ref ShiftUnitE UnitShiftE(in byte cell) => ref UnitEs(cell).ShiftE;
        public IdxsCellsC CellsForShift(in byte cell) => UnitShiftE(cell).ForShift;
        public NeedStepsC UnitNeedStepsForShiftC(in byte cell) => UnitShiftE(cell).NeedStepsC;

        ref AbilityUnitE UnitAbilityE(in byte cell) => ref UnitEs(cell).AbilityE;
        public UniqueButtonsC UnitButtonAbilitiesC(in byte cell) => UnitAbilityE(cell).UniqueButtonsC;
        public CooldownAbilitiesC UnitCooldownAbilitiesC(in byte cell) => UnitAbilityE(cell).CooldownsC;


        #region Effects

        public UnitEffectsE UnitEffectsE(in byte idx_cell) => UnitEs(idx_cell).EffectsE;
        public ref StunC StunUnitC(in byte idx) => ref UnitEffectsE(idx).StunC;
        public float StunUnit(in byte idx) => StunUnitC(idx).Stun;
        public ref ProtectionC ShieldUnitEffectC(in byte idx) => ref UnitEffectsE(idx).ShieldEffectC;
        public float ShieldEffect(in byte idx) => ShieldUnitEffectC(idx).Protection;
        public ref ShootsC FrozenArrawEffectC(in byte idx) => ref UnitEffectsE(idx).FrozenArrawC;
        public int FrozenArrawEffect(in byte cell) => FrozenArrawEffectC(cell).Shoots;
        public ref bool HaveKingEffect(in byte idx) => ref UnitEffectsE(idx).HaveKingEffect;

        #endregion

        #endregion


        #region Building

        ref BuildingE BuildingEs(in byte idx) => ref CellEs(idx).BuildEs;
        public BuildingTypes BuildingOnCellT(in byte idx) => BuildingEs(idx).BuildingT;
        internal void SetBuildingOnCellT(in byte cellIdx, in BuildingTypes buildingT) => BuildingEs(cellIdx).BuildingT = buildingT;
        public bool HaveBuildingOnCell(in byte idxCell) => BuildingOnCellT(idxCell).HaveBuilding();
        public bool IsBuildingOnCell(in byte cellIdx, params BuildingTypes[] buildingTs) => BuildingOnCellT(cellIdx).Is(buildingTs);
        public LevelTypes BuildingLevelT(in byte idx) => BuildingEs(idx).LevelT;
        public void SetBuildingLevelT(in byte idx, in LevelTypes levelT) => BuildingEs(idx).LevelT = levelT;
        public PlayerTypes BuildingPlayerT(in byte idx) => BuildingEs(idx).PlayerT;
        internal void SetBuildingPlayerT(in byte cellIdx, in PlayerTypes playerT) => BuildingEs(cellIdx).PlayerT = playerT;
        public ref HealthC BuildingHpC(in byte idx) => ref BuildingEs(idx).HealthC;
        public double BuildingHp(in byte idx) => BuildingHpC(idx).Health;
        public VisibleC BuildingVisibleC(in byte cell) => BuildingEs(cell).VisibleC;
        public ref ResourcesC WoodcutterExtractC(in byte idx) => ref BuildingEs(idx).WoodcutterExtractC;
        public ref ResourcesC FarmExtractFertilizeC(in byte idx) => ref BuildingEs(idx).FarmExtractC;

        #endregion


        ref EnvironmentE EnvironmentEs(in byte idx) => ref CellEs(idx).EnvironmentEs;
        public ref ResourcesC YoungForestC(in byte idx) => ref EnvironmentEs(idx).YoungForestC;
        public ref ResourcesC AdultForestC(in byte idx) => ref EnvironmentEs(idx).AdultForestC;
        public ref ResourcesC MountainC(in byte idx) => ref EnvironmentEs(idx).MountainC;
        public ref ResourcesC HillC(in byte idx) => ref EnvironmentEs(idx).HillC;
        public ref ResourcesC FertilizeC(in byte idx) => ref EnvironmentEs(idx).FertilizeC;

        public RiverE RiverE(in byte idx) => CellEs(idx).RiverEs;
        public RiverTypes RiverT(in byte cell) => RiverE(cell).RiverT;
        public void SetRiverT(in byte cell, in RiverTypes riverT) => RiverE(cell).RiverT = riverT;
        public ref HaveRiverAroundCellC HaveRiverC(in byte cell) => ref RiverE(cell).HaveRiverC;

        ref EffectE EffectEs(in byte idx) => ref CellEs(idx).EffectEs;
        public ref bool HaveFire(in byte idx) => ref EffectEs(idx).HaveFire;

        ref TrailE TrailE(in byte cell) => ref CellEs(cell).TrailE;
        public VisibleC TrailVisibleC(in byte cell) => TrailE(cell).VisibleC;
        public HealthTrailC HealthTrail(in byte cell) => TrailE(cell).HealthC;


        #region Space

        readonly Dictionary<string, byte> _idxs;

        public const byte XY_FOR_ARRAY = 2;
        public const byte X = 0;
        public const byte Y = 1;

        public byte GetIdxCellByXy(in byte[] xy) => _idxs[xy[X].ToString() + "_" + xy[Y]];

        #endregion


        #endregion


        public EntitiesModelGame(in EntitiesModelCommon eMC, in DataFromViewC dataFromViewC, in string nameRpcMethod, in List<object> actions)
        {
            Common = eMC;


            Resources = new Resources(default);

            DataFromViewC = dataFromViewC;

            for (var playerT = (PlayerTypes)0; playerT < PlayerTypes.End; playerT++)
            {
                _forPlayerEs[(byte)playerT] = new PlayerInfoEs(true);
            }

            var selectedBuildings = new Dictionary<BuildingTypes, bool>();
            for (var buildingT = BuildingTypes.None + 1; buildingT < BuildingTypes.End; buildingT++) selectedBuildings.Add(buildingT, false);
            SelectedE.BuildingsC = new SelectedBuildingsC(selectedBuildings);

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
    }
}