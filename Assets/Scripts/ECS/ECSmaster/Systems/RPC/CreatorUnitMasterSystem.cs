using Leopotam.Ecs;
using Photon.Pun;

internal class CreatorUnitMasterSystem : RPCMasterSystemReduction, IEcsRunSystem
{
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;
    internal UnitTypes UnitType => _eMM.RPCMasterEnt_RPCMasterCom.UnitType;

    internal CreatorUnitMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

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
                    haveFood = _eGM.FoodEnt_AmountDictCom.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_BUYING_PAWN;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_BUYING_PAWN;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_BUYING_PAWN;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEnt_AmountDictCom.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                        _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                        _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_BUYING_PAWN;
                        _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_BUYING_PAWN;
                        _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_BUYING_PAWN;

                        _eGM.InfoEnt_UnitsInfoCom.AmountPawnDict[Info.Sender.IsMasterClient] += StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                    }
                }
                else
                {
                    haveFood = _eGM.FoodEnt_AmountDictCom.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_BUYING_PAWN;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_BUYING_PAWN;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_BUYING_PAWN;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEnt_AmountDictCom.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_BUYING_PAWN;
                        _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_BUYING_PAWN;
                        _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_BUYING_PAWN;
                        _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_BUYING_PAWN;
                        _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_BUYING_PAWN;

                        _eGM.InfoEnt_UnitsInfoCom.AmountPawnDict[Info.Sender.IsMasterClient] += StartValuesGameConfig.AMOUNT_FOR_TAKE_UNIT;
                    }
                }

                break;

            case UnitTypes.Rook:

                if (Info.Sender.IsMasterClient)
                {
                    haveFood = _eGM.FoodEnt_AmountDictCom.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_BUYING_ROOK;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_BUYING_ROOK;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_BUYING_ROOK;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEnt_AmountDictCom.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                        _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                        _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_BUYING_ROOK;
                        _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_BUYING_ROOK;
                        _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_BUYING_ROOK;

                        _eGM.InfoEnt_UnitsInfoCom.AmountRookDict[Info.Sender.IsMasterClient] += 1;
                    }
                }
                else
                {
                    haveFood = _eGM.FoodEnt_AmountDictCom.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_BUYING_ROOK;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_BUYING_ROOK;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_BUYING_ROOK;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEnt_AmountDictCom.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_BUYING_ROOK;
                        _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_BUYING_ROOK;
                        _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_BUYING_ROOK;
                        _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_BUYING_ROOK;
                        _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_BUYING_ROOK;

                        _eGM.InfoEnt_UnitsInfoCom.AmountRookDict[Info.Sender.IsMasterClient] += 1;
                    }
                }

                break;


            case UnitTypes.Bishop:

                if (Info.Sender.IsMasterClient)
                {
                    haveFood = _eGM.FoodEnt_AmountDictCom.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_BUYING_BISHOP;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEnt_AmountDictCom.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                        _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                        _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                        _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                        _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_BUYING_BISHOP;

                        _eGM.InfoEnt_UnitsInfoCom.AmountBishopDict[Info.Sender.IsMasterClient] += 1;
                    }
                }
                else
                {
                    haveFood = _eGM.FoodEnt_AmountDictCom.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                    haveWood = _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                    haveOre = _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                    haveIron = _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                    haveGold = _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_BUYING_BISHOP;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        _eGM.FoodEnt_AmountDictCom.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_BUYING_BISHOP;
                        _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_BUYING_BISHOP;
                        _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_BUYING_BISHOP;
                        _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_BUYING_BISHOP;
                        _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_BUYING_BISHOP;

                        _eGM.InfoEnt_UnitsInfoCom.AmountBishopDict[Info.Sender.IsMasterClient] += 1;
                    }
                }

                break;


            default:
                break;
        }

        _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);
    }
}
