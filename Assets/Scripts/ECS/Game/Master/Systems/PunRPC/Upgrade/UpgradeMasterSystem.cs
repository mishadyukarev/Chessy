using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;
using static Assets.Scripts.Main;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal class UpgradeMasterSystem : RPCMasterSystemReduction
    {
        private PhotonMessageInfo Info => _eGM.RpcGeneralEnt_RPCCom.FromInfo;

        private UpgradeModTypes UpgradeModType => _eMM.RPCMasterEnt_RPCMasterCom.UpgradeModType;
        private int[] XyCellForUpgrade => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;

        public override void Run()
        {
            base.Run();

            switch (UpgradeModType)
            {
                case UpgradeModTypes.None:
                    break;

                case UpgradeModTypes.Unit:

                    bool haveFood = true;
                    bool haveWood = true;
                    bool haveOre = true;
                    bool haveIron = true;
                    bool haveGold = true;

                    switch (_eGM.CellUnitEnt_UnitTypeCom(XyCellForUpgrade).UnitType)
                    {
                        case UnitTypes.None:
                            break;

                        case UnitTypes.King:
                            break;

                        case UnitTypes.Pawn:
                            haveFood = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, Info.Sender.IsMasterClient) >= _startValuesGameConfig.FOOD_FOR_UPGRADE_PAWN;
                            haveWood = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, Info.Sender.IsMasterClient) >= _startValuesGameConfig.WOOD_FOR_UPGRADE_PAWN;
                            haveOre = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, Info.Sender.IsMasterClient) >= _startValuesGameConfig.ORE_FOR_UPGRADE_PAWN;
                            haveIron = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, Info.Sender.IsMasterClient) >= _startValuesGameConfig.IRON_FOR_UPGRADE_PAWN;
                            haveGold = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, Info.Sender.IsMasterClient) >= _startValuesGameConfig.GOLD_FOR_UPGRADE_PAWN;


                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, Info.Sender.IsMasterClient, _startValuesGameConfig.FOOD_FOR_UPGRADE_PAWN);
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, Info.Sender.IsMasterClient, _startValuesGameConfig.WOOD_FOR_UPGRADE_PAWN);
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, Info.Sender.IsMasterClient, _startValuesGameConfig.ORE_FOR_UPGRADE_PAWN);
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, Info.Sender.IsMasterClient, _startValuesGameConfig.IRON_FOR_UPGRADE_PAWN);
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, Info.Sender.IsMasterClient, _startValuesGameConfig.GOLD_FOR_UPGRADE_PAWN);

                                CellUnitWorker.ChangeUnitType(XyCellForUpgrade, UnitTypes.PawnSword);
                            }
                            break;


                        case UnitTypes.PawnSword:
                            break;                          


                        case UnitTypes.Rook:
                            haveFood = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, Info.Sender.IsMasterClient) >= _startValuesGameConfig.FOOD_FOR_UPGRADE_ROOK;
                            haveWood = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, Info.Sender.IsMasterClient) >= _startValuesGameConfig.WOOD_FOR_UPGRADE_ROOK;
                            haveOre = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, Info.Sender.IsMasterClient) >= _startValuesGameConfig.ORE_FOR_UPGRADE_ROOK;
                            haveIron = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, Info.Sender.IsMasterClient) >= _startValuesGameConfig.IRON_FOR_UPGRADE_ROOK;
                            haveGold = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, Info.Sender.IsMasterClient) >= _startValuesGameConfig.GOLD_FOR_UPGRADE_ROOK;


                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, Info.Sender.IsMasterClient, _startValuesGameConfig.FOOD_FOR_UPGRADE_ROOK);
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, Info.Sender.IsMasterClient, _startValuesGameConfig.WOOD_FOR_UPGRADE_ROOK);
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, Info.Sender.IsMasterClient, _startValuesGameConfig.ORE_FOR_UPGRADE_ROOK);
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, Info.Sender.IsMasterClient, _startValuesGameConfig.IRON_FOR_UPGRADE_ROOK);
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, Info.Sender.IsMasterClient, _startValuesGameConfig.GOLD_FOR_UPGRADE_ROOK);

                                CellUnitWorker.ChangeUnitType(XyCellForUpgrade, UnitTypes.RookCrossbow);
                            }
                            break;


                        case UnitTypes.RookCrossbow:
                            break;


                        case UnitTypes.Bishop:
                            haveFood = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, Info.Sender.IsMasterClient) >= _startValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP;
                            haveWood = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, Info.Sender.IsMasterClient) >= _startValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP;
                            haveOre = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, Info.Sender.IsMasterClient) >= _startValuesGameConfig.ORE_FOR_UPGRADE_BISHOP;
                            haveIron = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, Info.Sender.IsMasterClient) >= _startValuesGameConfig.IRON_FOR_UPGRADE_BISHOP;
                            haveGold = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, Info.Sender.IsMasterClient) >= _startValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP;


                            if (haveFood && haveWood && haveOre && haveIron && haveGold)
                            {
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, Info.Sender.IsMasterClient, _startValuesGameConfig.FOOD_FOR_UPGRADE_BISHOP);
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, Info.Sender.IsMasterClient, _startValuesGameConfig.WOOD_FOR_UPGRADE_BISHOP);
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, Info.Sender.IsMasterClient, _startValuesGameConfig.ORE_FOR_UPGRADE_BISHOP);
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, Info.Sender.IsMasterClient, _startValuesGameConfig.IRON_FOR_UPGRADE_BISHOP);
                                _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, Info.Sender.IsMasterClient, _startValuesGameConfig.GOLD_FOR_UPGRADE_BISHOP);

                                CellUnitWorker.ChangeUnitType(XyCellForUpgrade, UnitTypes.BishopCrossbow);
                            }
                            break;


                        case UnitTypes.BishopCrossbow:
                            break;

                        default:
                            break;
                    }

                    _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);

                    break;

                case UpgradeModTypes.Building:
                    break;

                default:
                    break;
            }
        }
    }
}
