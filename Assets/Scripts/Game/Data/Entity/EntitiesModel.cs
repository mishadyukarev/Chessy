using Game.Common;
using Photon.Pun;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class EntitiesModel
    {
        public PlayerTC WhoseMove;
        public PlayerTC WinnerC;
        public PlayerTC CurPlayerI;

        public SunSideTC SunSideTC;
        public DirectTC DirectWindTC;

        public CellClickC CellClickTC;
        public RayCastTC RayCastTC;

        public BuildingTC SelectedBuildingTC;
        public AbilityTC SelectedAbilityTC;

        public IdxC StartTeleportIdxC;
        public IdxC EndTeleportIdxC;
        public IdxC CurrentIdxC;
        public IdxC SelectedIdxC;
        public IdxC PreviousVisionIdxC;
        public IdxC CenterCloudIdxC;

        public bool IsMyMove;
        public bool MotionIsActive;
        public bool EnvIsActive;
        public bool FriendIsActive;
        public bool IsStartedGame;
        public bool IsClicked;

        public int Motions;

        public RpcPoolEs RpcPoolEs;
        public SelectedUnitE SelectedUnitE;
        public SelectedToolWeaponE SelectedTWE;
        public UnitAttackUnitE UnitAttackE;
        public AttackShieldE AttackShieldE;

        readonly ActionC[] _sounds0;
        readonly ActionC[] _sounds1;
        readonly ResourcesC[] _mistakeEconomyEs;
        readonly InfoPlayerPoolEs[] _forPlayerEs;
        readonly Dictionary<PlayerTypes, PlayerTC> _nextPlayer;
        public ref InfoPlayerPoolEs PlayerE(in PlayerTypes player) => ref _forPlayerEs[(byte)player - 1];
        public ref ResourcesC ResourcesC(in PlayerTypes playerT, in ResourceTypes resT) => ref PlayerE(playerT).ResourcesC(resT);
        public ref AmountC ToolWeaponsC(in PlayerTypes playerT, in LevelTypes levT, in ToolWeaponTypes twT) => ref PlayerE(playerT).LevelE(levT).ToolWeapons(twT);
        public ref UnitInfoE UnitInfo(in PlayerTypes playerT, in UnitTypes unitT) => ref PlayerE(playerT).UnitsInfoE(unitT);
        public ref ActionC Sound(in ClipTypes clip) => ref _sounds0[(int)clip - 1];
        public ref ActionC Sound(in AbilityTypes unique) => ref _sounds1[(int)unique - 1];
        public ref ResourcesC MistakeEconomy(in ResourceTypes resT) => ref _mistakeEconomyEs[(byte)resT - 1];
        public PlayerTC NextPlayer(in PlayerTypes playerT) => _nextPlayer[playerT];
        public PlayerTC NextPlayer(in PlayerTC playerTC) => _nextPlayer[playerTC.Player];

        public MistakeE MistakeE;
        public ref MistakeTC MistakeTC => ref MistakeE.MistakeTC;
        public ref TimerC MistakeTimerC => ref MistakeE.TimerC;


        #region Cells

        readonly CellPoolEs[] _cellEs;
        public byte LengthCells => (byte)_cellEs.Length;


        public ref CellPoolEs CellEs(in byte idx) => ref _cellEs[idx];


        #region Unit

        public ref CellUnitPoolEs UnitEs(in byte idx) => ref CellEs(idx).UnitEs;

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

        public ref CellUnitDamageE UnitDamageE(in byte idx_cell) => ref UnitEs(idx_cell).DamageE;
        public ref DamageC UnitDamageAttackC(in byte idx_cell) => ref UnitDamageE(idx_cell).DamageAttackC;
        public ref DamageC UnitDamageOnCellC(in byte idx_cell) => ref UnitDamageE(idx_cell).DamageOnCell;


        #region Effects

        public ref CellUnitEffectsE UnitEffectsE(in byte idx_cell) => ref UnitEs(idx_cell).EffectsE;
        public ref StunC UnitEffectStunC(in byte idx) => ref UnitEffectsE(idx).StunC;
        public ref ProtectionC UnitEffectShield(in byte idx) => ref UnitEffectsE(idx).ShieldEffectC;
        public ref ShootsC UnitEffectFrozenArrawC(in byte idx) => ref UnitEffectsE(idx).FrozenArrawC;

        #endregion

        #endregion



        public ref CellBuildingPoolEs BuildE(in byte idx) => ref CellEs(idx).BuildE;
        public ref BuildingTC BuildTC(in byte idx) => ref BuildE(idx).BuildingC;
        public ref LevelTC BuildLevelTC(in byte idx) => ref BuildE(idx).LevelTC;
        public ref PlayerTC BuildPlayerTC(in byte idx) => ref BuildE(idx).PlayerC;
        public ref HealthC BuildHpC(in byte idx) => ref BuildE(idx).HealthC;
        public ref bool BuildSmelterTC(in byte idx) => ref BuildE(idx).IsActiveSmelter;
        public ref ExtractE WoodcutterExtractE(in byte idx) => ref BuildE(idx).WoodcutterExtractE;
        public ref ExtractE FarmExtractFertilizeE(in byte idx) => ref BuildE(idx).FarmExtractE;


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

            var i = 0;

            var actions = (List<object>)forData[i++];
            var sounds0 = (Dictionary<ClipTypes, System.Action>)forData[i++];
            var sounds1 = (Dictionary<AbilityTypes, System.Action>)forData[i++];
            var isActiveParenCells = (bool[])forData[i++];
            var idCells = (int[])forData[i++];


            _nextPlayer = new Dictionary<PlayerTypes, PlayerTC>();
            _nextPlayer.Add(PlayerTypes.First, new PlayerTC(PlayerTypes.Second));
            _nextPlayer.Add(PlayerTypes.Second, new PlayerTC(PlayerTypes.First));

            _forPlayerEs = new InfoPlayerPoolEs[(byte)PlayerTypes.End];
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _forPlayerEs[(byte)player - 1] = new InfoPlayerPoolEs(true);
            }
            _mistakeEconomyEs = new ResourcesC[(byte)ResourceTypes.End];


            _sounds0 = new ActionC[(int)ClipTypes.End - 1];
            _sounds1 = new ActionC[(int)AbilityTypes.End - 1];
            foreach (var item in sounds0) _sounds0[(int)item.Key - 1] = new ActionC(item.Value);
            foreach (var item in sounds1) _sounds1[(int)item.Key - 1] = new ActionC(item.Value);

            RpcPoolEs = new RpcPoolEs(actions, namesMethods);


            _cellEs = new CellPoolEs[StartValues.ALL_CELLS_AMOUNT];
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

            for (idx = 0; idx < StartValues.ALL_CELLS_AMOUNT; idx++)
            {
                _cellEs[idx] = new CellPoolEs(isActiveParenCells, idCells[idx], xys[idx], idx, this);
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
                                MountainC(idx_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, CellEnvironment_Values.ENVIRONMENT_MAX);
                                amountMountains++;
                            }



                            else
                            {
                                if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                                {
                                    AdultForestC(idx_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, CellEnvironment_Values.ENVIRONMENT_MAX);
                                }
                            }
                        }

                        else
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                            {
                                AdultForestC(idx_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, CellEnvironment_Values.ENVIRONMENT_MAX);
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
                PlayerE(PlayerTypes.Second).ResourcesC(ResourceTypes.Food).Resources = 999999;


                for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
                {
                    var xy_0 = CellEs(idx_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (x == 7 && y == 8)
                    {
                        MountainC(idx_0).Resources = 0;

                        if (AdultForestC(idx_0).HaveAny)
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

                        UnitHpC(idx_0).Health = CellUnitStatHp_Values.MAX_HP;
                        UnitStepC(idx_0).Steps = CellUnitStatStep_Values.StandartForUnit(UnitTypes.King);
                        UnitWaterC(idx_0).Water = CellUnitStatWater_Values.MAX;
                    }

                    else if (x == 8 && y == 8)
                    {
                        MountainC(idx_0).Resources = 0;

                        if (AdultForestC(idx_0).HaveAny)
                        {
                            AdultForestC(idx_0).Resources = 0;
                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                CellEs(idx_0).TrailHealthC(dirT).Health = 0;
                            }
                        }

                        BuildTC(idx_0).Build = BuildingTypes.City;
                        BuildPlayerTC(idx_0).Player = PlayerTypes.Second;
                        BuildHpC(idx_0).Health = CellBuilding_Values.MaxHealth(BuildingTypes.City);
                        BuildLevelTC(idx_0).Level = LevelTypes.First;
                    }

                    else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                    {
                        MountainC(idx_0).Resources = 0;

                        UnitTC(idx_0).Unit = UnitTypes.Pawn;
                        UnitLevelTC(idx_0).Level = LevelTypes.First;
                        UnitPlayerTC(idx_0).Player = PlayerTypes.Second;
                        UnitConditionTC(idx_0).Condition = ConditionUnitTypes.Protected;

                        UnitHpC(idx_0).Health = CellUnitStatHp_Values.MAX_HP;
                        UnitStepC(idx_0).Steps = CellUnitStatStep_Values.StandartForUnit(UnitTypes.Pawn);
                        UnitWaterC(idx_0).Water = CellUnitStatWater_Values.MAX;

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