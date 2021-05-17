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

        if (_eGM.CellUnitComponent(xyPreviousCell).MinAmountSteps)
        {
            if (_eGM.CellUnitComponent(xyPreviousCell).IsHisUnit(info.Sender))
            {
                if (_eGM.CellUnitComponent(xySelectedCell).HaveUnit)
                {
                    var isFindedSimple = InstanceGame.CellManager.CellBaseOperations.TryFindCellInList(xySelectedCell, availableCellsSimpleAttack);
                    var isFindedUnique = InstanceGame.CellManager.CellBaseOperations.TryFindCellInList(xySelectedCell, availableCellsUniqueAttack);

                    if (isFindedSimple || isFindedUnique)
                    {
                        _eGM.CellUnitComponent(xyPreviousCell).AmountSteps = 0;
                        _eGM.CellUnitComponent(xyPreviousCell).IsProtected = false;
                        _eGM.CellUnitComponent(xyPreviousCell).IsRelaxed = false;

                        int damageToPrevious = 0;

                        if (_eGM.CellUnitComponent(xyPreviousCell).UnitType != UnitTypes.Rook && _eGM.CellUnitComponent(xyPreviousCell).UnitType != UnitTypes.Bishop)
                            damageToPrevious += _eGM.CellUnitComponent(xySelectedCell).SimplePowerDamage;



                        int damageToSelelected = 0;

                        damageToSelelected += _eGM.CellUnitComponent(xyPreviousCell).SimplePowerDamage;
                        if (isFindedUnique) damageToSelelected += _eGM.CellUnitComponent(xyPreviousCell).UniquePowerDamage;
                        damageToSelelected -= _eGM.CellUnitComponent(xySelectedCell).PowerProtection
                            (_eGM.CellEnvironmentComponent(xySelectedCell).ListEnvironmentTypes, _eGM.CellBuildingComponent(xySelectedCell).BuildingType);

                        if (damageToSelelected < 0) damageToSelelected = 0;


                        _eGM.CellUnitComponent(xyPreviousCell).AmountHealth -= damageToPrevious;
                        _eGM.CellUnitComponent(xySelectedCell).AmountHealth -= damageToSelelected;

                        bool isKilledAttacked = false;
                        bool isKilledDefender = false;

                        if (_eGM.CellUnitComponent(xyPreviousCell).AmountHealth <= StartValuesGameConfig.AMOUNT_FOR_DEATH)
                        {
                            _eGM.CellUnitComponent(xyPreviousCell).ResetUnit();
                            isKilledAttacked = true;
                        }

                        if (_eGM.CellUnitComponent(xySelectedCell).AmountHealth <= StartValuesGameConfig.AMOUNT_FOR_DEATH)
                        {
                            if (_eGM.CellUnitComponent(xySelectedCell).UnitType == UnitTypes.King) EndGame(_eGM.CellUnitComponent(xyPreviousCell).ActorNumber);

                            _eGM.CellUnitComponent(xySelectedCell).ResetUnit();
                            if (_eGM.CellUnitComponent(xyPreviousCell).UnitType != UnitTypes.Rook && _eGM.CellUnitComponent(xyPreviousCell).UnitType != UnitTypes.Bishop)
                            {
                                _eGM.CellUnitComponent(xySelectedCell).SetUnit(_eGM.CellUnitComponent(xyPreviousCell));
                                _eGM.CellUnitComponent(xyPreviousCell).ResetUnit();
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
            _eGM.SelectorComponentSelectorEnt.AttackUnitAction();
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

        if (_eGM.CellUnitComponent(xyPreviousCell).IsHisUnit(info.Sender) && _eGM.CellUnitComponent(xyPreviousCell).MinAmountSteps)
        {
            if (InstanceGame.CellManager.CellBaseOperations.TryFindCellInList(xySelectedCell, xyAvailableCellsForShift))
            {
                _eGM.CellUnitComponent(xySelectedCell).SetUnit(_eGM.CellUnitComponent(xyPreviousCell));


                _eGM.CellUnitComponent(xyPreviousCell).ResetUnit();


                _eGM.CellUnitComponent(xySelectedCell).AmountSteps
                    -= _eGM.CellUnitComponent(xySelectedCell).NeedAmountSteps(_eGM.CellEnvironmentComponent(xySelectedCell).ListEnvironmentTypes);
                if (_eGM.CellUnitComponent(xySelectedCell).AmountSteps < 0) _eGM.CellUnitComponent(xySelectedCell).AmountSteps = 0;

                _eGM.CellUnitComponent(xySelectedCell).IsProtected = false;
                _eGM.CellUnitComponent(xySelectedCell).IsRelaxed = false;
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
            if (!_eGM.CellUnitComponent(xyCell).IsProtected)
            {
                if (_eGM.CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    _eGM.CellUnitComponent(xyCell).IsProtected = true;
                    _eGM.CellUnitComponent(xyCell).IsRelaxed = false;
                    _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                }
            }
        }

        else
        {
            if (_eGM.CellUnitComponent(xyCell).IsProtected)
            {
                if (_eGM.CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    _eGM.CellUnitComponent(xyCell).IsProtected = false;
                    _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
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
            if (!_eGM.CellUnitComponent(xyCell).IsRelaxed)
            {
                if (_eGM.CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    _eGM.CellUnitComponent(xyCell).IsRelaxed = true;
                    _eGM.CellUnitComponent(xyCell).IsProtected = false;
                    _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                }
            }
        }

        else
        {
            if (_eGM.CellUnitComponent(xyCell).IsRelaxed)
            {
                if (_eGM.CellUnitComponent(xyCell).HaveMaxSteps)
                {
                    _eGM.CellUnitComponent(xyCell).IsRelaxed = false;
                    _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
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

        if (!_eGM.CellEnvironmentComponent(xyCell).HaveMountain && _eGM.CellUnitComponent(xyCell).HaveMaxSteps && !_eGM.CellBuildingComponent(xyCell).HaveBuilding)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    isBuilded = false;
                    break;

                case BuildingTypes.City:

                    _eGM.CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                    isBuilded = true;
                    _eGM.CellUnitComponent(xyCell).AmountSteps = 0;

                    if (info.Sender.IsMasterClient)
                    {
                        _eGM.BuildingsInfoComponent.IsBuildedCityMaster = isBuilded;
                        InstanceGame.CellManager.CellBaseOperations.CopyXYinTo(xyCell, _eGM.BuildingsInfoComponent.XYsettedCityMaster);

                        //_zoneComponentRef.Unref().XYMasterZone = InstanceGame.CellManager.CellFinderWay.TryGetXYAround(xyCellIN);
                    }
                    else
                    {
                        _eGM.BuildingsInfoComponent.IsBuildedCityOther = isBuilded;
                        InstanceGame.CellManager.CellBaseOperations.CopyXYinTo(xyCell, _eGM.BuildingsInfoComponent.XYsettedCityOther);

                        //_zoneComponentRef.Unref().XYOtherZone = InstanceGame.CellManager.CellFinderWay.TryGetXYAround(xyCellIN);
                    }

                    if (_eGM.CellEnvironmentComponent(xyCell).HaveTree) _eGM.CellEnvironmentComponent(xyCell).SetResetEnvironment(false, EnvironmentTypes.Tree);
                    if (_eGM.CellEnvironmentComponent(xyCell).HaveFood) _eGM.CellEnvironmentComponent(xyCell).SetResetEnvironment(false, EnvironmentTypes.Food);

                    break;


                case BuildingTypes.Farm:

                    if (_eGM.CellEnvironmentComponent(xyCell).HaveFood)
                    {
                        if (info.Sender.IsMasterClient)
                        {
                            haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                            haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                            haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                            haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_FARM;
                            haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]>= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;

                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]-= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;
                                _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                                _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                                _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                                _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_FARM;


                                _eGM.CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                                _eGM.BuildingsInfoComponent.AmountFarmMaster += 1; // !!!!!

                                _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                            }
                        }
                        else
                        {
                            haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                            haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                            haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                            haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_FARM;
                            haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;

                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;
                                _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                                _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                                _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                                _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_FARM;

                                _eGM.CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                                _eGM.BuildingsInfoComponent.AmountFarmOther += 1; // !!!!!

                                _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                            }
                        }
                    }
                    isBuilded = true;

                    break;

                case BuildingTypes.Woodcutter:

                    if (_eGM.CellEnvironmentComponent(xyCell).HaveTree)
                    {
                        if (info.Sender.IsMasterClient)
                        {
                            haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                            haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                            haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                            haveIron = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;
                            haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]>= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;

                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {

                                _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                                _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                                _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                                _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;
                                _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;

                                _eGM.CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                                _eGM.BuildingsInfoComponent.AmountWoodcutterMaster += 1; // !!!!!

                                _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                            }
                        }
                        else
                        {
                            haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                            haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                            haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                            haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;
                            haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;

                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {                              
                                _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                                _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                                _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                                _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;
                                _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;


                                _eGM.CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                                _eGM.BuildingsInfoComponent.AmountWoodcutterOther += 1; // !!!!!

                                _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                            }
                        }

                    }
                    isBuilded = true;

                    break;

                case BuildingTypes.Mine:

                    if (_eGM.CellEnvironmentComponent(xyCell).HaveHill)
                    {
                        if (info.Sender.IsMasterClient)
                        {
                            haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                            haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                            haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_MINE;
                            haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_MINE;
                            haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]>= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_MINE;


                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]-= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_MINE;
                                _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                                _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                                _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_MINE;
                                _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_MINE;


                                _eGM.CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                                _eGM.BuildingsInfoComponent.AmountMineMaster += 1; // !!!!!

                                _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                            }
                        }
                        else
                        {
                            haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                            haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                            haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_MINE;
                            haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_MINE;
                            haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_MINE;

                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.GOLD_FOR_BUILDING_MINE;
                                _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                                _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                                _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.ORE_FOR_BUILDING_MINE;
                                _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= InstanceGame.StartValuesGameConfig.IRON_FOR_BUILDING_MINE;


                                _eGM.CellBuildingComponent(xyCell).SetBuilding(buildingType, info.Sender);
                                _eGM.BuildingsInfoComponent.AmountMineOther += 1; // !!!!!

                                _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
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


    #region Unique Abilities Pawn

    public void UniqueAbilityPawnToMaster(int[] xy, UniqueAbilitiesPawnTypes uniqueAbilitiesPawnType) 
        => _photonView.RPC(nameof(UniqueAbilityPawnMaster), RpcTarget.MasterClient,xy, uniqueAbilitiesPawnType);

    [PunRPC]
    private void UniqueAbilityPawnMaster(int[] xy, UniqueAbilitiesPawnTypes uniqueAbilitiesPawnType, PhotonMessageInfo info)
    {
        switch (uniqueAbilitiesPawnType)
        {
            case UniqueAbilitiesPawnTypes.AbilityOne:



                break;

            case UniqueAbilitiesPawnTypes.AbilityTwo:
                break;

            case UniqueAbilitiesPawnTypes.AbilityThree:
                break;

            default:
                break;
        }

        RefreshAllToMaster();
    }

    #endregion


    #region Destroy

    internal void DestroyBuilding(int[] xyCell) => _photonView.RPC(nameof(DestroyBuildingMaster), RpcTarget.MasterClient, xyCell);

    [PunRPC]
    private void DestroyBuildingMaster(int[] xyCell, PhotonMessageInfo info)
    {
        if (_eGM.CellUnitComponent(xyCell).IsHisUnit(info.Sender))
        {
            if (_eGM.CellUnitComponent(xyCell).HaveMaxSteps)
            {
                if (info.Sender.IsMasterClient)
                {
                    switch (_eGM.CellBuildingComponent(xyCell).BuildingType)
                    {
                        case BuildingTypes.City:

                            EndGame(_eGM.CellUnitComponent(xyCell).ActorNumber);

                            break;


                        case BuildingTypes.Farm:

                            _eGM.BuildingsInfoComponent.AmountFarmMaster -= 1;
                            _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                            _eGM.CellBuildingComponent(xyCell).ResetBuilding();

                            break;


                        case BuildingTypes.Woodcutter:

                            _eGM.BuildingsInfoComponent.AmountWoodcutterMaster -= 1;
                            _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                            _eGM.CellBuildingComponent(xyCell).ResetBuilding();

                            break;

                        case BuildingTypes.Mine:

                            _eGM.BuildingsInfoComponent.AmountMineMaster -= 1;
                            _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                            _eGM.CellBuildingComponent(xyCell).ResetBuilding();

                            break;

                    }
                }

                else
                {
                    switch (_eGM.CellBuildingComponent(xyCell).BuildingType)
                    {
                        case BuildingTypes.City:

                            EndGame(_eGM.CellUnitComponent(xyCell).ActorNumber);

                            break;


                        case BuildingTypes.Farm:

                            _eGM.BuildingsInfoComponent.AmountFarmOther -= 1;
                            _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                            _eGM.CellBuildingComponent(xyCell).ResetBuilding();

                            break;

                        case BuildingTypes.Woodcutter:

                            _eGM.BuildingsInfoComponent.AmountWoodcutterOther -= 1;
                            _eGM.CellUnitComponent(xyCell).AmountSteps = 0;
                            _eGM.CellBuildingComponent(xyCell).ResetBuilding();

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
