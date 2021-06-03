using Photon.Pun;
using System.Collections.Generic;

internal sealed class UpgradeBuildingMasterSystem : RPCMasterSystemReduction
{
    internal BuildingTypes BuildingType => _eMM.RPCMasterEnt_RPCMasterCom.BuildingType;
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;

    public override void Run()
    {
        base.Run();


        bool haveFood = true;
        bool haveWood = true;
        bool haveOre = true;
        bool haveIron = true;
        bool haveGold = true;

        Dictionary<bool, int> currentUpgradeBuildingsDict = new Dictionary<bool, int>();

        var foodAmountDict = _eGM.FoodEnt_AmountDictCom.AmountDict;
        var woodAmountDict = _eGM.WoodEAmountDictC.AmountDict;
        var oreAmountDict = _eGM.OreEAmountDictC.AmountDict;
        var ironAmountDict = _eGM.IronEAmountDictC.AmountDict;
        var goldAmountDict = _eGM.GoldEAmountDictC.AmountDict;

        int minusFood = default;
        int minusWood = default;
        int minusOre = default;
        int minusIron = default;
        int minusGold = default;

        switch (BuildingType)
        {
            case BuildingTypes.None:
                break;

            case BuildingTypes.City:
                break;

            case BuildingTypes.Farm:
                minusFood = 0;
                minusWood = 0;
                minusOre = 0;
                minusIron = 0;
                minusGold = 1;

                currentUpgradeBuildingsDict = _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeFarmDict;
                break;

            case BuildingTypes.Woodcutter:
                minusFood = 0;
                minusWood = 0;
                minusOre = 0;
                minusIron = 0;
                minusGold = 1;

                currentUpgradeBuildingsDict = _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeWoodcutterDict;
                break;

            case BuildingTypes.Mine:
                minusFood = 0;
                minusWood = 0;
                minusOre = 0;
                minusIron = 0;
                minusGold = 1;

                currentUpgradeBuildingsDict = _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeMineDict;
                break;

            default:
                break;
        }


        haveFood = foodAmountDict[Info.Sender.IsMasterClient] >= minusFood;
        haveWood = woodAmountDict[Info.Sender.IsMasterClient] >= minusWood;
        haveOre = oreAmountDict[Info.Sender.IsMasterClient] >= minusOre;
        haveIron = ironAmountDict[Info.Sender.IsMasterClient] >= minusIron;
        haveGold = goldAmountDict[Info.Sender.IsMasterClient] >= minusGold;

        if (haveFood && haveWood && haveOre && haveIron && haveGold)
        {
            foodAmountDict[Info.Sender.IsMasterClient] -= minusFood;
            woodAmountDict[Info.Sender.IsMasterClient] -= minusWood;
            oreAmountDict[Info.Sender.IsMasterClient] -= minusOre;
            ironAmountDict[Info.Sender.IsMasterClient] -= minusIron;
            goldAmountDict[Info.Sender.IsMasterClient] -= minusGold;

            currentUpgradeBuildingsDict[Info.Sender.IsMasterClient] += 1;
        }
        _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);
    }
}
