using Photon.Pun;
using static MainGame;

internal partial class PhotonPunRPC //EconomyRPC
{

    #region CreateUnit

    internal void CreateUnit(in UnitTypes unitType) => PhotonView.RPC(nameof(CreateUnitMaster), RpcTarget.MasterClient, unitType);

    [PunRPC]
    private void CreateUnitMaster(UnitTypes unitType, PhotonMessageInfo info)
    {
        bool haveFood = true;
        bool haveWood = true;
        bool haveOre = true;
        bool haveIron = true;
        bool haveGold = true;

        switch (unitType)
        {
            case UnitTypes.None:
                break;

            case UnitTypes.King:
                break;

            case UnitTypes.Pawn:

                if (info.Sender.IsMasterClient)
                {
                    haveFood = _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_BUYING_PAWN;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_BUYING_PAWN;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_BUYING_PAWN;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                        _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                        _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_BUYING_PAWN;
                        _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_BUYING_PAWN;
                        _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_BUYING_PAWN;

                        _eGM.InfoEntityUnitsInfoComponent.AmountPawnDict[info.Sender.IsMasterClient] += StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                    }
                }
                else
                {
                    haveFood = _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_BUYING_PAWN;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_BUYING_PAWN;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_BUYING_PAWN;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                        _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                        _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_BUYING_PAWN;
                        _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_BUYING_PAWN;
                        _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_BUYING_PAWN;

                        _eGM.InfoEntityUnitsInfoComponent.AmountPawnDict[info.Sender.IsMasterClient] += StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                    }
                }

                break;

            case UnitTypes.Rook:

                if (info.Sender.IsMasterClient)
                {
                    haveFood = _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_BUYING_ROOK;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_BUYING_ROOK;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_BUYING_ROOK;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                        _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                        _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_BUYING_ROOK;
                        _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_BUYING_ROOK;
                        _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_BUYING_ROOK;

                        _eGM.InfoEntityUnitsInfoComponent.AmountRookDict[info.Sender.IsMasterClient] += 1;
                    }
                }
                else
                {
                    haveFood = _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_BUYING_ROOK;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_BUYING_ROOK;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_BUYING_ROOK;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEAmountDictC.AmountDict[InstanceGame.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                        _eGM.WoodEAmountDictC.AmountDict[InstanceGame.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                        _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_BUYING_ROOK;
                        _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_BUYING_ROOK;
                        _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_BUYING_ROOK;

                        _eGM.InfoEntityUnitsInfoComponent.AmountRookDict[info.Sender.IsMasterClient] += 1;
                    }
                }

                break;


            case UnitTypes.Bishop:

                if (info.Sender.IsMasterClient)
                {
                    haveFood = _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_BUYING_BISHOP;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                        _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                        _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                        _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                        _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_BUYING_BISHOP;

                        _eGM.InfoEntityUnitsInfoComponent.AmountBishopDict[info.Sender.IsMasterClient] += 1;
                    }
                }
                else
                {
                    haveFood = _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_BUYING_BISHOP;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                        _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                        _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                        _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                        _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_BUYING_BISHOP;

                        _eGM.InfoEntityUnitsInfoComponent.AmountBishopDict[info.Sender.IsMasterClient] += 1;
                    }
                }

                break;


            default:
                break;
        }

        MistakeEconomy(info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);

        RefreshAllToMaster();
    }

    #endregion


    #region Upgrade

    internal void UpgradeUnitToMaster(UnitTypes unitType) => PhotonView.RPC(nameof(UpgradeUnitMaster), RpcTarget.MasterClient, unitType);

    [PunRPC]
    private void UpgradeUnitMaster(UnitTypes unitType, PhotonMessageInfo info)
    {
        bool haveFood = true;
        bool haveWood = true;
        bool haveOre = true;
        bool haveIron = true;
        bool haveGold = true;

        switch (unitType)
        {
            case UnitTypes.None:
                break;

            case UnitTypes.King:
                break;

            case UnitTypes.Pawn:

                if (info.Sender.IsMasterClient)
                {
                    haveFood = _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_UPGRADE_PAWN;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_UPGRADE_PAWN;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_UPGRADE_PAWN;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_UPGRADE_PAWN;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_UPGRADE_PAWN;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_UPGRADE_PAWN;
                        _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_UPGRADE_PAWN;
                        _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_UPGRADE_PAWN;
                        _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_UPGRADE_PAWN;
                        _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_UPGRADE_PAWN;

                        for (int x = 0; x < StartValuesGameConfig.CELL_COUNT_X; x++)
                            for (int y = 0; y < StartValuesGameConfig.CELL_COUNT_Y; y++)
                            {
                                _eGM.CellUnitComponent(x, y).AmountUpgradePawnMaster += 1;
                                if (_eGM.CellUnitComponent(x, y).UnitType == UnitTypes.Pawn && _eGM.CellUnitComponent(x, y).IsHisUnit(info.Sender)) _eGM.CellUnitComponent(x, y).AmountHealth += InstanceGame.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_PAWN;
                            }

                    }
                }
                else
                {
                    haveFood = _eGM.FoodEAmountDictC.AmountDict[InstanceGame.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_UPGRADE_PAWN;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[InstanceGame.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_UPGRADE_PAWN;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_UPGRADE_PAWN;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_UPGRADE_PAWN;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_UPGRADE_PAWN;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEAmountDictC.AmountDict[InstanceGame.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_UPGRADE_PAWN;
                        _eGM.WoodEAmountDictC.AmountDict[InstanceGame.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_UPGRADE_PAWN;
                        _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_UPGRADE_PAWN;
                        _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_UPGRADE_PAWN;
                        _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_UPGRADE_PAWN;

                        for (int x = 0; x < StartValuesGameConfig.CELL_COUNT_X; x++)
                            for (int y = 0; y < StartValuesGameConfig.CELL_COUNT_Y; y++)
                            {
                                _eGM.CellUnitComponent(x, y).AmountUpgradePawnOther += 1;
                                if (_eGM.CellUnitComponent(x, y).UnitType == UnitTypes.Pawn && _eGM.CellUnitComponent(x, y).IsHisUnit(info.Sender))
                                {
                                    _eGM.CellUnitComponent(x, y).AmountHealth += InstanceGame.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_PAWN;
                                }
                            }

                    }
                }

                break;


            case UnitTypes.Rook:

                if (info.Sender.IsMasterClient)
                {
                    haveFood = _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_UPGRADE_ROOK;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_UPGRADE_ROOK;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_UPGRADE_ROOK;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_UPGRADE_ROOK;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_UPGRADE_ROOK;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_UPGRADE_ROOK;
                        _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_UPGRADE_ROOK;
                        _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_UPGRADE_ROOK;
                        _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_UPGRADE_ROOK;
                        _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_UPGRADE_ROOK;

                        for (int x = 0; x < StartValuesGameConfig.CELL_COUNT_X; x++)
                            for (int y = 0; y < StartValuesGameConfig.CELL_COUNT_Y; y++)
                            {
                                _eGM.CellUnitComponent(x, y).AmountUpgradeRookMaster += 1;
                                if (_eGM.CellUnitComponent(x, y).UnitType == UnitTypes.Rook && _eGM.CellUnitComponent(x, y).IsHisUnit(info.Sender)) _eGM.CellUnitComponent(x, y).AmountHealth += InstanceGame.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_ROOK;
                            }

                    }
                }
                else
                {
                    haveFood = _eGM.FoodEAmountDictC.AmountDict[InstanceGame.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_UPGRADE_ROOK;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[InstanceGame.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_UPGRADE_ROOK;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_UPGRADE_ROOK;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_UPGRADE_ROOK;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_UPGRADE_ROOK;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEAmountDictC.AmountDict[InstanceGame.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_UPGRADE_ROOK;
                        _eGM.WoodEAmountDictC.AmountDict[InstanceGame.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_UPGRADE_ROOK;
                        _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_UPGRADE_ROOK;
                        _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_UPGRADE_ROOK;
                        _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_UPGRADE_ROOK;

                        for (int x = 0; x < StartValuesGameConfig.CELL_COUNT_X; x++)
                            for (int y = 0; y < StartValuesGameConfig.CELL_COUNT_Y; y++)
                            {
                                _eGM.CellUnitComponent(x, y).AmountUpgradeRookOther += 1;
                                if (_eGM.CellUnitComponent(x, y).UnitType == UnitTypes.Rook && _eGM.CellUnitComponent(x, y).IsHisUnit(info.Sender)) _eGM.CellUnitComponent(x, y).AmountHealth += InstanceGame.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_ROOK;
                            }

                    }
                }

                break;


            case UnitTypes.Bishop:

                if (info.Sender.IsMasterClient)
                {
                    haveFood = _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_UPGRADE_BISHOP;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_UPGRADE_BISHOP;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP;
                        _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP;
                        _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_UPGRADE_BISHOP;
                        _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_UPGRADE_BISHOP;
                        _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP;

                        for (int x = 0; x < StartValuesGameConfig.CELL_COUNT_X; x++)
                            for (int y = 0; y < StartValuesGameConfig.CELL_COUNT_Y; y++)
                            {
                                _eGM.CellUnitComponent(x, y).AmountUpgradeBishopMaster += 1;
                                if (_eGM.CellUnitComponent(x, y).UnitType == UnitTypes.Bishop && _eGM.CellUnitComponent(x, y).IsHisUnit(info.Sender)) _eGM.CellUnitComponent(x, y).AmountHealth += InstanceGame.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_BISHOP;
                            }

                    }
                }
                else
                {
                    haveFood = _eGM.FoodEAmountDictC.AmountDict[InstanceGame.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[InstanceGame.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_UPGRADE_BISHOP;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_UPGRADE_BISHOP;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEAmountDictC.AmountDict[InstanceGame.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP;
                        _eGM.WoodEAmountDictC.AmountDict[InstanceGame.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP;
                        _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_UPGRADE_BISHOP;
                        _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_UPGRADE_BISHOP;
                        _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP;

                        for (int x = 0; x < StartValuesGameConfig.CELL_COUNT_X; x++)
                            for (int y = 0; y < StartValuesGameConfig.CELL_COUNT_Y; y++)
                            {
                                _eGM.CellUnitComponent(x, y).AmountUpgradeBishopOther += 1;
                                if (_eGM.CellUnitComponent(x, y).UnitType == UnitTypes.Bishop && _eGM.CellUnitComponent(x, y).IsHisUnit(info.Sender)) _eGM.CellUnitComponent(x, y).AmountHealth += InstanceGame.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_BISHOP;
                            }

                    }
                }

                break;

            default:
                break;
        }

        MistakeEconomy(info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);

        RefreshAllToMaster();
    }

    #endregion


    #region MeltOre

    internal void MeltOre() => PhotonView.RPC(nameof(MeltOreMaster), RpcTarget.MasterClient);

    [PunRPC]
    private void MeltOreMaster(PhotonMessageInfo info)
    {
        bool haveFood = true;
        bool haveWood = true;
        bool haveOre = true;
        bool haveIron = true;
        bool haveGold = true;

        if (info.Sender.IsMasterClient)
        {
            haveFood = _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_MELTING_ORE;
            haveWood = _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_MELTING_ORE;
            haveOre = _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_MELTING_ORE;
            haveIron = _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_MELTING_ORE;
            haveGold = _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_MELTING_ORE;

            if (haveFood && haveWood && haveOre && haveIron && haveGold)
            {
                _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_MELTING_ORE;
                _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_MELTING_ORE;
                _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_MELTING_ORE;
                _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_MELTING_ORE;
                _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_MELTING_ORE;

                _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] += 1;
            }
        }
        else
        {
            haveFood = _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_MELTING_ORE;
            haveWood = _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_MELTING_ORE;
            haveOre = _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_MELTING_ORE;
            haveIron = _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_MELTING_ORE;
            haveGold = _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_MELTING_ORE;

            if (haveFood && haveWood && haveOre && haveIron && haveGold)
            {
                _eGM.FoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_MELTING_ORE;
                _eGM.WoodEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_MELTING_ORE;
                _eGM.OreEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_MELTING_ORE;
                _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_MELTING_ORE;
                _eGM.GoldEAmountDictC.AmountDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_MELTING_ORE;

                _eGM.IronEAmountDictC.AmountDict[info.Sender.IsMasterClient] += 1;
            }
        }

        MistakeEconomy(info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);

        RefreshAllToMaster();
    }

    #endregion


    #region GetUnit

    internal void GetUnit(UnitTypes unitTypes) => PhotonView.RPC(nameof(GetUnitMaster), RpcTarget.MasterClient, unitTypes);

    [PunRPC]
    private void GetUnitMaster(UnitTypes unitType, PhotonMessageInfo info)
    {
        bool isGetted = false;

        switch (unitType)
        {
            case UnitTypes.None:
                break;


            case UnitTypes.King:

                if (info.Sender.IsMasterClient)
                    isGetted = _eGM.InfoEntityUnitsInfoComponent.AmountKingDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                else isGetted = _eGM.InfoEntityUnitsInfoComponent.AmountKingDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                break;


            case UnitTypes.Pawn:

                if (info.Sender.IsMasterClient)
                {
                    if (_eGM.InfoEntityUnitsInfoComponent.AmountPawnDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }
                else
                {
                    if (_eGM.InfoEntityUnitsInfoComponent.AmountPawnDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }

                break;


            case UnitTypes.Rook:

                if (info.Sender.IsMasterClient)
                {
                    if (_eGM.InfoEntityUnitsInfoComponent.AmountRookDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }
                else
                {
                    if (_eGM.InfoEntityUnitsInfoComponent.AmountRookDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }

                break;


            case UnitTypes.Bishop:

                if (info.Sender.IsMasterClient)
                    isGetted = _eGM.InfoEntityUnitsInfoComponent.AmountBishopDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                else isGetted = _eGM.InfoEntityUnitsInfoComponent.AmountBishopDict[info.Sender.IsMasterClient] >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                break;


            default:
                break;

        }

        PhotonView.RPC(nameof(GetUnitGeneral), info.Sender, unitType, isGetted);

        RefreshAllToMaster();
    }

    [PunRPC]
    private void GetUnitGeneral(UnitTypes unitType, bool isGetted)
    {
        if (isGetted)
        {
            _eGM.SelectedUnitEUnitTypeC.UnitType = unitType;
        }
    }

    #endregion


    #region SetUnit

    internal void SetUnit(in int[] xyCell, UnitTypes unitType)
        => PhotonView.RPC(nameof(SetUnitToMaster), RpcTarget.MasterClient, xyCell, unitType);

    [PunRPC]
    private void SetUnitToMaster(int[] xyCell, UnitTypes unitType, PhotonMessageInfo info)
    {
        bool isSetted = false;

        if (!_eGM.CellEnvironmentComponent(xyCell).HaveMountain && !_eGM.CellUnitComponent(xyCell).HaveUnit)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    break;


                case UnitTypes.King:

                    if (info.Sender.IsMasterClient)
                    {
                        if (_eGM.CellComponent(xyCell).IsStartMaster)
                        {
                            _eGM.CellUnitComponent(xyCell).SetUnit(UnitTypes.King, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_KING, false, false, info.Sender);
                            _eGM.CellUnitComponent(xyCell).AmountHealth = _eGM.CellUnitComponent(xyCell).MaxAmountHealth;
                            _eGM.InfoEntityUnitsInfoComponent.AmountKingDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }
                    else
                    {
                        if (_eGM.CellComponent(xyCell).IsStartOther)
                        {
                            _eGM.CellUnitComponent(xyCell).SetUnit(UnitTypes.King, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_KING, false, false, info.Sender);
                            _eGM.CellUnitComponent(xyCell).AmountHealth = _eGM.CellUnitComponent(xyCell).MaxAmountHealth;
                            _eGM.InfoEntityUnitsInfoComponent.AmountKingDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    _eGM.InfoEntityUnitsInfoComponent.IsSettedKingDict[info.Sender.IsMasterClient] = isSetted;

                    break;


                case UnitTypes.Pawn:

                    if (info.Sender.IsMasterClient)
                    {
                        if (_eGM.CellComponent(xyCell).IsStartMaster)
                        {
                            _eGM.CellUnitComponent(xyCell).SetUnit(UnitTypes.Pawn, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN, false, false, info.Sender);
                            _eGM.CellUnitComponent(xyCell).AmountHealth = _eGM.CellUnitComponent(xyCell).MaxAmountHealth;
                            _eGM.InfoEntityUnitsInfoComponent.AmountPawnDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    else
                    {
                        if (_eGM.CellComponent(xyCell).IsStartOther)
                        {
                            _eGM.CellUnitComponent(xyCell).SetUnit(UnitTypes.Pawn, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN, false, false, info.Sender);
                            _eGM.CellUnitComponent(xyCell).AmountHealth = _eGM.CellUnitComponent(xyCell).MaxAmountHealth;
                            _eGM.InfoEntityUnitsInfoComponent.AmountPawnDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    break;


                case UnitTypes.Rook:

                    if (info.Sender.IsMasterClient)
                    {
                        if (_eGM.CellComponent(xyCell).IsStartMaster)
                        {
                            _eGM.CellUnitComponent(xyCell).SetUnit(UnitTypes.Rook, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK, false, false, info.Sender);
                            _eGM.CellUnitComponent(xyCell).AmountHealth = _eGM.CellUnitComponent(xyCell).MaxAmountHealth;
                            _eGM.InfoEntityUnitsInfoComponent.AmountRookDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }
                    else
                    {
                        if (_eGM.CellComponent(xyCell).IsStartOther)
                        {
                            _eGM.CellUnitComponent(xyCell).SetUnit(UnitTypes.Rook, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK, false, false, info.Sender);
                            _eGM.CellUnitComponent(xyCell).AmountHealth = _eGM.CellUnitComponent(xyCell).MaxAmountHealth;
                            _eGM.InfoEntityUnitsInfoComponent.AmountRookDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    break;


                case UnitTypes.Bishop:

                    if (info.Sender.IsMasterClient)
                    {
                        if (_eGM.CellComponent(xyCell).IsStartMaster)
                        {
                            _eGM.CellUnitComponent(xyCell).SetUnit(UnitTypes.Bishop, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP, false, false, info.Sender);
                            _eGM.CellUnitComponent(xyCell).AmountHealth = _eGM.CellUnitComponent(xyCell).MaxAmountHealth;
                            _eGM.InfoEntityUnitsInfoComponent.AmountBishopDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }
                    else
                    {
                        if (_eGM.CellComponent(xyCell).IsStartOther)
                        {
                            _eGM.CellUnitComponent(xyCell).SetUnit(UnitTypes.Bishop, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP, false, false, info.Sender);
                            _eGM.CellUnitComponent(xyCell).AmountHealth = _eGM.CellUnitComponent(xyCell).MaxAmountHealth;
                            _eGM.InfoEntityUnitsInfoComponent.AmountBishopDict[info.Sender.IsMasterClient] -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    break;


                default:
                    break;
            }
        }

        PhotonView.RPC(nameof(SetUnitToGeneral), info.Sender, isSetted);

        RefreshAllToMaster();
    }

    [PunRPC]
    private void SetUnitToGeneral(bool isSetted)
    {
        if (isSetted) _eGM.SelectorESelectorC.SetterUnitDelegate();
    }

    #endregion

}
