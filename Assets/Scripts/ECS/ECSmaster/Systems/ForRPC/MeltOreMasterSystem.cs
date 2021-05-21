using Leopotam.Ecs;
using Photon.Pun;

internal class MeltOreMasterSystem : RPCMasterSystemReduction, IEcsRunSystem
{
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;

    internal MeltOreMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        bool haveFood = true;
        bool haveWood = true;
        bool haveOre = true;
        bool haveIron = true;
        bool haveGold = true;

        if (Info.Sender.IsMasterClient)
        {
            haveFood = _eGM.FoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_MELTING_ORE;
            haveWood = _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_MELTING_ORE;
            haveOre = _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_MELTING_ORE;
            haveIron = _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_MELTING_ORE;
            haveGold = _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_MELTING_ORE;

            if (haveFood && haveWood && haveOre && haveIron && haveGold)
            {
                _eGM.FoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_MELTING_ORE;
                _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_MELTING_ORE;
                _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_MELTING_ORE;
                _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_MELTING_ORE;
                _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_MELTING_ORE;

                _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] += 1;
            }
        }
        else
        {
            haveFood = _eGM.FoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.FOOD_FOR_MELTING_ORE;
            haveWood = _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.WOOD_FOR_MELTING_ORE;
            haveOre = _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.ORE_FOR_MELTING_ORE;
            haveIron = _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.IRON_FOR_MELTING_ORE;
            haveGold = _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= StartValuesGameConfig.GOLD_FOR_MELTING_ORE;

            if (haveFood && haveWood && haveOre && haveIron && haveGold)
            {
                _eGM.FoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.FOOD_FOR_MELTING_ORE;
                _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.WOOD_FOR_MELTING_ORE;
                _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.ORE_FOR_MELTING_ORE;
                _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.IRON_FOR_MELTING_ORE;
                _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= StartValuesGameConfig.GOLD_FOR_MELTING_ORE;

                _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] += 1;
            }
        }

        _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);
    }
}
