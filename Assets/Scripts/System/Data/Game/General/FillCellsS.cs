using Game.Common;
using Photon.Pun;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class FillCellsS : SystemAbstract
    {
        public FillCellsS(in Entities ents) : base(ents)
        {

            if (PhotonNetwork.IsMasterClient)
            {
                var amountMountains = 0;

                for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
                {
                    var xy_0 = Es.CellEs(idx_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (Es.CellEs(idx_0).IsActiveParentSelf)
                    {
                        if (y >= 4 && y <= 6 && x > 6)
                        {
                            if (amountMountains < 3 && UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.Mountain))
                            {
                                Es.MountainC(idx_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, CellEnvironment_Values.STANDART_MAX_AMOUNT_RESOURCES);
                                amountMountains++;
                            }

                            

                            else
                            {
                                if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                                {
                                    Es.AdultForestC(idx_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, CellEnvironment_Values.STANDART_MAX_AMOUNT_RESOURCES);
                                }
                            }
                        }

                        else
                        {
                            if (UnityEngine.Random.Range(0f, 1f) <= StartValues.SpawnPercent(EnvironmentTypes.AdultForest))
                            {
                                Es.AdultForestC(idx_0).SetRandom(StartValues.MIN_RESOURCES_ENVIRONMENT, CellEnvironment_Values.STANDART_MAX_AMOUNT_RESOURCES);
                            }
                        }

                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x < 4 && y == 5)
                        {
                            Es.RiverEs(idx_0).SetStart(DirectTypes.Up);
                        }
                        else if (x == 4 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);
                            Es.RiverEs(idx_0).SetStart(DirectTypes.Up, DirectTypes.Right);
                        }
                        else if (x >= 5 && x < 7 && y == 4)
                        {
                            Es.RiverEs(idx_0).SetStart(DirectTypes.Up);
                        }


                        foreach (var dir in Es.CellEs(idx_0).RiverEs.Keys)
                        {
                            if (Es.RiverEs(idx_0).HaveRive(dir).HaveRiver.Have)
                            {
                                var xy_next = CellWorker.GetXyCellByDirect(Es.CellEs(idx_0).CellE.XyC.Xy, dir);
                                var idx_next = CellWorker.GetIdxCell(xy_next);

                                Es.RiverEs(idx_next).RiverE.RiverTC.River = RiverTypes.EndRiver;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var xy_next = CellWorker.GetXyCellByDirect(Es.CellEs(idx_0).CellE.XyC.Xy, dir);
                            var idx_next = CellWorker.GetIdxCell(xy_next);

                            Es.RiverEs(idx_next).RiverE.RiverTC.River = RiverTypes.Corner;
                        }
                    }
                }
            }

            if (GameModeC.IsGameMode(GameModes.TrainingOff))
            {
                Es.InventorResourcesEs.Resource(ResourceTypes.Food, PlayerTypes.Second).ResourceC.Resources = 999999;

                foreach (byte idx_0 in CellWorker.Idxs)
                {
                    var xy_0 = Es.CellEs(idx_0).CellE.XyC.Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    if (x == 7 && y == 8)
                    {
                        Es.MountainC(idx_0).Resources = 0;

                        if (Es.AdultForestC(idx_0).HaveAny)
                        {
                            Es.AdultForestC(idx_0).Resources = 0;
                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                Es.TrailEs(idx_0).Trail(dirT).HealthC.Health = 0;
                            }
                        }

                        Es.UnitTC(idx_0).Unit = UnitTypes.King;
                        Es.UnitLevelTC(idx_0).Level = LevelTypes.First;
                        Es.UnitPlayerTC(idx_0).Player = PlayerTypes.Second;
                        Es.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.Protected;

                        Es.UnitHpC(idx_0).Health = CellUnitStatHp_Values.MAX_HP;
                        Es.UnitStepC(idx_0).Steps = CellUnitStatStep_Values.StandartForUnit(UnitTypes.King);
                        Es.UnitWaterC(idx_0).Water = CellUnitStatWater_Values.MAX_WATER;
                    }

                    else if (x == 8 && y == 8)
                    {
                        Es.MountainC(idx_0).Resources = 0;

                        if (Es.AdultForestC(idx_0).HaveAny)
                        {
                            Es.AdultForestC(idx_0).Resources = 0;
                            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                            {
                                Es.TrailEs(idx_0).Trail(dirT).HealthC.Health = 0;
                            }
                        }

                        Es.BuildTC(idx_0).Build = BuildingTypes.City;
                        Es.BuildPlayerTC(idx_0).Player = PlayerTypes.Second;
                        Es.BuildHpC(idx_0).Health = CellBuildingValues.MaxAmountHealth(BuildingTypes.City);
                    }

                    else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                    {
                        Es.MountainC(idx_0).Resources = 0;

                        Es.UnitTC(idx_0).Unit = UnitTypes.Pawn;
                        Es.UnitLevelTC(idx_0).Level = LevelTypes.First;
                        Es.UnitPlayerTC(idx_0).Player = PlayerTypes.Second;
                        Es.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.Protected;

                        Es.UnitHpC(idx_0).Health = CellUnitStatHp_Values.MAX_HP;
                        Es.UnitStepC(idx_0).Steps = CellUnitStatStep_Values.StandartForUnit(UnitTypes.Pawn);
                        Es.UnitWaterC(idx_0).Water = CellUnitStatWater_Values.MAX_WATER;

                        Es.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                        Es.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;


                        var rand = UnityEngine.Random.Range(0f, 1f);

                        if (rand >= 0.5f)
                        {
                            Es.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Sword;
                            Es.UnitExtraLevelTC(idx_0).Level = LevelTypes.Second;
                        }
                        else
                        {
                            Es.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Shield;
                            Es.UnitExtraLevelTC(idx_0).Level = LevelTypes.First;
                            Es.UnitExtraProtectionShieldTC(idx_0).Protection = CellUnitToolWeapon_Values.ProtectionShield(LevelTypes.First);
                        }
                    }
                }
            }
        }
    }
}