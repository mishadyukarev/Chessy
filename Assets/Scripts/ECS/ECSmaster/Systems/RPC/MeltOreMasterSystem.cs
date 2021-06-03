using Photon.Pun;

internal sealed class MeltOreMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;


    public override void Run()
    {
        base.Run();


        bool haveFood = true;
        bool haveWood = true;
        bool haveOre = true;
        bool haveIron = true;
        bool haveGold = true;

        if (Info.Sender.IsMasterClient)
        {
            haveFood = _eGM.FoodEnt_AmountDictCom.AmountDict[Info.Sender.IsMasterClient] >= _startValuesGameConfig.FOOD_FOR_MELTING_ORE;
            haveWood = _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= _startValuesGameConfig.WOOD_FOR_MELTING_ORE;
            haveOre = _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= _startValuesGameConfig.ORE_FOR_MELTING_ORE;
            haveIron = _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= _startValuesGameConfig.IRON_FOR_MELTING_ORE;
            haveGold = _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= _startValuesGameConfig.GOLD_FOR_MELTING_ORE;

            if (haveFood && haveWood && haveOre && haveIron && haveGold)
            {
                _eGM.FoodEnt_AmountDictCom.AmountDict[Info.Sender.IsMasterClient] -= _startValuesGameConfig.FOOD_FOR_MELTING_ORE;
                _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= _startValuesGameConfig.WOOD_FOR_MELTING_ORE;
                _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= _startValuesGameConfig.ORE_FOR_MELTING_ORE;
                _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= _startValuesGameConfig.IRON_FOR_MELTING_ORE;
                _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= _startValuesGameConfig.GOLD_FOR_MELTING_ORE;

                _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] += 1;
                _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] += 1;
            }
        }
        else
        {
            haveFood = _eGM.FoodEnt_AmountDictCom.AmountDict[Info.Sender.IsMasterClient] >= _startValuesGameConfig.FOOD_FOR_MELTING_ORE;
            haveWood = _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= _startValuesGameConfig.WOOD_FOR_MELTING_ORE;
            haveOre = _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= _startValuesGameConfig.ORE_FOR_MELTING_ORE;
            haveIron = _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= _startValuesGameConfig.IRON_FOR_MELTING_ORE;
            haveGold = _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] >= _startValuesGameConfig.GOLD_FOR_MELTING_ORE;

            if (haveFood && haveWood && haveOre && haveIron && haveGold)
            {
                _eGM.FoodEnt_AmountDictCom.AmountDict[Info.Sender.IsMasterClient] -= _startValuesGameConfig.FOOD_FOR_MELTING_ORE;
                _eGM.WoodEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= _startValuesGameConfig.WOOD_FOR_MELTING_ORE;
                _eGM.OreEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= _startValuesGameConfig.ORE_FOR_MELTING_ORE;
                _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= _startValuesGameConfig.IRON_FOR_MELTING_ORE;
                _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] -= _startValuesGameConfig.GOLD_FOR_MELTING_ORE;

                _eGM.IronEAmountDictC.AmountDict[Info.Sender.IsMasterClient] += 1;
                _eGM.GoldEAmountDictC.AmountDict[Info.Sender.IsMasterClient] += 1;
            }
        }

        _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);
    }
}
