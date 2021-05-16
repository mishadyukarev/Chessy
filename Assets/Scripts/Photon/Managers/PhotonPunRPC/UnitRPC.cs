using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using static MainGame;

internal partial class PhotonPunRPC
{

    #region AttackUnit

    internal void AttackUnit(int[] xyPreviousCell, int[] xySelectedCell) => _photonView.RPC(nameof(AttackUnitMaster), RpcTarget.MasterClient, xyPreviousCell, xySelectedCell);

    [PunRPC]
    private void AttackUnitMaster(int[] xyPreviousCell, int[] xySelectedCell, PhotonMessageInfo info)
    {
        var isAttacked = false;


        InstanceGame.CellManager.CellFinderWay.GetCellsForAttack(xyPreviousCell, info.Sender, out var availableCellsSimpleAttack, out var availableCellsUniqueAttack);

        if (CellUnitComponent(xyPreviousCell).MinAmountSteps)
        {
            if (CellUnitComponent(xyPreviousCell).IsHisUnit(info.Sender))
            {
                if (CellUnitComponent(xySelectedCell).HaveUnit)
                {
                    var isFindedSimple = InstanceGame.CellManager.CellBaseOperations.TryFindCellInList(xySelectedCell, availableCellsSimpleAttack);
                    var isFindedUnique = InstanceGame.CellManager.CellBaseOperations.TryFindCellInList(xySelectedCell, availableCellsUniqueAttack);

                    if (isFindedSimple || isFindedUnique)
                    {
                        CellUnitComponent(xyPreviousCell).AmountSteps = 0;
                        CellUnitComponent(xyPreviousCell).IsProtected = false;
                        CellUnitComponent(xyPreviousCell).IsRelaxed = false;

                        int damageToPrevious = 0;

                        if (CellUnitComponent(xyPreviousCell).UnitType != UnitTypes.Rook && CellUnitComponent(xyPreviousCell).UnitType != UnitTypes.Bishop)
                            damageToPrevious += CellUnitComponent(xySelectedCell).SimplePowerDamage;



                        int damageToSelelected = 0;

                        damageToSelelected += CellUnitComponent(xyPreviousCell).SimplePowerDamage;
                        if (isFindedUnique) damageToSelelected += CellUnitComponent(xyPreviousCell).UniquePowerDamage;
                        damageToSelelected -= CellUnitComponent(xySelectedCell).PowerProtection
                            (CellEnvironmentComponent(xySelectedCell).ListEnvironmentTypes, CellBuildingComponent(xySelectedCell).BuildingType);

                        if (damageToSelelected < 0) damageToSelelected = 0;


                        CellUnitComponent(xyPreviousCell).AmountHealth -= damageToPrevious;
                        CellUnitComponent(xySelectedCell).AmountHealth -= damageToSelelected;

                        bool isKilledAttacked = false;
                        bool isKilledDefender = false;

                        if (CellUnitComponent(xyPreviousCell).AmountHealth <= _startValuesGameConfig.AMOUNT_FOR_DEATH)
                        {
                            CellUnitComponent(xyPreviousCell).ResetUnit();
                            isKilledAttacked = true;
                        }

                        if (CellUnitComponent(xySelectedCell).AmountHealth <= _startValuesGameConfig.AMOUNT_FOR_DEATH)
                        {
                            if (CellUnitComponent(xySelectedCell).UnitType == UnitTypes.King) EndGame(CellUnitComponent(xyPreviousCell).ActorNumber);

                            CellUnitComponent(xySelectedCell).ResetUnit();
                            if (CellUnitComponent(xyPreviousCell).UnitType != UnitTypes.Rook && CellUnitComponent(xyPreviousCell).UnitType != UnitTypes.Bishop)
                            {
                                CellUnitComponent(xySelectedCell).SetUnit(CellUnitComponent(xyPreviousCell));
                                CellUnitComponent(xyPreviousCell).ResetUnit();
                            }
                            isKilledDefender = true;
                        }

                        isAttacked = true;
                    }
                }
            }
        }

        _photonView.RPC(nameof(AttackUnitGeneral), info.Sender, isAttacked);
        if (isAttacked) _photonView.RPC(nameof(AttackUnitGeneral), RpcTarget.All);

        RefreshAllToMaster();
    }

    [PunRPC]
    private void AttackUnitGeneral(bool isAttacked)
    {
        if (isAttacked)
        {
            _selectorComponentRef.Unref().AttackUnitAction();
        }
    }

    [PunRPC]
    private void AttackUnitGeneral() => _soundComponentRef.Unref().AttackSoundAction();

    #endregion


    #region ShiftUnit

    internal void ShiftUnitToMaster(in int[] xyPreviousCell, in int[] xySelectedCell)
        => _photonView.RPC(nameof(ShiftUnitMaster), RpcTarget.MasterClient, xyPreviousCell, xySelectedCell);

    [PunRPC]
    private void ShiftUnitMaster(int[] xyPreviousCell, int[] xySelectedCell, PhotonMessageInfo info)
    {
        List<int[]> xyAvailableCellsForShift = InstanceGame.CellManager.CellFinderWay.GetCellsForShift(xyPreviousCell);

        if (CellUnitComponent(xyPreviousCell).IsHisUnit(info.Sender) && CellUnitComponent(xyPreviousCell).MinAmountSteps)
        {
            if (InstanceGame.CellManager.CellBaseOperations.TryFindCellInList(xySelectedCell, xyAvailableCellsForShift))
            {
                CellUnitComponent(xySelectedCell).SetUnit(CellUnitComponent(xyPreviousCell));


                CellUnitComponent(xyPreviousCell).ResetUnit();


                CellUnitComponent(xySelectedCell).AmountSteps
                    -= CellUnitComponent(xySelectedCell).NeedAmountSteps(CellEnvironmentComponent(xySelectedCell).ListEnvironmentTypes);
                if (CellUnitComponent(xySelectedCell).AmountSteps < 0) CellUnitComponent(xySelectedCell).AmountSteps = 0;

                CellUnitComponent(xySelectedCell).IsProtected = false;
                CellUnitComponent(xySelectedCell).IsRelaxed = false;
            }
        }

        RefreshAllToMaster();
    }

    #endregion


    #region Protect

    public void ProtectUnitToMaster(bool isActive, in int[] xyCell)
        => _photonView.RPC(nameof(ProtectUnitMaster), RpcTarget.MasterClient, isActive, xyCell);

    [PunRPC]
    private void ProtectUnitMaster(bool isActive, int[] xyCell, PhotonMessageInfo info)
    {
        if (isActive)
        {
            if (!CellUnitComponent(xyCell).IsProtected)
            {
                if (CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    CellUnitComponent(xyCell).IsProtected = true;
                    CellUnitComponent(xyCell).IsRelaxed = false;
                    CellUnitComponent(xyCell).AmountSteps = 0;
                }
            }
        }

        else
        {
            if (CellUnitComponent(xyCell).IsProtected)
            {
                if (CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    CellUnitComponent(xyCell).IsProtected = false;
                    CellUnitComponent(xyCell).AmountSteps = 0;
                }
            }
        }

        RefreshAllToMaster();
    }

    #endregion


    #region Relax

    public void RelaxUnitToMaster(bool isActive, in int[] xyCell) => _photonView.RPC(nameof(RelaxUnitMaster), RpcTarget.MasterClient, isActive, xyCell);

    [PunRPC]
    private void RelaxUnitMaster(bool isActive, int[] xyCell, PhotonMessageInfo info)
    {
        if (isActive)
        {
            if (!CellUnitComponent(xyCell).IsRelaxed)
            {
                if (CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    CellUnitComponent(xyCell).IsRelaxed = true;
                    CellUnitComponent(xyCell).IsProtected = false;
                    CellUnitComponent(xyCell).AmountSteps = 0;
                }
            }
        }

        else
        {
            if (CellUnitComponent(xyCell).IsRelaxed)
            {
                if (CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    CellUnitComponent(xyCell).IsRelaxed = false;
                    CellUnitComponent(xyCell).AmountSteps = 0;
                }
            }
        }
        RefreshAllToMaster();
    }

    #endregion


    #region Build

    internal void Build(int[] xyCell, BuildingTypes buildingType) => _photonView.RPC("BuildMaster", RpcTarget.MasterClient, xyCell, buildingType);

    [PunRPC]
    private void BuildMaster(int[] xyCell, BuildingTypes buildingType, PhotonMessageInfo info)
    {
        bool haveFood = true;
        bool haveWood = true;
        bool haveOre = true;
        bool haveIron = true;
        bool haveGold = true;

        bool isBuilded;

        if (!CellEnvironmentComponent(xyCell).HaveMountain && CellUnitComponent(xyCell).HaveMaxSteps && !CellBuildingComponent(xyCell).HaveBuilding)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    isBuilded = false;
                    break;

                case BuildingTypes.City:

                    CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                    isBuilded = true;
                    CellUnitComponent(xyCell).AmountSteps = 0;

                    if (info.Sender.IsMasterClient)
                    {
                        BuildingsInfoComponent.IsBuildedCityMaster = isBuilded;
                        InstanceGame.CellManager.CellBaseOperations.CopyXYinTo(xyCell, BuildingsInfoComponent.XYsettedCityMaster);

                        //_zoneComponentRef.Unref().XYMasterZone = InstanceGame.CellManager.CellFinderWay.TryGetXYAround(xyCellIN);
                    }
                    else
                    {
                        BuildingsInfoComponent.IsBuildedCityOther = isBuilded;
                        InstanceGame.CellManager.CellBaseOperations.CopyXYinTo(xyCell, BuildingsInfoComponent.XYsettedCityOther);

                        //_zoneComponentRef.Unref().XYOtherZone = InstanceGame.CellManager.CellFinderWay.TryGetXYAround(xyCellIN);
                    }

                    if (CellEnvironmentComponent(xyCell).HaveTree) CellEnvironmentComponent(xyCell).SetResetEnvironment(false, EnvironmentTypes.Tree);
                    if (CellEnvironmentComponent(xyCell).HaveFood) CellEnvironmentComponent(xyCell).SetResetEnvironment(false, EnvironmentTypes.Food);

                    break;


                case BuildingTypes.Farm:

                    if (CellEnvironmentComponent(xyCell).HaveFood)
                    {
                        if (info.Sender.IsMasterClient)
                        {
                            haveFood = EconomyComponent.FoodMaster >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                            haveWood = EconomyComponent.WoodMaster >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                            haveOre = EconomyComponent.OreMaster >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                            haveIron = EconomyComponent.IronMaster >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_FARM;
                            haveGold = EconomyComponent.GoldMaster >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;

                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                EconomyComponent.GoldMaster -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;
                                EconomyComponent.FoodMaster -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                                EconomyComponent.WoodMaster -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                                EconomyComponent.OreMaster -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                                EconomyComponent.IronMaster -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_FARM;


                                CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                                BuildingsInfoComponent.AmountFarmMaster += 1; // !!!!!

                                CellUnitComponent(xyCell).AmountSteps = 0;
                            }
                        }
                        else
                        {
                            haveFood = EconomyComponent.FoodOther >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                            haveWood = EconomyComponent.WoodOther >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                            haveOre = EconomyComponent.OreOther >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                            haveIron = EconomyComponent.IronOther >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_FARM;
                            haveGold = EconomyComponent.GoldOther >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;

                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                EconomyComponent.GoldOther -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;
                                EconomyComponent.FoodOther -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                                EconomyComponent.WoodOther -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                                EconomyComponent.OreOther -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                                EconomyComponent.IronOther -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_FARM;

                                CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                                BuildingsInfoComponent.AmountFarmOther += 1; // !!!!!

                                CellUnitComponent(xyCell).AmountSteps = 0;
                            }
                        }
                    }
                    isBuilded = true;

                    break;

                case BuildingTypes.Woodcutter:

                    if (CellEnvironmentComponent(xyCell).HaveTree)
                    {
                        if (info.Sender.IsMasterClient)
                        {
                            haveFood = EconomyComponent.FoodMaster >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                            haveWood = EconomyComponent.WoodMaster >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                            haveOre = EconomyComponent.OreMaster >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                            haveIron = EconomyComponent.IronMaster >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;
                            haveGold = EconomyComponent.GoldMaster >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;

                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                EconomyComponent.GoldMaster -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;
                                EconomyComponent.FoodMaster -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                                EconomyComponent.WoodMaster -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                                EconomyComponent.OreMaster -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                                EconomyComponent.IronMaster -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;

                                CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                                BuildingsInfoComponent.AmountWoodcutterMaster += 1; // !!!!!

                                CellUnitComponent(xyCell).AmountSteps = 0;
                            }
                        }
                        else
                        {
                            haveFood = EconomyComponent.FoodOther >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                            haveWood = EconomyComponent.WoodOther >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                            haveOre = EconomyComponent.OreOther >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                            haveIron = EconomyComponent.IronOther >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;
                            haveGold = EconomyComponent.GoldOther >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;

                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                EconomyComponent.GoldOther -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;
                                EconomyComponent.FoodOther -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                                EconomyComponent.WoodOther -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                                EconomyComponent.OreOther -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                                EconomyComponent.IronOther -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;


                                CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                                BuildingsInfoComponent.AmountWoodcutterOther += 1; // !!!!!

                                CellUnitComponent(xyCell).AmountSteps = 0;
                            }
                        }

                    }
                    isBuilded = true;

                    break;

                case BuildingTypes.Mine:

                    if (CellEnvironmentComponent(xyCell).HaveHill)
                    {
                        if (info.Sender.IsMasterClient)
                        {
                            haveFood = EconomyComponent.FoodMaster >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                            haveWood = EconomyComponent.WoodMaster >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                            haveOre = EconomyComponent.OreMaster >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_MINE;
                            haveIron = EconomyComponent.IronMaster >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_MINE;
                            haveGold = EconomyComponent.GoldMaster >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_MINE;


                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                EconomyComponent.GoldMaster -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_MINE;
                                EconomyComponent.FoodMaster -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                                EconomyComponent.WoodMaster -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                                EconomyComponent.OreMaster -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_MINE;
                                EconomyComponent.IronMaster -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_MINE;


                                CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                                BuildingsInfoComponent.AmountMineMaster += 1; // !!!!!

                                CellUnitComponent(xyCell).AmountSteps = 0;
                            }
                        }
                        else
                        {
                            haveFood = EconomyComponent.FoodOther >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                            haveWood = EconomyComponent.WoodOther >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                            haveOre = EconomyComponent.OreOther >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_MINE;
                            haveIron = EconomyComponent.IronOther >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_MINE;
                            haveGold = EconomyComponent.GoldOther >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_MINE;

                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                EconomyComponent.GoldOther -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_MINE;
                                EconomyComponent.FoodOther -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                                EconomyComponent.WoodOther -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                                EconomyComponent.OreOther -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_MINE;
                                EconomyComponent.IronOther -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_MINE;


                                CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                                BuildingsInfoComponent.AmountMineOther += 1; // !!!!!

                                CellUnitComponent(xyCell).AmountSteps = 0;
                            }
                        }

                    }
                    isBuilded = true;

                    break;

                default:
                    isBuilded = false;
                    break;
            }
        }
        else
        {
            isBuilded = false;
        }

        MistakeEconomy(info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);

        RefreshAllToMaster();
    }

    #endregion


    #region Destroy

    internal void DestroyBuilding(int[] xyCell) => _photonView.RPC(nameof(DestroyBuildingMaster), RpcTarget.MasterClient, xyCell);

    [PunRPC]
    private void DestroyBuildingMaster(int[] xyCell, PhotonMessageInfo info)
    {
        if (CellUnitComponent(xyCell).IsHisUnit(info.Sender))
        {
            if (CellUnitComponent(xyCell).HaveMaxSteps)
            {
                if (info.Sender.IsMasterClient)
                {
                    switch (CellBuildingComponent(xyCell).BuildingType)
                    {
                        case BuildingTypes.City:

                            EndGame(CellUnitComponent(xyCell).ActorNumber);

                            break;


                        case BuildingTypes.Farm:

                            BuildingsInfoComponent.AmountFarmMaster -= 1;
                            CellUnitComponent(xyCell).AmountSteps = 0;
                            CellBuildingComponent(xyCell).ResetBuilding();

                            break;


                        case BuildingTypes.Woodcutter:

                            BuildingsInfoComponent.AmountWoodcutterMaster -= 1;
                            CellUnitComponent(xyCell).AmountSteps = 0;
                            CellBuildingComponent(xyCell).ResetBuilding();

                            break;

                        case BuildingTypes.Mine:

                            BuildingsInfoComponent.AmountMineMaster -= 1;
                            CellUnitComponent(xyCell).AmountSteps = 0;
                            CellBuildingComponent(xyCell).ResetBuilding();

                            break;

                    }
                }

                else
                {
                    switch (CellBuildingComponent(xyCell).BuildingType)
                    {
                        case BuildingTypes.City:

                            EndGame(CellUnitComponent(xyCell).ActorNumber);

                            break;


                        case BuildingTypes.Farm:

                            BuildingsInfoComponent.AmountFarmOther -= 1;
                            CellUnitComponent(xyCell).AmountSteps = 0;
                            CellBuildingComponent(xyCell).ResetBuilding();

                            break;

                        case BuildingTypes.Woodcutter:

                            BuildingsInfoComponent.AmountWoodcutterOther -= 1;
                            CellUnitComponent(xyCell).AmountSteps = 0;
                            CellBuildingComponent(xyCell).ResetBuilding();

                            break;

                        case BuildingTypes.Mine:
                            break;
                    }
                }

            }
        }
        RefreshAllToMaster();
    }

    #endregion

}
