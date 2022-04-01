﻿using Chessy.Common;
using Chessy.Common.Component;
using Chessy.Game.Entity.Model.Cell;
using Chessy.Game.Entity.Model.Cell.Unit;
using Chessy.Game.Model.Component;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.Entity.Cell.Unit;
using Chessy.Game.Values;
using System;
using System.Collections.Generic;

namespace Chessy.Game.Entity.Model
{
    public sealed class EntitiesModelGame
    {
        readonly Dictionary<ClipTypes, ActionC> _sounds0 = new Dictionary<ClipTypes, ActionC>();
        readonly Dictionary<AbilityTypes, ActionC> _sounds1 = new Dictionary<AbilityTypes, ActionC>();
        readonly ResourcesC[] _mistakeEconomyEs;
        readonly Dictionary<PlayerTypes, PlayerInfoEs> _forPlayerEs;

        public bool NeedUpdateView;

        public bool IsStartedGame { get; internal set; }
        public bool IsSelectedCity { get; internal set; }
        public bool HaveTreeUnit { get; internal set; }
        public bool IsClicked { get; internal set; }
 

        public MistakeC MistakeC;
        public InfoGameC MotionsC;
        public ZonesInfoC ZoneInfoC;
        public CellsC CellsC;
        public WhereTeleportC WhereTeleportC;
        public PlayerTC WhoseMove;
        public PlayerTC WinnerPlayerTC;
        public PlayerTC CurPlayerITC;
        public CellClickTC CellClickTC;
        public LessonTC LessonTC;
        public RaycastTC RaycastTC;

        public readonly ResourcesE Resources;
        public RpcPoolEs RpcPoolEs;
        public WeatherE WeatherE;
        public SelectedE SelectedE;
        public SelectedUnitE SelectedUnitE;

        public PlayerInfoEs PlayerInfoE(in PlayerTypes player) => _forPlayerEs[player];
        public ref ResourcesC ResourcesC(in PlayerTypes playerT, in ResourceTypes resT) => ref PlayerInfoE(playerT).ResourcesC(resT);
        public ref int ToolWeaponsC(in PlayerTypes playerT, in LevelTypes levT, in ToolWeaponTypes twT) => ref PlayerInfoE(playerT).LevelE(levT).ToolWeapons(twT);
        public ref PlayerLevelInfoE UnitInfoE(in PlayerTypes playerT, in LevelTypes levT) => ref PlayerInfoE(playerT).LevelE(levT);
        public ref PlayerLevelBuildingInfoE BuildingsInfo(in PlayerTypes playerT, in LevelTypes levT, in BuildingTypes buildT) => ref PlayerInfoE(playerT).LevelE(levT).BuildingInfoE(buildT);
        public ActionC SoundActionC(in ClipTypes clip) => _sounds0[clip];
        public ActionC SoundActionC(in AbilityTypes unique) => _sounds1[unique];
        public ref ResourcesC MistakeEconomy(in ResourceTypes resT) => ref _mistakeEconomyEs[(byte)resT - 1];

        #region Cells

        readonly CellEs[] _cellEs;
        public byte LengthCells => (byte)_cellEs.Length;


        public CellEs CellEs(in byte idx) => _cellEs[idx];


        #region Unit

        public ref UnitEs UnitEs(in byte idx) => ref CellEs(idx).UnitEs;

        public ref UnitMainE UnitMainE(in byte idx_cell) => ref UnitEs(idx_cell).MainE;
        public ref UnitTC UnitTC(in byte idx) => ref UnitMainE(idx).UnitTC;
        public ref PlayerTC UnitPlayerTC(in byte idx) => ref UnitMainE(idx).PlayerTC;
        public ref LevelTC UnitLevelTC(in byte idx) => ref UnitMainE(idx).LevelTC;
        public ref ConditionUnitTC UnitConditionTC(in byte idx) => ref UnitMainE(idx).ConditionTC;
        public ref IsRightArcherC UnitIsRightArcherC(in byte idx) => ref UnitMainE(idx).IsRightArcherC;

        public ref StatsE UnitStatsE(in byte idx_cell) => ref UnitEs(idx_cell).StatsE;
        public ref HealthC UnitHpC(in byte idx) => ref UnitStatsE(idx).HealthC;
        public ref StepsC UnitStepC(in byte idx) => ref UnitStatsE(idx).StepC;
        public ref WaterC UnitWaterC(in byte idx) => ref UnitStatsE(idx).WaterC;
        public ref DamageC DamageAttackC(in byte idx) => ref UnitStatsE(idx).DamageSimpleAttackC;
        public ref DamageC DamageOnCellC(in byte idx) => ref UnitStatsE(idx).DamageOnCellC;

        public ref MainToolWeaponE UnitMainTWE(in byte idx) => ref UnitEs(idx).MainToolWeaponE;
        public ref ToolWeaponTC UnitMainTWTC(in byte idx) => ref UnitMainTWE(idx).ToolWeaponTC;
        public ref LevelTC UnitMainTWLevelTC(in byte idx) => ref UnitMainTWE(idx).LevelTC;

        public ref ExtraToolWeaponE UnitExtraTWE(in byte idx_cell) => ref UnitEs(idx_cell).ExtraToolWeaponE;
        public ref ToolWeaponTC UnitExtraTWTC(in byte idx) => ref UnitExtraTWE(idx).ToolWeaponTC;
        public ref LevelTC UnitExtraLevelTC(in byte idx) => ref UnitExtraTWE(idx).LevelTC;
        public ref ProtectionC UnitExtraProtectionC(in byte idx) => ref UnitExtraTWE(idx).ProtectionC;

        public ref CellUnitExtractE UnitExtactE(in byte idx_cell) => ref UnitEs(idx_cell).ExtractE;
        public ref ResourcesC PawnExtractAdultForestE(in byte idx) => ref UnitExtactE(idx).PawnExtractAdultForestE;
        public ref ResourcesC PawnExtractHillE(in byte idx) => ref UnitExtactE(idx).PawnExtractHillE;

        public ref WhoLastDiedHereE LastDiedE(in byte idx) => ref UnitEs(idx).WhoLastDiedHereE;
        public ref UnitTC LastDiedUnitTC(in byte idx) => ref LastDiedE(idx).UnitTC;
        public ref LevelTC LastDiedLevelTC(in byte idx) => ref LastDiedE(idx).LevelTC;
        public ref PlayerTC LastDiedPlayerTC(in byte idx) => ref LastDiedE(idx).PlayerTC;


        #region Effects

        public ref EffectsE UnitEffectsE(in byte idx_cell) => ref UnitEs(idx_cell).EffectsE;
        public ref StunC UnitEffectStunC(in byte idx) => ref UnitEffectsE(idx).StunC;
        public ref ProtectionC UnitEffectShield(in byte idx) => ref UnitEffectsE(idx).ShieldEffectC;
        public ref ShootsC UnitEffectFrozenArrawC(in byte idx) => ref UnitEffectsE(idx).FrozenArrawC;

        #endregion

        #endregion


        #region Building

        public ref CellBuildingEs BuildEs(in byte idx) => ref CellEs(idx).BuildEs;
        public ref BuildingE BuildingMainE(in byte idx_cell) => ref BuildEs(idx_cell).MainE;
        public ref BuildingTC BuildingTC(in byte idx) => ref BuildingMainE(idx).BuildingTC;
        public ref LevelTC BuildingLevelTC(in byte idx) => ref BuildingMainE(idx).LevelTC;
        public ref PlayerTC BuildingPlayerTC(in byte idx) => ref BuildingMainE(idx).PlayerTC;
        public ref HealthC BuildingHpC(in byte idx) => ref BuildingMainE(idx).HealthC;

        public ref CellBuildingExtractE BuildingExtractE(in byte idx_cell) => ref BuildEs(idx_cell).ExtractE;
        public ref ResourcesC WoodcutterExtractE(in byte idx) => ref BuildingExtractE(idx).WoodcutterExtractC;
        public ref ResourcesC FarmExtractFertilizeE(in byte idx) => ref BuildingExtractE(idx).FarmExtractC;

        #endregion


        public ref CellEnvironmentEs EnvironmentEs(in byte idx) => ref CellEs(idx).EnvironmentEs;
        public ref ResourcesC YoungForestC(in byte idx) => ref EnvironmentEs(idx).YoungForestC;
        public ref ResourcesC AdultForestC(in byte idx) => ref EnvironmentEs(idx).AdultForestC;
        public ref ResourcesC MountainC(in byte idx) => ref EnvironmentEs(idx).MountainC;
        public ref ResourcesC HillC(in byte idx) => ref EnvironmentEs(idx).HillC;
        public ref ResourcesC FertilizeC(in byte idx) => ref EnvironmentEs(idx).FertilizeC;

        public ref CellRiverE RiverEs(in byte idx) => ref CellEs(idx).RiverEs;

        public ref CellEffectE EffectEs(in byte idx) => ref CellEs(idx).EffectEs;
        public ref bool HaveFire(in byte idx) => ref EffectEs(idx).HaveFire;


        #region Space

        readonly Dictionary<string, byte> _idxs;

        public const byte XY_FOR_ARRAY = 2;
        public const byte X = 0;
        public const byte Y = 1;

        public byte GetIdxCellByXy(in byte[] xy) => _idxs[xy[X].ToString() + "_" + xy[Y]];

        #endregion




        #endregion


        public EntitiesModelGame(in List<object> forData, in List<string> namesMethods)
        {
            Resources = new ResourcesE(default);

            var i = 0;

            var actions = (List<object>)forData[i++];
            var sounds0 = (Dictionary<ClipTypes, Action>)forData[i++];
            var sounds1 = (Dictionary<AbilityTypes, Action>)forData[i++];
            var isActiveParenCells = (bool[])forData[i++];
            var idCells = (int[])forData[i++];

            _forPlayerEs = new Dictionary<PlayerTypes, PlayerInfoEs>();
            _mistakeEconomyEs = new ResourcesC[(byte)ResourceTypes.End];


            for (var playerT = PlayerTypes.None; playerT < PlayerTypes.End; playerT++)
            {
                _forPlayerEs.Add(playerT, new PlayerInfoEs(true));
            }

            var selectedBuildings = new Dictionary<BuildingTypes, bool>();
            for (var buildingT = BuildingTypes.None + 1; buildingT < BuildingTypes.End; buildingT++) selectedBuildings.Add(buildingT, false);
            SelectedE.BuildingsC = new SelectedBuildingsC(selectedBuildings);



            foreach (var item in sounds0) _sounds0.Add(item.Key, new ActionC(item.Value));
            foreach (var item in sounds1) _sounds1.Add(item.Key, new ActionC(item.Value));

            RpcPoolEs = new RpcPoolEs(actions, namesMethods);


            _cellEs = new CellEs[StartValues.CELLS];
            _idxs = new Dictionary<string, byte>();
            var xys = new List<byte[]>();

            byte idx = 0;
            for (byte x = 0; x < StartValues.X_AMOUNT; x++)
                for (byte y = 0; y < StartValues.Y_AMOUNT; y++)
                {
                    _idxs.Add(x.ToString() + "_" + y, idx);
                    xys.Add(new byte[] { x, y });
                    idx++;
                }

            for (idx = 0; idx < StartValues.CELLS; idx++)
            {
                _cellEs[idx] = new CellEs(isActiveParenCells, idCells[idx], xys[idx], idx, this);
            }
        }
    }
}