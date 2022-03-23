using Chessy.Common;
using Chessy.Common.Component;
using Chessy.Common.Entity;
using Chessy.Common.Enum;
using Chessy.Game.Entity.Model.Cell;
using Chessy.Game.Entity.Model.Cell.Unit;
using Chessy.Game.System.Model;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using System;
using System.Collections.Generic;

namespace Chessy.Game.Entity.Model
{
    public sealed class EntitiesModelGame
    {
        readonly Dictionary<ClipTypes, ActionC> _sounds0;
        readonly ActionC[] _sounds1;
        readonly ResourcesC[] _mistakeEconomyEs;
        readonly Dictionary<PlayerTypes, PlayerInfoEs> _forPlayerEs;
        readonly Dictionary<PlayerTypes, PlayerTC> _nextPlayer;
        readonly Dictionary<UnitTypes, bool> _isAnimal;
        readonly Dictionary<string, bool> _isMelee;

        public bool IsStartedGame;
        public bool IsSelectedCity;
        public bool HaveTreeUnit;

        public MistakeC MistakeC;
        public InfoGameC MotionsC;
        public ZonesInfoC ZoneInfoC;
        public CellsC CellsC;
        public WhereTeleportC WhereTeleportC;
        public PlayerTC WhoseMove;
        public PlayerTC WinnerC;
        public PlayerTC CurPlayerITC;
        public CellClickC CellClickTC;


        public readonly ResourcesE Resources;
        public RpcPoolEs RpcPoolEs;
        public WeatherE WeatherE;
        public SelectedE SelectedE;

        public PlayerInfoEs PlayerInfoE(in PlayerTypes player) => _forPlayerEs[player];
        public ref ResourcesC ResourcesC(in PlayerTypes playerT, in ResourceTypes resT) => ref PlayerInfoE(playerT).ResourcesC(resT);
        public ref int ToolWeaponsC(in PlayerTypes playerT, in LevelTypes levT, in ToolWeaponTypes twT) => ref PlayerInfoE(playerT).LevelE(levT).ToolWeapons(twT);
        public ref PlayerLevelInfoE UnitInfoE(in PlayerTypes playerT, in LevelTypes levT) => ref PlayerInfoE(playerT).LevelE(levT);
        public ref PlayerLevelInfoE UnitInfo(in PlayerTC playerTC, in LevelTC levTC) => ref PlayerInfoE(playerTC.Player).LevelE(levTC.Level);
        public ref PlayerLevelInfoE UnitInfo(in UnitMainE unitMainE) => ref PlayerInfoE(unitMainE.PlayerTC.Player).LevelE(unitMainE.LevelTC.Level);
        public PlayerUnitInfoE UnitUnfo(in PlayerTypes playerT, in UnitTypes unitT) => PlayerInfoE(playerT).UnitE(unitT);
        public ref PlayerLevelBuildingInfoE BuildingsInfo(in PlayerTypes playerT, in LevelTypes levT, in BuildingTypes buildT) => ref PlayerInfoE(playerT).LevelE(levT).BuildingInfoE(buildT);
        public ref PlayerLevelBuildingInfoE BuildingsInfo(in PlayerTC playerT, in LevelTC levT, in BuildingTC buildT) => ref PlayerInfoE(playerT.Player).LevelE(levT.Level).BuildingInfoE(buildT.Building);
        public ref PlayerLevelBuildingInfoE BuildingsInfo(in BuildingE buildMainE) => ref PlayerInfoE(buildMainE.PlayerTC.Player).LevelE(buildMainE.LevelTC.Level).BuildingInfoE(buildMainE.BuildingTC.Building);
        public ActionC Sound(in ClipTypes clip) => _sounds0[clip];
        public ref ActionC Sound(in AbilityTypes unique) => ref _sounds1[(int)unique - 1];
        public ref ResourcesC MistakeEconomy(in ResourceTypes resT) => ref _mistakeEconomyEs[(byte)resT - 1];
        public PlayerTC NextPlayer(in PlayerTypes playerT) => _nextPlayer[playerT];
        public PlayerTC NextPlayer(in PlayerTC playerTC) => _nextPlayer[playerTC.Player];

        public bool IsAnimal(in UnitTypes unitT) => _isAnimal[unitT];
        public bool IsMelee(in UnitTypes unitT, in bool haveBowCrossbow) => _isMelee[unitT.ToString() + haveBowCrossbow];
        public bool IsMelee(in byte idx_cell) => _isMelee[UnitTC(idx_cell).Unit.ToString() + UnitMainTWTC(idx_cell).Is(ToolWeaponTypes.BowCrossbow)];


        #region Cells

        readonly CellEs[] _cellEs;
        public byte LengthCells => (byte)_cellEs.Length;


        public ref CellEs CellEs(in byte idx) => ref _cellEs[idx];


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

        public ref CellUnitMainToolWeaponE UnitMainTWE(in byte idx) => ref UnitEs(idx).MainToolWeaponE;
        public ref ToolWeaponTC UnitMainTWTC(in byte idx) => ref UnitMainTWE(idx).ToolWeaponTC;
        public ref LevelTC UnitMainTWLevelTC(in byte idx) => ref UnitMainTWE(idx).LevelTC;

        public ref ExtraToolWeaponE UnitExtraTWE(in byte idx_cell) => ref UnitEs(idx_cell).ExtraToolWeaponE;
        public ref ToolWeaponTC UnitExtraTWTC(in byte idx) => ref UnitExtraTWE(idx).ToolWeaponTC;
        public ref LevelTC UnitExtraLevelTC(in byte idx) => ref UnitExtraTWE(idx).LevelTC;
        public ref ProtectionC UnitExtraProtectionTC(in byte idx) => ref UnitExtraTWE(idx).ProtectionC;

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
        public ref HealthC BuildHpC(in byte idx) => ref BuildingMainE(idx).HealthC;

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


        public EntitiesModelGame(in List<object> forData, in List<string> namesMethods, in EntitiesModelCommon eMC)
        {
            Resources = new ResourcesE(default);

            var i = 0;

            var actions = (List<object>)forData[i++];
            var sounds0 = (Dictionary<ClipTypes, Action>)forData[i++];
            var sounds1 = (Dictionary<AbilityTypes, Action>)forData[i++];
            var isActiveParenCells = (bool[])forData[i++];
            var idCells = (int[])forData[i++];


            _nextPlayer = new Dictionary<PlayerTypes, PlayerTC>();
            _nextPlayer.Add(PlayerTypes.First, new PlayerTC(PlayerTypes.Second));
            _nextPlayer.Add(PlayerTypes.Second, new PlayerTC(PlayerTypes.First));
            _forPlayerEs = new Dictionary<PlayerTypes, PlayerInfoEs>();
            _mistakeEconomyEs = new ResourcesC[(byte)ResourceTypes.End];
            _isAnimal = new Dictionary<UnitTypes, bool>();
            _isMelee = new Dictionary<string, bool>();


            for (var playerT = PlayerTypes.None; playerT < PlayerTypes.End; playerT++)
            {
                _forPlayerEs.Add(playerT, new PlayerInfoEs(true));
            }

            for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
            {
                if (unitT == UnitTypes.Wolf) _isAnimal.Add(unitT, true);
                else _isAnimal.Add(unitT, false);


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
            SelectedE.BuildingsC = new SelectedBuildingsC(selectedBuildings);




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
        }

        public void StartGame(in GameModeTC gameModeTC)
        {
            ZoneInfoC.IsActiveFriend = gameModeTC.Is(GameModes.WithFriendOff);
            WhoseMove = new PlayerTC(StartValues.WHOSE_MOVE);
            CellClickTC = new CellClickC(StartValues.CELL_CLICK);
            IsSelectedCity = false;
            HaveTreeUnit = false;
            MistakeC.MistakeT = MistakeTypes.None;
            WinnerC.Player = PlayerTypes.None;
            ZoneInfoC = default;
            CellsC = default;

            WeatherE.WindC = new WindC(StartValues.DIRECT_WIND, StartValues.STRENGTH_WIND, StartValues.MAX_STREANGTH_WIND, StartValues.MIN_SNREANGTH_WIND);
            WeatherE.SunC = new SunC(StartValues.SUN_SIDE);
            WeatherE.CloudC = new CloudC(StartValues.START_CLOUD);


            SelectedE.ToolWeaponC = new SelectedToolWeaponC(StartValues.SELECTED_TOOL_WEAPON, StartValues.SELECTED_LEVEL_TOOL_WEAPON);





            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                FertilizeC(idx_0).Resources = 0;
                AdultForestC(idx_0).Resources = 0;
                YoungForestC(idx_0).Resources = 0;
                HillC(idx_0).Resources = 0;
                MountainC(idx_0).Resources = 0;

                UnitTC(idx_0).Unit = UnitTypes.None;
                BuildingTC(idx_0).Building = BuildingTypes.None;

                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    CellEs(idx_0).TrailHealthC(dirT).Health = 0;
                }
            }


            for (var playerT = PlayerTypes.None; playerT < PlayerTypes.End; playerT++)
            {
                PlayerInfoE(playerT).StartGame();

                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    PlayerInfoE(playerT).ResourcesC(resT) = new ResourcesC(StartValues.Resources(resT));
                }
            }


            switch (gameModeTC.GameMode)
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


                for (byte idx_0 = 0; idx_0 < LengthCells; idx_0++)
                {
                    new MountainThrowHillsUpdMS(idx_0, this);
                }

            }


            if (gameModeTC.Is(GameModes.TrainingOff))
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
                            TakeAdultForestResourcesS.TakeAdultForestResources(1f, idx_0, this);
                        }
                        UnitTC(idx_0).Unit = UnitTypes.King;
                        UnitLevelTC(idx_0).Level = LevelTypes.First;
                        UnitPlayerTC(idx_0).Player = PlayerTypes.Second;
                        UnitConditionTC(idx_0).Condition = ConditionUnitTypes.Protected;

                        UnitHpC(idx_0).Health = HpValues.MAX;
                        UnitStepC(idx_0).Steps = StepValues.MAX;
                        UnitWaterC(idx_0).Water = WaterValues.MAX;
                    }

                    //else if (x == 8 && y == 8)
                    //{
                    //    MountainC(idx_0).Resources = 0;

                    //    if (AdultForestC(idx_0).HaveAnyResources)
                    //    {
                    //        TakeAdultForestResourcesS.TakeAdultForestResources(1f, idx_0, this);
                    //    }

                    //    //BuildingMainE(idx_0).Set(BuildingTypes.City, LevelTypes.First, Building_Values.HELTH_CITY, PlayerTypes.Second);
                    //}

                    else if (x == 6 && y == 8 || x == 8 && y == 8 || x <= 8 && x >= 6 && y == 7 || x <= 8 && x >= 6 && y == 9)
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
                            UnitExtraProtectionTC(idx_0).Protection = ToolWeaponValues.SHIELD_PROTECTION_LEVEL_FIRST;
                        }
                    }
                }
            }



            new GetDataCells(this);
        }
    }
}