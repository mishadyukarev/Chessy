﻿using Chessy.Common;
using Chessy.Game.Entity.Cell;
using Chessy.Game.Entity.Cell.Unit;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public sealed class EntitiesModel
    {
        readonly Dictionary<ClipTypes, ActionC> _sounds0;
        readonly ActionC[] _sounds1;
        readonly ResourcesC[] _mistakeEconomyEs;
        readonly Dictionary<PlayerTypes, ForPlayerPoolEs> _forPlayerEs;
        readonly Dictionary<PlayerTypes, PlayerTC> _nextPlayer;
        readonly Dictionary<UnitTypes, bool> _isAnimal;
        readonly Dictionary<UnitTypes, bool> _isHero;
        readonly Dictionary<string, bool> _isMelee;
        readonly Dictionary<string, bool> _canSetUnit = new Dictionary<string, bool>();


        public PlayerTC WhoseMove;
        public PlayerTC WinnerC;
        public PlayerTC CurPlayerITC;

        public SunSideTC SunSideTC;
        public DirectTC DirectWindTC;
        public StrengthC StrengthWind;

        public CellClickC CellClickTC;
        public RayCastTC RayCastTC;

        public AbilityTC SelectedAbilityTC;
        public SelectedBuildingsC SelectedBuildingsC;

        public IdxCellC StartTeleportIdxC;
        public IdxCellC EndTeleportIdxC;
        public IdxCellC CurrentIdxC;
        public IdxCellC SelectedIdxC;
        public IdxCellC PreviousVisionIdxC;
        public IdxCellC CenterCloudIdxC;

        public bool MotionIsActive;
        public bool EnvIsActive;
        public bool FriendIsActive;
        public bool IsStartedGame;
        public bool IsClicked;
        public bool IsSelectedCity;

        public int Motions;

        public RpcPoolEs RpcPoolEs;
        public SelectedUnitE SelectedUnitE;
        public SelectedToolWeaponE SelectedTWE;


        public ForPlayerPoolEs PlayerInfoE(in PlayerTypes player) => _forPlayerEs[player];
        public ref ResourcesC ResourcesC(in PlayerTypes playerT, in ResourceTypes resT) => ref PlayerInfoE(playerT).ResourcesC(resT);
        public ref AmountC ToolWeaponsC(in PlayerTypes playerT, in LevelTypes levT, in ToolWeaponTypes twT) => ref PlayerInfoE(playerT).LevelE(levT).ToolWeapons(twT);
        public ref PlayerLevelInfoE UnitInfoE(in PlayerTypes playerT, in LevelTypes levT) => ref PlayerInfoE(playerT).LevelE(levT);
        public ref PlayerLevelInfoE UnitInfo(in PlayerTC playerTC, in LevelTC levTC) => ref PlayerInfoE(playerTC.Player).LevelE(levTC.Level);
        public ref PlayerLevelInfoE UnitInfo(in CellUnitMainE unitMainE) => ref PlayerInfoE(unitMainE.PlayerTC.Player).LevelE(unitMainE.LevelTC.Level);
        public PlayerUnitInfoE UnitUnfo(in PlayerTypes playerT, in UnitTypes unitT) => PlayerInfoE(playerT).UnitE(unitT);
        public ref PlayerLevelBuildingInfoE BuildingsInfo(in PlayerTypes playerT, in LevelTypes levT, in BuildingTypes buildT) => ref PlayerInfoE(playerT).LevelE(levT).BuildingInfoE(buildT);
        public ref PlayerLevelBuildingInfoE BuildingsInfo(in PlayerTC playerT, in LevelTC levT, in BuildingTC buildT) => ref PlayerInfoE(playerT.Player).LevelE(levT.Level).BuildingInfoE(buildT.Building);
        public ref PlayerLevelBuildingInfoE BuildingsInfo(in CellBuildingMainE buildMainE) => ref PlayerInfoE(buildMainE.PlayerC.Player).LevelE(buildMainE.LevelTC.Level).BuildingInfoE(buildMainE.BuildingC.Building);
        public ActionC Sound(in ClipTypes clip) => _sounds0[clip];
        public ref ActionC Sound(in AbilityTypes unique) => ref _sounds1[(int)unique - 1];
        public ref ResourcesC MistakeEconomy(in ResourceTypes resT) => ref _mistakeEconomyEs[(byte)resT - 1];
        public PlayerTC NextPlayer(in PlayerTypes playerT) => _nextPlayer[playerT];
        public PlayerTC NextPlayer(in PlayerTC playerTC) => _nextPlayer[playerTC.Player];

        public MistakeE MistakeE;
        public ref MistakeTC MistakeTC => ref MistakeE.MistakeTC;
        public ref TimerC MistakeTimerC => ref MistakeE.TimerC;

        public bool IsAnimal(in UnitTypes unitT) => _isAnimal[unitT];
        public bool IsHero(in UnitTypes unitT) => _isHero[unitT];
        public bool IsMelee(in UnitTypes unitT, in bool haveBowCrossbow) => _isMelee[unitT.ToString() + haveBowCrossbow];
        public bool IsMelee(in byte idx_cell) => _isMelee[UnitTC(idx_cell).Unit.ToString() + UnitMainTWTC(idx_cell).Is(ToolWeaponTypes.BowCrossbow)];
        public bool CanSetUnit(in byte idx_cell, in bool haveUnit, in PlayerTypes playerT) => _canSetUnit[idx_cell.ToString() + haveUnit + playerT];


        #region Cells

        readonly CellEs[] _cellEs;
        public byte LengthCells => (byte)_cellEs.Length;


        public ref CellEs CellEs(in byte idx) => ref _cellEs[idx];


        #region Unit

        public ref UnitEs UnitEs(in byte idx) => ref CellEs(idx).UnitEs;

        public ref CellUnitMainE UnitMainE(in byte idx_cell) => ref UnitEs(idx_cell).MainE;
        public ref UnitTC UnitTC(in byte idx) => ref UnitMainE(idx).UnitTC;
        public ref PlayerTC UnitPlayerTC(in byte idx) => ref UnitMainE(idx).PlayerTC;
        public ref LevelTC UnitLevelTC(in byte idx) => ref UnitMainE(idx).LevelTC;
        public ref ConditionUnitTC UnitConditionTC(in byte idx) => ref UnitMainE(idx).ConditionTC;
        public ref IsRightArcherC UnitIsRightArcherC(in byte idx) => ref UnitMainE(idx).IsRightArcherC;

        public ref CellUnitStatsE UnitStatsE(in byte idx_cell) => ref UnitEs(idx_cell).StatsE;
        public ref HealthC UnitHpC(in byte idx) => ref UnitStatsE(idx).HealthC;
        public ref StepsC UnitStepC(in byte idx) => ref UnitStatsE(idx).StepC;
        public ref WaterC UnitWaterC(in byte idx) => ref UnitStatsE(idx).WaterC;
        public ref DamageC DamageAttackC(in byte idx) => ref UnitStatsE(idx).DamageAttackC;
        public ref DamageC DamageOnCellC(in byte idx) => ref UnitStatsE(idx).DamageOnCellC;

        public ref CellUnitMainToolWeaponE UnitMainTWE(in byte idx) => ref UnitEs(idx).MainToolWeaponE;
        public ref ToolWeaponTC UnitMainTWTC(in byte idx) => ref UnitMainTWE(idx).ToolWeaponTC;
        public ref LevelTC UnitMainTWLevelTC(in byte idx) => ref UnitMainTWE(idx).LevelTC;

        public ref CellUnitExtraToolWeaponE UnitExtraTWE(in byte idx_cell) => ref UnitEs(idx_cell).ExtraToolWeaponE;
        public ref ToolWeaponTC UnitExtraTWTC(in byte idx) => ref UnitExtraTWE(idx).ToolWeaponTC;
        public ref LevelTC UnitExtraLevelTC(in byte idx) => ref UnitExtraTWE(idx).LevelTC;
        public ref ProtectionC UnitExtraProtectionTC(in byte idx) => ref UnitExtraTWE(idx).ProtectionC;

        public ref CellUnitExtractE UnitExtactE(in byte idx_cell) => ref UnitEs(idx_cell).ExtractE;
        public ref ResourcesC PawnExtractAdultForestE(in byte idx) => ref UnitExtactE(idx).PawnExtractAdultForestE;
        public ref ResourcesC PawnExtractHillE(in byte idx) => ref UnitExtactE(idx).PawnExtractHillE;

        public ref CellUnitWhoLastDiedHereE LastDiedE(in byte idx) => ref UnitEs(idx).WhoLastDiedHereE;
        public ref UnitTC LastDiedUnitTC(in byte idx) => ref LastDiedE(idx).UnitTC;
        public ref LevelTC LastDiedLevelTC(in byte idx) => ref LastDiedE(idx).LevelTC;
        public ref PlayerTC LastDiedPlayerTC(in byte idx) => ref LastDiedE(idx).PlayerTC;

        public ref AttackToUnitE AttackUnitE(in byte idx_cell) => ref UnitEs(idx_cell).AttackUnitE;
        public ref DamageC DamageAttackUnitC(in byte idx_cell) => ref AttackUnitE(idx_cell).AttackDamageC;
        public ref PlayerTC AttackUnitKillerTC(in byte idx_cell) => ref AttackUnitE(idx_cell).WhoKillerC;
        public ref IdxCellC AttackUnitFromIdxC(in byte idx_cell) => ref AttackUnitE(idx_cell).FromIdx;


        #region Effects

        public ref CellUnitEffectsE UnitEffectsE(in byte idx_cell) => ref UnitEs(idx_cell).EffectsE;
        public ref StunC UnitEffectStunC(in byte idx) => ref UnitEffectsE(idx).StunC;
        public ref ProtectionC UnitEffectShield(in byte idx) => ref UnitEffectsE(idx).ShieldEffectC;
        public ref ShootsC UnitEffectFrozenArrawC(in byte idx) => ref UnitEffectsE(idx).FrozenArrawC;

        #endregion

        #endregion


        #region Building

        public ref CellBuildingEs BuildEs(in byte idx) => ref CellEs(idx).BuildEs;
        public ref CellBuildingMainE BuildingMainE(in byte idx_cell) => ref BuildEs(idx_cell).MainE;
        public ref BuildingTC BuildingTC(in byte idx) => ref BuildingMainE(idx).BuildingC;
        public ref LevelTC BuildingLevelTC(in byte idx) => ref BuildingMainE(idx).LevelTC;
        public ref PlayerTC BuildingPlayerTC(in byte idx) => ref BuildingMainE(idx).PlayerC;
        public ref HealthC BuildHpC(in byte idx) => ref BuildingMainE(idx).HealthC;
        public ref bool IsActiveSmelter(in byte idx) => ref BuildingMainE(idx).IsActiveSmelter;

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

        public EntitiesModel(in List<object> forData, in List<string> namesMethods)
        {
            CenterCloudIdxC.Idx = StartValues.START_WIND;
            FriendIsActive = GameModeC.IsGameMode(GameModes.WithFriendOff);
            DirectWindTC = new DirectTC(StartValues.DIRECT_WIND);
            SunSideTC = new SunSideTC(StartValues.SUN_SIDE);
            WhoseMove = new PlayerTC(StartValues.WHOSE_MOVE);
            CellClickTC = new CellClickC(StartValues.CELL_CLICK);
            SelectedTWE = new SelectedToolWeaponE(StartValues.SELECTED_TOOL_WEAPON, StartValues.SELECTED_LEVEL_TOOL_WEAPON);
            StrengthWind = new StrengthC(StartValues.STRENGTH_WIND);

            var i = 0;

            var actions = (List<object>)forData[i++];
            var sounds0 = (Dictionary<ClipTypes, Action>)forData[i++];
            var sounds1 = (Dictionary<AbilityTypes, Action>)forData[i++];
            var isActiveParenCells = (bool[])forData[i++];
            var idCells = (int[])forData[i++];


            _nextPlayer = new Dictionary<PlayerTypes, PlayerTC>();
            _nextPlayer.Add(PlayerTypes.First, new PlayerTC(PlayerTypes.Second));
            _nextPlayer.Add(PlayerTypes.Second, new PlayerTC(PlayerTypes.First));

            _forPlayerEs = new Dictionary<PlayerTypes, ForPlayerPoolEs>();
            for (var playerT = PlayerTypes.None; playerT < PlayerTypes.End; playerT++)
            {
                _forPlayerEs.Add(playerT, new ForPlayerPoolEs(true));
            }
            _mistakeEconomyEs = new ResourcesC[(byte)ResourceTypes.End];


            _isAnimal = new Dictionary<UnitTypes, bool>();
            _isHero = new Dictionary<UnitTypes, bool>();
            _isMelee = new Dictionary<string, bool>();

            for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
            {
                if (unitT == UnitTypes.Camel) _isAnimal.Add(unitT, true);
                else _isAnimal.Add(unitT, false);


                var ishero = false;
                switch (unitT)
                {
                    case UnitTypes.Elfemale:
                        ishero = true;
                        break;

                    case UnitTypes.Snowy:
                        ishero = true;
                        break;

                    case UnitTypes.Undead:
                        ishero = true;
                        break;

                    case UnitTypes.Hell:
                        ishero = true;
                        break;
                }
                _isHero.Add(unitT, ishero);


                foreach (var haveBowCrossbow in new[] { true, false })
                {
                    var isMelee = true;

                    switch (unitT)
                    {
                        case UnitTypes.Pawn:
                            if (haveBowCrossbow) isMelee = false;
                            break;

                        case UnitTypes.Elfemale:
                            isMelee = false;
                            break;

                        case UnitTypes.Snowy:
                            isMelee = false;
                            break;

                        case UnitTypes.Undead:
                            break;

                        case UnitTypes.Hell:
                            break;
                    }

                    _isMelee.Add(unitT.ToString() + haveBowCrossbow, isMelee);
                }
            }


            var selectedBuildings = new Dictionary<BuildingTypes, bool>();
            for (var buildingT = BuildingTypes.None + 1; buildingT < BuildingTypes.End; buildingT++) selectedBuildings.Add(buildingT, false);
            SelectedBuildingsC = new SelectedBuildingsC(selectedBuildings);



            _sounds0 = new Dictionary<ClipTypes, ActionC>();
            _sounds1 = new ActionC[(int)AbilityTypes.End - 1];
            foreach (var item in sounds0) _sounds0[item.Key] = new ActionC(item.Value);
            foreach (var item in sounds1) _sounds1[(int)item.Key - 1] = new ActionC(item.Value);

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



            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                foreach (var haveUnit in new[] { true, false })
                {
                    for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                    {
                        var xy = CellEs(idx_0).CellE.XyC.Xy;
                        var x = xy[0];
                        var y = xy[1];

                        var canSet = false;

                        if (playerT == PlayerTypes.First)
                        {
                            if (y < 3 && x > 3 && x < 12)
                            {
                                canSet = true;
                            }
                        }
                        else
                        {
                            if (y > 7 && x > 3 && x < 12)
                            {
                                canSet = true;
                            }
                        }


                        _canSetUnit.Add(idx_0.ToString() + haveUnit + playerT, canSet);
                    }
                }
            }



            switch (GameModeC.CurGameMode)
            {
                case GameModes.TrainingOff:
                    CurPlayerITC.Player = PlayerTypes.First;
                    break;

                case GameModes.WithFriendOff:
                    CurPlayerITC.Player = WhoseMove.Player;
                    break;

                case GameModes.PublicOn:
                    CurPlayerITC.Player = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
                    break;

                case GameModes.WithFriendOn:
                    CurPlayerITC.Player = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
                    break;

                default: throw new Exception();
            }



            if (PhotonNetwork.IsMasterClient)
            {
                var amountMountains = 0;

                for (byte idx_0 = 0; idx_0 < LengthCells; idx_0++)
                {
                    var xy_0 = CellEs(idx_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (CellEs(idx_0).IsActiveParentSelf)
                    {
                        if (y >= 4 && y <= 6 && x > 6)
                        {
                            if (amountMountains < 3 && UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.Mountain))
                            {
                                MountainC(idx_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, EnvironmentValues.MAX_RESOURCES);
                                amountMountains++;
                            }



                            else
                            {
                                if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                                {
                                    AdultForestC(idx_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, EnvironmentValues.MAX_RESOURCES);
                                }
                            }
                        }

                        else
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                            {
                                AdultForestC(idx_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, EnvironmentValues.MAX_RESOURCES);
                            }
                        }

                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x < 4 && y == 5)
                        {
                            RiverEs(idx_0).SetStart(DirectTypes.Up);
                        }
                        else if (x == 4 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);
                            RiverEs(idx_0).SetStart(DirectTypes.Up, DirectTypes.Right);
                        }
                        else if (x >= 5 && x < 7 && y == 4)
                        {
                            RiverEs(idx_0).SetStart(DirectTypes.Up);
                        }


                        for (var dir = DirectTypes.Up; dir <= DirectTypes.Left; dir++)
                        {
                            if (RiverEs(idx_0).HaveRive(dir))
                            {
                                var idx_next = CellEs(idx_0).AroundCellE(dir).IdxC.Idx;

                                RiverEs(idx_next).RiverTC.River = RiverTypes.EndRiver;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var idx_next = CellEs(idx_0).AroundCellE(dir).IdxC.Idx;

                            RiverEs(idx_next).RiverTC.River = RiverTypes.Corner;
                        }
                    }
                }
            }


            if (GameModeC.IsGameMode(GameModes.TrainingOff))
            {
                PlayerInfoE(PlayerTypes.Second).ResourcesC(ResourceTypes.Food).Resources = 999999;


                for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
                {
                    var xy_0 = CellEs(idx_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (x == 7 && y == 8)
                    {
                        MountainC(idx_0).Resources = 0;

                        if (AdultForestC(idx_0).HaveAnyResources)
                        {
                            AdultForestC(idx_0).Resources = 0;
                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                CellEs(idx_0).TrailHealthC(dirT).Health = 0;
                            }
                        }
                        UnitTC(idx_0).Unit = UnitTypes.King;
                        UnitLevelTC(idx_0).Level = LevelTypes.First;
                        UnitPlayerTC(idx_0).Player = PlayerTypes.Second;
                        UnitConditionTC(idx_0).Condition = ConditionUnitTypes.Protected;


                        UnitHpC(idx_0).Health = HpValues.MAX;
                        UnitStepC(idx_0).Steps = StepValues.MAX;
                        UnitWaterC(idx_0).Water = WaterValues.MAX;
                    }

                    else if (x == 8 && y == 8)
                    {
                        MountainC(idx_0).Resources = 0;

                        if (AdultForestC(idx_0).HaveAnyResources)
                        {
                            AdultForestC(idx_0).Resources = 0;
                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                CellEs(idx_0).TrailHealthC(dirT).Health = 0;
                            }
                        }

                        //BuildingMainE(idx_0).Set(BuildingTypes.City, LevelTypes.First, Building_Values.HELTH_CITY, PlayerTypes.Second);
                    }

                    else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                    {
                        MountainC(idx_0).Resources = 0;

                        UnitTC(idx_0).Unit = UnitTypes.Pawn;
                        UnitLevelTC(idx_0).Level = LevelTypes.First;
                        UnitPlayerTC(idx_0).Player = PlayerTypes.Second;
                        UnitConditionTC(idx_0).Condition = ConditionUnitTypes.Protected;

                        UnitHpC(idx_0).Health = HpValues.MAX;
                        UnitStepC(idx_0).Steps = StepValues.MAX;
                        UnitWaterC(idx_0).Water = WaterValues.MAX;

                        UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                        UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;

                        var rand = UnityEngine.Random.Range(0f, 1f);

                        if (rand >= 0.5f)
                        {
                            UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Sword;
                            UnitExtraLevelTC(idx_0).Level = LevelTypes.Second;
                        }
                        else
                        {
                            UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Shield;
                            UnitExtraLevelTC(idx_0).Level = LevelTypes.First;
                            UnitExtraProtectionTC(idx_0).Protection = CellUnitToolWeapon_Values.SHIELD_PROTECTION_LEVEL_FIRST;
                        }
                    }
                }
            }
        }
    }
}