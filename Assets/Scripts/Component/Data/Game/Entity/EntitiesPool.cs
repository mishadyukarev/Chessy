using ECS;
using Game.Common;
using Photon.Pun;
using System.Collections.Generic;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellEs;
using static Game.Game.CellRiverE;
using static Game.Game.CellUnitEs;
using static Game.Game.EntityCellCloudPool;

namespace Game.Game
{
    public struct EntitiesPool
    {
        public static CellUnitWaterE[] UnitWaters { get; private set; }
        public static CellUnitHpE[] UnitHps { get; private set; }
        public static CellUnitStepEs UnitStep { get; private set; }
        public static CellUnitStunEs[] UnitStuns { get; private set; }
        public static CellUnitElseEs UnitElse { get; private set; }
        public static CellUnitDefendEffectEs UnitDefendEffect { get; private set; }

        public static CellIceWallEs[] IceWalls { get; private set; }

        public EntitiesPool(in EcsWorld gameW, in List<object> forData, in List<string> namesMethods)
        {
            var i = 0;

            var actions = (List<object>)forData[i++];
            var isActiveParenCells = (bool[])forData[i++];
            var idCells = (int[])forData[i++];
            var sounds0 = (Dictionary<ClipTypes, System.Action>)forData[i++];
            var sounds1 = (Dictionary<UniqueAbilityTypes, System.Action>)forData[i++];


            new EntityPool(gameW, actions, namesMethods);

            new CellUnitEs(gameW);
            
            new CellUnitEffectsEs(gameW);
            new CellUnitBuildingButtonEs(gameW);
            new CellUnitStepsInConditionEs(gameW);
            new CellUnitVisibleEs(gameW);
            new CellUnitUniqueButtonsEs(gameW);


            UnitStep = new CellUnitStepEs(gameW);
            UnitDefendEffect = new CellUnitDefendEffectEs(gameW);
            UnitElse = new CellUnitElseEs(gameW);
            new CellUnitAbilityUniqueEs(gameW);
            new CellUnitTWE(gameW);
            

            UnitHps = new CellUnitHpE[CellStartValues.ALL_CELLS_AMOUNT];
            UnitWaters = new CellUnitWaterE[CellStartValues.ALL_CELLS_AMOUNT];
            IceWalls = new CellIceWallEs[CellStartValues.ALL_CELLS_AMOUNT];
            UnitStuns = new CellUnitStunEs[CellStartValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < UnitWaters.Length; idx++)
            {
                UnitWaters[idx] = new CellUnitWaterE(gameW, idx);
                UnitHps[idx] = new CellUnitHpE(gameW, idx);
                IceWalls[idx] = new CellIceWallEs(gameW, idx);
                UnitStuns[idx] = new CellUnitStunEs(gameW, idx);
            }



            new CellTrailEs(gameW);
            new CellBuildE(gameW);
            new CellEnvironmentEs(gameW);
            new CellFireEs(gameW);
            new EntityCellCloudPool(gameW);
            new CellRiverE(gameW);
            new CellEs(gameW, isActiveParenCells, idCells);
            new CellParentE(gameW);

            new CurrentDirectWindE(gameW);
            new CenterCloudEnt(gameW);
            new DirectsWindForElfemaleE(gameW);

            new AvailableCenterUpgradeEs(gameW);
            new AvailableCenterHeroEs(gameW);
            new UnitStatUpgradesEs(gameW);
            new BuildingUpgradesEs(gameW);

            new SelectedIdxE(gameW);
            new CurrentIdxE(gameW);

            new EntWhereEnviroments(gameW);
            new WhereUnitsE(gameW);
            new WhereBuildsE(gameW);

            new InventorUnitsE(gameW);
            new InventorResourcesE(gameW);
            new InventorToolWeaponE(gameW);

            new CellsForSetUnitsEs(gameW);
            new CellsForShiftUnitsEs(gameW);
            new CellsForAttackUnitsEs(gameW);
            new CellsForArsonArcherEs(gameW);

            new SelectedToolWeaponE(gameW);
            new WhoseMoveE(gameW);
            new MistakeE(gameW);
            new EntHint(gameW);
            new SelectedUnitE(gameW);
            new StatUnitsUpgradesE(gameW);
            new GetterUnitsEs(gameW);
            new SoundE(gameW, sounds0, sounds1);
            new SunSidesE(gameW);
            new SelectedUniqueAbilityC(gameW);



            new EntityMPool(gameW);
            new FreezeDirectEnemyME(gameW);
            new IceWallME(gameW);


            if (PhotonNetwork.IsMasterClient)
            {
                int random;

                foreach (byte idx_0 in Idxs)
                {
                    var xy_0 = Cell<XyC>(idx_0).Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    ref var cloud_0 = ref Cloud<HaveEffectC>(idx_0);

                    if (IsActiveC(idx_0).IsActive)
                    {
                        if (xy_0[1] >= 4 && xy_0[1] <= 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.Mountain))
                            {
                                SetNew(EnvironmentTypes.Mountain, idx_0);
                            }

                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.AdultForest))
                                {
                                    SetNew(EnvironmentTypes.AdultForest, idx_0);
                                }

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.Hill))
                                {
                                    SetNew(EnvironmentTypes.Hill, idx_0);
                                }
                            }
                        }

                        else
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.AdultForest))
                            {
                                SetNew(EnvironmentTypes.AdultForest, idx_0);
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= CellEnvironmentValues.StartPercentForSpawn(EnvironmentTypes.Fertilizer))
                                {
                                    SetNew(EnvironmentTypes.Fertilizer, idx_0);
                                }
                            }
                        }

                        if (xy_0[0] == 5 && xy_0[1] == 5)
                        {
                            cloud_0.Have = true;
                            CenterCloudEnt.CenterCloud<IdxC>().Idx = idx_0;

                            CellSpaceSupport.TryGetXyAround(xy_0, out var dirs);
                            foreach (var item in dirs)
                            {
                                var idx_1 = IdxCell(item.Value);
                                //WindC.Set(item.Key, idx_1);
                            }
                        }


                        ref var river_0 = ref River(idx_0);


                        var corners = new List<DirectTypes>();

                        if (x >= 3 && x <= 6 && y == 5)
                        {
                            CellRiverE.SetStart(idx_0, DirectTypes.Up);
                        }
                        else if (x == 7 && y == 5)
                        {
                            corners.Add(DirectTypes.UpRight);
                            corners.Add(DirectTypes.Down);
                            CellRiverE.SetStart(idx_0, DirectTypes.Up, DirectTypes.Right);
                        }
                        else if (x >= 8 && x <= 12 && y == 4)
                        {
                            CellRiverE.SetStart(idx_0, DirectTypes.Up);
                        }


                        foreach (var dir in CellRiverE.Keys)
                        {
                            if (CellRiverE.HaveRive(dir, idx_0).Have)
                            {
                                var xy_next = CellSpaceSupport.GetXyCellByDirect(Cell<XyC>(idx_0).Xy, dir);
                                var idx_next = IdxCell(xy_next);

                                CellRiverE.River(idx_next).River = RiverTypes.End;
                            }
                        }

                        foreach (var dir in corners)
                        {
                            var xy_next = CellSpaceSupport.GetXyCellByDirect(Cell<XyC>(idx_0).Xy, dir);
                            var idx_next = IdxCell(xy_next);

                            CellRiverE.River(idx_next).River = RiverTypes.Corner;
                        }
                    }
                }
            }

            if (GameModeC.IsGameMode(GameModes.TrainingOff))
            {
                InventorResourcesE.Resource(ResourceTypes.Food, PlayerTypes.Second).Amount = 999999;

                foreach (byte idx_0 in Idxs)
                {
                    var xy_0 = Cell<XyC>(idx_0).Xy;
                    var x = xy_0[0];
                    var y = xy_0[1];

                    ref var unit_0 = ref Unit(idx_0);
                    ref var levUnit_0 = ref UnitElse.Level(idx_0);
                    ref var ownUnit_0 = ref UnitElse.Owner(idx_0);

                    ref var hp_0 = ref EntitiesPool.UnitHps[idx_0].Hp;
                    ref var condUnit_0 = ref EntitiesPool.UnitElse.Condition(idx_0);
                    ref var waterUnit_0 = ref EntitiesPool.UnitWaters[idx_0].Water;


                    ref var tw_0 = ref CellUnitTWE.UnitTW<ToolWeaponC>(idx_0);
                    ref var twLevel_0 = ref CellUnitTWE.UnitTW<LevelTC>(idx_0);
                    ref var protShiel_0 = ref CellUnitTWE.UnitTW<ProtectionC>(idx_0);

                    ref var build_0 = ref Build<BuildingTC>(idx_0);
                    ref var ownBuild_0 = ref Build<PlayerTC>(idx_0);

                    if (x == 7 && y == 8)
                    {
                        Remove(EnvironmentTypes.Mountain, idx_0);
                        Remove(EnvironmentTypes.AdultForest, idx_0);

                        CellUnitEs.SetNew((UnitTypes.King, LevelTypes.First, PlayerTypes.Second, default, default), idx_0);

                        condUnit_0.Condition = ConditionUnitTypes.Protected;
                    }

                    else if (x == 8 && y == 8)
                    {
                        Remove(EnvironmentTypes.Mountain, idx_0);
                        Remove(EnvironmentTypes.AdultForest, idx_0);


                        CellBuildE.SetNew(BuildingTypes.City, PlayerTypes.Second, idx_0);
                    }

                    else if (x == 6 && y == 8 || x == 9 && y == 8 || x <= 9 && x >= 6 && y == 7 || x <= 9 && x >= 6 && y == 9)
                    {
                        Remove(EnvironmentTypes.Mountain, idx_0);

                        SetNew((UnitTypes.Pawn, LevelTypes.First, PlayerTypes.Second, default, default), idx_0);


                        int rand = UnityEngine.Random.Range(0, 100);

                        if (rand >= 50)
                        {
                            CellUnitTWE.SetNew(idx_0, ToolWeaponTypes.Sword, LevelTypes.Second);
                        }
                        else
                        {
                            CellUnitTWE.SetNew(idx_0, ToolWeaponTypes.Shield, LevelTypes.First);
                        }

                        condUnit_0.Condition = ConditionUnitTypes.Protected;
                    }
                }
            }
        }
    }
}