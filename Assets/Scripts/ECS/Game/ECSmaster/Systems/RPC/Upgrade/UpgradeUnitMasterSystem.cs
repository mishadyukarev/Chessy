using Leopotam.Ecs;
using Photon.Pun;

internal class UpgradeUnitMasterSystem : RPCMasterSystemReduction, IEcsRunSystem
{
    internal UnitTypes UnitType => _eMM.RPCMasterEnt_RPCMasterCom.UnitType;
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;

    public override void Run()
    {
        base.Run();

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

                haveFood = _eGM.EconomyEnt_EconomyCom.Food(Info.Sender.IsMasterClient) >= _startValuesGameConfig.FOOD_FOR_UPGRADE_PAWN;
                haveWood = _eGM.EconomyEnt_EconomyCom.Wood(Info.Sender.IsMasterClient) >= _startValuesGameConfig.WOOD_FOR_UPGRADE_PAWN;
                haveOre = _eGM.EconomyEnt_EconomyCom.Ore(Info.Sender.IsMasterClient) >= _startValuesGameConfig.ORE_FOR_UPGRADE_PAWN;
                haveIron = _eGM.EconomyEnt_EconomyCom.Iron(Info.Sender.IsMasterClient) >= _startValuesGameConfig.IRON_FOR_UPGRADE_PAWN;
                haveGold = _eGM.EconomyEnt_EconomyCom.Gold(Info.Sender.IsMasterClient) >= _startValuesGameConfig.GOLD_FOR_UPGRADE_PAWN;


                if (haveFood && haveWood && haveOre && haveIron && haveGold)
                {
                    _eGM.EconomyEnt_EconomyCom.TakeFood(Info.Sender.IsMasterClient, _startValuesGameConfig.FOOD_FOR_UPGRADE_PAWN);
                    _eGM.EconomyEnt_EconomyCom.TakeWood(Info.Sender.IsMasterClient, _startValuesGameConfig.WOOD_FOR_UPGRADE_PAWN);
                    _eGM.EconomyEnt_EconomyCom.TakeOre(Info.Sender.IsMasterClient, _startValuesGameConfig.ORE_FOR_UPGRADE_PAWN);
                    _eGM.EconomyEnt_EconomyCom.TakeIron(Info.Sender.IsMasterClient, _startValuesGameConfig.IRON_FOR_UPGRADE_PAWN);
                    _eGM.EconomyEnt_EconomyCom.TakeGold(Info.Sender.IsMasterClient, _startValuesGameConfig.GOLD_FOR_UPGRADE_PAWN);

                    _eGM.UnitInventorEnt_UpgradeUnitCom.AddAmountUpgrades(UnitTypes.Pawn, Info.Sender.IsMasterClient);
                    for (int x = 0; x < _startValuesGameConfig.CELL_COUNT_X; x++)
                        for (int y = 0; y < _startValuesGameConfig.CELL_COUNT_Y; y++)
                        {
                            if (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType == UnitTypes.Pawn
                                && _eGM.CellUnitEnt_CellOwnerCom(x, y).IsHim(Info.Sender))
                                _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += _startValuesGameConfig.HEALTH_UPGRADE_ADDING_PAWN;
                        }

                }

                break;


            case UnitTypes.Rook:

                haveFood = _eGM.EconomyEnt_EconomyCom.Food(Info.Sender.IsMasterClient) >= _startValuesGameConfig.FOOD_FOR_UPGRADE_ROOK;
                haveWood = _eGM.EconomyEnt_EconomyCom.Wood(Info.Sender.IsMasterClient) >= _startValuesGameConfig.WOOD_FOR_UPGRADE_ROOK;
                haveOre = _eGM.EconomyEnt_EconomyCom.Ore(Info.Sender.IsMasterClient) >= _startValuesGameConfig.ORE_FOR_UPGRADE_ROOK;
                haveIron = _eGM.EconomyEnt_EconomyCom.Iron(Info.Sender.IsMasterClient) >= _startValuesGameConfig.IRON_FOR_UPGRADE_ROOK;
                haveGold = _eGM.EconomyEnt_EconomyCom.Gold(Info.Sender.IsMasterClient) >= _startValuesGameConfig.GOLD_FOR_UPGRADE_ROOK;


                if (haveFood && haveWood && haveOre && haveIron && haveGold)
                {
                    _eGM.EconomyEnt_EconomyCom.TakeFood(Info.Sender.IsMasterClient, _startValuesGameConfig.FOOD_FOR_UPGRADE_ROOK);
                    _eGM.EconomyEnt_EconomyCom.TakeWood(Info.Sender.IsMasterClient, _startValuesGameConfig.WOOD_FOR_UPGRADE_ROOK);
                    _eGM.EconomyEnt_EconomyCom.TakeOre(Info.Sender.IsMasterClient, _startValuesGameConfig.ORE_FOR_UPGRADE_ROOK);
                    _eGM.EconomyEnt_EconomyCom.TakeIron(Info.Sender.IsMasterClient, _startValuesGameConfig.IRON_FOR_UPGRADE_ROOK);
                    _eGM.EconomyEnt_EconomyCom.TakeGold(Info.Sender.IsMasterClient, _startValuesGameConfig.GOLD_FOR_UPGRADE_ROOK);

                    _eGM.UnitInventorEnt_UpgradeUnitCom.AddAmountUpgrades(UnitTypes.Rook, Info.Sender.IsMasterClient);
                    for (int x = 0; x < _startValuesGameConfig.CELL_COUNT_X; x++)
                        for (int y = 0; y < _startValuesGameConfig.CELL_COUNT_Y; y++)
                        {
                            if (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType == UnitTypes.Rook
                                && _eGM.CellUnitEnt_CellOwnerCom(x, y).IsHim(Info.Sender))
                                _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += _startValuesGameConfig.HEALTH_UPGRADE_ADDING_ROOK;
                        }

                }
                break;


            case UnitTypes.Bishop:
                haveFood = _eGM.EconomyEnt_EconomyCom.Food(Info.Sender.IsMasterClient) >= _startValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP;
                haveWood = _eGM.EconomyEnt_EconomyCom.Wood(Info.Sender.IsMasterClient) >= _startValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP;
                haveOre = _eGM.EconomyEnt_EconomyCom.Ore(Info.Sender.IsMasterClient) >= _startValuesGameConfig.ORE_FOR_UPGRADE_BISHOP;
                haveIron = _eGM.EconomyEnt_EconomyCom.Iron(Info.Sender.IsMasterClient) >= _startValuesGameConfig.IRON_FOR_UPGRADE_BISHOP;
                haveGold = _eGM.EconomyEnt_EconomyCom.Gold(Info.Sender.IsMasterClient) >= _startValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP;


                if (haveFood && haveWood && haveOre && haveIron && haveGold)
                {
                    _eGM.EconomyEnt_EconomyCom.TakeFood(Info.Sender.IsMasterClient, _startValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP);
                    _eGM.EconomyEnt_EconomyCom.TakeWood(Info.Sender.IsMasterClient, _startValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP);
                    _eGM.EconomyEnt_EconomyCom.TakeOre(Info.Sender.IsMasterClient, _startValuesGameConfig.ORE_FOR_UPGRADE_BISHOP);
                    _eGM.EconomyEnt_EconomyCom.TakeIron(Info.Sender.IsMasterClient, _startValuesGameConfig.IRON_FOR_UPGRADE_BISHOP);
                    _eGM.EconomyEnt_EconomyCom.TakeGold(Info.Sender.IsMasterClient, _startValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP);

                    _eGM.UnitInventorEnt_UpgradeUnitCom.AddAmountUpgrades(UnitTypes.Bishop, Info.Sender.IsMasterClient);
                    for (int x = 0; x < _startValuesGameConfig.CELL_COUNT_X; x++)
                        for (int y = 0; y < _startValuesGameConfig.CELL_COUNT_Y; y++)
                        {
                            if (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType == UnitTypes.Bishop && _eGM.CellUnitEnt_CellOwnerCom(x, y).IsHim(Info.Sender)) _eGM.CellUnitEnt_CellUnitCom(x, y).AmountHealth += _startValuesGameConfig.HEALTH_UPGRADE_ADDING_BISHOP;
                        }

                }
                break;

            default:
                break;
        }

        _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);

    }
}