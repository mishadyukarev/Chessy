using Chessy.Common;
using Chessy.Common.Component;
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
        public NeedUpdateViewC NeedUpdateViewC;
        public ZonesInfoC ZoneInfoC;
        public WhereTeleportC WhereTeleportC;
        public CellClickTC CellClickTC;
        public GameModeTC GameModeTC;
        public TimerC MotionTimerC;
        public RaycastTC RaycastTC;
        public LessonTC LessonTC;
        public MotionsC MotionsC;
        public PlayerTC WinnerPlayerTC;
        public CellsC CellsC;
        public PlayerTC WhoseMovePlayerTC;
        public PlayerTC CurPlayerITC;

        public readonly Resources Resources;
        public MistakeE MistakeE;
        public RpcPoolEs RpcPoolEs;
        public WeatherE WeatherE;
        public SelectedE SelectedE;
        public SelectedUnitE SelectedUnitE;

        public bool NeedUpdateView
        {
            get => NeedUpdateViewC.NeedUpdateView;
            set => NeedUpdateViewC.NeedUpdateView = value;
        }
        public bool IsStartedGame { get; internal set; }
        public bool IsSelectedCity { get; internal set; }
        public bool HaveTreeUnit { get; internal set; }
        public bool IsClicked { get; internal set; }
        public bool IsActivatedIdxAndXyInfoCells { get; internal set; }
        public int AmountPlantedYoungForests { get; internal set; }
        public GameModeTypes GameModeT
        {
            get => GameModeTC.GameModeT;
            internal set => GameModeTC.GameModeT = value;
        }
        public float MotionTimer
        {
            get => MotionTimerC.Timer;
            internal set => MotionTimerC.Timer = value;
        }
        public LessonTypes LessonT
        {
            get => LessonTC.LessonT;
            internal set => LessonTC.LessonT = value;
        }
        public int Motions
        {
            get => MotionsC.Motions;
            internal set => MotionsC.Motions = value;
        }
        public PlayerTypes WinnerPlayerT
        {
            get => WinnerPlayerTC.PlayerT;
            internal set => WinnerPlayerTC.PlayerT = value;
        }
        public byte SelectedCell
        {
            get => CellsC.Selected;
            internal set => CellsC.Selected = value;
        }
        public byte CurrentCellIdx
        {
            get => CellsC.Current;
            internal set => CellsC.Current = value;
        }
        public PlayerTypes CurPlayerIT
        {
            get => CurPlayerITC.PlayerT;
            internal set => CurPlayerITC.PlayerT = value;
        }
        public PlayerTypes WhoseMovePlayerT => WhoseMovePlayerTC.PlayerT;

        public ref MistakeTC MistakeTC => ref MistakeE.MistakeTC;
        public MistakeTypes MistakeT
        {
            get => MistakeTC.MistakeT;
            internal set => MistakeTC.MistakeT = value;
        }
        public ref TimerC MistakeTimerC => ref MistakeE.TimerC;
        public float MistakeTimer
        {
            get => MistakeTimerC.Timer;
            internal set => MistakeTimerC.Timer = value;
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
        public ref UnitTC UnitTC(in byte idx) => ref UnitMainE(idx).UnitTC;
        public UnitTypes UnitT(in byte cell) => UnitTC(cell).UnitT;
        public ref PlayerTC UnitPlayerTC(in byte idx) => ref UnitMainE(idx).PlayerTC;
        public PlayerTypes UnitPlayerT(in byte idx) => UnitPlayerTC(idx).PlayerT;
        public ref LevelTC UnitLevelTC(in byte idx) => ref UnitMainE(idx).LevelTC;
        public LevelTypes UnitLevelT(in byte idx) => UnitLevelTC(idx).LevelT;
        public ref ConditionUnitTC UnitConditionTC(in byte idx) => ref UnitMainE(idx).ConditionTC;
        public ConditionUnitTypes UnitConditionT(in byte idx) => UnitConditionTC(idx).Condition;
        public ref IsRightArcherC UnitIsRightArcherC(in byte idx) => ref UnitMainE(idx).IsRightArcherC;
        public bool IsRightArcherUnit(in byte idx) => UnitIsRightArcherC(idx).IsRight;
        public VisibleC UnitVisibleC(in byte cell) => UnitMainE(cell).VisibleC;
        public CanSetUnitHereC CanSetUnitHereC(in byte cell) => UnitMainE(cell).CanSetUnitHereC;
        public IdxsCellsC UnitForArsonC(in byte cell) => UnitMainE(cell).ForArson;
        public ref NeedUpdateViewC UnitNeedUpdateViewC(in byte cell) => ref UnitMainE(cell).NeedUpdateViewC;

        public StatsE StatsUnitE(in byte idx_cell) => UnitEs(idx_cell).StatsE;
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

        public MainToolWeaponE MainToolWeaponE(in byte idx) => UnitEs(idx).MainToolWeaponE;
        public ref ToolWeaponTC MainToolWeaponTC(in byte idx) => ref MainToolWeaponE(idx).ToolWeaponTC;
        public ToolWeaponTypes MainToolWeaponT(in byte cell) => MainToolWeaponTC(cell).ToolWeaponT;
        public ref LevelTC MainTWLevelTC(in byte idx) => ref MainToolWeaponE(idx).LevelTC;
        public LevelTypes MainTWLevelT(in byte idx) => MainTWLevelTC(idx).LevelT;

        public ExtraToolWeaponE UnitExtraTWE(in byte idx_cell) => UnitEs(idx_cell).ExtraToolWeaponE;
        public ref ToolWeaponTC ExtraToolWeaponTC(in byte idx) => ref UnitExtraTWE(idx).ToolWeaponTC;
        public ToolWeaponTypes ExtraToolWeaponT(in byte idx) => ExtraToolWeaponTC(idx).ToolWeaponT;
        public ref LevelTC ExtraTWLevelTC(in byte idx) => ref UnitExtraTWE(idx).LevelTC;
        public LevelTypes ExtraTWLevelT(in byte idx) => ExtraTWLevelTC(idx).LevelT;
        public ref ProtectionC ExtraTWProtectionC(in byte idx) => ref UnitExtraTWE(idx).ProtectionC;
        public float ExtraTWProtection(in byte idx) => ExtraTWProtectionC(idx).Protection;

        ref CellUnitExtractE UnitExtactE(in byte idx_cell) => ref UnitEs(idx_cell).ExtractE;
        public ref ResourcesC PawnExtractAdultForestC(in byte idx) => ref UnitExtactE(idx).PawnExtractAdultForestE;
        public float PawnExtractAdultForest(in byte idx) => PawnExtractAdultForestC(idx).Resources;
        public ref ResourcesC PawnExtractHillC(in byte idx) => ref UnitExtactE(idx).PawnExtractHillE;
        public float PawnExtractHill(in byte idx) => PawnExtractHillC(idx).Resources;

        ref WhoLastDiedHereE LastDiedE(in byte idx) => ref UnitEs(idx).WhoLastDiedHereE;
        public ref UnitTC LastDiedUnitTC(in byte idx) => ref LastDiedE(idx).UnitTC;
        public ref LevelTC LastDiedLevelTC(in byte idx) => ref LastDiedE(idx).LevelTC;
        public ref PlayerTC LastDiedPlayerTC(in byte idx) => ref LastDiedE(idx).PlayerTC;

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
        public ref BuildingTC BuildingTC(in byte idx) => ref BuildingEs(idx).BuildingTC;
        public BuildingTypes BuildingT(in byte cell) => BuildingTC(cell).BuildingT;
        public ref LevelTC BuildingLevelTC(in byte idx) => ref BuildingEs(idx).LevelTC;
        public LevelTypes BuildingLevelT(in byte idx) => BuildingLevelTC(idx).LevelT;
        public ref PlayerTC BuildingPlayerTC(in byte idx) => ref BuildingEs(idx).PlayerTC;
        public PlayerTypes BuildingPlayerT(in byte idx) => BuildingPlayerTC(idx).PlayerT;
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
        public ref RiverTC RiverTC(in byte cell) => ref RiverE(cell).RiverTC;
        public RiverTypes RiverT(in byte cell) => RiverTC(cell).RiverT;
        public ref HaveRiverC HaveRiverC(in byte cell) => ref RiverE(cell).HaveRiverC;

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


        public EntitiesModelGame(in EntitiesModelCommon eMC, in DataFromViewC dataFromViewC, in List<string> namesMethods, in List<object> actions)
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

            RpcPoolEs = new RpcPoolEs(actions, namesMethods);


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