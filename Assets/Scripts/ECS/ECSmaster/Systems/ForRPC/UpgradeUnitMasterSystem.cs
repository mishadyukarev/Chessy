using Leopotam.Ecs;
using Photon.Pun;

internal class UpgradeUnitMasterSystem : RPCMasterSystemReduction, IEcsRunSystem
{
    internal UnitTypes UnitType => _eMM.MasterRPCEntUnitTypeCom.UnitType;
    internal PhotonMessageInfo Info => _eGM.GeneralRPCEntFromInfoCom.FromInfo;

    internal UpgradeUnitMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        bool haveFood = true;
        bool haveWood = true;
        bool haveOre = true;
        bool haveIron = true;
        bool haveGold = true;

        switch (UnitType)
        {
            case UnitTypes.None:
                break;

            case UnitTypes.King:
                break;

            case UnitTypes.Pawn:

                if (Info.Sender.IsMasterClient)
                {
                    haveFood = _eGM.FoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_UPGRADE_PAWN;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_UPGRADE_PAWN;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_UPGRADE_PAWN;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_UPGRADE_PAWN;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_UPGRADE_PAWN;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_UPGRADE_PAWN;
                        _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_UPGRADE_PAWN;
                        _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_UPGRADE_PAWN;
                        _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_UPGRADE_PAWN;
                        _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_UPGRADE_PAWN;

                        _eGM.InfoEnt_UpgradeCom.AmountUpgradePawnDict[Info.Sender.IsMasterClient] += 1;
                        for (int x = 0; x < StartValuesGameConfig.CELL_COUNT_X; x++)
                            for (int y = 0; y < StartValuesGameConfig.CELL_COUNT_Y; y++)
                            {
                                if (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType == UnitTypes.Pawn 
                                    && _eGM.CellUnitEnt_OwnerCom(x, y).IsHim(Info.Sender)) 
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += StartValuesGameConfig.HEALTH_UPGRADE_ADDING_PAWN;
                            }

                    }
                }
                else
                {
                    haveFood = _eGM.FoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_UPGRADE_PAWN;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_UPGRADE_PAWN;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_UPGRADE_PAWN;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_UPGRADE_PAWN;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_UPGRADE_PAWN;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_UPGRADE_PAWN;
                        _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_UPGRADE_PAWN;
                        _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_UPGRADE_PAWN;
                        _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_UPGRADE_PAWN;
                        _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_UPGRADE_PAWN;

                        _eGM.InfoEnt_UpgradeCom.AmountUpgradePawnDict[Info.Sender.IsMasterClient] += 1;
                        for (int x = 0; x < StartValuesGameConfig.CELL_COUNT_X; x++)
                            for (int y = 0; y < StartValuesGameConfig.CELL_COUNT_Y; y++)
                            {
                                if (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType == UnitTypes.Pawn && _eGM.CellUnitEnt_OwnerCom(x, y).IsHim(Info.Sender))
                                {
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += StartValuesGameConfig.HEALTH_UPGRADE_ADDING_PAWN;
                                }
                            }

                    }
                }

                break;


            case UnitTypes.Rook:

                if (Info.Sender.IsMasterClient)
                {
                    haveFood = _eGM.FoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_UPGRADE_ROOK;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_UPGRADE_ROOK;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_UPGRADE_ROOK;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_UPGRADE_ROOK;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_UPGRADE_ROOK;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_UPGRADE_ROOK;
                        _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_UPGRADE_ROOK;
                        _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_UPGRADE_ROOK;
                        _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_UPGRADE_ROOK;
                        _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_UPGRADE_ROOK;

                        _eGM.InfoEnt_UpgradeCom.AmountUpgradeRookDict[Info.Sender.IsMasterClient] += 1;
                        for (int x = 0; x < StartValuesGameConfig.CELL_COUNT_X; x++)
                            for (int y = 0; y < StartValuesGameConfig.CELL_COUNT_Y; y++)
                            {
                                if (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType == UnitTypes.Rook 
                                    && _eGM.CellUnitEnt_OwnerCom(x, y).IsHim(Info.Sender))
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += StartValuesGameConfig.HEALTH_UPGRADE_ADDING_ROOK;
                            }

                    }
                }
                else
                {
                    haveFood = _eGM.FoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_UPGRADE_ROOK;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_UPGRADE_ROOK;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_UPGRADE_ROOK;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_UPGRADE_ROOK;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_UPGRADE_ROOK;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_UPGRADE_ROOK;
                        _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_UPGRADE_ROOK;
                        _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_UPGRADE_ROOK;
                        _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_UPGRADE_ROOK;
                        _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_UPGRADE_ROOK;

                        _eGM.InfoEnt_UpgradeCom.AmountUpgradeRookDict[Info.Sender.IsMasterClient] += 1;
                        for (int x = 0; x < StartValuesGameConfig.CELL_COUNT_X; x++)
                            for (int y = 0; y < StartValuesGameConfig.CELL_COUNT_Y; y++)
                            {
                                if (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType == UnitTypes.Rook 
                                    && _eGM.CellUnitEnt_OwnerCom(x, y).IsHim(Info.Sender)) 
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += StartValuesGameConfig.HEALTH_UPGRADE_ADDING_ROOK;
                            }

                    }
                }

                break;


            case UnitTypes.Bishop:

                if (Info.Sender.IsMasterClient)
                {
                    haveFood = _eGM.FoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_UPGRADE_BISHOP;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_UPGRADE_BISHOP;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP;
                        _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP;
                        _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_UPGRADE_BISHOP;
                        _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_UPGRADE_BISHOP;
                        _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP;

                        _eGM.InfoEnt_UpgradeCom.AmountUpgradeBishopDict[Info.Sender.IsMasterClient] += 1;
                        for (int x = 0; x < StartValuesGameConfig.CELL_COUNT_X; x++)
                            for (int y = 0; y < StartValuesGameConfig.CELL_COUNT_Y; y++)
                            {
                                if (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType == UnitTypes.Bishop && _eGM.CellUnitEnt_OwnerCom(x, y).IsHim(Info.Sender)) _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += StartValuesGameConfig.HEALTH_UPGRADE_ADDING_BISHOP;
                            }

                    }
                }
                else
                {
                    haveFood = _eGM.FoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_UPGRADE_BISHOP;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_UPGRADE_BISHOP;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP;


                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP;
                        _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP;
                        _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_UPGRADE_BISHOP;
                        _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_UPGRADE_BISHOP;
                        _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP;

                        _eGM.InfoEnt_UpgradeCom.AmountUpgradeBishopDict[Info.Sender.IsMasterClient] += 1;
                        for (int x = 0; x < StartValuesGameConfig.CELL_COUNT_X; x++)
                            for (int y = 0; y < StartValuesGameConfig.CELL_COUNT_Y; y++)
                            {
                                if (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType == UnitTypes.Bishop 
                                    && _eGM.CellUnitEnt_OwnerCom(x, y).IsHim(Info.Sender)) 
                                    _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += StartValuesGameConfig.HEALTH_UPGRADE_ADDING_BISHOP;
                            }

                    }
                }

                break;

            default:
                break;
        }

        _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);

    }
}