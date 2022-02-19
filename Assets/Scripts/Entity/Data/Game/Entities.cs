using ECS;
using Game.Common;
using Photon.Pun;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class Entities
    {
        public readonly RpcE RpcE;

        public PlayerTC WhoseMove;
        public PlayerTC WinnerC;
        public PlayerTC CurPlayerI;

        public SelectedUnitE SelectedUnitE;
        public SelectedToolWeaponE SelectedTWE;

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


        readonly ActionC[] _sounds0;
        readonly ActionC[] _sounds1;
        readonly ResourcesC[] _mistakeEconomyEs;
        readonly InfoPlayerPoolEs[] _forPlayerEs;
        public ref InfoPlayerPoolEs PlayerE(in PlayerTypes player) => ref _forPlayerEs[(byte)player - 1];
        public ref UnitInfoE UnitInfo(in PlayerTypes playerT, in UnitTypes unitT) => ref PlayerE(playerT).UnitsInfoE(unitT);
        public ref ActionC Sound(in ClipTypes clip) => ref _sounds0[(int)clip - 1];
        public ref ActionC Sound(in AbilityTypes unique) => ref _sounds1[(int)unique - 1];
        public ref ResourcesC MistakeEconomy(in ResourceTypes resT) => ref _mistakeEconomyEs[(byte)resT - 1];

        public MistakeE MistakeE;
        public ref MistakeTC MistakeTC => ref MistakeE.MistakeTC;
        public ref TimerC MistakeTimerC => ref MistakeE.TimerC;


        #region Cells

        readonly CellPoolEs[] _cellEs;
        public byte LengthCells => (byte)_cellEs.Length;


        public ref CellPoolEs CellEs(in byte idx) => ref _cellEs[idx];


        #region Unit

        public ref CellUnitPoolEs UnitEs(in byte idx) => ref CellEs(idx).UnitEs;

        public ref UnitTC UnitTC(in byte idx) => ref UnitEs(idx).UnitTC;
        public ref PlayerTC UnitPlayerTC(in byte idx) => ref UnitEs(idx).PlayerTC;
        public ref LevelTC UnitLevelTC(in byte idx) => ref UnitEs(idx).LevelTC;
        public ref ConditionUnitTC UnitConditionTC(in byte idx) => ref UnitEs(idx).ConditionTC;
        public ref IsRightArcherC UnitIsRightArcherC(in byte idx) => ref UnitEs(idx).IsRightArcherC;
        public ref HealthC UnitHpC(in byte idx) => ref UnitEs(idx).HealthC;
        public ref StepsC UnitStepC(in byte idx) => ref UnitEs(idx).StepC;
        public ref WaterC UnitWaterC(in byte idx) => ref UnitEs(idx).WaterC;
        public ref ToolWeaponTC UnitMainTWTC(in byte idx) => ref UnitEs(idx).MainToolWeaponTC;
        public ref LevelTC UnitMainTWLevelTC(in byte idx) => ref UnitEs(idx).MainLevelTC;
        public ref ToolWeaponTC UnitExtraTWTC(in byte idx) => ref UnitEs(idx).ExtraToolWeaponTC;
        public ref LevelTC UnitExtraLevelTC(in byte idx) => ref UnitEs(idx).ExtraTWLevelTC;
        public ref ProtectionC UnitExtraProtectionTC(in byte idx) => ref UnitEs(idx).ExtraTWShieldC;
        public ref CellUnitExtractPawnE UnitExtractPawnE(in byte idx) => ref UnitEs(idx).ExtractPawnE;


        #region Effects

        public ref StunC UnitStunC(in byte idx) => ref UnitEs(idx).StunC;
        public ref ProtectionC UnitEffectShield(in byte idx) => ref UnitEs(idx).ShieldEffectC;
        public ref FrozenArrawC UnitFrozenArrawC(in byte idx) => ref UnitEs(idx).FrozenArrawC;

        #endregion

        #endregion

        public ref WhoLastDiedHereE LastDiedE(in byte idx) => ref CellEs(idx).WhoLastDiedHereE;
        public ref UnitTC LastDiedUnitTC(in byte idx) => ref LastDiedE(idx).UnitTC;
        public ref LevelTC LastDiedLevelTC(in byte idx) => ref LastDiedE(idx).LevelTC;
        public ref PlayerTC LastDiedPlayerTC(in byte idx) => ref LastDiedE(idx).PlayerTC;

        public ref CellBuildingE BuildE(in byte idx) => ref CellEs(idx).BuildE;
        public ref BuildingTC BuildTC(in byte idx) => ref BuildE(idx).BuildingC;
        public ref LevelTC BuildLevelTC(in byte idx) => ref BuildE(idx).LevelTC;
        public ref PlayerTC BuildPlayerTC(in byte idx) => ref BuildE(idx).PlayerC;
        public ref HealthC BuildHpC(in byte idx) => ref BuildE(idx).HealthC;
        public ref bool BuildSmelterTC(in byte idx) => ref BuildE(idx).IsActiveSmelter;

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


        //public void Melt_Master(in PlayerTypes player)
        //{
        //    if (Resource(ResourceTypes.Wood, player).ResourceC.Resources >= 10
        //        && Resource(ResourceTypes.Ore, player).ResourceC.Resources >= 10)
        //    {
        //        Resource(ResourceTypes.Wood, player).ResourceC.Resources -= 10;
        //        Resource(ResourceTypes.Ore, player).ResourceC.Resources -= 10;

        //        if (UnityEngine.Random.Range(0f, 1f) <= 0.7f)
        //        {
        //            if (UnityEngine.Random.Range(0f, 1f) <= 0.2f)
        //            {
        //                Resource(ResourceTypes.Gold, player).ResourceC.Resources += 1;
        //            }
        //            else
        //            {
        //                Resource(ResourceTypes.Iron, player).ResourceC.Resources += 1;
        //            }
        //        }
        //    }
        //}

        //public bool CanBuyBuilding_Master(in BuildingTypes build, in PlayerTypes player, out Dictionary<ResourceTypes, float> needRes)
        //{
        //    needRes = new Dictionary<ResourceTypes, float>();
        //    var canCreatBuild = true;

        //    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
        //    {
        //        needRes.Add(res, Resource(res, player).Need(build));
        //        if (canCreatBuild) canCreatBuild = Resource(res, player).CanBuy(build);
        //    }
        //    return canCreatBuild;
        //}
        //public void BuyBuilding_Master(in BuildingTypes build, in PlayerTypes player)
        //{
        //    for (var resType = ResourceTypes.None + 1; resType < ResourceTypes.End; resType++)
        //        Resource(resType, player).Buy(build);
        //}

        //public bool CanBuyResourceFromMarket_Master(in MarketBuyTypes marketBuyT, in PlayerTypes player, out Dictionary<ResourceTypes, float> needRes)
        //{
        //    needRes = new Dictionary<ResourceTypes, float>();

        //    needRes.Add(ResourceTypes.Food, 0);
        //    needRes.Add(ResourceTypes.Wood, 0);
        //    needRes.Add(ResourceTypes.Ore, 0);
        //    needRes.Add(ResourceTypes.Iron, 0);
        //    needRes.Add(ResourceTypes.Gold, 0);

        //    switch (marketBuyT)
        //    {
        //        case MarketBuyTypes.FoodToWood:
        //            needRes[ResourceTypes.Food] = ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT);
        //            break;

        //        case MarketBuyTypes.WoodToFood:
        //            needRes[ResourceTypes.Wood] = ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT);
        //            break;

        //        case MarketBuyTypes.GoldToFood:
        //            needRes[ResourceTypes.Gold] = ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT);
        //            break;

        //        case MarketBuyTypes.GoldToWood:
        //            needRes[ResourceTypes.Gold] = ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT);
        //            break;

        //        default: throw new Exception();
        //    }

        //    foreach (var item in needRes) if (item.Value > Resource(item.Key, player).ResourceC.Resources) return false;
        //    return true;
        //}
        //public void TryBuyResourcesFromMarket_Master(in MarketBuyTypes marketBuyT, in Player sender, in Entities ents)
        //{
        //    var whoseMove = ents.WhoseMove.Player;

        //    if (CanBuyResourceFromMarket_Master(marketBuyT, whoseMove, out var needRes))
        //    {
        //        switch (marketBuyT)
        //        {
        //            case MarketBuyTypes.FoodToWood:
        //                ForPlayerE(whoseMove).ResourcesC(ResourceTypes.Food).Resources -= ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT);
        //                ForPlayerE(whoseMove).ResourcesC(ResourceTypes.Wood).Resources += ResourcesInInventorValues.ResourcesAfterBuyInMarket(marketBuyT);
        //                break;

        //            case MarketBuyTypes.WoodToFood:
        //                Resource(ResourceTypes.Wood, whoseMove).ResourceC.Resources -= ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT);
        //                Resource(ResourceTypes.Food, whoseMove).ResourceC.Resources += ResourcesInInventorValues.ResourcesAfterBuyInMarket(marketBuyT);
        //                break;

        //            case MarketBuyTypes.GoldToFood:
        //                Resource(ResourceTypes.Gold, whoseMove).ResourceC.Resources -= ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT);
        //                Resource(ResourceTypes.Food, whoseMove).ResourceC.Resources += ResourcesInInventorValues.ResourcesAfterBuyInMarket(marketBuyT);
        //                break;

        //            case MarketBuyTypes.GoldToWood:
        //                Resource(ResourceTypes.Gold, whoseMove).ResourceC.Resources -= ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT);
        //                Resource(ResourceTypes.Wood, whoseMove).ResourceC.Resources += ResourcesInInventorValues.ResourcesAfterBuyInMarket(marketBuyT);
        //                break;

        //            default: throw new Exception();
        //        }
        //    }
        //    else
        //    {
        //        ents.RpcE.MistakeEconomyToGeneral(sender, needRes);
        //    }
        //}

        //public bool CanBuyTW(ToolWeaponTypes tw, LevelTypes lev, PlayerTypes player, out Dictionary<ResourceTypes, float> needRes)
        //{
        //    needRes = new Dictionary<ResourceTypes, float>();
        //    var canCreatBuild = true;

        //    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
        //    {
        //        var difAmountRes = Resource(res, player).ResourceC.Resources - ResourcesInInventorValues.ForBuyToolWeapon(tw, lev, res);
        //        needRes.Add(res, ResourcesInInventorValues.ForBuyToolWeapon(tw, lev, res));

        //        if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
        //    }

        //    return canCreatBuild;
        //}
        //public void BuyTW(ToolWeaponTypes tw, LevelTypes level, PlayerTypes player)
        //{
        //    for (var resType = ResourceTypes.None + 1; resType < ResourceTypes.End; resType++)
        //        Resource(resType, player).ResourceC.Resources -= ResourcesInInventorValues.ForBuyToolWeapon(tw, level, resType);
        //}




        public Entities(in List<object> forData, in List<string> namesMethods)
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

            RpcE = new RpcE(actions, namesMethods);


            _cellEs = new CellPoolEs[StartValues.ALL_CELLS_AMOUNT];

            _idxs = new Dictionary<string, byte>();

            byte idx = 0;
            for (byte x = 0; x < StartValues.X_AMOUNT; x++)
                for (byte y = 0; y < StartValues.Y_AMOUNT; y++)
                {
                    _idxs.Add(x.ToString() + "_" + y, idx++);
                }

            idx = 0;
            for (byte x = 0; x < StartValues.X_AMOUNT; x++)
                for (byte y = 0; y < StartValues.Y_AMOUNT; y++)
                {
                    _cellEs[idx] = new CellPoolEs(isActiveParenCells[idx], idCells[idx], new byte[] { x, y }, idx++, this);
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
                                MountainC(idx_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, CellEnvironment_Values.STANDART_MAX_AMOUNT_RESOURCES);
                                amountMountains++;
                            }



                            else
                            {
                                if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                                {
                                    AdultForestC(idx_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, CellEnvironment_Values.STANDART_MAX_AMOUNT_RESOURCES);
                                }
                            }
                        }

                        else
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                            {
                                AdultForestC(idx_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, CellEnvironment_Values.STANDART_MAX_AMOUNT_RESOURCES);
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
                            UnitExtraProtectionTC(idx_0).Protection = CellUnitToolWeapon_Values.ProtectionShield(LevelTypes.First);
                        }
                    }
                }
            }
        }
    }
}