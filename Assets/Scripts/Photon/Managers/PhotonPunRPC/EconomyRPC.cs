using Leopotam.Ecs;
using Photon.Pun;
using static MainGame;

internal partial class PhotonPunRPC //EconomyRPC
{

    #region CreateUnit

    internal void CreateUnit(in UnitTypes unitType) => _photonView.RPC(nameof(CreateUnitMaster), RpcTarget.MasterClient, unitType);

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
                    haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                    haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                    haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_BUYING_PAWN;
                    haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_BUYING_PAWN;
                    haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]>= StartValuesGameConfig.GOLD_FOR_BUYING_PAWN;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                        _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                        _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_BUYING_PAWN;
                        _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_BUYING_PAWN;
                        _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]-= StartValuesGameConfig.GOLD_FOR_BUYING_PAWN;

                        _eGM.UnitsInfoComponent.AmountUnitPawnMaster += StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                    }
                }
                else
                {
                    haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                    haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                    haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_BUYING_PAWN;
                    haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_BUYING_PAWN;
                    haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_BUYING_PAWN;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                        _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                        _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_BUYING_PAWN;
                        _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_BUYING_PAWN;
                        _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_BUYING_PAWN;

                        _eGM.UnitsInfoComponent.AmountUnitPawnOther += StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                    }
                }

                break;

            case UnitTypes.Rook:

                if (info.Sender.IsMasterClient)
                {
                    haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                    haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                    haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_BUYING_ROOK;
                    haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_BUYING_ROOK;
                    haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]>= StartValuesGameConfig.GOLD_FOR_BUYING_ROOK;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                        _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                        _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_BUYING_ROOK;
                        _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_BUYING_ROOK;
                        _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]-= StartValuesGameConfig.GOLD_FOR_BUYING_ROOK;

                        _eGM.UnitsInfoComponent.AmountRookMaster += 1;
                    }
                }
                else
                {
                    haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                    haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                    haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_BUYING_ROOK;
                    haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_BUYING_ROOK;
                    haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_BUYING_ROOK;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                        _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                        _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_BUYING_ROOK;
                        _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_BUYING_ROOK;
                        _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_BUYING_ROOK;

                        _eGM.UnitsInfoComponent.AmountRookOther += 1;
                    }
                }

                break;


            case UnitTypes.Bishop:

                if (info.Sender.IsMasterClient)
                {
                    haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                    haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                    haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                    haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                    haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]>= StartValuesGameConfig.GOLD_FOR_BUYING_BISHOP;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                        _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                        _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                        _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                        _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]-= StartValuesGameConfig.GOLD_FOR_BUYING_BISHOP;

                        _eGM.UnitsInfoComponent.AmountBishopMaster += 1;
                    }
                }
                else
                {
                    haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                    haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                    haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                    haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                    haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_BUYING_BISHOP;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                        _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                        _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                        _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                        _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_BUYING_BISHOP;

                        _eGM.UnitsInfoComponent.AmountBishopOther += 1;
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

    internal void UpgradeUnitToMaster(UnitTypes unitType) => _photonView.RPC(nameof(UpgradeUnitMaster), RpcTarget.MasterClient, unitType);

    [PunRPC]
    private void UpgradeUnitMaster(UnitTypes unitType,PhotonMessageInfo info)
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
                    haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_UPGRADE_PAWN;
                    haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_UPGRADE_PAWN;
                    haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_UPGRADE_PAWN;
                    haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_UPGRADE_PAWN;
                    haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]>= StartValuesGameConfig.GOLD_FOR_UPGRADE_PAWN;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_UPGRADE_PAWN;
                        _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_UPGRADE_PAWN;
                        _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_UPGRADE_PAWN;
                        _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_UPGRADE_PAWN;
                        _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]-= StartValuesGameConfig.GOLD_FOR_UPGRADE_PAWN;

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
                    haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_UPGRADE_PAWN;
                    haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_UPGRADE_PAWN;
                    haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_UPGRADE_PAWN;
                    haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_UPGRADE_PAWN;
                    haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_UPGRADE_PAWN;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_UPGRADE_PAWN;
                        _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_UPGRADE_PAWN;
                        _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_UPGRADE_PAWN;
                        _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_UPGRADE_PAWN;
                        _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_UPGRADE_PAWN;

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
                    haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_UPGRADE_ROOK;
                    haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_UPGRADE_ROOK;
                    haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_UPGRADE_ROOK;
                    haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_UPGRADE_ROOK;
                    haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]>= StartValuesGameConfig.GOLD_FOR_UPGRADE_ROOK;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_UPGRADE_ROOK;
                        _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_UPGRADE_ROOK;
                        _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_UPGRADE_ROOK;
                        _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_UPGRADE_ROOK;
                        _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]-= StartValuesGameConfig.GOLD_FOR_UPGRADE_ROOK;

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
                    haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_UPGRADE_ROOK;
                    haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_UPGRADE_ROOK;
                    haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_UPGRADE_ROOK;
                    haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_UPGRADE_ROOK;
                    haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_UPGRADE_ROOK;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_UPGRADE_ROOK;
                        _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_UPGRADE_ROOK;
                        _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_UPGRADE_ROOK;
                        _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_UPGRADE_ROOK;
                        _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_UPGRADE_ROOK;

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
                    haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP;
                    haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP;
                    haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_UPGRADE_BISHOP;
                    haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_UPGRADE_BISHOP;
                    haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]>= StartValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP;
                        _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP;
                        _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_UPGRADE_BISHOP;
                        _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_UPGRADE_BISHOP;
                        _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]-= StartValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP;

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
                    haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP;
                    haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP;
                    haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_UPGRADE_BISHOP;
                    haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_UPGRADE_BISHOP;
                    haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP;
                        _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP;
                        _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_UPGRADE_BISHOP;
                        _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_UPGRADE_BISHOP;
                        _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP;

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

    internal void MeltOre() => _photonView.RPC(nameof(MeltOreMaster), RpcTarget.MasterClient);

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
            haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_MELTING_ORE;
            haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_MELTING_ORE;
            haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_MELTING_ORE;
            haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_MELTING_ORE;
            haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]>= StartValuesGameConfig.GOLD_FOR_MELTING_ORE;

            if (haveFood && haveWood && haveOre && haveIron && haveGold)
            {
                _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_MELTING_ORE;
                _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_MELTING_ORE;
                _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_MELTING_ORE;
                _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_MELTING_ORE;
                _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient]-= StartValuesGameConfig.GOLD_FOR_MELTING_ORE;

                _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] += 1;
            }
        }
        else
        {
            haveFood = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_MELTING_ORE;
            haveWood = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_MELTING_ORE;
            haveOre = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_MELTING_ORE;
            haveIron = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_MELTING_ORE;
            haveGold = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_MELTING_ORE;

            if (haveFood && haveWood && haveOre && haveIron && haveGold)
            {
                _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_MELTING_ORE;
                _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_MELTING_ORE;
                _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_MELTING_ORE;
                _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_MELTING_ORE;
                _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_MELTING_ORE;

                _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[info.Sender.IsMasterClient] += 1;
            }
        }

        MistakeEconomy(info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);

        RefreshAllToMaster();
    }

    #endregion


    #region GetUnit

    internal void GetUnit(UnitTypes unitTypes) => _photonView.RPC(nameof(GetUnitMaster), RpcTarget.MasterClient, unitTypes);

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
                    isGetted = _eGM.UnitsInfoComponent.AmountKingMaster >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                else isGetted = _eGM.UnitsInfoComponent.AmountKingOther >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                break;


            case UnitTypes.Pawn:

                if (info.Sender.IsMasterClient)
                {
                    if (_eGM.UnitsInfoComponent.AmountUnitPawnMaster >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }
                else
                {
                    if (_eGM.UnitsInfoComponent.AmountUnitPawnOther >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }

                break;


            case UnitTypes.Rook:

                if (info.Sender.IsMasterClient)
                {
                    if (_eGM.UnitsInfoComponent.AmountRookMaster >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }
                else
                {
                    if (_eGM.UnitsInfoComponent.AmountRookOther >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }

                break;


            case UnitTypes.Bishop:

                if (info.Sender.IsMasterClient)
                    isGetted = _eGM.UnitsInfoComponent.AmountBishopMaster >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                else isGetted = _eGM.UnitsInfoComponent.AmountBishopOther >= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                break;


            default:
                break;

        }

        _photonView.RPC(nameof(GetUnitGeneral), info.Sender, unitType, isGetted);

        RefreshAllToMaster();
    }

    [PunRPC]
    private void GetUnitGeneral(UnitTypes unitType, bool isGetted)
    {
        if (isGetted)
        {
            _eGM.SelectedUnitComponentSelectorEnt.SelectedUnitType = unitType;
        }
    }

    #endregion


    #region SetUnit

    internal void SetUnit(in int[] xyCell, UnitTypes unitType)
        => _photonView.RPC("SetUnitToMaster", RpcTarget.MasterClient, xyCell, unitType);

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
                            _eGM.UnitsInfoComponent.AmountKingMaster -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
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
                            _eGM.UnitsInfoComponent.AmountKingOther -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    break;


                case UnitTypes.Pawn:

                    if (info.Sender.IsMasterClient)
                    {
                        if (_eGM.CellComponent(xyCell).IsStartMaster)
                        {
                            _eGM.CellUnitComponent(xyCell).SetUnit(UnitTypes.Pawn, default, StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN, false, false, info.Sender);
                            _eGM.CellUnitComponent(xyCell).AmountHealth = _eGM.CellUnitComponent(xyCell).MaxAmountHealth;
                            _eGM.UnitsInfoComponent.AmountUnitPawnMaster -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
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
                            _eGM.UnitsInfoComponent.AmountUnitPawnOther -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
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
                            _eGM.UnitsInfoComponent.AmountRookMaster -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
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
                            _eGM.UnitsInfoComponent.AmountRookOther -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
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
                            _eGM.UnitsInfoComponent.AmountBishopMaster -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
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
                            _eGM.UnitsInfoComponent.AmountBishopOther -= StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    break;


                default:
                    break;
            }
        }

        if (unitType == UnitTypes.King)
        {
            if (info.Sender.IsMasterClient) _eGM.UnitsInfoComponent.IsSettedKingMaster = isSetted;
            else _eGM.UnitsInfoComponent.IsSettedKingOther = isSetted;
        }


        _photonView.RPC("SetUnitToGeneral", info.Sender, isSetted);

        RefreshAllToMaster();
    }

    [PunRPC]
    private void SetUnitToGeneral(bool isSetted)
    {
        if (isSetted) _eGM.SelectorComponentSelectorEnt.SetterUnitDelegate();
    }

    #endregion

}
