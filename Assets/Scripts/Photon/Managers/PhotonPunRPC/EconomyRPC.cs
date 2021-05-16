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
                    haveFood = EconomyComponent.FoodMaster >= _startValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                    haveWood = EconomyComponent.WoodMaster >= _startValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                    haveOre = EconomyComponent.OreMaster >= _startValuesGameConfig.ORE_FOR_BUYING_PAWN;
                    haveIron = EconomyComponent.IronMaster >= _startValuesGameConfig.IRON_FOR_BUYING_PAWN;
                    haveGold = EconomyComponent.GoldMaster >= _startValuesGameConfig.GOLD_FOR_BUYING_PAWN;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        EconomyComponent.FoodMaster -= _startValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                        EconomyComponent.WoodMaster -= _startValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                        EconomyComponent.OreMaster -= _startValuesGameConfig.ORE_FOR_BUYING_PAWN;
                        EconomyComponent.IronMaster -= _startValuesGameConfig.IRON_FOR_BUYING_PAWN;
                        EconomyComponent.GoldMaster -= _startValuesGameConfig.GOLD_FOR_BUYING_PAWN;

                        UnitsInfoComponent.AmountUnitPawnMaster += _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                    }
                }
                else
                {
                    haveFood = EconomyComponent.FoodOther >= _startValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                    haveWood = EconomyComponent.WoodOther >= _startValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                    haveOre = EconomyComponent.OreOther >= _startValuesGameConfig.ORE_FOR_BUYING_PAWN;
                    haveIron = EconomyComponent.IronOther >= _startValuesGameConfig.IRON_FOR_BUYING_PAWN;
                    haveGold = EconomyComponent.GoldOther >= _startValuesGameConfig.GOLD_FOR_BUYING_PAWN;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        EconomyComponent.FoodOther -= _startValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                        EconomyComponent.WoodOther -= _startValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                        EconomyComponent.OreOther -= _startValuesGameConfig.ORE_FOR_BUYING_PAWN;
                        EconomyComponent.IronOther -= _startValuesGameConfig.IRON_FOR_BUYING_PAWN;
                        EconomyComponent.GoldOther -= _startValuesGameConfig.GOLD_FOR_BUYING_PAWN;

                        UnitsInfoComponent.AmountUnitPawnOther += _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                    }
                }

                break;

            case UnitTypes.Rook:

                if (info.Sender.IsMasterClient)
                {
                    haveFood = EconomyComponent.FoodMaster >= _startValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                    haveWood = EconomyComponent.WoodMaster >= _startValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                    haveOre = EconomyComponent.OreMaster >= _startValuesGameConfig.ORE_FOR_BUYING_ROOK;
                    haveIron = EconomyComponent.IronMaster >= _startValuesGameConfig.IRON_FOR_BUYING_ROOK;
                    haveGold = EconomyComponent.GoldMaster >= _startValuesGameConfig.GOLD_FOR_BUYING_ROOK;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        EconomyComponent.FoodMaster -= _startValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                        EconomyComponent.WoodMaster -= _startValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                        EconomyComponent.OreMaster -= _startValuesGameConfig.ORE_FOR_BUYING_ROOK;
                        EconomyComponent.IronMaster -= _startValuesGameConfig.IRON_FOR_BUYING_ROOK;
                        EconomyComponent.GoldMaster -= _startValuesGameConfig.GOLD_FOR_BUYING_ROOK;

                        UnitsInfoComponent.AmountRookMaster += 1;
                    }
                }
                else
                {
                    haveFood = EconomyComponent.FoodOther >= _startValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                    haveWood = EconomyComponent.WoodOther >= _startValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                    haveOre = EconomyComponent.OreOther >= _startValuesGameConfig.ORE_FOR_BUYING_ROOK;
                    haveIron = EconomyComponent.IronOther >= _startValuesGameConfig.IRON_FOR_BUYING_ROOK;
                    haveGold = EconomyComponent.GoldOther >= _startValuesGameConfig.GOLD_FOR_BUYING_ROOK;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        EconomyComponent.FoodOther -= _startValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                        EconomyComponent.WoodOther -= _startValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                        EconomyComponent.OreOther -= _startValuesGameConfig.ORE_FOR_BUYING_ROOK;
                        EconomyComponent.IronOther -= _startValuesGameConfig.IRON_FOR_BUYING_ROOK;
                        EconomyComponent.GoldOther -= _startValuesGameConfig.GOLD_FOR_BUYING_ROOK;

                        UnitsInfoComponent.AmountRookOther += 1;
                    }
                }

                break;


            case UnitTypes.Bishop:

                if (info.Sender.IsMasterClient)
                {
                    haveFood = EconomyComponent.FoodMaster >= _startValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                    haveWood = EconomyComponent.WoodMaster >= _startValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                    haveOre = EconomyComponent.OreMaster >= _startValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                    haveIron = EconomyComponent.IronMaster >= _startValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                    haveGold = EconomyComponent.GoldMaster >= _startValuesGameConfig.GOLD_FOR_BUYING_BISHOP;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        EconomyComponent.FoodMaster -= _startValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                        EconomyComponent.WoodMaster -= _startValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                        EconomyComponent.OreMaster -= _startValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                        EconomyComponent.IronMaster -= _startValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                        EconomyComponent.GoldMaster -= _startValuesGameConfig.GOLD_FOR_BUYING_BISHOP;

                        UnitsInfoComponent.AmountBishopMaster += 1;
                    }
                }
                else
                {
                    haveFood = EconomyComponent.FoodOther >= _startValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                    haveWood = EconomyComponent.WoodOther >= _startValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                    haveOre = EconomyComponent.OreOther >= _startValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                    haveIron = EconomyComponent.IronOther >= _startValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                    haveGold = EconomyComponent.GoldOther >= _startValuesGameConfig.GOLD_FOR_BUYING_BISHOP;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        EconomyComponent.FoodOther -= _startValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                        EconomyComponent.WoodOther -= _startValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                        EconomyComponent.OreOther -= _startValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                        EconomyComponent.IronOther -= _startValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                        EconomyComponent.GoldOther -= _startValuesGameConfig.GOLD_FOR_BUYING_BISHOP;

                        UnitsInfoComponent.AmountBishopOther += 1;
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
                    haveFood = EconomyComponent.FoodMaster >= _startValuesGameConfig.FOOD_FOR_UPGRADE_PAWN;
                    haveWood = EconomyComponent.WoodMaster >= _startValuesGameConfig.WOOD_FOR_UPGRADE_PAWN;
                    haveOre = EconomyComponent.OreMaster >= _startValuesGameConfig.ORE_FOR_UPGRADE_PAWN;
                    haveIron = EconomyComponent.IronMaster >= _startValuesGameConfig.IRON_FOR_UPGRADE_PAWN;
                    haveGold = EconomyComponent.GoldMaster >= _startValuesGameConfig.GOLD_FOR_UPGRADE_PAWN;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        EconomyComponent.FoodMaster -= _startValuesGameConfig.FOOD_FOR_UPGRADE_PAWN;
                        EconomyComponent.WoodMaster -= _startValuesGameConfig.WOOD_FOR_UPGRADE_PAWN;
                        EconomyComponent.OreMaster -= _startValuesGameConfig.ORE_FOR_UPGRADE_PAWN;
                        EconomyComponent.IronMaster -= _startValuesGameConfig.IRON_FOR_UPGRADE_PAWN;
                        EconomyComponent.GoldMaster -= _startValuesGameConfig.GOLD_FOR_UPGRADE_PAWN;

                        for (int x = 0; x < _startValuesGameConfig.CELL_COUNT_X; x++)
                            for (int y = 0; y < _startValuesGameConfig.CELL_COUNT_Y; y++)
                            {
                                CellUnitComponent(x, y).AmountUpgradePawnMaster += 1;
                                if (CellUnitComponent(x, y).UnitType == UnitTypes.Pawn && CellUnitComponent(x, y).IsHisUnit(info.Sender)) CellUnitComponent(x, y).AmountHealth += InstanceGame.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_PAWN;
                            }

                    }
                }
                else
                {
                    haveFood = EconomyComponent.FoodOther >= _startValuesGameConfig.FOOD_FOR_UPGRADE_PAWN;
                    haveWood = EconomyComponent.WoodOther >= _startValuesGameConfig.WOOD_FOR_UPGRADE_PAWN;
                    haveOre = EconomyComponent.OreOther >= _startValuesGameConfig.ORE_FOR_UPGRADE_PAWN;
                    haveIron = EconomyComponent.IronOther >= _startValuesGameConfig.IRON_FOR_UPGRADE_PAWN;
                    haveGold = EconomyComponent.GoldOther >= _startValuesGameConfig.GOLD_FOR_UPGRADE_PAWN;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        EconomyComponent.FoodOther -= _startValuesGameConfig.FOOD_FOR_UPGRADE_PAWN;
                        EconomyComponent.WoodOther -= _startValuesGameConfig.WOOD_FOR_UPGRADE_PAWN;
                        EconomyComponent.OreOther -= _startValuesGameConfig.ORE_FOR_UPGRADE_PAWN;
                        EconomyComponent.IronOther -= _startValuesGameConfig.IRON_FOR_UPGRADE_PAWN;
                        EconomyComponent.GoldOther -= _startValuesGameConfig.GOLD_FOR_UPGRADE_PAWN;

                        for (int x = 0; x < _startValuesGameConfig.CELL_COUNT_X; x++)
                            for (int y = 0; y < _startValuesGameConfig.CELL_COUNT_Y; y++)
                            {
                                CellUnitComponent(x, y).AmountUpgradePawnOther += 1;
                                if (CellUnitComponent(x, y).UnitType == UnitTypes.Pawn && CellUnitComponent(x, y).IsHisUnit(info.Sender))
                                {
                                    CellUnitComponent(x, y).AmountHealth += InstanceGame.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_PAWN;
                                }
                            }

                    }
                }

                break;


            case UnitTypes.Rook:

                if (info.Sender.IsMasterClient)
                {
                    haveFood = EconomyComponent.FoodMaster >= _startValuesGameConfig.FOOD_FOR_UPGRADE_ROOK;
                    haveWood = EconomyComponent.WoodMaster >= _startValuesGameConfig.WOOD_FOR_UPGRADE_ROOK;
                    haveOre = EconomyComponent.OreMaster >= _startValuesGameConfig.ORE_FOR_UPGRADE_ROOK;
                    haveIron = EconomyComponent.IronMaster >= _startValuesGameConfig.IRON_FOR_UPGRADE_ROOK;
                    haveGold = EconomyComponent.GoldMaster >= _startValuesGameConfig.GOLD_FOR_UPGRADE_ROOK;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        EconomyComponent.FoodMaster -= _startValuesGameConfig.FOOD_FOR_UPGRADE_ROOK;
                        EconomyComponent.WoodMaster -= _startValuesGameConfig.WOOD_FOR_UPGRADE_ROOK;
                        EconomyComponent.OreMaster -= _startValuesGameConfig.ORE_FOR_UPGRADE_ROOK;
                        EconomyComponent.IronMaster -= _startValuesGameConfig.IRON_FOR_UPGRADE_ROOK;
                        EconomyComponent.GoldMaster -= _startValuesGameConfig.GOLD_FOR_UPGRADE_ROOK;

                        for (int x = 0; x < _startValuesGameConfig.CELL_COUNT_X; x++)
                            for (int y = 0; y < _startValuesGameConfig.CELL_COUNT_Y; y++)
                            {
                                CellUnitComponent(x, y).AmountUpgradeRookMaster += 1;
                                if (CellUnitComponent(x, y).UnitType == UnitTypes.Rook && CellUnitComponent(x, y).IsHisUnit(info.Sender)) CellUnitComponent(x, y).AmountHealth += InstanceGame.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_ROOK;
                            }

                    }
                }
                else
                {
                    haveFood = EconomyComponent.FoodOther >= _startValuesGameConfig.FOOD_FOR_UPGRADE_ROOK;
                    haveWood = EconomyComponent.WoodOther >= _startValuesGameConfig.WOOD_FOR_UPGRADE_ROOK;
                    haveOre = EconomyComponent.OreOther >= _startValuesGameConfig.ORE_FOR_UPGRADE_ROOK;
                    haveIron = EconomyComponent.IronOther >= _startValuesGameConfig.IRON_FOR_UPGRADE_ROOK;
                    haveGold = EconomyComponent.GoldOther >= _startValuesGameConfig.GOLD_FOR_UPGRADE_ROOK;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        EconomyComponent.FoodOther -= _startValuesGameConfig.FOOD_FOR_UPGRADE_ROOK;
                        EconomyComponent.WoodOther -= _startValuesGameConfig.WOOD_FOR_UPGRADE_ROOK;
                        EconomyComponent.OreOther -= _startValuesGameConfig.ORE_FOR_UPGRADE_ROOK;
                        EconomyComponent.IronOther -= _startValuesGameConfig.IRON_FOR_UPGRADE_ROOK;
                        EconomyComponent.GoldOther -= _startValuesGameConfig.GOLD_FOR_UPGRADE_ROOK;

                        for (int x = 0; x < _startValuesGameConfig.CELL_COUNT_X; x++)
                            for (int y = 0; y < _startValuesGameConfig.CELL_COUNT_Y; y++)
                            {
                                CellUnitComponent(x, y).AmountUpgradeRookOther += 1;
                                if (CellUnitComponent(x, y).UnitType == UnitTypes.Rook && CellUnitComponent(x, y).IsHisUnit(info.Sender)) CellUnitComponent(x, y).AmountHealth += InstanceGame.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_ROOK;
                            }

                    }
                }

                break;


            case UnitTypes.Bishop:

                if (info.Sender.IsMasterClient)
                {
                    haveFood = EconomyComponent.FoodMaster >= _startValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP;
                    haveWood = EconomyComponent.WoodMaster >= _startValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP;
                    haveOre = EconomyComponent.OreMaster >= _startValuesGameConfig.ORE_FOR_UPGRADE_BISHOP;
                    haveIron = EconomyComponent.IronMaster >= _startValuesGameConfig.IRON_FOR_UPGRADE_BISHOP;
                    haveGold = EconomyComponent.GoldMaster >= _startValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        EconomyComponent.FoodMaster -= _startValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP;
                        EconomyComponent.WoodMaster -= _startValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP;
                        EconomyComponent.OreMaster -= _startValuesGameConfig.ORE_FOR_UPGRADE_BISHOP;
                        EconomyComponent.IronMaster -= _startValuesGameConfig.IRON_FOR_UPGRADE_BISHOP;
                        EconomyComponent.GoldMaster -= _startValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP;

                        for (int x = 0; x < _startValuesGameConfig.CELL_COUNT_X; x++)
                            for (int y = 0; y < _startValuesGameConfig.CELL_COUNT_Y; y++)
                            {
                                CellUnitComponent(x, y).AmountUpgradeBishopMaster += 1;
                                if (CellUnitComponent(x, y).UnitType == UnitTypes.Bishop && CellUnitComponent(x, y).IsHisUnit(info.Sender)) CellUnitComponent(x, y).AmountHealth += InstanceGame.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_BISHOP;
                            }

                    }
                }
                else
                {
                    haveFood = EconomyComponent.FoodOther >= _startValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP;
                    haveWood = EconomyComponent.WoodOther >= _startValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP;
                    haveOre = EconomyComponent.OreOther >= _startValuesGameConfig.ORE_FOR_UPGRADE_BISHOP;
                    haveIron = EconomyComponent.IronOther >= _startValuesGameConfig.IRON_FOR_UPGRADE_BISHOP;
                    haveGold = EconomyComponent.GoldOther >= _startValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        EconomyComponent.FoodOther -= _startValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP;
                        EconomyComponent.WoodOther -= _startValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP;
                        EconomyComponent.OreOther -= _startValuesGameConfig.ORE_FOR_UPGRADE_BISHOP;
                        EconomyComponent.IronOther -= _startValuesGameConfig.IRON_FOR_UPGRADE_BISHOP;
                        EconomyComponent.GoldOther -= _startValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP;

                        for (int x = 0; x < _startValuesGameConfig.CELL_COUNT_X; x++)
                            for (int y = 0; y < _startValuesGameConfig.CELL_COUNT_Y; y++)
                            {
                                CellUnitComponent(x, y).AmountUpgradeBishopOther += 1;
                                if (CellUnitComponent(x, y).UnitType == UnitTypes.Bishop && CellUnitComponent(x, y).IsHisUnit(info.Sender)) CellUnitComponent(x, y).AmountHealth += InstanceGame.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_BISHOP;
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
            haveFood = EconomyComponent.FoodMaster >= _startValuesGameConfig.FOOD_FOR_MELTING_ORE;
            haveWood = EconomyComponent.WoodMaster >= _startValuesGameConfig.WOOD_FOR_MELTING_ORE;
            haveOre = EconomyComponent.OreMaster >= _startValuesGameConfig.ORE_FOR_MELTING_ORE;
            haveIron = EconomyComponent.IronMaster >= _startValuesGameConfig.IRON_FOR_MELTING_ORE;
            haveGold = EconomyComponent.GoldMaster >= _startValuesGameConfig.GOLD_FOR_MELTING_ORE;

            if (haveFood && haveWood && haveOre && haveIron && haveGold)
            {
                EconomyComponent.FoodMaster -= _startValuesGameConfig.FOOD_FOR_MELTING_ORE;
                EconomyComponent.WoodMaster -= _startValuesGameConfig.WOOD_FOR_MELTING_ORE;
                EconomyComponent.OreMaster -= _startValuesGameConfig.ORE_FOR_MELTING_ORE;
                EconomyComponent.IronMaster -= _startValuesGameConfig.IRON_FOR_MELTING_ORE;
                EconomyComponent.GoldMaster -= _startValuesGameConfig.GOLD_FOR_MELTING_ORE;

                EconomyComponent.IronMaster += 1;
            }
        }
        else
        {
            haveFood = EconomyComponent.FoodOther >= _startValuesGameConfig.FOOD_FOR_MELTING_ORE;
            haveWood = EconomyComponent.WoodOther >= _startValuesGameConfig.WOOD_FOR_MELTING_ORE;
            haveOre = EconomyComponent.OreOther >= _startValuesGameConfig.ORE_FOR_MELTING_ORE;
            haveIron = EconomyComponent.IronOther >= _startValuesGameConfig.IRON_FOR_MELTING_ORE;
            haveGold = EconomyComponent.GoldOther >= _startValuesGameConfig.GOLD_FOR_MELTING_ORE;

            if (haveFood && haveWood && haveOre && haveIron && haveGold)
            {
                EconomyComponent.FoodOther -= _startValuesGameConfig.FOOD_FOR_MELTING_ORE;
                EconomyComponent.WoodOther -= _startValuesGameConfig.WOOD_FOR_MELTING_ORE;
                EconomyComponent.OreOther -= _startValuesGameConfig.ORE_FOR_MELTING_ORE;
                EconomyComponent.IronOther -= _startValuesGameConfig.IRON_FOR_MELTING_ORE;
                EconomyComponent.GoldOther -= _startValuesGameConfig.GOLD_FOR_MELTING_ORE;

                EconomyComponent.IronOther += 1;
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
                    isGetted = UnitsInfoComponent.AmountKingMaster >= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                else isGetted = UnitsInfoComponent.AmountKingOther >= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                break;


            case UnitTypes.Pawn:

                if (info.Sender.IsMasterClient)
                {
                    if (UnitsInfoComponent.AmountUnitPawnMaster >= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }
                else
                {
                    if (UnitsInfoComponent.AmountUnitPawnOther >= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }

                break;


            case UnitTypes.Rook:

                if (info.Sender.IsMasterClient)
                {
                    if (UnitsInfoComponent.AmountRookMaster >= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }
                else
                {
                    if (UnitsInfoComponent.AmountRookOther >= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT)
                    {
                        isGetted = true;
                    }
                    else isGetted = false;
                }

                break;


            case UnitTypes.Bishop:

                if (info.Sender.IsMasterClient)
                    isGetted = UnitsInfoComponent.AmountBishopMaster >= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

                else isGetted = UnitsInfoComponent.AmountBishopOther >= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;

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
            _selectedUnitComponentRef.Unref().SelectedUnitType = unitType;
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

        if (!CellEnvironmentComponent(xyCell).HaveMountain && !CellUnitComponent(xyCell).HaveUnit)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    break;


                case UnitTypes.King:

                    if (info.Sender.IsMasterClient)
                    {
                        if (CellComponent(xyCell).IsStartMaster)
                        {
                            CellUnitComponent(xyCell).SetUnit(UnitTypes.King, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_KING, false, false, info.Sender);
                            CellUnitComponent(xyCell).AmountHealth = CellUnitComponent(xyCell).MaxAmountHealth;
                            UnitsInfoComponent.AmountKingMaster -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }
                    else
                    {
                        if (CellComponent(xyCell).IsStartOther)
                        {
                            CellUnitComponent(xyCell).SetUnit(UnitTypes.King, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_KING, false, false, info.Sender);
                            CellUnitComponent(xyCell).AmountHealth = CellUnitComponent(xyCell).MaxAmountHealth;
                            UnitsInfoComponent.AmountKingOther -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    break;


                case UnitTypes.Pawn:

                    if (info.Sender.IsMasterClient)
                    {
                        if (CellComponent(xyCell).IsStartMaster)
                        {
                            CellUnitComponent(xyCell).SetUnit(UnitTypes.Pawn, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN, false, false, info.Sender);
                            CellUnitComponent(xyCell).AmountHealth = CellUnitComponent(xyCell).MaxAmountHealth;
                            UnitsInfoComponent.AmountUnitPawnMaster -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    else
                    {
                        if (CellComponent(xyCell).IsStartOther)
                        {
                            CellUnitComponent(xyCell).SetUnit(UnitTypes.Pawn, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN, false, false, info.Sender);
                            CellUnitComponent(xyCell).AmountHealth = CellUnitComponent(xyCell).MaxAmountHealth;
                            UnitsInfoComponent.AmountUnitPawnOther -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    break;


                case UnitTypes.Rook:

                    if (info.Sender.IsMasterClient)
                    {
                        if (CellComponent(xyCell).IsStartMaster)
                        {
                            CellUnitComponent(xyCell).SetUnit(UnitTypes.Rook, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK, false, false, info.Sender);
                            CellUnitComponent(xyCell).AmountHealth = CellUnitComponent(xyCell).MaxAmountHealth;
                            UnitsInfoComponent.AmountRookMaster -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }
                    else
                    {
                        if (CellComponent(xyCell).IsStartOther)
                        {
                            CellUnitComponent(xyCell).SetUnit(UnitTypes.Rook, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK, false, false, info.Sender);
                            CellUnitComponent(xyCell).AmountHealth = CellUnitComponent(xyCell).MaxAmountHealth;
                            UnitsInfoComponent.AmountRookOther -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }

                    break;


                case UnitTypes.Bishop:

                    if (info.Sender.IsMasterClient)
                    {
                        if (CellComponent(xyCell).IsStartMaster)
                        {
                            CellUnitComponent(xyCell).SetUnit(UnitTypes.Bishop, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP, false, false, info.Sender);
                            CellUnitComponent(xyCell).AmountHealth = CellUnitComponent(xyCell).MaxAmountHealth;
                            UnitsInfoComponent.AmountBishopMaster -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                            isSetted = true;
                        }
                        else isSetted = false;
                    }
                    else
                    {
                        if (CellComponent(xyCell).IsStartOther)
                        {
                            CellUnitComponent(xyCell).SetUnit(UnitTypes.Bishop, default, _startValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP, false, false, info.Sender);
                            CellUnitComponent(xyCell).AmountHealth = CellUnitComponent(xyCell).MaxAmountHealth;
                            UnitsInfoComponent.AmountBishopOther -= _startValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
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
            if (info.Sender.IsMasterClient) UnitsInfoComponent.IsSettedKingMaster = isSetted;
            else UnitsInfoComponent.IsSettedKingOther = isSetted;
        }


        _photonView.RPC("SetUnitToGeneral", info.Sender, isSetted);

        RefreshAllToMaster();
    }

    [PunRPC]
    private void SetUnitToGeneral(bool isSetted)
    {
        if (isSetted) _selectorComponentRef.Unref().SetterUnitDelegate();
    }

    #endregion

}
